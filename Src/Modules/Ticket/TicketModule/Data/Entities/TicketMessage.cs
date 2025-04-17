using System.ComponentModel.DataAnnotations;
using Common.Domain;

namespace TicketModule.Data.Entities;

class TicketMessage : BaseEntity
{
    public Guid UserId { get; set; }
    public Guid TicketId { get; set; }
    [MaxLength(100)]
    public string UserFullName { get; set; }
    public string Text { get; set; }
    public Ticket Ticket { get; set; }
}