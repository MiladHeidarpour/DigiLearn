namespace TransactionModule.Domain.Enums;

public enum TransactionStatus : short
{
    Pending = 0,
    PaymentSuccess = 1,
    PaymentError = 2,
    CancelPayment = 3
}