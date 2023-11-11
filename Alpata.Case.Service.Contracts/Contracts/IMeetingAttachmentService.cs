using Alpata.Case.Service.Contracts.Dtos.MeetingAttachment;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alpata.Case.Service.Contracts.Contracts
{
    public interface IMeetingAttachmentService
    {
        public Task UploadAsync(IFormCollection form);
        public Task DeleteAsync(Guid id);
        public Task<MeetingAttachmentDto> GetAsync(Guid id);
        public Task<List<MeetingAttachmentDto>> GetByMeetingIdAsync(Guid meetingId);
    }
}
