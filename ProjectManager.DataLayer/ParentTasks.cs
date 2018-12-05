using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManager.DataLayer
{
    [Table("ParentTask")]
    public class ParentTasks
    {

        public int ParentTaskId { get; set; }

        [Column("Parent_Task")]
        [StringLength(150)]
        public string ParentTaskName { get; set; }

        public int ProjectID { get; set; }
      

    }
}
