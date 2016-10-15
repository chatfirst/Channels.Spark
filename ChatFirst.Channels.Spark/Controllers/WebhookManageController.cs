using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ChatFirst.Channels.Spark.Services;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;

namespace ChatFirst.Channels.Spark.Controllers
{
    [Route("webhook/manage/{userToken}/{botName}")]
    public class WebhookManageController : ApiController
    {
        private const string uri = "https://api.ciscospark.com/v1";
        IChannelsService _channelsService = new ChannelService();

        // POST: api/WebhookManage
        public async Task<IHttpActionResult> Post(string userToken, string botName)
        {
            Trace.TraceInformation($"Setting webhook {userToken}:{botName}");

            var rq = new RestClient(uri)
            {
                Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(
                    await _channelsService.GetBotToken(userToken, botName), "Bearer")
            };

            var rc = new RestRequest("webhooks", Method.POST);

            var builder = new UriBuilder("https", "ch-channel-spark.azurewebsites.net", 443, $"api/webhook/{userToken}/{botName}");
            var webhook = builder.Uri.ToString();
            Trace.TraceInformation($"Webhook to set {webhook}");

            rc.AddJsonBody(
                new
                {
                    name = $"ChatFirst WebHook for {botName}",
                    targetUrl = webhook,
                    resource = "messages",
                    @event = "created",
                    secret = $"{userToken}:{botName}" // todo hash it
                });

            var result = await rq.ExecuteTaskAsync(rc);
            
            Trace.TraceInformation($"Answer {result.ResponseStatus}/{result.StatusCode}: {result.Content}");

            if (result.StatusCode != HttpStatusCode.OK)
                return Ok(result.Content);

            dynamic data = JObject.Parse(result.Content);

            await _channelsService.SaveWebhookId(userToken, botName, data.id);

            return Ok(result.Content);
        }


        // DELETE: api/WebhookManage/5
        public Task<IHttpActionResult> Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
