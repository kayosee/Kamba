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

namespace Kamba.Client
{
    internal class FileProxy : IDokanOperations
    {
        private Client _client;
        public FileProxy(string ip,int port)
        {
            var tcpClient = new TcpClient();
            tcpClient.Connect(IPAddress.Parse(ip), port);
            _client = new Client(tcpClient.Client);
        }
        public void Cleanup(string fileName, IDokanFileInfo info)
        {
           var response= _client.MakeRequest(new CleanupRequest(fileName, info));
        }

        public void CloseFile(string fileName, IDokanFileInfo info)
        {
            var response = _client.MakeRequest(new CloseFileRequest(fileName, info));
        }

        public NtStatus CreateFile(string fileName, DokanNet.FileAccess access, FileShare share, FileMode mode, FileOptions options, FileAttributes attributes, IDokanFileInfo info)
        {
            var response = _client.MakeRequest(new CreateFileRequest(fileName, access,share,mode,options,attributes,info));
            return NtStatus.Success;
        }

        public NtStatus DeleteDirectory(string fileName, IDokanFileInfo info)
        {
            var response = _client.MakeRequest(new DeleteDirectoryRequest(fileName, info));
            return NtStatus.Success;
        }

        public NtStatus DeleteFile(string fileName, IDokanFileInfo info)
        {
            var response = _client.MakeRequest(new DeleteFileRequest(fileName, info));
            return NtStatus.Success;
        }

        public NtStatus FindFiles(string fileName, out IList<FileInformation> files, IDokanFileInfo info)
        {
            var response = _client.MakeRequest(new FindFilesRequest(fileName, info));
            files = response.Files;
            return NtStatus.Success;
        }

        public NtStatus FindFilesWithPattern(string fileName, string searchPattern, out IList<FileInformation> files, IDokanFileInfo info)
        {
            var response = _client.MakeRequest(new FindFilesWithPatternRequest(fileName, searchPattern, info));
            files = response.Files; 
            return NtStatus.Success;
        }

        public NtStatus FindStreams(string fileName, out IList<FileInformation> streams, IDokanFileInfo info)
        {
            var response = _client.MakeRequest(new FindStreamsRequest(fileName, info));
            streams = response.Files; 
            return NtStatus.Success;
        }

        public NtStatus FlushFileBuffers(string fileName, IDokanFileInfo info)
        {
            var response = _client.MakeRequest(new FlushFileBuffersRequest(fileName, info));
            return NtStatus.Success;
        }

        public NtStatus GetDiskFreeSpace(out long freeBytesAvailable, out long totalNumberOfBytes, out long totalNumberOfFreeBytes, IDokanFileInfo info)
        {
            var response = _client.MakeRequest(new GetDiskFreeSpaceRequest(info));
            return NtStatus.Success;
        }

        public NtStatus GetFileInformation(string fileName, out FileInformation fileInfo, IDokanFileInfo info)
        {
            var response = _client.MakeRequest(new GetFileInformationRequest(fileName, info));
            fileInfo = response.FileInfo;
            return NtStatus.Success;
        }

        public NtStatus GetFileSecurity(string fileName, out FileSystemSecurity security, AccessControlSections sections, IDokanFileInfo info)
        {
            var response = _client.MakeRequest(new GetFileSecurityRequest(fileName, sections, info));
            return NtStatus.Success;
        }

        public NtStatus GetVolumeInformation(out string volumeLabel, out FileSystemFeatures features, out string fileSystemName, out uint maximumComponentLength, IDokanFileInfo info)
        {
            var response = _client.MakeRequest(new GetVolumeInformationRequest(info));
        }

        public NtStatus LockFile(string fileName, long offset, long length, IDokanFileInfo info)
        {
            var response = _client.MakeRequest(new LockFileRequest(fileName, offset, length, info));
        }

        public NtStatus Mounted(string mountPoint, IDokanFileInfo info)
        {
            var response = _client.MakeRequest(new MountedRequest(mountPoint,info));
        }

        public NtStatus MoveFile(string oldName, string newName, bool replace, IDokanFileInfo info)
        {
            var response = _client.MakeRequest(new MoveFileRequest(oldName, newName, replace, info));
        }

        public NtStatus ReadFile(string fileName, byte[] buffer, out int bytesRead, long offset, IDokanFileInfo info)
        {
            var response = _client.MakeRequest(new ReadFileRequest(fileName, buffer,offset, info));
        }

        public NtStatus SetAllocationSize(string fileName, long length, IDokanFileInfo info)
        {
            var response = _client.MakeRequest(new SetAllocationSizeRequest(fileName, length, info));
        }

        public NtStatus SetEndOfFile(string fileName, long length, IDokanFileInfo info)
        {
            var response = _client.MakeRequest(new SetEndOfFileRequest(fileName, length, info));
        }

        public NtStatus SetFileAttributes(string fileName, FileAttributes attributes, IDokanFileInfo info)
        {
            var response = _client.MakeRequest(new SetFileAttributesRequest(fileName, attributes, info));
        }

        public NtStatus SetFileSecurity(string fileName, FileSystemSecurity security, AccessControlSections sections, IDokanFileInfo info)
        {
            var response = _client.MakeRequest(new SetFileSecurityRequest(fileName, security,sections, info));
        }

        public NtStatus SetFileTime(string fileName, DateTime? creationTime, DateTime? lastAccessTime, DateTime? lastWriteTime, IDokanFileInfo info)
        {
            var response = _client.MakeRequest(new SetFileTimeRequest(fileName, creationTime,lastAccessTime,lastWriteTime, info));
        }

        public NtStatus UnlockFile(string fileName, long offset, long length, IDokanFileInfo info)
        {
            var response = _client.MakeRequest(new UnlockFileRequest(fileName, offset,length, info));
        }

        public NtStatus Unmounted(IDokanFileInfo info)
        {
            var response = _client.MakeRequest(new UnmountedRequest(info));
        }

        public NtStatus WriteFile(string fileName, byte[] buffer, out int bytesWritten, long offset, IDokanFileInfo info)
        {
            var response = _client.MakeRequest(new WriteFileRequest(fileName, buffer,offset, info));
        }
    }
}
