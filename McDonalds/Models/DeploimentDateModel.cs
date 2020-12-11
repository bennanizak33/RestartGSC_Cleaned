using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace McDonalds.Models
{
    public class DeploimentDateModel
    {
        public int RestaurantId { get; set; }

        public DateTime? DeploimentDate { get; set; }
    }
}