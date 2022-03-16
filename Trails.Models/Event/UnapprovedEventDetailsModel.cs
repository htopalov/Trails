using Trails.Data.Enums;

namespace Trails.Models.Event
{
    public class UnapprovedEventDetailsModel
    {
        public int Counter { get; set; }

        public string Description { get; set; }

        public double Length { get; set; }

        public EventType Type { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Creator { get; set; }

        public string CreatorPhoneNumber { get; set; }

        public string CreatorEmail { get; set; }
    }
}
