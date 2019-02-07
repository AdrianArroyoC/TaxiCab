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
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        [Required]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm}", ApplyFormatInEditMode = true)]
        public TimeSpan Time { get; set; }
        [DisplayFormat(DataFormatString = "{0:hh\\:mm tt}")]
        public DateTime? TimeForDisplay { get { return (DateTime?)DateTime.Today.Add(Time); } }
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
        private float _Total;

        public float Total
        { 
            get
            {
                return _Total;
            }
            set
            {
                float total = 3.0f;
                int timeCovered = TimeInNoMotion + TimeMore6mph;
                TimeSpan startTime = Time;
                TimeSpan endTime = EndTime(startTime, TimeLess6mph + timeCovered);
                if (!Date.DayOfWeek.Equals(DayOfWeek.Saturday) && !Date.DayOfWeek.Equals(DayOfWeek.Sunday))
                {
                    if (IntoHours(startTime, endTime, new TimeSpan(16, 0, 0), new TimeSpan(20, 0, 0)))
                    {
                        total += 1.0f;
                    }
                }
                if (IntoHours(startTime, endTime, new TimeSpan(20, 0, 0), new TimeSpan(23, 59, 0)))
                {
                    total += 0.5f;
                }
                else if (IntoHours(startTime, endTime, new TimeSpan(0, 0, 0), new TimeSpan(6, 0, 0)))
                {
                    total += 0.5f;
                }
                total += (MilesLess6mph * 5 * 0.35f);
                total += (timeCovered * 0.35f);
                if (NewYorkTax)
                {
                    total += 0.5f;
                }
                _Total = total;
            }
        }

        public TimeSpan EndTime(TimeSpan startTime, int time)
        {
            int hours = startTime.Hours;
            int minutes = 0;
            if (time > 60)
            {
                hours += (time / 60);
                minutes = time % 60;
            }
            else
            {
                minutes = time;
            }
            minutes += startTime.Minutes;
            if (minutes > 60)
            {
                hours++;
                minutes %= 60;
            }
            if (hours > 23)
            {
                hours -= 24;
            }
            return new TimeSpan(hours, minutes, 0);
        }

        public bool IntoHours(TimeSpan startTime, TimeSpan endTime, TimeSpan startLimit, TimeSpan endLimit)
        {
            bool into = false;
            if (startTime >= startLimit && startTime <= endLimit)
            {
                into = true;
            }
            if (endTime >= startLimit && endTime <= endLimit)
            {
                into = true;
            }
            return into;
        }
    }
}