using Kamba.Common.Request;
using Kamba.Common.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Win32;

namespace Kamba.Server.Handler
{
    internal class CleanupHandler : IHandler<CleanupRequest, CleanupResponse>
    {
        public CleanupResponse Process(CleanupRequest request)
        {
            if(request.Info.Context != null)
            {
                PInvoke.CloseHandle((Windows.Win32.Foundation.HANDLE)request.Info.Context);
                request.Info.Context = null;
            }

            return new CleanupResponse(request);
        }
    }
}
