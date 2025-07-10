using TransactionModule.Domain;
using TransactionModule.Domain.Enums;
using TransactionModule.Services.Dtos.Commands;
using TransactionModule.Services.Dtos.Queries;

namespace TransactionModule.Services;

public interface IUserTransactionService
{
    #region Commands

    Task<Guid> CreateTransaction(CreateTransactionCommand command);
    Task PaymentSuccess(TransactionPaymentSuccessCommand command);
    Task PaymentError(TransactionPaymentErrorCommand command);

    #endregion

    #region Queries
    Task<UserTransaction> GetTransactionById(Guid id);
    Task<UserTransactionFilterDto> GetTransactionsByFilter(UserTransactionFilterParams queryParams);
    Task<int> GetCancelTransactionsCount(string stDate, string eDate, TransactionFor transactionFor, TransactionStatus status);
    #endregion
}