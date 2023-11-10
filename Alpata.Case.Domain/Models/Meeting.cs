using Alpata.Case.Domain.Base;

namespace Alpata.Case.Domain.Models
{
    public class Meeting : Entity
    {
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string? Explanation { get; set; }
        public virtual ICollection<MeetingAttachment> Attachments { get; set; }
    }
}
