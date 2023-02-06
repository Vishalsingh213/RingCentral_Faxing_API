using System;
using System.Dynamic;
using System.Text;
using Org.BouncyCastle.Asn1.Ocsp;
using RingCentral;
using Send_SMS;

namespace Send_Fax
{
    class Program
    {
        const string RECIPIENT = "4703749786";  // "(470) 374-9786";//4703749786
        const string RINGCENTRAL_CLIENTID = "NpSPSuTvT3WkhNizHN5W0A";
        const string RINGCENTRAL_CLIENTSECRET = "Bpos0UbbReS2KdtBM0kkDA3xypTcNuSqeqBRpqXyVWdQ";
        const bool RINGCENTRAL_PRODUCTION = false;

        const string RINGCENTRAL_USERNAME = "+14703749786";
        const string RINGCENTRAL_PASSWORD = "Curitics@0734";
        const string RINGCENTRAL_EXTENSION = "101";
        const string accountId = "+14703749786";
        const string extensionId = "101";
        const string messageId =/* "12470766004";*//*"12472105004"*/ "12520224004";

        static RestClient restClient;

        static void Main(string[] args)
        {
            restClient = new RestClient(RINGCENTRAL_CLIENTID, RINGCENTRAL_CLIENTSECRET, RINGCENTRAL_PRODUCTION);
            restClient.Authorize(RINGCENTRAL_USERNAME, RINGCENTRAL_EXTENSION, RINGCENTRAL_PASSWORD).Wait();
            //send_fax().Wait();
            getS().Wait();
        }

        static private async Task send_fax()
        {
            var attachment1 = new Attachment { fileName = "Brand_New_Day_5064.pdf", contentType = "application/pdf", bytes = System.IO.File.ReadAllBytes("Brand_New_Day_5064.pdf") };
            var extension = restClient.Restapi().Account().Extension();

            var attachments = new Attachment[] { attachment1 };
            var response = extension.Fax().Post(new
            {
                to = new CallerInfo[] { new CallerInfo { phoneNumber = RECIPIENT } }
            }, attachments).Result;
            Console.WriteLine("Fax sent. Message status: " + response.messageStatus);
            Console.WriteLine(response);
        }

        static private async Task getS()
        {
            var res = await restClient.Restapi().Account().Extension().MessageStore(messageId).Get();
            Console.WriteLine(DateTime.Parse(res.lastModifiedTime));
            Console.WriteLine(res);
        }
    }
}
