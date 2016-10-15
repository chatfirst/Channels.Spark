using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ChatFirst.Channels.Spark.Services;

namespace ChatFirst.Channels.Spark.Controllers
{
    public class UsersController : ApiController
    {
        private readonly IChannelsService _channelsService = new ChannelService();

        [Route("api/users/{userToken}/{botName}/{roomId}")]
        public async Task<IHttpActionResult> Get(string userToken, string botName, string roomId)
        {
            var sparkClient = new SparkClient(
                await _channelsService.GetBotToken(userToken, botName));

            var users = await sparkClient.GetUsersInRoom(roomId);

            return Ok(users.Where(i => !i.personDisplayName.Contains("(bot)")).Select(i => new
            {
                userId = i.personId,
                userName = i.personDisplayName  
            }));
        }
    }
}
