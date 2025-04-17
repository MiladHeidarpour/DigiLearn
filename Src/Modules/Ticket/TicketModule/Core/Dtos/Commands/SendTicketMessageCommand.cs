namespace TicketModule.Core.Dtos.Command;

public class SendTicketMessageCommand
{
    public Guid UserId { get; set; }
    public Guid TicketId { get; set; }
    public string OwnerFullName { get; set; }
    public string Text { get; set; }
}