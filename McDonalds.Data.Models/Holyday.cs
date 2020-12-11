using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace McDonalds.Data.Models
{
    public class Holyday
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HolydayId { get; set; }

        public string Code { get; set; }

        public string Label { get; set; }

        public DateTime Date { get; set; }
    }
}