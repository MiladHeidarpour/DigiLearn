using System.Text;
using Common.EventBus.Abstractions;
using Common.EventBus.Events;
using CoreModule.Infrastructure.Persistent;
using CoreModule.Infrastructure.Persistent.UserAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace CoreModule.Infrastructure.EventHandlers;

public class UserEditedEventHandler : BackgroundService
{
    private readonly IEventBus _eventBus;
    private readonly string _queueName = "coreModuleUserEdited";
    private readonly ILogger<UserEditedEventHandler> _logger;
    private readonly IServiceScopeFactory _serviceFactory;

    public UserEditedEventHandler(IEventBus eventBus, ILogger<UserEditedEventHandler> logger, IServiceScopeFactory serviceFactory)
    {
        _eventBus = eventBus;
        _logger = logger;
        _serviceFactory = serviceFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Delay(TimeSpan.FromMilliseconds(5), stoppingToken);
        var connection = _eventBus.Connection;
        var model = connection.CreateModel();

        var serviceScope = _serviceFactory.CreateScope();
        var context = serviceScope.ServiceProvider.GetRequiredService<CoreModuleEFContext>();

        model.ExchangeDeclare(Exchanges.UserTopicExchange, ExchangeType.Topic, true, false, null);
        model.QueueDeclare(_queueName, true, false, false, null);
        model.QueueBind(_queueName, Exchanges.UserTopicExchange, "user.edited", null);

        var consumer = new EventingBasicConsumer(model);
        consumer.Received += async (sender, args) =>
        {
            try
            {
                var userJson = Encoding.UTF8.GetString(args.Body.ToArray());
                var user = JsonConvert.DeserializeObject<UserEdited>(userJson);
                var oldUser = await context.Users.FirstOrDefaultAsync(f => f.Id == user.UserId, stoppingToken);

                if (oldUser == null)
                {
                    context.Users.Add(new User()
                    {
                        Id = user.UserId,
                        Avatar = "Default.png",
                        CreationDate = user.CreationDate,
                        Email = user.Email,
                        Name = user.Name,
                        Family = user.Family,
                        PhoneNumber = user.PhoneNumber
                    });
                }
                else
                {
                    oldUser.Name=user.Name;
                    oldUser.Family=user.Family;
                    oldUser.PhoneNumber=user.PhoneNumber;
                    oldUser.Email=user.Email;
                    context.Users.Update(oldUser);
                }

                await context.SaveChangesAsync(stoppingToken);
                model.BasicAck(args.DeliveryTag, false);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
            }
        };
        model.BasicConsume(consumer, _queueName, false);


    }
}