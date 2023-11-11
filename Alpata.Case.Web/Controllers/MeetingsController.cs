using Alpata.Case.Service.Contracts.Contracts;
using Alpata.Case.Service.Contracts.Dtos.Meeting;
using Alpata.Case.Service.Contracts.Dtos.User;
using Alpata.Case.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Alpata.Case.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeetingsController : ControllerBase
    {
        private readonly IMeetingService _meetingsService;
        public MeetingsController(IMeetingService meetingService)
        {
                _meetingsService = meetingService;
        }
        [HttpGet]
        public Task<IEnumerable<MeetingDto>> GetAllAsync()
        {
            return _meetingsService.GetAllAsync();
        }
        [HttpGet("{id}")]
        public Task<MeetingDto> GetByIdAsync(Guid id)
        {
            return _meetingsService.GetByIdAsync(id);
        }
        [HttpPost]
        public Task<MeetingDto> CreateAsync(MeetingCreateDto meetingCreateDto)
        {
            return _meetingsService.AddAsync(meetingCreateDto);
        }
        [HttpPut]
        public Task UpdateAsync(MeetingUpdateDto meetingUpdateDto)
        {
            return _meetingsService.UpdateAsync(meetingUpdateDto);
        }

        [HttpDelete("{id}")]
        public Task DeleteAsync(Guid id)
        {
            return _meetingsService.DeleteAsync(id);
        }


    }
}
