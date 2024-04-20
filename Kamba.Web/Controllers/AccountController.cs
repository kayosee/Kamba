using Microsoft.AspNetCore.Mvc;

namespace Kamba.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class AccountController : Controller
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Login()
        {
            return null;
        }
    }
}
