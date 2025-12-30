using System;
using System.Collections.Generic;
using System.IO;

namespace cYo.Projects.ComicRack.Engine.IO.Provider.Readers.Pdf;

public class PdfNative : IComicAccessor
{
    private class ImageStreamInfo : ProviderImageInfo
    {
        private readonly long offset;

        public long Offset => offset;

        public ImageStreamInfo(string name, long size, long offset)
            : base(0, name, size)
        {
            this.offset = offset;
        }

        public ImageStreamInfo(long size, long offset)
            : this(null, size, offset)
        {
        }
    }

    private class Match
    {
        private int matchPos;

        private readonly char[] tag;

        public int Length => tag.Length;

        public Match(string tag)
        {
            this.tag = tag.ToCharArray();
        }

        public bool IsMatch(int b)
        {
            if (tag[matchPos] == b)
            {
                matchPos++;
            }
            else if (matchPos != 0)
            {
                matchPos = 0;
                if (tag[matchPos] == b)
                {
                    matchPos++;
                }
            }
            if (matchPos == tag.Length)
            {
                matchPos = 0;
                return true;
            }
            return false;
        }
    }

    private class Reader
    {
        private byte[] readBuffer = new byte[100000];

        private long bufferOffset;

        private int bufferLen;

        private int bufferPos;

        private Stream readStream;

        public Reader(Stream readStream)
        {
            this.readStream = readStream;
        }

        public long GetPosition()
        {
            return bufferOffset + bufferPos;
        }

        public int ReadByte()
        {
            if (bufferPos >= bufferLen)
            {
                bufferOffset += bufferLen;
                bufferLen = readStream.Read(readBuffer, 0, readBuffer.Length);
                bufferPos = 0;
                if (bufferLen == 0)
                {
                    return -1;
                }
            }
            return readBuffer[bufferPos++];
        }
    }

    public bool IsFormat(string source)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<ProviderImageInfo> GetEntryList(string source)
    {
        Match streamStartTag = new("stream");
        Match streamEndTag = new("endstream");
        using (FileStream readStream = File.OpenRead(source))
        {
            Reader reader = new(readStream);
            int b = 0;
            while (b != -1)
            {
                while ((b = reader.ReadByte()) != -1 && !streamStartTag.IsMatch(b)) ;
                b = reader.ReadByte();
                while (b is not (-1) and (10 or 13))
                {
                    b = reader.ReadByte();
                }
                long offset = reader.GetPosition() - 1L;
                if (b != byte.MaxValue || reader.ReadByte() != 216)
                {
                    b = reader.ReadByte();
                }
                else
                {
                    while ((b = reader.ReadByte()) != -1 && !streamEndTag.IsMatch(b)) ;
                    yield return new ImageStreamInfo(reader.GetPosition() - offset - streamEndTag.Length - 1, offset);
                }
            }
            reader = null;
        }
    }

    public byte[] ReadByteImage(string source, ProviderImageInfo info)
    {
        return LoadBitmapData(source, (ImageStreamInfo)info);
    }

    public ComicInfo ReadInfo(string source)
    {
        return null;
    }

    public bool WriteInfo(string source, ComicInfo info)
    {
        return false;
    }

    private static byte[] LoadBitmapData(string file, ImageStreamInfo si)
    {
        try
        {
            byte[] array = new byte[si.Size];
            using (FileStream fileStream = File.OpenRead(file))
            {
                fileStream.Seek(si.Offset, SeekOrigin.Begin);
                return fileStream.Read(array, 0, array.Length) != array.Length ? null : array;
            }
        }
        catch (Exception)
        {
            return null;
        }
    }
}
