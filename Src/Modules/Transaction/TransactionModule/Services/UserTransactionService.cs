using Common.Application.DateUtil;
using Microsoft.EntityFrameworkCore;
using TransactionModule.Context;
using TransactionModule.Domain;
using TransactionModule.Domain.Enums;
using TransactionModule.Services.Dtos.Commands;
using TransactionModule.Services.Dtos.Queries;

namespace TransactionModule.Services;

public class UserTransactionService : IUserTransactionService
{
    private readonly TransactionContext _context;
    //private readonly IUserAcl _userAcl;
    public UserTransactionService(TransactionContext context)
    {
        _context = context;
        //_userAcl = userAcl;
    }
    public async Task<Guid> CreateTransaction(CreateTransactionCommand command)
    {
        var transaction = new UserTransaction(command.UserId, command.PaymentAmount, command.TransactionFor,
            command.LinkId, command.PaymentGateway);
        await _context.AddAsync(transaction);
        await _context.SaveChangesAsync();
        return transaction.Id;
    }

    public async Task PaymentSuccess(TransactionPaymentSuccessCommand command)
    {
        var transaction = await GetTransactionById(command.TransactionId);
        transaction.PaymentSuccess(command.Authority, command.CardPen, command.RefId);
        _context.Update(transaction);
        await _context.SaveChangesAsync();
    }

    public async Task PaymentError(TransactionPaymentErrorCommand command)
    {
        var transaction = await GetTransactionById(command.TransactionId);
        transaction.PaymentError(command.ErrorMessage, command.RefId, command.Authority, command.Canceled);
        _context.Update(transaction);
        await _context.SaveChangesAsync();
    }
    public async Task<UserTransaction> GetTransactionById(Guid id)
    {
        return await _context.UserTransactions.FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<UserTransactionFilterDto> GetTransactionsByFilter(UserTransactionFilterParams queryParams)
    {
        var result = _context.UserTransactions
            .OrderByDescending(d => d.CreationDate).AsQueryable();


        if (queryParams.TransactionFor != null)
            result = result.Where(r => r.TransactionFor == queryParams.TransactionFor);

        if (queryParams.Status != null && queryParams.Status != TransactionStatus.Pending)
            result = result.Where(r => r.Status == queryParams.Status);

        if (queryParams.UserId != null)
            result = result.Where(r => r.UserId == queryParams.UserId);

        if (queryParams.StartDate != null)
        {
            DateTime startDate = (DateTime)queryParams.StartDate;
            result = result.Where(r => (r.PaymentDate != null && r.PaymentDate >= queryParams.StartDate) || r.CreationDate >= startDate);
        }

        if (queryParams.EndDate != null)
        {
            var endDate = (DateTime)queryParams.EndDate;
            result = result.Where(r => r.PaymentDate != null && r.PaymentDate <= queryParams.EndDate || r.CreationDate <= endDate);
        }

        var skip = (queryParams.PageId - 1) * queryParams.Take;
        var totalPrice = 0;

        var model = new UserTransactionFilterDto()
        {
            Filters = queryParams,
            Transactions = await result.Skip(skip).Take(queryParams.Take).Select(s =>
                new UserTransactionDto()
                {
                    PaymentLinkId = s.PaymentLinkId,
                    TransactionFor = s.TransactionFor,
                    RefId = s.RefId,
                    UserId = s.UserId,
                    Status = s.Status,
                    PaymentDate = s.PaymentDate,
                    Authority = s.Authority,
                    CardPan = s.CardPan,
                    CreateDate = s.CreationDate,
                    Id = s.Id,
                    PaymentAmount = s.PaymentAmount,
                    PaymentErrorMessage = s.PaymentErrorMessage,
                    PaymentGateWay = s.PaymentGateway,
                    //UserFullName = _userAcl.GetUserFullName(s.UserId)
                }).ToListAsync(),

        };
        model.GeneratePaging(result, queryParams.Take, queryParams.PageId);


        //Choon Mablagh Bishatra az int.MaxValue Hast Nemishe Az Kole Data Sum Gereft
        var paymentTake = 200;
        var pageCount = (int)Math.Ceiling(model.EntityCount / (double)paymentTake);
        for (int i = 1; i <= pageCount; i++)
        {
            var paymentSkip = (i - 1) * paymentTake;
            totalPrice += await result
                .Where(r => r.Status == TransactionStatus.PaymentSuccess)
                .Skip(paymentSkip)
                .Take(paymentTake)
                .SumAsync(s => s.PaymentAmount);

        }
        model.TotalPayment = totalPrice;
        return model;
    }

    public async Task<int> GetCancelTransactionsCount(string stDate, string eDate, TransactionFor transactionFor, TransactionStatus status)
    {
        var result = _context.UserTransactions.Where(p => p.Status == TransactionStatus.PaymentError).AsQueryable();

        if (transactionFor != null)
            result = result.Where(r => r.TransactionFor == transactionFor);

        if (status != TransactionStatus.Pending)
            result = result.Where(r => r.Status == status);

        if (!string.IsNullOrEmpty(stDate))
        {
            DateTime startDate = (DateTime)stDate.ToMiladi();
            result = result.Where(r => (r.PaymentDate != null && r.PaymentDate >= stDate.ToMiladi()) || r.CreationDate >= startDate);
        }

        if (!string.IsNullOrEmpty(eDate))
        {
            var endDate = (DateTime)eDate.ToMiladi();
            result = result.Where(r => r.PaymentDate != null && r.PaymentDate <= eDate.ToMiladi() || r.CreationDate <= endDate);
        }

        var res = await result.ToListAsync();

        return res.Count();
    }
}