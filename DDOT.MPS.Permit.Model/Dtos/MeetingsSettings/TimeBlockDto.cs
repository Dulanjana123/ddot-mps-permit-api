using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDOT.MPS.Permit.Model.Dtos
{
    public class TimeBlockDto
    {
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
    }

}