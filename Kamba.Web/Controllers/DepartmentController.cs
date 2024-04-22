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
        private KambaContext _context;
        public DepartmentController(KambaContext context)
        {
            _context = context;
        }
        /// <summary>
        /// 读取部门列表
        /// </summary>
        /// <returns>部门列表</returns>
        [HttpGet]
        public IActionResult List()
        {
            var query = _context.Departments.ToList();
            return Ok(query);
        }
        /// <summary>
        /// 添加部门
        /// </summary>
        /// <param name="model">部门信息</param>
        /// <returns>部门信息</returns>
        [HttpPut]
        public IActionResult Add(Department model)
        {
            _context.Departments.Add(model);
            _context.SaveChanges();
            return Ok(model);
        }
        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="id">部门Id</param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var model = _context.Departments.Find(id);
            if (model == null)
                return BadRequest(model);
            _context.Departments.Remove(model);
            _context.SaveChanges();
            return Ok(model);
        }
        /// <summary>
        /// 授予权限
        /// </summary>
        /// <param name="departmentId">部门ID</param>
        /// <param name="privileges">权限列表</param>
        /// <returns></returns>
        [HttpPost]
        [Route("grantPrivileges")]
        public IActionResult GrantPrivileges(int departmentId, int[] privileges)
        {
            foreach (var privilege in privileges)
            {
                _context.DepartmentPrivileges.Add(new DepartmentPrivilege
                {
                    DepartmentId = departmentId,
                    PrivilegeId = privilege
                });
                _context.SaveChanges();
            }
            return Ok();
        }
        /// <summary>
        /// 修改部门
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Update(Department model)
        {
            _context.Departments.Update(model);
            _context.SaveChanges();
            return Ok();
        }
        /// <summary>
        /// 撤消权限
        /// </summary>
        /// <param name="departmentId">部门ID</param>
        /// <param name="privileges">权限列表</param>
        /// <returns></returns>
        [HttpPost]
        [Route("revokePrivileges")]
        public IActionResult RevokePrivileges(int departmentId, int[] privileges)
        {
            foreach (var privilege in privileges)
            {
                _context.DepartmentPrivileges.Remove(new DepartmentPrivilege
                {
                    DepartmentId = departmentId,
                    PrivilegeId = privilege
                });
                _context.SaveChanges();
            }
            return Ok();
        }

    }
}
