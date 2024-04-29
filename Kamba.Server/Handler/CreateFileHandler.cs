using DokanNet;
using Kamba.Common.Request;
using Kamba.Common.Response;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileAccess = DokanNet.FileAccess;

namespace Kamba.Server.Handler
{
    internal class CreateFileHandler : IHandler<CreateFileRequest, CreateFileResponse>
    {
        private const FileAccess DataAccess = FileAccess.ReadData | FileAccess.WriteData | FileAccess.AppendData |
                                              FileAccess.Execute |
                                              FileAccess.GenericExecute | FileAccess.GenericWrite |
                                              FileAccess.GenericRead;

        private const FileAccess DataWriteAccess = FileAccess.WriteData | FileAccess.AppendData |
                                                   FileAccess.Delete |
                                                   FileAccess.GenericWrite;
        public CreateFileResponse Process(CreateFileRequest request)
        {
            var folder = ConfigurationManager.AppSettings["folder"];
            var response = new CreateFileResponse(request);
            var result = DokanResult.Success;
            var filePath = Path.Combine(folder, request.FileName);

            if (request.IsDirectory)
            {
                try
                {
                    switch (request.Mode)
                    {
                        case FileMode.Open:
                            if (!Directory.Exists(filePath))
                            {
                                try
                                {
                                    if (!File.GetAttributes(filePath).HasFlag(FileAttributes.Directory))
                                    {
                                        response.ResponseCode = DokanNet.NtStatus.NotADirectory;
                                        return response;
                                    }
                                }
                                catch (Exception)
                                {
                                    response.ResponseCode = DokanNet.NtStatus.ObjectNameNotFound;
                                    return response;
                                }
                                response.ResponseCode = DokanNet.NtStatus.ObjectPathNotFound;
                                return response;
                            }

                            new DirectoryInfo(filePath).EnumerateFileSystemInfos().Any();
                            // you can't list the directory
                            break;

                        case FileMode.CreateNew:
                            if (Directory.Exists(filePath))
                            {
                                response.ResponseCode = NtStatus.ObjectNameCollision;
                                return response;
                            }
                            try
                            {
                                File.GetAttributes(filePath).HasFlag(FileAttributes.Directory);
                                response.ResponseCode = NtStatus.ObjectNameCollision;
                                return response;
                            }
                            catch (IOException)
                            {
                            }

                            Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                            break;
                    }
                }
                catch (UnauthorizedAccessException)
                {
                    response.ResponseCode = NtStatus.AccessDenied;
                    return response;
                }
            }
            else
            {
                var pathExists = true;
                var pathIsDirectory = false;

                var readWriteAttributes = (request.Access & DataAccess) == 0;
                var readAccess = (request.Access & DataWriteAccess) == 0;

                try
                {
                    pathExists = (Directory.Exists(filePath) || File.Exists(filePath));
                    pathIsDirectory = pathExists ? File.GetAttributes(filePath).HasFlag(FileAttributes.Directory) : false;
                }
                catch (IOException)
                {
                }

                switch (request.Mode)
                {
                    case FileMode.Open:

                        if (pathExists)
                        {
                            // check if driver only wants to read attributes, security info, or open directory
                            if (readWriteAttributes || pathIsDirectory)
                            {
                                if (pathIsDirectory && (request.Access & FileAccess.Delete) == FileAccess.Delete
                                    && (request.Access & FileAccess.Synchronize) != FileAccess.Synchronize)
                                //It is a DeleteFile request on a directory
                                {
                                    response.ResponseCode = NtStatus.AccessDenied;
                                    return response;
                                }
                                response.ResponseCode = NtStatus.Success;
                                response.Info.IsDirectory = pathIsDirectory;
                                response.Info.Context = new object();
                                // must set it to something if you return DokanError.Success

                            }
                        }
                        else
                        {
                            response.ResponseCode = NtStatus.ObjectNameNotFound;
                            return response;
                        }
                        break;
                    case FileMode.CreateNew:
                        if (pathExists)
                        {
                            response.ResponseCode = NtStatus.ObjectNameCollision;
                            return response;
                        }
                        break;

                    case FileMode.Truncate:
                        if (!pathExists)
                        {
                            response.ResponseCode = NtStatus.ObjectNameNotFound; 
                            return response;
                        }
                        break;
                }

                try
                {
                    System.IO.FileAccess streamAccess = readAccess ? System.IO.FileAccess.Read : System.IO.FileAccess.ReadWrite;

                    if (request.Mode == System.IO.FileMode.CreateNew && readAccess) streamAccess = System.IO.FileAccess.ReadWrite;

                    response.Info.Context = new FileStream(filePath, request.Mode,streamAccess, request.Share, 4096, request.Options);

                    if (pathExists && (request.Mode == FileMode.OpenOrCreate
                                       || request.Mode == FileMode.Create))
                        result = DokanResult.AlreadyExists;

                    bool fileCreated = request.Mode == FileMode.CreateNew || request.Mode == FileMode.Create || (!pathExists && request.Mode == FileMode.OpenOrCreate);
                    if (fileCreated)
                    {
                        FileAttributes new_attributes = request.Attributes;
                        new_attributes |= FileAttributes.Archive; // Files are always created as Archive
                        // FILE_ATTRIBUTE_NORMAL is override if any other attribute is set.
                        new_attributes &= ~FileAttributes.Normal;
                        File.SetAttributes(filePath, new_attributes);
                    }
                }
                catch (UnauthorizedAccessException) // don't have access rights
                {
                    if (info.Context is FileStream fileStream)
                    {
                        // returning AccessDenied cleanup and close won't be called,
                        // so we have to take care of the stream now
                        fileStream.Dispose();
                        info.Context = null;
                    }
                    return Trace(nameof(CreateFile), fileName, info, access, share, mode, options, attributes,
                        DokanResult.AccessDenied);
                }
                catch (DirectoryNotFoundException)
                {
                    return Trace(nameof(CreateFile), fileName, info, access, share, mode, options, attributes,
                        DokanResult.PathNotFound);
                }
                catch (Exception ex)
                {
                    var hr = (uint)Marshal.GetHRForException(ex);
                    switch (hr)
                    {
                        case 0x80070020: //Sharing violation
                            return Trace(nameof(CreateFile), fileName, info, access, share, mode, options, attributes,
                                DokanResult.SharingViolation);
                        default:
                            throw;
                    }
                }
            }
            return Trace(nameof(CreateFile), fileName, info, access, share, mode, options, attributes,
                result);
        }
    }
}
