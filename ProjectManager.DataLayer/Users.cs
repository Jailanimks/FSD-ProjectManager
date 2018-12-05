using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ProjectManager.DataLayer
{
    [Table("Users")]
    public class Users
    {
        public int UserID { get; set; }

        [Column("FirstName")]
        [StringLength(150)]
        public string FirstName { get; set; }
        

        [Column("LastName")]
        [StringLength(150)]
        public string LastName { get; set; }


        [Column("Employee_ID")]
        [StringLength(50)]
        public string EmployeeId { get; set; }

    }
}
