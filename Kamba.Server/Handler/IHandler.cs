using Kamba.Common.Request;
using Kamba.Common.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kamba.Server.Handler
{
    internal interface IHandler<T,R> where T : FileRequest where R : FileResponse
    {
        R Process(T request);
    }
}
