namespace TicketModule.Core.Dtos.Queries;

public class TicketMessageDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid TicketId { get; set; }
    public string UserFullName { get; set; }
    public string Text { get; set; }
    public DateTime CreationDate  { get; set; }
}