using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ChatFirst.Channels.Spark.Services;

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
            var id = await sparkClient.SendMessage(text.roomId, text.text);

            return Ok();
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
