
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using ControlBS.BusinessObjects.Response;
using ControlBS.WebApi.Utils.Helpers;
using Microsoft.Extensions.Options;
using Quartz;
using Serilog;

namespace QuartzJob
{
    public class SendNotificationJob
     : IJob
    {
        private readonly AppSettings _appSettings;

        public SendNotificationJob(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("Authorization", $"key = {_appSettings.SecretFCM}");
                using StringContent jsonContent = new(
                JsonSerializer.Serialize(new
                {
                    to = "/topics/all",
                    notification = new
                    {
                        body = "Ya puedes marcar tu asistencia",
                        title = "ControlBS",
                        redirect = "product"
                    },
                    data = new
                    {
                        title = "Push Notification",
                        message = "Test Push Notifiication",
                        redirect = "product"
                    },
                    priority = "high"
                }),
                Encoding.UTF8,
                "application/json");

                using HttpResponseMessage response = await httpClient.PostAsync(
                    " https://fcm.googleapis.com/fcm/send",
                    jsonContent);


                var jsonResponse = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"{jsonResponse}\n");
            }
            catch (System.Exception e)
            {
                Response<ErrorResponse> errorResponse = new Response<ErrorResponse>(e);
                Log.Error(errorResponse.errors.First().ToString());
                throw;
            }
        }
    }
}