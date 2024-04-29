using DokanNet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Kamba.Common;

public class ByteArrayStream : IDisposable
{
    private byte[] _data;
    private long _readPos;
    private long _writePos;

    public ByteArrayStream()
    {
        _data = new byte[0];
        _readPos = 0;
        _writePos = 0;
    }
    public ByteArrayStream(long length)
    {
        _data = new byte[length];
        _readPos = 0;
        _writePos = 0;
    }
    public ByteArrayStream(byte[] data)
    {
        _data = new byte[data.Length];
        Array.Copy(data, 0, _data, 0, data.Length);
        _readPos = 0;
        _writePos = 0;
    }
    public long Length => _data.LongLength;
    public long WritePosition { get => _writePos; set => _writePos = value; }
    public long ReadPosition { get => _readPos; set => _readPos = value; }
    public int Peek(byte[] buffer, int offset, int count)
    {
        if (_data.Length > 0)
        {
            var min = Math.Min(count, _data.Length);
            Span<byte> span = buffer;
            Span<byte> data = _data;
            var slice = span.Slice(offset, min);
            data.Slice((int)_readPos, count).CopyTo(slice);
            return min;
        }

        throw new EndOfStreamException();
    }
    public int Read(byte[] buffer, int offset, int count)
    {
        if (_data.Length > 0)
        {
            var min = Math.Min(count, _data.Length);
            Span<byte> span = buffer;
            Span<byte> data = _data;
            var slice = span.Slice(offset, min);
            data.Slice((int)_readPos, count).CopyTo(slice);
            _readPos += min;
            return min;
        }

        throw new EndOfStreamException();
    }
    public ushort ReadUInt16()
    {
        var size = sizeof(ushort);
        if (_data.LongLength == 0 || _readPos + size > _data.LongLength)
            throw new EndOfStreamException();

        Span<byte> span = _data;
        var result = BitConverter.ToUInt16(span.Slice((int)_readPos, size));
        _readPos += size;
        return result;
    }

