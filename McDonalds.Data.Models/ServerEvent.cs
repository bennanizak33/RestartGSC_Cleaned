using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace McDonalds.Data.Models
{
	public class ServerEvent
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
		public int ServerEventId { get; set; }

        public int RestaurantId { get; set; }

        public Event Event { get; set; }

		public DateTime Date { get; set; }

        public DateTime? UpTimes { get; set; }

        public string Detail { get; set; }
	}
}