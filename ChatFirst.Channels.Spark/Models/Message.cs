using System.Collections.Generic;

namespace ChatFirst.Channels.Spark.Models
{
    public class Message
    {
        /// <summary>
        /// Unique message identifier
        /// </summary>
        public int MessageId { get; set; }

        /// <summary>
        /// Optional. For text messages, the actual UTF-8 text of the message, 0-4096 characters.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Optional. Message is a photo, information about the photo
        /// </summary>
        public Entity Photo { get; set; }

        /// <summary>
        /// Optional. Message is a gallery
        /// </summary>
        public List<Entity> LinkedEntities { get; set; }

        /// <summary>
        /// Optional. Custom keyboards to send in answer if any
        /// </summary>
        public List<List<string>> KeyboardOptions { get; set; }
    }
}