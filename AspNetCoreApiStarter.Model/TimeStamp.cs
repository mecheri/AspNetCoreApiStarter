using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspNetCoreApiStarter.Model
{
    public class TimeStamp
    {
        public byte[] Value { get; private set; }

        public TimeStamp(byte[] bytes)
        {
            this.Value = bytes;
        }

        /// <summary>
        /// Permet la conversion explicite vers un objet timestamp.
        /// </summary>
        /// <param name="value">Le tableau d'octets à convertir.</param>
        public static explicit operator TimeStamp(byte[] value)
        {
            return new TimeStamp(value);
        }

        /// <summary>
        /// Permet la conversion explicite vers un objet timestamp.
        /// </summary>
        /// <param name="value">Le tableau d'octets à convertir.</param>
        public static explicit operator TimeStamp(ulong value)
        {
            return new TimeStamp(BitConverter.GetBytes(value));
        }
        /// <summary>
        /// Permet la conversion implicite d'un objet TimeStamp vers ulong.
        /// </summary>
        /// <param name="email">L'objet email à convertir.</param>
        public static implicit operator ulong(TimeStamp ts)
        {
            return BitConverter.ToUInt64(ts.Value, 0);
        }
    }
}
