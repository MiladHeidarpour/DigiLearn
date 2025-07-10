using TransactionModule.Services.ZarinPal.Dtos.Payment;
using TransactionModule.Services.ZarinPal.Dtos.RefFound;
using TransactionModule.Services.ZarinPal.Dtos.UnVerification;
using TransactionModule.Services.ZarinPal.Dtos.Verification;

namespace TransactionModule.Services.ZarinPal;

public interface IZarinPalService
{
    Task<PaymentResponseData> CreatePaymentRequest(int amount,
        string description,
        string callBackUrl,
        string mobile = null, string email = null);
    Task<FinallyVerificationResponse> CreateVerificationRequest(string authority, int price);
    Task<UnVerificationFinallyResponse> GetUnVerificationRequests();
    Task<ReFoundResponse> ReFoundRequest(string authority);

    //SandBox
    Task<FinallyVerificationResponse> SandBox_CreateVerificationRequest(string authority, int price);
    Task<PaymentResponseData> SandBox_CreatePaymentRequest(int amount,
        string description,
        string callBackUrl,
        string mobile = null, string email = null);
}