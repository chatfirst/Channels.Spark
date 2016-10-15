using System.Collections.Generic;

namespace ChatFirst.Channels.Spark.Models
{
    public class Entity
    {
        public string Handle { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }

        /// <summary>
        /// Options, what to do with selected entity
        /// </summary>
        public List<string> EntityOptions { get; set; }
    }
}