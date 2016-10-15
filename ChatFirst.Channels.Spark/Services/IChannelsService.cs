using System.Threading.Tasks;

namespace ChatFirst.Channels.Spark.Services
{
    public interface IChannelsService
    {
        Task<string> GetBearerToken(string userToken, string botName);
        Task<string> GetBotToken(string userToken, string botName);
    }

    public class ChannelService : IChannelsService
    {
        public Task<string> GetBearerToken(string userToken, string botName)
        {
            return Task.FromResult("OWYzMTcyNDAtYTYyZC00Nzk2LTg1NDAtMGQ5ZmUwMmRlZDQ0YjAzMmY3ZWEtMmMz");
        }

        public Task<string> GetBotToken(string userToken, string botName)
        {
            return Task.FromResult("");
        }
    }
}