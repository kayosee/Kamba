using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kamba.Model
{
    [Table(nameof(RolePrivilege))]
    public class RolePrivilege
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [ForeignKey(nameof(RoleId))]
        public int RoleId {  get; set; }
        /// <summary>
        /// 0:读,写,删除
        /// </summary>
        [Required]
        public int Type { get; set; }
        /// <summary>
        /// 路径
        /// </summary>
        [Required]
        [StringLength(4000)]
        public string Path { get; set; }
    }
}
