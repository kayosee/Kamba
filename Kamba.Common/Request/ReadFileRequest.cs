﻿using DokanNet;

namespace Kamba.Common.Request
{
    public class ReadFileRequest : FileRequest
    {
        private long offset;

        public ReadFileRequest(int clientId, long requestId, string fileName, long offset, IDokanFileInfo info) : base(DataType.ReadFileRequest, clientId, requestId, fileName, info)
        {
            this.offset = offset;
        }
        public ReadFileRequest(ByteArrayStream stream) : base(stream)
        {
            offset = stream.ReadInt64();
        }
        protected override ByteArrayStream GetStream()
        {
            var stream = base.GetStream();
            stream.Write(offset);
            return stream;
        }
    }
}