using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Logbook.Core.DTO;

namespace LogbookUI.Models
{
    public class HomeViewModel
    {
        public string ActiveLogbookName { get; set; }
        public Guid ActiveLogbookId { get; set; }
        public AnnouncementPreview[] Announcements { get; set; }
    }

    public class AnnouncementViewModel
    {
        public Guid AnnouncementId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime PublishDate { get; set; }
    }
}