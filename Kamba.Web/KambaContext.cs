using Kamba.Model;
using Microsoft.EntityFrameworkCore;
namespace Kamba.Web
{
    /// <summary>
    /// 
    /// </summary>
    public class KambaContext : DbContext
    {
        /// <summary>
        /// 
        /// </summary>
        public KambaContext() : base() 
        {
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=.\\kamba.db");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        /// <summary>
        /// 用户
        /// </summary>
        public virtual DbSet<User> Users { get; set; }
        /// <summary>
        /// 角色
        /// </summary>
        public virtual DbSet<Role> Roles { get; set; }
        /// <summary>
        /// 用户角色
        /// </summary>
        public virtual DbSet<UserRole> UserRoles { get; set; }
        /// <summary>
        /// 部门
        /// </summary>
        public virtual DbSet<Department> Departments { get; set; }
        /// <summary>
        /// 部门权限
        /// </summary>
        public virtual DbSet<DepartmentPrivilege> DepartmentPrivileges { get; set; }
        /// <summary>
        /// 角色权限
        /// </summary>
        public virtual DbSet<RolePrivilege> RolePrivileges { get; set; }

    }
}
