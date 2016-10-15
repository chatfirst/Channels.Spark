using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using ChatFirst.Channels.Spark.Models;
using ChatFirst.Channels.Spark.Services;

namespace ChatFirst.Channels.Spark.Controllers
{
    public class PushController : ApiController
    {
        private readonly IChannelsService _channelsService = new ChannelService();

        [Route("api/push/{userToken}/{botName}/{userId}")]
        public async Task<IHttpActionResult> Post(string userToken, string botName, string userId, [FromBody] TalkResponce message)
        {
            if (!userId.Contains("-"))
                return BadRequest("Invalid user id for Spark. Expected {roomId}-{peopleId}");

            var botToken = await _channelsService.GetBotToken(userToken, botName);
            var client = new SparkClient(botToken);        
            var ids = userId.Split('-');

            foreach (var item in message.Messages)
            {
                await client.SendMessage(ids[0], item.Text);
            }

            return Ok();
        }
    }

    public class TalkResponce
    {
        /// <summary>
        /// Unique session identifier
        /// </summary>
        public long TalkId { get; set; }

        /// <summary>
        /// Responce Messages
        /// </summary>
        public List<Message> Messages { get; set; }
    }   
}
