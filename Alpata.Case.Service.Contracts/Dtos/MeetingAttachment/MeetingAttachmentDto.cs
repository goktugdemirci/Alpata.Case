using Alpata.Case.Service.Contracts.Dtos.Base;

namespace Alpata.Case.Service.Contracts.Dtos.MeetingAttachment
{
    public class MeetingAttachmentDto : EntityDto
    {
        public string FileName { get; set; }
        public string GuidName { get; set; }
    }
}
