using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClassLibrary
{
    public class Project
    {
        public Project()
        {
            Employees = new HashSet<Employee>();
        }

        [Key]
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string Discription { get; set; }
        public virtual ICollection<Employee> Employees  { get; set; }


    }
}
