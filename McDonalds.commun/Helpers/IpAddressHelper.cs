using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace McDonalds.Commun.Helpers
{
    public class IpAddressHelper
    {
        #region [Utils] IpToCc / CcToIp

        public static IPAddress CcToIp(int cc, byte host = 0)
        {
            byte[] parts = new byte[4];

            parts[0] = 10;
            parts[1] = cc < 1536 ? Convert.ToByte((cc / 256) + 19) : Convert.ToByte((cc / 256) - 2);
            parts[2] = Convert.ToByte(cc % 256);
            parts[3] = host;
            return new IPAddress(parts);
        }

        public static int IpToCc(IPAddress ip)
        {
            int cc = 0;
            byte[] digits = ip.GetAddressBytes();

            switch (digits[1])
            {
                case 4:
                case 5:
                    cc = Convert.ToInt32(digits[2]) + 256 * (Convert.ToInt32(digits[1]) + 2);
                    break;
                case 19:
                    cc = Convert.ToInt32(digits[2]);
                    break;
                default:
                    cc = Convert.ToInt32(digits[2]) + 256 * (Convert.ToInt32(digits[1]) - 19);
                    break;
            }

            return cc;
        }

        #endregion
    }
}