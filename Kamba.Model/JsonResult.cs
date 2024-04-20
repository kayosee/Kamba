using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kamba.Model
{
    public class JsonResult
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public object? Data { get; set; }
        public JsonResult(bool success)
        {
            Success = success;
            Message = string.Empty;
            Data = null;
        }
        public JsonResult(bool success, object data)
        {
            Success = success;
            Message = string.Empty;
            Data = data;
        }
    }
}
