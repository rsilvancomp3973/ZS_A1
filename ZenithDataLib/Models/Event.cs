using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZenithDataLib.Models
{
    public class Event
    {

        public int EventId { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Event Start Time"), Required]
        public DateTime EventFrom { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Event End Time"), Required]
        public DateTime EventTo { get; set; }

        [Display(Name = "Created by")]
        public ApplicationUser EnteredBy { get; set; }

        public ActivityType ActivityType { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Created on"), Required]
        public DateTime CreationDate { get; set; }

        [Display(Name = "Is Active"), Required]
        public Boolean isActive { get; set; }

        public int ActivityTypeId { get; set; }


    }
}
