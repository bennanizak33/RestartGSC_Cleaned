using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace McDonalds.Data.Models
{
	public class Restaurant
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int RestaurantId { get; set; }

		public string Nom { get; set; }

		public string ShortName { get; set; }

		public string ServerName { get; set; }

		public string ServerIpAddress { get; set; }

		public string Email { get; set; }

		public string City { get; set; }

		public string Address_1 { get; set; }

		public string Address_2 { get; set; }

		public string Address_3 { get; set; }

		public string Address_4 { get; set; }

		public int ZipCode { get; set; }

		public string PhoneNumber { get; set; }

		public string FaxNumber { get; set; }

		public DateTime? OpeningDate { get; set; }

		public DateTime? PermanentClosureDate { get; set; }

	}
}