using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;

namespace ChatFirst.Channels.Spark.Services
{
    public interface ISparkClient
    {
        Task<SparkMessage> GetMessage(string id);
        Task<string> SendMessage(string roomId, string text);
    }

    public class SparkClient : ISparkClient
    {
        private readonly string _bearerToken;
        private readonly IRestClient _client = new RestClient("https://api.ciscospark.com/v1");

        public SparkClient(string bearerToken)
        {
            _bearerToken = bearerToken;
            _client.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator("Bearer", bearerToken);
        }

        public async Task<SparkMessage> GetMessage(string id)
        {
            var request = new RestRequest("messages/{messageId}");
            request.AddUrlSegment("messageId", id);

            var result = await _client.ExecuteTaskAsync<SparkMessage>(request);
            Trace.TraceInformation(result.Content);

            return result.Data;
        }

        public async Task<string> SendMessage(string roomId, string text)
        {
            var request = new RestRequest("messages", Method.POST);
            request.AddJsonBody(new
            {
                roomId = roomId,
                text = text
            });

            var result = await _client.ExecuteTaskAsync(request);
            Trace.TraceInformation(result.Content);

            dynamic data = JObject.Parse(result.Content);

            return data.id;
        }
    }

//    {
//  "roomId" : "Y2lzY29zcGFyazovL3VzL1JPT00vYmJjZWIxYWQtNDNmMS0zYjU4LTkxNDctZjE0YmIwYzRkMTU0",
//  "toPersonId" : "Y2lzY29zcGFyazovL3VzL1BFT1BMRS9mMDZkNzFhNS0wODMzLTRmYTUtYTcyYS1jYzg5YjI1ZWVlMmX",
//  "toPersonEmail" : "julie@example.com",
//  "text" : "PROJECT UPDATE - A new project plan has been published on Box: http://box.com/s/lf5vj. The PM for this project is Mike C. and the Engineering Manager is Jane W.",
//  "markdown" : "**PROJECT UPDATE** A new project plan has been published [on Box](http://box.com/s/lf5vj). The PM for this project is <@personEmail:mike@example.com> and the Engineering Manager is <@personEmail:jane@example.com>.",
//  "files" : [ "http://www.example.com/images/media.png" ]
//}

public class SparkMessage
    {
        public string id { get; set; }
        public string roomId { get; set; }
        public string roomType { get; set; }
        public string toPersonId { get; set; }
        public string toPersonEmail { get; set; }
        public string text { get; set; }
        public string markdown { get; set; }
        public List<string> files { get; set; }
        public string personId { get; set; }
        public string personEmail { get; set; }
        public string created { get; set; }
        public List<string> mentionedPeople { get; set; }
    }
}