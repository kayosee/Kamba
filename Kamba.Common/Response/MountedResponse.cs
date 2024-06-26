﻿using DokanNet;
using Kamba.Common.Request;

namespace Kamba.Common.Response
{
    public class MountedResponse : FileResponse
    {
        private int mountPointLength;
        private string mountPoint;

        public MountedResponse(int clientId, long requestId, string mountPoint, IDokanFileInfo info) : base(DataType.MountedResponse, clientId, requestId, "", info)
        {
            this.mountPoint = mountPoint;
        }
        public MountedResponse(MountedRequest request) : base(DataType.MountedResponse, request) 
        {
            this.mountPoint = string.Empty;
        }
        public MountedResponse(ByteArrayStream stream) : base(stream)
        {
            mountPointLength = stream.ReadInt32();
            var buffer = new byte[mountPointLength];
            stream.Read(buffer, 0, buffer.Length);
            mountPoint = System.Text.Encoding.UTF8.GetString(buffer);
        }
        protected override ByteArrayStream GetStream()
        {
            var stream = base.GetStream();
            var buffer = System.Text.Encoding.UTF8.GetBytes(mountPoint);
            mountPointLength = buffer.Length;
            stream.Write(buffer, 0, buffer.Length);
            return stream;
        }
    }
}