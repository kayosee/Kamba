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
    [Table(nameof(UserRole))]
    [PrimaryKey(nameof(UserId),nameof(RoleId))]
    public class UserRole
    {
        [Required]
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        [Required]
        [ForeignKey(nameof(Role))]
        public int RoleId { get; set; }
    }
}
