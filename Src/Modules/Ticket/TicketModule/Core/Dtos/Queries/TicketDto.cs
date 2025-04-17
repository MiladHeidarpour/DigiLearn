using System.ComponentModel.DataAnnotations;
using TicketModule.Data.Entities;

namespace TicketModule.Core.Dtos.Queries;

public class TicketDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string OwnerFullName { get; set; }
    public string PhoneNumber { get; set; }
    public string Title { get; set; }
    public string Text { get; set; }
    public TicketStatus TicketStatus { get; set; }
    public DateTime CreationDate { get; set; }
    public List<TicketMessageDto> TicketMessages { get; set; }
}