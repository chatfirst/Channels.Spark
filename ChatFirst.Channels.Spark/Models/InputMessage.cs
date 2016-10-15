namespace ChatFirst.Channels.Spark.Models
{
    public class InputMessage
    {
        /// <summary>
        /// Unique id of interlocutor prior to current bot
        /// </summary>
        public string InterlocutorId { get; set; }

        /// <summary>
        /// Message text
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Username of interlocutor if any
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// First name of interlocutor if any
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Last name of interlocutor if any
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Profile pic url
        /// </summary>
        public string ProfilePic { get; set; }

        /// <summary>
        /// User locale language[_territory] where a two-letter language code is from ISO 639, 
        /// a two-letter territory code is from ISO 3166
        /// </summary>
        public string Locale { get; set; }

        /// <summary>
        /// Timezone modfier
        /// </summary>
        public int Timezone { get; set; }

        /// <summary>
        /// User gender
        /// </summary>
        public string Gender { get; set; }
    }
}