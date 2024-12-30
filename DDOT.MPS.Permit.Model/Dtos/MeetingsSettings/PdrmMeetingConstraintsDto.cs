using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDOT.MPS.Permit.Model.Dtos
{
    public class PdrmMeetingConstraintsDto
    {
        public CalendarDatesSelectionDto CalendarDatesSelectionDto { get; set; }
        public TimeBlocksSelectionDto TimeBlocksSelectionDto { get; set; }
    }

}
