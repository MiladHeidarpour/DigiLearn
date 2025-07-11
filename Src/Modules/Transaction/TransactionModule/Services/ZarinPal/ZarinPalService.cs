﻿using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System.Reflection;
using TransactionModule.Services.ZarinPal.Dtos.Payment;
using TransactionModule.Services.ZarinPal.Dtos.RefFound;
using TransactionModule.Services.ZarinPal.Dtos.UnVerification;
using TransactionModule.Services.ZarinPal.Dtos.Verification;

namespace TransactionModule.Services.ZarinPal;
public class ZarinpalRequestResponse
{
    public ZarinpalData Data { get; set; }
    public object Errors { get; set; }
}

public class ZarinpalData
{
    public int Code { get; set; }
    public string Message { get; set; }
    public string Authority { get; set; }
    public string Fee_Type { get; set; }
    public int Fee { get; set; }
}
public class ZarinPalService : IZarinPalService
{
    private string MerchantId { get; }
    private string PaymentUrl { get; }
    private string VerifyUrl { get; }
    private string SandBoxPaymentUrl { get; }
    private string SandBoxVerifyUrl { get; }
    private string UnVerifiedUrl { get; }
    private string ReFoundUrl { get; }
    private string ReFoundToken { get; }
    public string GateWayUrl { get; set; }
    public string SandBoxGateWayUrl { get; set; }
    public ZarinPalService(IConfiguration configuration)
    {
        var configuration1 = configuration;
        MerchantId = configuration1.GetSection("ZarinPal")["merchant"];
        PaymentUrl = configuration1.GetSection("ZarinPal")["paymentUrl"];
        VerifyUrl = configuration1.GetSection("ZarinPal")["verifyUrl"];
        UnVerifiedUrl = configuration1.GetSection("ZarinPal")["unVerifiedUrl"];
        ReFoundUrl = configuration1.GetSection("ZarinPal")["reFoundUrl"];
        ReFoundToken = configuration1.GetSection("ZarinPal")["reFoundToken"];
        GateWayUrl = configuration1.GetSection("ZarinPal")["StartPay"];
        //
        SandBoxPaymentUrl = configuration1.GetSection("ZarinPal-sandBox")["paymentUrl"];
        SandBoxVerifyUrl = configuration1.GetSection("ZarinPal-sandBox")["verifyUrl"];
        SandBoxGateWayUrl = configuration1.GetSection("ZarinPal-sandBox")["StartPay"];

    }

    public async Task<PaymentResponseData> CreatePaymentRequest(int amount,
        string description,
        string callBackUrl,
        string mobile = null, string email = null)
    {
        var client = new RestClient(PaymentUrl);
        var request = new RestRequest(Method.POST);
        request.AddHeader("Content-Type", "application/json");
        //Zarin pal Amount Type = Rial
        //For Convert Tooman TO Rial Should do amount * 10 
        var body = new PaymentRequest
        {
            Mobile = mobile,
            CallbackUrl = callBackUrl,
            Description = description,
            Email = email,
            Amount = amount * 10,
            MerchantId = MerchantId
        };
        var jsonBody = JsonConvert.SerializeObject(body);
        request.AddJsonBody(jsonBody);
        var response = await client.ExecuteAsync(request);
        var result = JsonConvert.DeserializeObject<PaymentResponse>(response.Content);
        if (result?.Data.Status == 100)
            result.Data.GateWayUrl = GateWayUrl + result.Data.Authority;
        return result?.Data;
    }



    public async Task<FinallyVerificationResponse> CreateVerificationRequest(string authority, int price)
    {
        var client = new RestClient(VerifyUrl);
        var request = new RestRequest(Method.POST);
        request.AddHeader("Content-Type", "application/json");

        //Zarin pal Amount Type = Rial
        //For Convert Tooman TO Rial Should do amount * 10 
        var body = new VerificationRequest
        {
            Amount = price * 10,
            MerchantId = MerchantId,
            Authority = authority
        };
        var jsonBody = JsonConvert.SerializeObject(body);
        request.AddJsonBody(jsonBody);
        var response = await client.ExecuteAsync(request);
        if (response.IsSuccessful)
        {
            var result = JsonConvert.DeserializeObject<VerificationResponse>(response.Content);
            var res = new FinallyVerificationResponse
            {
                Message = null,
                CardPan = result?.Data.CardPan,
                RefId = result.Data.RefId,
                Status = result.Data.Status
            };

            return res;
        }
        else
        {
            var result = JsonConvert.DeserializeObject<ErrorVerificationResponse>(response.Content);
            var res = new FinallyVerificationResponse
            {
                Message = result.Errors.Message,
                CardPan = null,
                RefId = 0,
                Status = result.Errors.Code
            };

            return res;
        }
    }



    public async Task<UnVerificationFinallyResponse> GetUnVerificationRequests()
    {
        var client = new RestClient(UnVerifiedUrl);
        var request = new RestRequest(Method.POST);
        request.AddHeader("Content-Type", "application/json");

        var body = new UnVerificationRequest()
        {
            MerchantId = MerchantId
        };
        var jsonBody = JsonConvert.SerializeObject(body);
        request.AddJsonBody(jsonBody);
        var response = await client.ExecuteAsync(request);
        var result = JsonConvert.DeserializeObject<UnVerificationResponse>(response.Content);
        return result?.Data;
    }

