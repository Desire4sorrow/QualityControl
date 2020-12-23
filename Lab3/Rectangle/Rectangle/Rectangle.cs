using System;
using System.Collections.Generic;
using System.Text;

namespace CoverageLab
{
    public class Rectangle
    {
        private uint length;
        private uint width;
        private uint perimeter;
        private uint square;

        public Rectangle(string l, string w)
        {
            if (CheckArgs(l, w))
            {
                length = Convert.ToUInt32(l);
                width = Convert.ToUInt32(w);
                CalcPerim();
                CalcSquare();
            }
        }

        public static bool CheckArgs(string l, string w) //наличие 2х аргументов
        {
            try
            {
                Convert.ToUInt32(l);
                Convert.ToUInt32(w);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public uint GetLength()
        {
            return length;
        }

        public uint GetWidth()
        {
            return width;
        }

        public void SetLength(string newLength)
        {
            try
            {
                length = Convert.ToUInt32(newLength); //число без знака
            }
            catch
            { }
        }

        public void SetWidth(string newWidth)
        {
            try
            {
                width = Convert.ToUInt32(newWidth); //число без знака
            }
            catch
            { }
        }

        public void CalcPerim()
        {
            perimeter = (width + length) * 2;
        }

        public void CalcSquare()
        {
            square = width * length;
        }

        public uint GetSquare()
        {
            return square;
        }

        public uint GetPerim()
        {
            return perimeter;
        }
    }
}