using System.Diagnostics;
using System.Threading.Tasks;

namespace ChatFirst.Channels.Spark.Services
{
    public interface IChannelsService
    {
        Task<string> GetBearerToken(string userToken, string botName);
        Task<string> GetBotToken(string userToken, string botName);
        Task SaveWebhookId(string userToken, string botName, object id);
    }

    public class ChannelService : IChannelsService
    {
        public Task<string> GetBearerToken(string userToken, string botName)
        {
            return Task.FromResult("OWYzMTcyNDAtYTYyZC00Nzk2LTg1NDAtMGQ5ZmUwMmRlZDQ0YjAzMmY3ZWEtMmMz");
        }

        public Task<string> GetBotToken(string userToken, string botName)
        {
            return Task.FromResult("MGM5ODI0ZDAtZmQ2OC00YzA2LWI1OTgtYjI3OTc0ZTY3YzVhZWNkZjEzNWMtNjY5");
        }

        public Task SaveWebhookId(string userToken, string botName, object id)
        {
            Trace.TraceInformation($"Created webhook: {userToken}:{botName}:{id}");
            return Task.FromResult(0);
        }
    }
}