    public async Task<ReFoundResponse> ReFoundRequest(string authority)
    {
        var client = new RestClient(ReFoundUrl);
        var request = new RestRequest(Method.POST);
        request.AddHeader("authorization", $"Bearer {ReFoundToken}");
        request.AddHeader("Content-Type", "application/json");

        request.AddJsonBody(new ReFoundRequest()
        {
            Authority = authority,
            MerchantId = MerchantId
        });
        var result = await client.ExecuteAsync<ReFoundResponse>(request);
        return result.Data;
    }


    #region SandBox
    public async Task<PaymentResponseData> SandBox_CreatePaymentRequest(int amount,
        string description,
        string callBackUrl,
        string mobile = null, string email = null)
    {
        // Use the official API endpoint
        var client = new RestClient("https://sandbox.zarinpal.com/pg/v4/payment/request.json");
        var request = new RestRequest(Method.POST);
        request.AddHeader("Content-Type", "application/json");

        var body = new
        {
            merchant_id = "1f7a8a5a-8a15-4607-9265-8f23eb58886c", // Use snake_case
            amount = amount*10, // Zarinpal requires amount in Rial
            callback_url = callBackUrl, // Use snake_case
            description = description,
            mobile= mobile,
            email=email,
        };
        request.AddJsonBody(body);

        var response = await client.ExecuteAsync<ZarinpalRequestResponse>(request);

        // Check for successful response (Code == 100 in nested Data object)
        if (response.IsSuccessful && response.Data?.Data != null && response.Data.Data.Code == 100)
        {
            return new PaymentResponseData()
            {
                Status = 100, // Success status
                Authority = response.Data.Data.Authority,
                GateWayUrl = "https://sandbox.zarinpal.com/pg/StartPay/" + response.Data.Data.Authority
            };
        }

        // Return a failure response if anything goes wrong
        return new PaymentResponseData()
        {
            Status = -1, // Failure status
            Message = "Failed to create payment request."
        };
    }

    //public async Task<FinallyVerificationResponse> SandBox_CreateVerificationRequest(string authority, int price)
    //{
    //    var client = new RestClient(SandBoxVerifyUrl);
    //    var request = new RestRequest(Method.POST);
    //    request.AddHeader("Content-Type", "application/json");

    //    //Zarin pal Amount Type = Rial
    //    //For Convert Tooman TO Rial Should do amount * 10 
    //    var body = new
    //    {
    //        merchant_id = "1f7a8a5a-8a15-4607-9265-8f23eb58886c", // Use snake_case
    //        amount = price, // Zarinpal requires amount in Rial
    //        callback_url = callBackUrl, // Use snake_case

    //    };
    //    var body = new
    //    {
    //        Amount = price,
    //        MerchantID = "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX",
    //        Authority = authority
    //    };
    //    request.AddJsonBody(body);
    //    var response = await client.ExecuteAsync<SandBoxVerificationResponse>(request);
    //    var result = response.Data;

    //    return new FinallyVerificationResponse()
    //    {
    //        RefId = result.RefId,
    //        Message = " ",
    //        Status = result.Status
    //    };
    //}

    public async Task<FinallyVerificationResponse> SandBox_CreateVerificationRequest(string authority, int price)
    {
        var client = new RestClient("https://sandbox.zarinpal.com/pg/v4/payment/verify.json");
        var request = new RestRequest(Method.POST);
        request.AddHeader("Content-Type", "application/json");

        var body = new
        {
            merchant_id = "1f7a8a5a-8a15-4607-9265-8f23eb58886c", // !!! مرچنت کد تست خود را اینجا قرار بده
            amount = price*10,
            authority = authority
        };
        request.AddJsonBody(body);

        var response = await client.ExecuteAsync<ZarinpalVerificationResponse>(request);

        // Check if the API call was successful and the transaction was verified (Code 100 or 101)
        if (response.IsSuccessful && response.Data?.Data != null && (response.Data.Data.Code == 100 || response.Data.Data.Code == 101))
        {
            return new FinallyVerificationResponse()
            {
                Status = response.Data.Data.Code,
                RefId = response.Data.Data.Ref_Id,
                CardPan = response.Data.Data.Card_Pan,
                Message = response.Data.Data.Message
            };
        }

        // Handle failures by extracting error code and message
        int errorCode = -1; // Default error code
        string errorMessage = "Verification failed.";

        if (response.Data?.Errors is Newtonsoft.Json.Linq.JObject errorObject)
        {
            errorCode = errorObject.Value<int>("code");
            errorMessage = errorObject.Value<string>("message");
        }

        return new FinallyVerificationResponse()
        {
            Status = errorCode,
            Message = errorMessage
        };
    }

    #endregion
}

// Top-level response wrapper
public class ZarinpalVerificationResponse
{
    public ZarinpalVerificationData Data { get; set; }
    public object Errors { get; set; }
}

// Nested data object containing the verification details
public class ZarinpalVerificationData
{
    public int Code { get; set; }
    public string Message { get; set; }
    public string Card_Hash { get; set; }
    public string Card_Pan { get; set; }
    public long Ref_Id { get; set; }
    public string Fee_Type { get; set; }
    public int Fee { get; set; }
}