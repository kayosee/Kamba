using DokanNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using Kamba.Common;
using Kamba.Common.Request;
using FlakeId;
using Kamba.Common.Response;
namespace Kamba.Client
{
    internal class FileProxy : IDokanOperations
    {
        private Client _client;
        public FileProxy(string ip,int port,string username,string password)
        {
            var tcpClient = new TcpClient();
            tcpClient.Connect(IPAddress.Parse(ip), port);
            _client = new Client(tcpClient.Client, username, password);
        }
        public void Cleanup(string fileName, IDokanFileInfo info)
        {
            var response = _client.MakeRequest<CleanupResponse>(new CleanupRequest(_client.Id, Id.Create(), fileName, info));
        }

        public void CloseFile(string fileName, IDokanFileInfo info)
        {
            var response = _client.MakeRequest<CloseFileResponse>(new CloseFileRequest(_client.Id, Id.Create(), fileName, info));
        }

        public NtStatus CreateFile(string fileName, DokanNet.FileAccess access, FileShare share, FileMode mode, FileOptions options, FileAttributes attributes, IDokanFileInfo info)
        {
            var response = _client.MakeRequest<CreateFileResponse>(new CreateFileRequest(_client.Id, Id.Create(), fileName, access, share, mode, options, attributes, info));
            if (response != null)
                return response.ResponseCode;
            return NtStatus.Unsuccessful;
        }

        public NtStatus DeleteDirectory(string fileName, IDokanFileInfo info)
        {
            var response = _client.MakeRequest<DeleteDirectoryResponse>(new DeleteDirectoryRequest(_client.Id, Id.Create(), fileName, info));
            if(response != null) 
                return response.ResponseCode;
            return NtStatus.Unsuccessful;
        }

        public NtStatus DeleteFile(string fileName, IDokanFileInfo info)
        {
            var response = _client.MakeRequest<DeleteFileResponse>(new DeleteFileRequest(_client.Id, Id.Create(), fileName, info));
            if (response != null)
                return response.ResponseCode;
            return NtStatus.Unsuccessful;
        }

        public NtStatus FindFiles(string fileName, out IList<FileInformation> files, IDokanFileInfo info)
        {
            files = new List<FileInformation>();
            var response = _client.MakeRequest<FindFilesResponse>(new FindFilesRequest(_client.Id, Id.Create(), fileName, info));
            if (response != null)
            {
                files = response.Files;
                return response.ResponseCode;
            }
            return NtStatus.Unsuccessful;
        }

        public NtStatus FindFilesWithPattern(string fileName, string searchPattern, out IList<FileInformation> files, IDokanFileInfo info)
        {
            files = new List<FileInformation>();
            var response = _client.MakeRequest<FindFilesWithPatternResponse>(new FindFilesWithPatternRequest(_client.Id, Id.Create(), fileName, searchPattern, info));
            if (response != null)
            {
                files = response.Files;
                return response.ResponseCode;
            }
            return NtStatus.Unsuccessful;
        }

        public NtStatus FindStreams(string fileName, out IList<FileInformation> streams, IDokanFileInfo info)
        {
            streams = new List<FileInformation>();
            var response = _client.MakeRequest<FindStreamsResponse>(new FindStreamsRequest(_client.Id, Id.Create(), fileName, info));
            if (response != null)
            {
                streams = response.Files;
                return response.ResponseCode;
            }
            return NtStatus.Unsuccessful;
        }

        public NtStatus FlushFileBuffers(string fileName, IDokanFileInfo info)
        {
            var response = _client.MakeRequest<FlushFileBuffersResponse>(new FlushFileBuffersRequest(_client.Id, Id.Create(), fileName, info));
            if (response != null)
                return response.ResponseCode;
            return NtStatus.Unsuccessful;
        }

        public NtStatus GetDiskFreeSpace(out long freeBytesAvailable, out long totalNumberOfBytes, out long totalNumberOfFreeBytes, IDokanFileInfo info)
        {
            freeBytesAvailable = 0;
            totalNumberOfBytes = 0;
            totalNumberOfFreeBytes = 0;
            var response = _client.MakeRequest<GetDiskFreeSpaceResponse>(new GetDiskFreeSpaceRequest(_client.Id, Id.Create(), info));
            if (response != null)
            {
                freeBytesAvailable = response.FreeBytesAvailable;
                totalNumberOfBytes = response.TotalNumberOfBytes;
                totalNumberOfFreeBytes = response.TotalNumberOfFreeBytes;
                return response.ResponseCode;
            }
            return NtStatus.Unsuccessful;
        }

        public NtStatus GetFileInformation(string fileName, out FileInformation fileInfo, IDokanFileInfo info)
        {
            fileInfo = new FileInformation();
            var response = _client.MakeRequest<GetFileInformationResponse>(new GetFileInformationRequest(_client.Id, Id.Create(), fileName, info));
            if (response != null)
            {
                fileInfo = response.FileInfo;
                return response.ResponseCode;
            }
            return NtStatus.Unsuccessful;
        }
        public NtStatus GetFileSecurity(string fileName, out FileSystemSecurity security, AccessControlSections sections, IDokanFileInfo info)
        {
            throw new NotImplementedException();
            /*
            security = new FileSystemSecurity();
            var response = _client.MakeRequest<GetFileSecurityResponse>(new GetFileSecurityRequest(_client.Id, Id.Create(), fileName, sections, info));
            return NtStatus.Success;
            */
        }

