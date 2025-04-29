using Common.Query;
using Common.Query.Filter;
using TicketModule.Data.Entities;

namespace TicketModule.Core.Dtos.Queries;

public class TicketFilterData:BaseDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Title { get; set; }
    public TicketStatus Status { get; set; }
    public DateTime CreationDate { get; set; }
}
public class TicketFilterResult:BaseFilter<TicketFilterData,TicketFilterParams>
{
    
}

public class TicketFilterParams:BaseFilterParam
{
    public Guid? UserId { get; set; }
}