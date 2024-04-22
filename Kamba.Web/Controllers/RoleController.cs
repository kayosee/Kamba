using Kamba.Model;
using Kamba.Web.Auxiliary;
using Microsoft.AspNetCore.Mvc;

namespace Kamba.Web.Controllers
{
    /// <summary>
    /// 角色管理
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class RoleController : Controller
    {
        private KambaContext _context;
        public RoleController(KambaContext context)
        {
            _context = context;
        }
        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="model">角色</param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Add(Role model)
        {
            _context.Add(model);
            _context.SaveChanges();
            return Ok(model);
        }
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="id">角色ID</param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var model = _context.Roles.Find(id);
            if (model == null)
            {
                return BadRequest();
            }
            _context.Roles.Remove(model);
            _context.SaveChanges();
            return Ok();
        }
        /// <summary>
        /// 角色列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult List()
        {
            var query = (_context.Roles.ToList());
            return Ok(query);
        }
        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="model">角色信息</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Update(Role model)
        {
            _context.Roles.Update(model);
            _context.SaveChanges();
            return Ok();
        }
        /// <summary>
        /// 授予权限
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <param name="privileges">权限列表</param>
        /// <returns></returns>
        [HttpPost]
        [Route("grantPrivileges1")]
        public IActionResult GrantPrivileges(int roleId, int[] privileges)
        {
            foreach (var privilege in privileges)
            {
                _context.RolePrivileges.Add(new RolePrivilege
                {
                    RoleId = roleId,
                    PrivilegeId = privilege
                });
                _context.SaveChanges();
            }
            return Ok();
        }
        /// <summary>
        /// 撤消权限
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <param name="privileges">权限列表</param>
        /// <returns></returns>
        [HttpPost]
        [Route("revokePrivileges")]
        public IActionResult RevokePrivileges(int roleId, int[] privileges)
        {
            foreach (var privilege in privileges)
            {
                _context.RolePrivileges.Remove(new RolePrivilege
                {
                    RoleId = roleId,
                    PrivilegeId = privilege
                });
                _context.SaveChanges();
            }
            return Ok();
        }

    }
}
