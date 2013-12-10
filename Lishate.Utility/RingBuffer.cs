using System;
using System.Collections.Generic;
using System.Text;

namespace Lishate.Utility
{
    public class RingBuffer
    {
        private byte[] _buffer;
        public byte[] Content
        {
            get { return _buffer; }
            set { _buffer = value; }
        }

        private int _readIndex = 0;
        public int ReadIndex
        {
            get { return _readIndex; }
            set { _readIndex = value; }
        }

        private int _writeIndex = 0;
        public int WriteIndex
        {
            get { return _writeIndex; }
            set { _writeIndex = value; }
        }

        private int _ringLength = 0;
        public int RingLength
        {
            get { return _ringLength; }
        }

        public int UserLength
        {
            get { return _writeIndex - _readIndex; }
        }

        public int EmptyLength
        {
            get { return _ringLength + _readIndex - _writeIndex; }
        }

        public bool CanWrite()
        {
            if (EmptyLength > 0)
            {
                return false;
            }
            return true;
        }

        public bool CanRead()
        {
            if (UserLength > 0)
            {
                return true;
            }
            return false;
        }

        private bool WriteByte(byte b)
        {
            if (CanWrite() == true)
            {
                _buffer[_writeIndex % _ringLength] = b;
                _writeIndex++;
                return true;
            }
            return false;
        }

        private bool ReadByte( ref byte b)
        {
            if (CanRead() == true)
            {
                b = _buffer[_readIndex % _ringLength];
                _readIndex++;
                return true;
            }
            return false;
        }

        public int WriteToBuf(byte[] src, int index, int length)
        {
            for (int i = 0; i < length; i++)
            {
                if (WriteByte(src[index +i]) == false)
                {
                    return i;
                }
            }
            return length;
        }

        public int ReadToBuf(byte[] src, int index, int length)
        {
            for (int i = 0; i < length; i++)
            {
                if(ReadByte(ref src[index+i]) == false)
                {
                    return i;
                }
            }
            return length;
        }

        public bool GetNextByte(ref byte b)
        {
            return ReadByte(ref b);
        }

        public bool WriteNextByte(byte b)
        {
            return WriteByte(b);
        }

        public bool GetIndexByte(int index, ref byte b)
        {
            if (index > UserLength)
            {
                return false;
            }
            b = _buffer[(index + _readIndex) % _ringLength];
            return true;
        }



        public RingBuffer(int length)
        {
            _ringLength = length;
            _buffer = new byte[_ringLength];
            _readIndex = 0;
            _writeIndex = 0;
        }
    }
}
