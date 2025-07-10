using Newtonsoft.Json;

namespace TransactionModule.Services.ZarinPal.Dtos.UnVerification;

public class UnVerificationRequest
{
    [JsonProperty("merchant_id")]
    public string MerchantId { get; set; }
}