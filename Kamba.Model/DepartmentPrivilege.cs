using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace Kamba.Model
{
    [Table(nameof(DepartmentPrivilege))]
    [PrimaryKey(nameof(DepartmentId), nameof(PrivilegeId), nameof(Path))]
    public class DepartmentPrivilege
    {
        [ForeignKey(nameof(DepartmentId))]
        public int DepartmentId { get;set; }
        [Key]
        public int PrivilegeId { get;set; }
        [Key]
        [MaxLength(4000)]
        public string Path { get; set; }
    }
}
