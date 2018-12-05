using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManager.DataLayer
{
    [Table("Projects")]
    public class Projects
    {

        public int ProjectID { get; set; }

        [Column("Project")]
        [StringLength(150)]
        public string ProjectName { get; set; }

        [Column(TypeName = "Date")]
        public DateTime? StartDate { get; set; }

        [Column(TypeName = "Date")]
        public DateTime? EndDate { get; set; }
        [Column("Priority")]
        public int Priority { get; set; }

        [Column("ManagerID")]
        public int ManagerID { get; set; }

        [Column("Suspended")]
        public bool Suspended { get; set; }

    }
}
