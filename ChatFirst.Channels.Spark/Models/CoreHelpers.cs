using System;
using System.Diagnostics;
using System.Text;

namespace ChatFirst.Channels.Spark.Models
{
    /// <summary>
    /// Helpers methods to communicate with core
    /// </summary>
    public static class CoreHelpers
    {
        /// <summary>
        /// Decode input message from Base64
        /// </summary>
        /// <param name="base64EncodedData"></param>
        /// <returns></returns>
        public static string Base64Decode(string base64EncodedData)
        {
            try
            {
                var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
                return Encoding.UTF8.GetString(base64EncodedBytes);
            }
            catch (Exception ex)
            {
                Trace.TraceError($"Failed to decode base64 with: {ex.Message} on {ex.StackTrace}");
                return base64EncodedData;
            }
        }
    }
}