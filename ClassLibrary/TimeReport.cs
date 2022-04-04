using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClassLibrary
{
    public class TimeReport
    {
        [Key]
        public int TimeReportId { get; set; }
        public int EmployeeId { get; set; }
        
        public DateTime FillingDate { get; set; }
        public int WorkedHours { get; set; }
        public int Week { get; set; }
        public virtual Employee Employee { get; set; }
}
}
