using AspNetCoreApiStarter.Model;
using System;
using Xunit;

namespace AspNetCoreApiStarter.Tests
{
    public class TimeStamp_Tests
    {
        [Fact]
        public void TimeStamp_BigEndian()
        {
            // sql server transmet le timestamp en big endian
            // .net & windows on une gestion en little endian
            byte[] byteArrayBigEndian = { 0, 0, 0, 0, 0, 0, 0, 8 };
            Array.Reverse((byte[])byteArrayBigEndian); // conversion effectuée dans le type handler pour dapper

            // byte [] to ulong
            TimeStamp tsOut = new TimeStamp(byteArrayBigEndian);
            ulong uOut = tsOut;

            // ulong to byte
            TimeStamp tsIn = (TimeStamp)uOut;

            Assert.Equal(byteArrayBigEndian, tsIn.Value);
        }
    }
}
