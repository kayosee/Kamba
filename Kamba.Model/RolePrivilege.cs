using Microsoft.EntityFrameworkCore;
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
    [PrimaryKey(nameof(RoleId),nameof(PrivilegeId),nameof(Path))]
    public class RolePrivilege
    {
        [Key]
        [ForeignKey(nameof(RoleId))]
        public int RoleId {  get; set; }
        /// <summary>
        /// 0:读,写,删除
        /// </summary>
        [Key]
        public int PrivilegeId { get; set; }
        /// <summary>
        /// 路径
        /// </summary>
        [Key]
        [StringLength(4000)]
        public string Path { get; set; }
    }
}
