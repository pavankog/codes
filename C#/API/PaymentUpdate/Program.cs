using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using PaymentUpdate.modal;
using System.Text;
using System.Linq;

namespace PaymentUpdate
{
    class Program
    {
        private static readonly HttpClient tokenClient = new HttpClient();
        private static readonly HttpClient pendingTransclient = new HttpClient();
        private static readonly HttpClient inVoiceTransclient = new HttpClient();
        private static readonly HttpClient submitTransclient = new HttpClient();
        static async Task Main(string[] args)
        {
            try
            {
                //if event passes the userId, account Id via argument
                // int userId=args[0];
                // int  accountId=args[1];
                int userId = 54;
                int accountId = 345;

                await ProcessPendingTransations(userId, accountId);

                //remove the  below line if you are giving production build in 'release' mode
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Excption:" + ex.Message);
                Console.ReadLine();
            }

        }
        private static async Task ProcessPendingTransations(int userId, int accountId)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("config.json", optional: false);
            IConfiguration config = builder.Build();
            string tokenApi = config.GetSection("Token").Value.ToString();
            string pendingtransactionsUrl = config.GetSection("PendingtransactionsUrl").Value.ToString();
            string invoiceUrl = config.GetSection("InvoiceUrl").Value.ToString();
            string savepaymentsUri = config.GetSection("SavepaymentsUri").Value.ToString();

            HttpRequestMessage tokenreq = new HttpRequestMessage(HttpMethod.Get, tokenApi);
            tokenClient.DefaultRequestHeaders
                            .Accept
                            .Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
            tokenreq.Headers.Add("Cookie", "fpc=AmuVKpuQ8AFKu-3FaJ0WFk96_kGhAQAAAOs9RdoOAAAA; stsservicecookie=estsfd; x-ms-gateway-slice=estsfd");
            tokenreq.Content = new FormUrlEncodedContent(
                                       new Dictionary<string, string>
                                        {
                                         {"grant_type", "client_credentials"},
                                         {"client_id", "32e77e11-f666-4efc-8ef2-143b386b920f"},
                                         {"scope", "api://5931b0f4-5d90-45d1-9c24-149bd8e05326/.default"},
                                         {"client_secret", "Q~w7Q~OTVGlraTO4dGWZie8N.tF6-ZoMx-ZUH"},
                                        }
                                       );

