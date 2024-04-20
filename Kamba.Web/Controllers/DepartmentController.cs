using Kamba.Model;
using Microsoft.AspNetCore.Mvc;

namespace Kamba.Web.Controllers
{
    /// <summary>
    /// 部门
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class DepartmentController : Controller
    {
        /// <summary>
        /// 读取部门列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult List()
        {
            using (var context = new KambaContext())
            {
                var query = context.Departments.ToList();
                return Ok(query);
            }
        }
        /// <summary>
        /// 添加部门
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Add(Department model)
        {
            using(var context = new KambaContext())
            {
                context.Departments.Add(model);
                context.SaveChanges();
                return Ok(model);
            }
        }
        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            using (var context = new KambaContext())
            {
                var model = context.Departments.Find(id);
                if (model == null)
                    return BadRequest(model);
                context.Departments.Remove(model);
                context.SaveChanges();
                return Ok(model);
            }
        }
    }
}
