using Alpata.Case.Domain.Models;
using Alpata.Case.Domain.Repository;
using Alpata.Case.Service.Contracts.Common;
using Alpata.Case.Service.Contracts.Contracts;
using Alpata.Case.Service.Contracts.Dtos.MeetingAttachment;
using Alpata.Case.Services.Base;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alpata.Case.Services
{
    public class MeetingAttachmentService : IMeetingAttachmentService
    {
        private readonly IRepository<MeetingAttachment> _repository;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        public MeetingAttachmentService(IRepository<MeetingAttachment> repository, IMapper mapper, IFileService fileService)
        {
            _repository = repository;
            _mapper = mapper;
            _fileService = fileService;
        }
        public async Task DeleteAsync(Guid id)
        {
            var entity =  await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(id);
            _fileService.Delete(entity.GuidName);
        }

        public async Task<MeetingAttachmentDto> GetAsync(Guid id)
        {
            var entity = await _repository.GetByIdAsync(id);
            var mappedEntity = _mapper.Map<MeetingAttachmentDto>(entity);
            return mappedEntity;
        }

        public async Task<List<MeetingAttachmentDto>> GetByMeetingIdAsync(Guid meetingId)
        {
            List<MeetingAttachment> entityList = await _repository.GetQueryable().Where(x => x.MeetingId == meetingId).ToListAsync();
            List<MeetingAttachmentDto> entityDtoList = _mapper.Map<List<MeetingAttachmentDto>>(entityList);
            return entityDtoList;
        }

        public async Task UploadAsync(IFormCollection form)
        {
            Guid meetingId = Guid.Parse(form["meetingId"]);
            foreach (IFormFile item in form.Files)
            {
                var meetingAttachment = new MeetingAttachment();
                meetingAttachment.FileName = item.FileName;
                meetingAttachment.GuidName = await _fileService.CopyFileToDirectory(item);
                meetingAttachment.MeetingId = meetingId;
                await _repository.AddAsync(meetingAttachment);
            }
        }
    }
}
