using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kamba.Common
{
    public enum DataType
    {
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
        CleanupResponse,
        CloseFileResponse,
        CreateFileResponse,
        DeleteDirectoryResponse,
        DeleteFileResponse,
        FindFilesResponse,
        FindFilesWithPatternResponse,
        FindStreamsResponse,
        FlushFileBuffersResponse,
        GetDiskFreeSpaceResponse,
        GetVolumeInformationResponse,
        LockFileResponse,
        MountedResponse,
        MoveFileResponse,
        ReadFileResponse,
        SetAllocationSizeResponse,
        SetEndOfFileResponse,
        SetFileAttributesResponse,
        SetFileSecurityResponse,
        SetFileTimeResponse,
        UnlockFileResponse,
        UnmountedResponse,
        WriteFileResponse,
        GetFileSecurityResponse,
        GetFileInformationResponse,
    }
}
