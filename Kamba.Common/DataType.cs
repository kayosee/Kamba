using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kamba.Common
{
    public enum DataType
    {
        FileReadRequest,
        FileReadResponse,
        FileWriteRequest,
        FileDeleteRequest,
        AuthenticateRequest,
        AuthenticateResponse,
        CleanupRequest,
        CloseFileRequest,
        WriteFileRequest,
        CreateFileRequest,
        DeleteDirectoryRequest,
        DeleteFileRequest,
        FindFilesRequest,
        FindFilesWithPatternRequest,
        FindStreamsRequest,
        GetDiskFreeSpaceRequest,
        GetFileInformationRequest,
        GetFileSecurityRequest,
        GetVolumeInformationRequest,
        LockFileRequest,
        MountedRequest,
        MoveFileRequest,
        ReadFileRequest,
        SetAllocationSizeRequest,
        SetEndOfFileRequest,
        SetFileAttributesRequest,
        SetFileSecurityRequest,
        UnlockFileRequest,
        UnmountedRequest,
    }
}
