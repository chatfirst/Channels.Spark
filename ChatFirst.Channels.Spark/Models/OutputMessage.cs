using System.Collections.Generic;

namespace ChatFirst.Channels.Spark.Models
{
    public class OutputMessage
    {
        /// <summary>
        /// Unique session identifier
        /// </summary>
        public int TalkId { get; set; }

        /// <summary>
        /// Responce Messages
        /// </summary>
        public List<Message> Messages { get; set; }
    }
}