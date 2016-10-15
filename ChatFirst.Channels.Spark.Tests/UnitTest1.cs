using System;
using System.Diagnostics;
using Devchamp.SentenceCompression;
using NUnit.Framework;

namespace ChatFirst.Channels.Spark.Tests
{
    [TestFixture]
    public class UnitTest1
    {
        [TestCase("Y2lzY29zcGFyazovL3VzL1JPT00vOWQ2YzA1MTAtOTJjYS0xMWU2LWJlZmMtODk3YjI0ZjVkY2Qy")]
        [TestCase("Y2lzY29zcGFyazovL3VzL1BFT1BMRS9hMDgxNjFlYy1jYmU5LTQzODAtYWVkYS0zNTFlZTUwMmRkYWY")]
        [TestCase("http://github.com/antirez/smaz/tree/master")] 
        public void TestMethod1(string str)
        {
            var actual = SmazSharp.Smaz.Compress(str);

            Console.WriteLine($"length: {actual.Length}/{str.Length}");
            Assert.That(actual.Length, Is.LessThanOrEqualTo(str.Length));

            var decom = SmazSharp.Smaz.Decompress(actual);

            Assert.That(decom, Is.EqualTo(Convert.ToString(decom)));
        }

    }
}
