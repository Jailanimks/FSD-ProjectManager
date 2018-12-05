using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManager.DataLayer
{
    [Table("Task")]
    public class TaskData
    {
        
        public int TaskId { get; set; }

        [Column("Task")]
        [StringLength(150)]
        public string TaskName { get; set; }

        [Column("ParentId")]
        public int? ParentTaskId { get; set; }

        public int ProjectID { get; set; }

        [Column(TypeName = "Date")]
        public DateTime StartDate { get; set; }

        [Column(TypeName = "Date")]
        public DateTime EndDate { get; set; }
        [Column("Priority")]
        public int Priority { get; set; }

        [Column("Status")]
        public bool Status { get; set; }

        public int UserID { get; set; }

    }
}
