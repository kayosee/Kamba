using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kamba.Model
{
    [Table(nameof(DepartmentPrivilege))]
    public class DepartmentPrivilege
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [ForeignKey(nameof(DepartmentId))]
        public int DepartmentId { get;set; }
        [Required]
        public int Type { get;set; }
        [Required]
        [MaxLength(4000)]
        public string Path { get; set; }
    }
}
