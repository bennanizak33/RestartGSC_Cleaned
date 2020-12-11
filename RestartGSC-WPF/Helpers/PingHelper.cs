using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Web;

namespace RestartGSC_WPF.Helpers
{
    public class PingHelper
    {
        public static PingReply CheckAddress(string ipAddress)
        {
            return new Ping()
                .Send(
                    ipAddress,
                    120,
                    Encoding.ASCII.GetBytes("Ping"),
                    new PingOptions()
                    {
                        DontFragment = true
                    }
                );
        }
    }
}