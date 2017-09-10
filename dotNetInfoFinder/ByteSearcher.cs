using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNetVersionFinder
{
    public class ByteSearcher
    {
        public static int Find(byte[] needle, byte[] haystack)
        {
            var haystackLength = haystack.Length;
            var needleLength = needle.Length;
            var found = false;

            if (needleLength <= haystackLength)
            {
                for (int h = haystackLength - needleLength; h >= needleLength; h--)
                {
                    found = true;
                    for (int n = 0; n < needleLength; n++)
                    {
                        if (haystack[h + n] != needle[n])
                        {
                            found = false;

                            // Leave inner loop
                            n = needleLength;
                        }
                    }

                    if (found)
                    {
                        return h;
                    }
                }
            }

            return -1;
        }

        public static byte[] ReturnBytes(byte[] byteArray, int startingPoint, int numberOfBytes)
        {
            var returnBytes = new byte[numberOfBytes];

            try
            {
                for (int i = 0; i < numberOfBytes; i++)
                {
                    returnBytes[i] = byteArray[i + startingPoint];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return returnBytes;
        }

    }
}
