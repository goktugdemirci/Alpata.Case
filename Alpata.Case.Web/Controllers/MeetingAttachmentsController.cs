using Alpata.Case.Service.Contracts.Contracts;
using Alpata.Case.Service.Contracts.Dtos.Meeting;
using Alpata.Case.Service.Contracts.Dtos.MeetingAttachment;
using Alpata.Case.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Alpata.Case.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeetingAttachmentsController : ControllerBase
    {
        private readonly IMeetingAttachmentService _meetingAttachmentService;
        public MeetingAttachmentsController(IMeetingAttachmentService meetingAttachmentService)
        {
            _meetingAttachmentService = meetingAttachmentService;
        }

        [HttpGet("{id}")]
        public Task<MeetingAttachmentDto> GetAsync(Guid id)
        {
            return _meetingAttachmentService.GetAsync(id);
        }

        [HttpGet("ByMeetingId/{meetingId}")]
        public Task<List<MeetingAttachmentDto>> GetByMeetingIdAsync(Guid meetingId)
        {
            return _meetingAttachmentService.GetByMeetingIdAsync(meetingId);
        }
        [HttpPost]
        public Task UploadAsync()
        {
            return _meetingAttachmentService.UploadAsync(Request.Form);
        }
        [HttpDelete("{id}")]
        public Task DeleteAsync(Guid Id) 
        {
            return _meetingAttachmentService.DeleteAsync(Id);
        }
    }
}
