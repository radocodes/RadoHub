using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;

namespace RadoHub.Services.Services
{
    public class WebPingService : BackgroundService
    {
        private readonly IEnumerable<string> urlsForPinging;
        private readonly Ping pingSender;
        private readonly HttpClient httpClient;

        public WebPingService()
        {
            this.urlsForPinging = new string[]
            {
                "http://emaivanova.com",
                "https://radohub.com",
            };

            this.pingSender = new Ping();
            this.httpClient = new HttpClient();
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                foreach (var url in urlsForPinging)
                {
                    var request = await httpClient.GetAsync(url);
                    //Debug.WriteLine($"===== test GET request: {url} - time: {DateTime.Now} =====");


                    // Only ping approach didn't awake pinged application at server successfully (following)
                    //var reply = await pingSender.SendPingAsync(url);
                    //Debug.WriteLine($"{reply.Buffer.Length} bytes from {reply.Address}: url={url} status={reply.Status} time={reply.RoundtripTime}ms");
                }

                await Task.Delay(TimeSpan.FromMinutes(19), stoppingToken);
            }
        }
    }
}
