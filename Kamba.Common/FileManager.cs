using DokanNet;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Kamba.Common
{
    public class FileManager
    {
        private Dictionary<long, FileStream> fileStreams = new Dictionary<long, FileStream>();
        public FileStream this[long index]
        {
            get { return fileStreams[index]; }
            set { fileStreams[index] = value; }
        }
        public void Add(long index, FileStream stream)
        {
            fileStreams.Add(index, stream);
        }
        public void Remove(long index)
        {
            fileStreams.Remove(index);
        }
    }
}
