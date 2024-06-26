﻿using DokanNet;

namespace Kamba.Common.Request
{
    public class MountedRequest : FileRequest
    {
        private int mountPointLength;
        private string mountPoint;

        public MountedRequest(int clientId, long requestId, string mountPoint, IDokanFileInfo info) : base(DataType.MountedRequest, clientId, requestId,"",info)
        {
            this.mountPoint = mountPoint;
        }
        public MountedRequest(ByteArrayStream stream) : base(stream)
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