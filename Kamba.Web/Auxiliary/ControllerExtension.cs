using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace Kamba.Web.Auxiliary
{
    /// <summary>
    /// 
    /// </summary>
    public static class ControllerExtension
    {
        /// <summary>
        /// 返回结果
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="success"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static IActionResult JsonResult(this Controller controller, bool success, object data = null)
        {
            return controller.Json(new JsonResult(success, data));
        }
    }
}
