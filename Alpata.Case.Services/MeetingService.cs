using Alpata.Case.Domain.Models;
using Alpata.Case.Domain.Repository;
using Alpata.Case.Service.Contracts.Contracts;
using Alpata.Case.Service.Contracts.Dtos.Meeting;
using Alpata.Case.Services.Base;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alpata.Case.Services
{
    public class MeetingService : BaseApplicationService<Meeting, MeetingDto, MeetingCreateDto, MeetingUpdateDto>, IMeetingService
    {
        public MeetingService(IRepository<Meeting> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
