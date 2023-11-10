using Alpata.Case.Service.Contracts.Dtos.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alpata.Case.Service.Contracts.Dtos.Meeting
{
    public class MeetingCreateDto:EntityDto
    {
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Explanation { get; set; }
    }
}
