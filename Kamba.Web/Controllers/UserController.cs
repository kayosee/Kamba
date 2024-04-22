using Kamba.Model;
using Kamba.Web.Auxiliary;
using Microsoft.AspNetCore.Mvc;

namespace Kamba.Web.Controllers
{
    /// <summary>
    /// 用户管理
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private KambaContext _context;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public UserController(KambaContext context)
        {
            _context = context;
        }
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="model">用户</param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Add(User model)
        {
            using (var context = new KambaContext())
            {
                context.Add(model);
                context.SaveChanges();
                return Ok(model);
            }
        }
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            using (var context = new KambaContext())
            {
                var model = context.Users.Find(id);
                if (model == null)
                {
                    return BadRequest();
                }
                context.Users.Remove(model);
                context.SaveChanges();
                return Ok();
            }
        }
        /// <summary>
        /// 用户列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult List()
        {
            var query = (_context.Users.ToList());
            return Ok(query);
        }
        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Update(User model)
        {
            _context.Users.Update(model);
            _context.SaveChanges();
            return Ok();
        }
        /// <summary>
        /// 授予用户角色
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/grantRoles")]
        public IActionResult GrantRoles(int userId, int[] roleIds)
        {
            foreach (var roleId in roleIds)
            {
                _context.UserRoles.Add(new UserRole() { UserId = userId, RoleId = roleId });
            }
            _context.SaveChanges();
            return Ok();
        }
        /// <summary>
        /// 撤销角色
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/revokeRoles")]
        public IActionResult RevokeRoles(int userId, int[] roleIds)
        {
            foreach (var roleId in roleIds)
            {
                var model = _context.UserRoles.First(f => f.UserId == userId && f.RoleId == roleId);
                if (model != null)
                    _context.UserRoles.Remove(model);
            }
            _context.SaveChanges();
            return Ok();
        }
    }
}