            //token Api
            HttpResponseMessage tokenresp = await tokenClient.SendAsync(tokenreq);
            if (tokenresp.StatusCode == System.Net.HttpStatusCode.OK)
            {
                TokenDetails result = JsonConvert.DeserializeObject<TokenDetails>(tokenresp.Content.ReadAsStringAsync().Result);
                tokenClient.Dispose();
                HttpRequestMessage pendingTransReq = new HttpRequestMessage(HttpMethod.Get, $"{pendingtransactionsUrl}?userId={userId}&accountId={accountId}");
                pendingTransReq.Headers.Authorization = new AuthenticationHeaderValue("Bearer", result.access_token);
                HttpResponseMessage pendingTransRes = await pendingTransclient.SendAsync(pendingTransReq);
                if (pendingTransRes.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var pendingTransaction = JsonConvert.DeserializeObject<List<Transactions>>(pendingTransRes.Content.ReadAsStringAsync().Result);
                    pendingTransclient.Dispose();
                    if (pendingTransaction.Any())
                    {
                        foreach (Transactions transactions in pendingTransaction)
                        {
                            var invoiceReq = new HttpRequestMessage(HttpMethod.Get, $"{invoiceUrl}/{transactions.invoiceNumber}");
                            invoiceReq.Headers.Authorization = new AuthenticationHeaderValue("Bearer", result.access_token);
                            HttpResponseMessage inoviceRes = await inVoiceTransclient.SendAsync(invoiceReq);
                            if (inoviceRes.StatusCode == System.Net.HttpStatusCode.OK)
                            {
                                Invoice invoice = JsonConvert.DeserializeObject<Invoice>(inoviceRes.Content.ReadAsStringAsync().Result);
                                Console.WriteLine($"invoiceStatus: {invoice.invoiceStatus} \n" + $" \n invoiceNumber:{invoice.invoiceNumber}\n customerName: {invoice.customerName}");

                                #region  submit payment api 
                                if (invoice.invoiceStatus == "Paid")
                                {
                                    PaymentDeatils paymentDetails = new PaymentDeatils
                                    {
                                        feeAmount = invoice.totalAmount,
                                        paymentCollection = new PaymentCollection
                                        {
                                            payment = new List<Payment>
                                                         {
                                                            new Payment{
                                                                      accountDetail= new AccountDetail
                                                                                         {
                                                                                           firstName = invoice.customerName.Split(' ')[0],
                                                                                           lastName = invoice.customerName.Split(' ')[1],
                                                                                           email = invoice.emailAddress
                                                                                         },
                                                                     feeAmount=invoice.totalAmount
                                                            },
                                                            new Payment{
                                                                // add next payment if it is there
                                                            }
                                                         }
                                        }
                                    };

                                    // string paymentContent = JsonConvert.SerializeObject(payment);
                                    //remove below line after 
                                    string paymentContent = GetJsonString();

                                    var buffer = Encoding.UTF8.GetBytes(paymentContent);
                                    var byteContent = new ByteArrayContent(buffer);
                                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                                    submitTransclient.BaseAddress = new Uri(savepaymentsUri);
                                    var submitresult = submitTransclient.PostAsync("", byteContent).Result;
                                    if (submitresult.StatusCode == System.Net.HttpStatusCode.OK)
                                    {
                                        Console.WriteLine("sucessfuly submitted the records");
                                    }
                                }
                                #endregion
                            }
                            else
                            {
                                //To do :- log errrors to database or  files using log4net or Ilogger Interface
                                throw new Exception($"Error in processing pinvoiceUrlAPI the token\n Status code: {inoviceRes.StatusCode.ToString()}");

                            }

                        }
                        inVoiceTransclient.Dispose();
                    }
                    else
                    {
                        Console.WriteLine("No pending transaxtions are avalible.");
                    }
                }
                else
                {
                    //To do :- log errrors to database or  files using log4net or Ilogger Interface
                    throw new Exception($"Error in processing pendingtransactionsAPI the token\n Status code: {pendingTransRes.StatusCode.ToString()}");
                }
            }
            else
            {
                //To do :- log errrors to database or  files using log4net or Ilogger Interface
                throw new Exception($"Error in validating the token\n Status code: {tokenresp.StatusCode.ToString()}");
            }
        }



