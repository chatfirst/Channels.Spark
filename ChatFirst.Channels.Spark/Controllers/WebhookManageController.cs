using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ChatFirst.Channels.Spark.Services;
using RestSharp;
using RestSharp.Authenticators;

namespace ChatFirst.Channels.Spark.Controllers
{
    [Route("webhook/manage")]
    public class WebhookManageController : ApiController
    {
        private const string uri = "https://api.ciscospark.com/v1";
        IChannelsService _channelsService = new ChannelService();


        // GET: api/WebhookManage
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/WebhookManage/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/WebhookManage
        public async Task<IHttpActionResult> Post(string userToken, string botName)
        {
            var rq = new RestClient(uri)
            {
                Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator("Bearer",
                    await _channelsService.GetBearerToken(userToken, botName))
            };

            var rc = new RestRequest("webhook", Method.POST);

            var builder = new UriBuilder("https", "ch-channel-spark.azurewebsites.net", 443, $"api/webhook/{userToken}/{botName}");

            rc.AddJsonBody(
                new
                {
                    name = $"ChatFirst WebHook for {botName}",
                    targetUrl = builder.Uri.ToString(),
                    resource = "messages",
                    @event = "created",
                    secret = $"{userToken}:{botName}" // todo hash it
                });

            return Ok();
        }

        public class WebhookCreateRequest
        {
            public string name { get; set; }
            public string targetUrl { get; set; }
            public string resource { get; set; }
            public string @event { get; set; }
            public string filter { get; set; }
            public string secret { get; set; }
        }

        // PUT: api/WebhookManage/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/WebhookManage/5
        public void Delete(int id)
        {
        }
    }
}
