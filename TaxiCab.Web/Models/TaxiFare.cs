using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaxiCab.Web.Models
{
    public class TaxiFare
    {
        public int Id { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [Required]
        [DataType(DataType.Time)]
        public TimeSpan Time { get; set; }
        [Required]
        [Display(Name = "Start at")]
        public string StartAt { get; set; }
        [Required]
        [Display(Name = "End at")]
        public string EndAt { get; set; }
        [Required]
        [Display(Name = "Miles at less 6 mph")]
        public int MilesLess6mph { get; set; }
        [Required]
        [Display(Name = "Time at less 6 mph")]
        public int TimeLess6mph { get; set; }
        [Required]
        [Display(Name = "Time in no motion")]
        public int TimeInNoMotion { get; set; }
        [Required]
        [Display(Name = "Time at more than 6 mph")]
        public int TimeMore6mph { get; set; }
        [Required]
        [Display(Name = "New York Tax")]
        public bool NewYorkTax { get; set; }
        public float Total { get; set; }
    }
}