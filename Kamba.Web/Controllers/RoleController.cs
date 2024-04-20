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
        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="model">角色</param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Add(Role model)
        {
            using (var context = new KambaContext())
            {
                context.Add(model);
                context.SaveChanges();
                return Ok(model);
            }
        }
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            using (var context = new KambaContext())
            {
                var model = context.Roles.Find(id);
                if (model == null) 
                {
                    return BadRequest();
                }
                context.Roles.Remove(model);
                context.SaveChanges();
                return Ok();
            }
        }
        /// <summary>
        /// 角色列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult List()
        {
            using (var context = new KambaContext())
            {
                var query = (context.Roles.ToList());
                return Ok(query);
            }
        }
        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Update(Role model)
        {
            using(var  context = new KambaContext())
            {
                var old = context.Roles.Find(model.Id);
                if(old == null)
                    return BadRequest("指定的角色编号不存在。");
                context.Roles.Remove(model);
                context.SaveChanges();
                return Ok();
            }
        }
    }
}
