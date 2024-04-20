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
            using (var context = new KambaContext())
            {
                var query = (context.Users.ToList());
                return Ok(query);
            }
        }
        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Update(User model)
        {
            using(var  context = new KambaContext())
            {
                var old = context.Users.Find(model.Id);
                if(old == null)
                    return BadRequest("指定的用户编号不存在。");
                context.Users.Remove(model);
                context.SaveChanges();
                return Ok();
            }
        }
        /// <summary>
        /// 授予用户角色
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult GrantRoles(int userId, int[] roleIds)
        {
            using(var context = new KambaContext())
            {
                foreach(var roleId in roleIds)
                {
                    context.UserRoles.Add(new UserRole() { UserId = userId, RoleId = roleId });                    
                }
                context.SaveChanges();
                return Ok();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult RevokeRoles(int userId, int[] roleIds)
        {
            using (var context = new KambaContext())
            {
                foreach (var roleId in roleIds)
                {
                    var model = context.UserRoles.First(f => f.UserId == userId && f.RoleId == roleId);
                    if (model != null)
                        context.UserRoles.Remove(model);
                }
                context.SaveChanges();
                return Ok();
            }
        }
    }
}
