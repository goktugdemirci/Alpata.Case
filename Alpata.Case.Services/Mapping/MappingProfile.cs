using Alpata.Case.Domain.Models;
using Alpata.Case.Service.Contracts.Dtos.Meeting;
using Alpata.Case.Service.Contracts.Dtos.MeetingAttachment;
using Alpata.Case.Service.Contracts.Dtos.User;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alpata.Case.Services.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(x=>x.FullName, y=> y.MapFrom(t=> $"{t.Name} {t.Surname}"));
            CreateMap<UserCreateDto, User>();
            CreateMap<UserUpdateDto, User>();

            CreateMap<Meeting, MeetingDto>();
            CreateMap<MeetingCreateDto, Meeting>();
            CreateMap<MeetingUpdateDto, Meeting>();

            CreateMap<MeetingAttachment, MeetingAttachmentDto>();
        }
    }
}