        public NtStatus GetVolumeInformation(out string volumeLabel, out FileSystemFeatures features, out string fileSystemName, out uint maximumComponentLength, IDokanFileInfo info)
        {
            volumeLabel = "";
            features = FileSystemFeatures.None;
            fileSystemName = "";
            maximumComponentLength = 0;
            var response = _client.MakeRequest<GetVolumeInformationResponse>(new GetVolumeInformationRequest(_client.Id, Id.Create(), info));
            if (response != null)
            {
                volumeLabel = response.VolumeLabel;
                features = response.Features;
                fileSystemName = response.FileSystemName;
                maximumComponentLength = response.MaximumComponentLength;
                return response.ResponseCode;
            }
            return NtStatus.Unsuccessful;
        }

        public NtStatus LockFile(string fileName, long offset, long length, IDokanFileInfo info)
        {
            var response = _client.MakeRequest<LockFileResponse>(new LockFileRequest(_client.Id, Id.Create(), fileName, offset, length, info));
            if (response != null)
            {
                return response.ResponseCode;
            }
            return NtStatus.Unsuccessful;
        }

        public NtStatus Mounted(string mountPoint, IDokanFileInfo info)
        {
            var response = _client.MakeRequest<MountedResponse>(new MountedRequest(_client.Id, Id.Create(), mountPoint,info));
            if (response != null)
            {
                return response.ResponseCode;
            }
            return NtStatus.Unsuccessful;
        }

        public NtStatus MoveFile(string oldName, string newName, bool replace, IDokanFileInfo info)
        {
            var response = _client.MakeRequest<MoveFileResponse>(new MoveFileRequest(_client.Id, Id.Create(), oldName, newName, replace, info));
            if (response != null)
            {
                return response.ResponseCode;
            }
            return NtStatus.Unsuccessful;
        }

        public NtStatus ReadFile(string fileName, byte[] buffer, out int bytesRead, long offset, IDokanFileInfo info)
        {
            bytesRead = 0;
            var response = _client.MakeRequest<ReadFileResponse>(new ReadFileRequest(_client.Id, Id.Create(), fileName, offset, info));
            if (response != null)
            {
                response.Buffer.CopyTo(buffer, bytesRead);
                return response.ResponseCode;
            }
            return NtStatus.Unsuccessful;
        }

        public NtStatus SetAllocationSize(string fileName, long length, IDokanFileInfo info)
        {
            var response = _client.MakeRequest<SetAllocationSizeResponse>(new SetAllocationSizeRequest(_client.Id, Id.Create(), fileName, length, info));
            if (response != null)
            {
                return response.ResponseCode;
            }
            return NtStatus.Unsuccessful;
        }

        public NtStatus SetEndOfFile(string fileName, long length, IDokanFileInfo info)
        {
            var response = _client.MakeRequest<SetEndOfFileResponse>(new SetEndOfFileRequest(_client.Id, Id.Create(), fileName, length, info));
            if (response != null)
            {
                return response.ResponseCode;
            }
            return NtStatus.Unsuccessful;
        }

        public NtStatus SetFileAttributes(string fileName, FileAttributes attributes, IDokanFileInfo info)
        {
            var response = _client.MakeRequest<SetFileAttributesResponse>(new SetFileAttributesRequest(_client.Id, Id.Create(), fileName, attributes, info));
            if (response != null)
            {
                return response.ResponseCode;
            }
            return NtStatus.Unsuccessful;
        }

        public NtStatus SetFileSecurity(string fileName, FileSystemSecurity security, AccessControlSections sections, IDokanFileInfo info)
        {
            throw new NotImplementedException();
        }

        public NtStatus SetFileTime(string fileName, DateTime? creationTime, DateTime? lastAccessTime, DateTime? lastWriteTime, IDokanFileInfo info)
        {
            var response = _client.MakeRequest<SetFileTimeResponse>(new SetFileTimeRequest(_client.Id, Id.Create(), fileName, creationTime,lastAccessTime,lastWriteTime, info));
            if (response != null)
            {
                return response.ResponseCode;
            }
            return NtStatus.Unsuccessful;
        }

        public NtStatus UnlockFile(string fileName, long offset, long length, IDokanFileInfo info)
        {
            var response = _client.MakeRequest<UnlockFileResponse>(new UnlockFileRequest(_client.Id, Id.Create(), fileName, offset,length, info));
            if (response != null)
            {
                return response.ResponseCode;
            }
            return NtStatus.Unsuccessful;
        }

        public NtStatus Unmounted(IDokanFileInfo info)
        {
            var response = _client.MakeRequest<UnmountedResponse>(new UnmountedRequest(_client.Id, Id.Create(), info));
            if (response != null)
            {
                return response.ResponseCode;
            }
            return NtStatus.Unsuccessful;
        }

        public NtStatus WriteFile(string fileName, byte[] buffer, out int bytesWritten, long offset, IDokanFileInfo info)
        {
            bytesWritten = 0;
            var response = _client.MakeRequest<WriteFileResponse>(new WriteFileRequest(_client.Id, Id.Create(), fileName, buffer,offset, info));
            if (response != null)
            {
                bytesWritten = response.BytesWritten;
                return response.ResponseCode;
            }
            return NtStatus.Unsuccessful;
        }
    }
}
