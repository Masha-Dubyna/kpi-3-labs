using NUnit.Framework;
using IIG.BinaryFlag;
using System;

namespace KPI
{
    public class BlackBoxTesting
    {
        [Test]
        public void Constructor_WithMinUlongValue()
        {
            Assert.DoesNotThrow(() => new MultipleBinaryFlag(0));
        }

        [Test]
        public void Constructor_WithMaxUlongValue()
        {
            Assert.DoesNotThrow(() => new MultipleBinaryFlag(UInt64.MaxValue));
        }

        [Test]
        public void Constructor_WithLesserThanMinValue_ThrowsExeption()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new MultipleBinaryFlag(1));
        }

        [Test]
        public void Constructor_WithNumberBiggerThanMax_ThrowsExeption()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new MultipleBinaryFlag(17179868704));
        }

        [Test]
        public void Constructor_WithMinValue_DoesNotThrowExeption_GetReturnsFalse()
        {
            MultipleBinaryFlag flag = new MultipleBinaryFlag(2, false);
            Assert.False(flag.GetFlag());
        }

        [Test]
        public void Constructor_WithMaxValue_DoesNotThrowExeption_GetReturnsTrue()
        {
            MultipleBinaryFlag flag = new MultipleBinaryFlag(17179868703, true);
            Assert.True(flag.GetFlag());
        }

        [Test]
        public void Constructor_WithIntermediateValue_DoesNotThrowExeption_GetReturnsTrue()
        {
            MultipleBinaryFlag flag = new MultipleBinaryFlag(15);
            Assert.True(flag.GetFlag());
        }

        [Test]
        public void Despose_IsFlagBecomeNull()
        {
            MultipleBinaryFlag flag = new MultipleBinaryFlag(15);
            flag.Dispose();
            Assert.Null(flag);
        }

        [Test]
        public void Set_WithCorrectIndex()
        {
            MultipleBinaryFlag flag = new MultipleBinaryFlag(2, false);
            flag.SetFlag(0);
            flag.SetFlag(1);
            Assert.True(flag.GetFlag());
        }

        [Test]
        public void Set_WithIncorrectIndex()
        {
            MultipleBinaryFlag flag = new MultipleBinaryFlag(2, false);
            Assert.Throws<ArgumentOutOfRangeException>(() => flag.SetFlag(2));
        }

        [Test]
        public void Reset_WithCorrectIndex()
        {
            MultipleBinaryFlag flag = new MultipleBinaryFlag(2, true);
            flag.ResetFlag(0);
            Assert.False(flag.GetFlag());
        }

        [Test]
        public void Reset_WithIncorrectIndex()
        {
            MultipleBinaryFlag flag = new MultipleBinaryFlag(2, true);
            Assert.Throws<ArgumentOutOfRangeException>(() => flag.ResetFlag(2));
        }

    }
}