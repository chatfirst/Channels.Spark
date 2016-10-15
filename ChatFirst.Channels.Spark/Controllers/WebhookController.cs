using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ChatFirst.Channels.Spark.Models;
using ChatFirst.Channels.Spark.Services;
using RestSharp;
using RestSharp.Authenticators;

namespace ChatFirst.Channels.Spark.Controllers
{
    public class WebhookController : ApiController
    {
        private readonly IChannelsService _channelsService = new ChannelService();

        public WebhookController()
        {
            
        }

        public WebhookController(IChannelsService channelsService)
        {
            //_channelsService = channelsService;
        }

        [Route("api/webhook/{userToken}/{botName}")]
        public async Task<IHttpActionResult> Post(string userToken, string botName, [FromBody] WebhookMessage message)
        {
            Trace.TraceInformation($"Get data from {message.id}");

            if (message.data.personEmail.Contains("sparkbot.io"))
                return Ok();

            ISparkClient sparkClient = new SparkClient(await _channelsService.GetBotToken(userToken, botName));

            var text = await sparkClient.GetMessage(message.data.id);
            var nameParts = message.name.Split(' ');
            var coreAnswer = await Talk(userToken, botName, new InputMessage()
            {
                InterlocutorId = $"{message.data.roomId}-{message.data.personId}",
                FirstName = nameParts.FirstOrDefault(),
                LastName = nameParts.LastOrDefault(),
                Text = text.text,
                Username = text.personEmail
            });

            foreach (var item in coreAnswer.Messages)
            {
                await sparkClient.SendMessage(text.roomId, item.Text);
            }

            return Ok();
        }

        private async Task<OutputMessage> Talk(string userToken, string botName, InputMessage message)
        {
            //var bt = new LogBotToken(userToken, botName, message.InterlocutorId.ToString(), "telegram");
            var client = new RestClient("https://ch-core-mp.azurewebsites.net")
            {
                Authenticator = new HttpBasicAuthenticator(userToken, string.Empty)
            };

            var rq = new RestRequest("v2/talk/{botname}");
            rq.AddUrlSegment("botname", botName);
            rq.AddJsonBody(message);

            var result = await client.ExecutePostTaskAsync<OutputMessage>(rq);
            Trace.TraceInformation(result.Content);

            result.Data.Messages = result.Data.Messages.Select(msg =>
            {
                msg.Text = CoreHelpers.Base64Decode(msg.Text);
                return msg;
            }).ToList();

            return result.Data;
        }
    }



    public class Data
    {
        public string id { get; set; }
        public string roomId { get; set; }
        public string personId { get; set; }
        public string personEmail { get; set; }
        public string created { get; set; }
    }

    public class WebhookMessage
    {
        public string id { get; set; }
        public string name { get; set; }
        public string resource { get; set; }
        public string @event { get; set; }
        public string filter { get; set; }
        public Data data { get; set; }
    }
}
