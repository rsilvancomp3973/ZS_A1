using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZenithDataLib.Models
{
    public class ActivityType
    {

        public int ActivityTypeId { get; set; }

        [MaxLength(40), MinLength(2)]
        [Display(Name = "Activity Description"), Required]
        public string ActivityDescription { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Created on"), Required]
        public DateTime CreationDate { get; set; }
        public List<Event> Events { get; set; }
    }
}