    public uint ReadUInt32()
    {
        var size = sizeof(uint);
        if (_data.LongLength == 0 || _readPos + size > _data.LongLength)
            throw new EndOfStreamException();

        Span<byte> span = _data;
        var result = BitConverter.ToUInt32(span.Slice((int)_readPos, size));
        _readPos += size;
        return result;
    }
    public ulong ReadUInt64()
    {
        var size = sizeof(ulong);
        if (_data.LongLength == 0 || _readPos + size > _data.LongLength)
            throw new EndOfStreamException();

        Span<byte> span = _data;
        var result = BitConverter.ToUInt64(span.Slice((int)_readPos, size));
        _readPos += size;
        return result;
    }
    public short ReadInt16()
    {
        var size = sizeof(short);

        if (_data.LongLength == 0 || _readPos + size > _data.LongLength)
            throw new EndOfStreamException();

        Span<byte> span = _data;
        var result = BitConverter.ToInt16(span.Slice((int)_readPos, size));
        _readPos += size;
        return result;
    }
    public int ReadInt32()
    {
        var size = sizeof(int);
        if (_data.LongLength == 0 || _readPos + size > _data.LongLength)
            throw new EndOfStreamException();

        Span<byte> span = _data;
        var result = BitConverter.ToInt32(span.Slice((int)_readPos, size));
        _readPos += size;
        return result;
    }
    public long ReadInt64()
    {
        var size = sizeof(long);
        if (_data.LongLength == 0 || _readPos + size > _data.LongLength)
            throw new EndOfStreamException();

        Span<byte> span = _data;
        var result = BitConverter.ToInt64(span.Slice((int)_readPos, size));
        _readPos += size;
        return result;
    }
    public long Seek(long offset, SeekOrigin origin)
    {
        switch (origin)
        {
            case SeekOrigin.Begin:
                _readPos = offset;
                _writePos = offset;
                break;
            case SeekOrigin.Current:
                _readPos = _readPos + offset;
                _writePos = _writePos + offset;
                break;
            case SeekOrigin.End:
                _readPos = _readPos + _data.Length;
                _writePos = _writePos + _data.Length;
                break;
        }

        return _readPos;
    }
    public void SetLength(long value)
    {
        Array.Resize(ref _data, (int)value);
    }
    public void Write(byte[] buffer, int offset, int count)
    {
        if (_writePos + count > _data.Length)
            SetLength(_writePos + count);

        Span<byte> span = buffer;
        Span<byte> data = _data;
        span.Slice(offset, count).CopyTo(data.Slice((int)_writePos));
        _writePos += count;
    }
    public void Write(byte value)
    {
        if (_writePos + 1 >= _data.Length)
            SetLength(_writePos + 1);

        Write(new byte[] { value }, 0, 1);
    }
    public void Write(short value)
    {
        if (_writePos + sizeof(short) >= _data.Length)
            SetLength(_writePos + sizeof(short));

        var buffer = BitConverter.GetBytes(value);
        Write(buffer, 0, buffer.Length);
    }
    public void Write(int value)
    {
        if (_writePos + sizeof(int) >= _data.Length)
            SetLength(_writePos + sizeof(int));

        var buffer = BitConverter.GetBytes(value);
        Write(buffer, 0, buffer.Length);
    }
    public void Write(long value)
    {
        if (_writePos + sizeof(long) >= _data.Length)
            SetLength(_writePos + sizeof(long));

        var buffer = BitConverter.GetBytes(value);
        Write(buffer, 0, buffer.Length);
    }
    public void Write(uint value)
    {
        if (_writePos + sizeof(uint) >= _data.Length)
            SetLength(_writePos + sizeof(uint));

        var buffer = BitConverter.GetBytes(value);
        Write(buffer, 0, buffer.Length);
    }
    public byte[] GetBuffer()
    {
        return _data;
    }
    public void Dispose()
    {
    }
    public byte ReadByte()
    {
        if (_readPos >= _data.LongLength)
            throw new EndOfStreamException();

        var result = _data[_readPos];
        _readPos += 1;
        return result;
    }
    public FileInformation ReadFileInformation()
    {
        var result = new FileInformation();
        int fileNameLength = ReadInt32();
        var buffer = new byte[fileNameLength];
        Read(buffer, 0, fileNameLength);
        result.FileName = System.Text.Encoding.UTF8.GetString(buffer, 0, fileNameLength);
        result.Attributes = (FileAttributes)ReadInt32();
        result.CreationTime = DateTime.FromBinary(ReadInt64());
        result.LastAccessTime = DateTime.FromBinary(ReadInt64());
        result.LastWriteTime = DateTime.FromBinary(ReadInt64());
        result.Length = ReadInt64();
        return result;
    }
    public void WriteFileInformation(FileInformation fileInformation)
    {
        var buffer = System.Text.Encoding.UTF8.GetBytes(fileInformation.FileName);
        int fileNameLength = buffer.Length;
        Write(fileNameLength);
        Write(buffer, 0, fileNameLength);
        Write((int)fileInformation.Attributes);
        Write(fileInformation.CreationTime.GetValueOrDefault().Ticks);
        Write(fileInformation.LastAccessTime.GetValueOrDefault().Ticks);
        Write(fileInformation.LastWriteTime.GetValueOrDefault().Ticks);
        Write(fileInformation.Length);
    }
    public DokanNet.IDokanFileInfo ReadDokanFileInfo()
    {
        MockDokanFileInfo fileInfo = new MockDokanFileInfo();
        fileInfo.DeleteOnClose = ReadByte() == 1;
        fileInfo.IsDirectory = ReadByte() == 1;
        fileInfo.NoCache = ReadByte() == 1;
        fileInfo.PagingIo = ReadByte() == 1;
        fileInfo.ProcessId = ReadInt32();
        fileInfo.SynchronousIo = ReadByte() == 1;
        fileInfo.WriteToEndOfFile = ReadByte() == 1;
        return fileInfo;
    }
    public void WriteDokanFileInfo(IDokanFileInfo fileInfo)
    {
        Write((byte)(fileInfo.DeleteOnClose ? 1 : 0));
        Write((byte)(fileInfo.IsDirectory ? 1 : 0));
        Write((byte)(fileInfo.NoCache ? 1 : 0));
        Write((byte)(fileInfo.PagingIo ? 1 : 0));
        Write(fileInfo.ProcessId);
        Write((byte)(fileInfo.SynchronousIo ? 1 : 0));
        Write((byte)(fileInfo.WriteToEndOfFile ? 1 : 0));
    }
}
