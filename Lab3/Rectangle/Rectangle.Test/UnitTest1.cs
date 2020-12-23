using NUnit.Framework;
using System;

namespace Rectangle.Tests
{
    public class Tests
    {
        [TestCase("-1", "0", false)]
        [TestCase("0", "0", true)]
        [TestCase("a", "0", false)]
        [TestCase("10", "12", true)]
        [TestCase("FF", "1", false)]
        [TestCase("4294967295", "0", true)]
        [TestCase("-5E+500", "5E+500", false)]

        public void CheckArgsTests(string l, string w, bool answer)
        {
            Assert.AreEqual(CoverageLab.Rectangle.CheckArgs(l, w), answer); //корректность вводимых аргументов
        }

        [TestCase("0", "0", 0)]
        [TestCase("12", "10", 12)]
        [TestCase("-2", "0", 0)]
        [TestCase("a", "0", 0)]
        [TestCase("FF", "0", 0)]
        [TestCase("5E+500", "0", 0)]
        [TestCase("-5E+500", "0", 0)]

        public void GetLengthTests(string l, string w, int answer)
        { 
            CoverageLab.Rectangle rectangle = new CoverageLab.Rectangle(l, w);
            Assert.AreEqual(rectangle.GetLength(), Convert.ToUInt32(answer)); //длина
        }

        [TestCase("0", "0", 0)]
        [TestCase("12", "10", 10)]
        [TestCase("-2", "-10", 0)]
        [TestCase("a", "a", 0)]
        [TestCase("FF", "AA", 0)]
        [TestCase("5E+500", "5E+500", 0)]
        [TestCase("-5E+500", "-5E+500", 0)]

        public void GetWidthTests(string l, string w, int answer)
        {
            CoverageLab.Rectangle rectangle = new CoverageLab.Rectangle(l, w); //ширина
            Assert.AreEqual(rectangle.GetWidth(), Convert.ToUInt32(answer));
        }

        [TestCase("0", "1", 1)]
        [TestCase("12", "10", 10)]
        [TestCase("2", "-10", 2)]
        [TestCase("-2", "-10", 0)]
        [TestCase("a", "12", 12)]
        [TestCase("2", "AA", 2)]
        [TestCase("5", "5E+500", 5)]
        [TestCase("-5E+500", "5", 5)]

        public void SetLengthTests(string l, string newL, int answer)
        {
            CoverageLab.Rectangle rectangle = new CoverageLab.Rectangle(l, "0"); 
            rectangle.SetLength(newL);
            Assert.AreEqual(rectangle.GetLength(), Convert.ToUInt32(answer));
        }

        [TestCase("0", "1", 1)]
        [TestCase("12", "10", 10)]
        [TestCase("2", "-10", 0)]
        [TestCase("-2", "-10", 0)]
        [TestCase("a", "12", 12)]
        [TestCase("2", "AA", 0)]
        [TestCase("5", "5E+500", 0)]
        [TestCase("-5E+500", "5", 5)]

        public void SetWidthTests(string l, string newL, int answer)
        {
            CoverageLab.Rectangle rectangle = new CoverageLab.Rectangle(l, "0");
            rectangle.SetWidth(newL);
            Assert.AreEqual(rectangle.GetWidth(), Convert.ToUInt32(answer));
        }

        [TestCase("0", "0", 0)]
        [TestCase("12", "10", 44)]
        [TestCase("-2", "-10", 0)]
        [TestCase("a", "a", 0)]
        [TestCase("FF", "AA", 0)]
        [TestCase("5E+500", "5E+500", 0)]
        [TestCase("-5E+500", "-5E+500", 0)]

        public void GetPerimeterTests(string l, string w, int answer)
        {
            CoverageLab.Rectangle rectangle = new CoverageLab.Rectangle(l, w);
            Assert.AreEqual(rectangle.GetPerim(), Convert.ToUInt32(answer));
        }

        [TestCase("0", "0", 0)]
        [TestCase("12", "10", 120)]
        [TestCase("-2", "-10", 0)]
        [TestCase("a", "a", 0)]
        [TestCase("FF", "AA", 0)]
        
        [TestCase("5E+500", "5E+500", 0)]
        [TestCase("-5E+500", "-5E+500", 0)]

        public void GetSquareTests(string l, string w, int answer)
        {
            CoverageLab.Rectangle rectangle = new CoverageLab.Rectangle(l, w);
            Assert.AreEqual(rectangle.GetSquare(), Convert.ToUInt32(answer));
        }

        [TestCase("0", "0", "1", "0", 0)]
        [TestCase("12", "10", "10", "12", 120)]
        [TestCase("-2", "-10", "2", "1", 2)]
        [TestCase("12", "9", "aa", "-9", 108)]
        [TestCase("FF", "AA", "10", "10", 100)]
        [TestCase("5E+500", "5E+500", "9", "5", 45)]
        [TestCase("-5E+500", "-5E+500", "3", "2", 6)]

        public void CalcSquareTests(string l, string w, string newL, string newW, int answer)
        {
            CoverageLab.Rectangle rectangle = new CoverageLab.Rectangle(l, w);
            rectangle.SetLength(newL);
            rectangle.SetWidth(newW);
            rectangle.CalcSquare();
            Assert.AreEqual(rectangle.GetSquare(), Convert.ToUInt32(answer));
        }

        [TestCase("0", "0", "1", "0", 2)]
        [TestCase("12", "10", "10", "12", 44)]
        [TestCase("-2", "-10", "2", "1", 6)]
        [TestCase("12", "9", "aa", "-9", 42)]
        [TestCase("12", "9", "10", "-9", 38)]
        [TestCase("FF", "AA", "10", "10", 40)]
        [TestCase("5E+500", "5E+500", "9", "5", 28)]
        [TestCase("-5E+500", "-5E+500", "3", "2", 10)]

        public void CalcPerimTests(string l, string w, string newL, string newW, int answer)
        {
            CoverageLab.Rectangle rectangle = new CoverageLab.Rectangle(l, w);
            rectangle.SetLength(newL);
            rectangle.SetWidth(newW);
            rectangle.CalcPerim();
            Assert.AreEqual(rectangle.GetPerim(), Convert.ToUInt32(answer));
        }

    }
}