        private static string GetJsonString()
        {
            return @"{
" + "\n" +
            @"  ""transactionHeaderId"": 1121,
" + "\n" +
            @"  ""action"": ""string"",
" + "\n" +
            @"  ""accountKey"": 11250,
" + "\n" +
            @"  ""paymentDate"": ""2019-01-06T17:16:40"",
" + "\n" +
            @"  ""paymentConfirmationID"": ""2847B151-78B1-44AA-B497-872626093198"",
" + "\n" +
            @"  ""paymentSource"": ""KANSASSTRIPE"",
" + "\n" +
            @"  ""paymentReferenceNumber"": ""string"",
" + "\n" +
            @"  ""orderStatus"": ""Completed"",
" + "\n" +
            @"  ""createdby"": 28,
" + "\n" +
            @"  ""createdDatetime"": ""2019-01-06T17:16:40"",
" + "\n" +
            @"  ""modifiedBy"": 28,
" + "\n" +
            @"  ""modifiedDateTime"": ""2019-01-06T17:16:40"",
" + "\n" +
            @"  ""transactionIdentifier"": ""KS27vsPUT3hS2Qg"",
" + "\n" +
            @"  ""hostTransactionId"": ""a5a255c1-6c40-42ba-af9c-3efc1cd0a536"",
" + "\n" +
            @"  ""hostAuthorizationCode"": ""CV5J5iOgD5wXBp3"",
" + "\n" +
            @"  ""paymentReceiptConfirmation"": ""INV0002387"",
" + "\n" +
            @"  ""paymentApprovalStatus"": 0,
" + "\n" +
            @"  ""amount"": 400.00,
" + "\n" +
            @"  ""feeAmount"": 400.00,
" + "\n" +
            @"  ""totalRemitted"": 400.00,
" + "\n" +
            @"  ""paymentCollection"": {
" + "\n" +
            @"    ""payment"": [
" + "\n" +
            @"      {
" + "\n" +
            @"        ""transactionType"": ""Payment Type"",
" + "\n" +
            @"        ""licenseKey"": 27,
" + "\n" +
            @"        ""licenseTypeKey"": 24,
" + "\n" +
            @"        ""licenseSubTypeKey"": 12,
" + "\n" +
            @"        ""licenseType"": ""Small Animal License"",
" + "\n" +
            @"        ""licenseSubType"": """",
" + "\n" +
            @"        ""licenseNumber"": ""CB0000TL"",
" + "\n" +
            @"        ""licensePermitExpirationDate"": ""2019-01-06T17:16:40"",
" + "\n" +
            @"        ""feeTypeID"": 0,
" + "\n" +
            @"        ""feeType"": ""Renewal"",
" + "\n" +
            @"        ""feeAmount"": 400.00,
" + "\n" +
            @"        ""createdby"": 28,
" + "\n" +
            @"        ""createdDatetime"": ""2019-01-06T17:16:40"",
" + "\n" +
            @"        ""sendForPayment"": true,
" + "\n" +
            @"        ""accountDetail"": {
" + "\n" +
            @"          ""businessName"": ""Kansas Humane Society of Wichita"",
" + "\n" +
            @"          ""prefixTypeID"": 0,
" + "\n" +
            @"          ""prefixType"": ""string"",
" + "\n" +
            @"          ""firstName"": ""testing"",
" + "\n" +
            @"          ""middleName"": ""string"",
" + "\n" +
            @"          ""lastName"": ""string"",
" + "\n" +
            @"          ""suffixTypeID"": 0,
" + "\n" +
            @"          ""suffixType"": ""string"",
" + "\n" +
            @"          ""alternateLastName"": ""string"",
" + "\n" +
            @"          ""addressLine1"": ""3313 N Hillside"",
" + "\n" +
            @"          ""addressLine2"": ""string"",
" + "\n" +
            @"          ""city"": ""Wichita"",
" + "\n" +
            @"          ""stateID"": 20,
" + "\n" +
            @"          ""state"": ""KANSAS"",
" + "\n" +
            @"          ""zipCode"": ""67219"",
" + "\n" +
            @"          ""countryID"": 226,
" + "\n" +
            @"          ""country"": ""UNITED STATES"",
" + "\n" +
            @"          ""countyID"": 970,
" + "\n" +
            @"          ""county"": ""SEDGWICK"",
" + "\n" +
            @"          ""preferredPhoneTypeID"": 0,
" + "\n" +
            @"          ""preferredPhoneType"": ""string"",
" + "\n" +
            @"          ""phone1"": ""3165249196"",
" + "\n" +
            @"          ""extn1"": ""string"",
" + "\n" +
            @"          ""phone2"": ""3165540354"",
" + "\n" +
            @"          ""extn2"": ""string"",
" + "\n" +
            @"          ""fax"": ""string"",
" + "\n" +
            @"          ""mobilePhone"": ""3162594501"",
" + "\n" +
            @"          ""email"": ""tyler.kauer@ks.gov"",
" + "\n" +
            @"          ""webpage"": ""string"",
" + "\n" +
            @"          ""inactive"": false,
" + "\n" +
            @"          ""createdby"": 28,
" + "\n" +
            @"          ""createdDatetime"": ""2019-01-06T17:16:40""
" + "\n" +
            @"        }
" + "\n" +
            @"      }
" + "\n" +
            @"    ]
" + "\n" +
            @"  }
" + "\n" +
            @"}";
        }
    }
}