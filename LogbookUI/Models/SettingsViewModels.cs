using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Logbook.Core.DTO;

namespace LogbookUI.Models
{
    public class ActivitySettingsViewModel
    {
        public IEnumerable<ActivityDTO> Activities { get; set; }
    }

    public class EditActivityViewModel
    {
        public Guid ActivityId { get; set; }

        //   public IEnumerable<ActivityDTO> Activities { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Image")]
        public string ImageUrl { get; set; }
        
        public FieldDTO Fields { get; set; }
    }
}