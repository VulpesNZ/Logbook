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

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Image")]
        public string ImageUrl { get; set; }
        
        public IEnumerable<FieldDTO> Fields { get; set; }
    }

    public class EditFieldViewModel
    {
        public Guid FieldId { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        public IEnumerable<FieldOptionDTO> FieldOptions { get; set; }
    }

    public class EditFieldOptionViewModel
    {
        public Guid FieldOptionId { get; set; }

        [Display(Name = "Name")]
        public string Text { get; set; }
    }
}