using Alpata.Case.Domain.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alpata.Case.Domain.Models
{
    public class MeetingAttachment : Entity
    {
        public string FileName { get; set; }
        public string GuidName { get; set; }
        [ForeignKey("Meeting")]
        public Guid MeetingId { get; set; }
        public virtual Meeting Meeting { get; set; }
    }
}
