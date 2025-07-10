using TransactionModule.Domain.Enums;

namespace TransactionModule.Services.Dtos.Commands;

public class CreateTransactionCommand
{
    public Guid UserId { get; set; }
    public int PaymentAmount { get; set; }
    public Guid LinkId { get; set; }
    public PaymentGateway PaymentGateway { get; set; }
    public TransactionFor TransactionFor { get; set; }
}