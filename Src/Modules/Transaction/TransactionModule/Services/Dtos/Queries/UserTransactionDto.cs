using Common.Application;
using TransactionModule.Domain.Enums;

namespace TransactionModule.Services.Dtos.Queries;

public class UserTransactionDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string UserFullName { get; set; }
    public int PaymentAmount { get; set; }
    public Guid PaymentLinkId { get; set; }
    public long? RefId { get; set; }
    public string Authority { get; set; }
    public string CardPan { get; set; }
    public string PaymentErrorMessage { get; set; }
    public string PaymentGateWay { get; set; }
    public TransactionStatus Status { get; set; }
    public TransactionFor TransactionFor { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime? PaymentDate { get; set; }
}

public class UserTransactionFilterParams
{
    public int PageId { get; set; } = 1;
    public int Take { get; set; } = 20;
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public TransactionStatus? Status { get; set; }
    public TransactionFor? TransactionFor { get; set; }
    public Guid? UserId { get; set; }
}

public class UserTransactionFilterDto :PaginateBase
{
    public UserTransactionFilterParams Filters { get; set; }
    public List<UserTransactionDto> Transactions { get; set; }
    public int TotalPayment { get; set; }
}