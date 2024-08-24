using System;
using System.Collections.Generic;

namespace OnlineCateringProject.Models
{
    public partial class Message
    {
        public int MessageId { get; set; }
        public int? SenderId { get; set; }
        public int? ReceiverId { get; set; }
        public string? MessageText { get; set; }
        public DateTime? SentDate { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
