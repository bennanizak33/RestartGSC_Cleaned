using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace McDonalds.Data.Models
{
	public enum Event
	{
		Blocage,
		RedemarrageOK,
		RedemarrageNOK,
		Maintenance,
		DemandeRejete,
		PingOk,
		PingNOK
	}
}