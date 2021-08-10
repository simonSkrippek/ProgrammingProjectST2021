using System;

namespace WhackAStoodent.Helper
{
    public static class ByteArrayExtension
    {
        public  static byte[] GetSubArray(this byte[] originalArray, uint startIndex, uint length)
        {
            var ret = new byte[length];
            Array.Copy(originalArray, startIndex, ret, 0, length);
            return ret;
        }
        public  static byte[] GetSubArray(this byte[] originalArray, int startIndex, int length)
        {
            var ret = new byte[length];
            Array.Copy(originalArray, startIndex, ret, 0, length);
            return ret;
        }
    }
}