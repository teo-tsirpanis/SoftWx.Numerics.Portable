// Copyright ©2015 SoftWx, Inc.
// Released under the MIT License the text of which appears at the end of this file.
// <authors> Steve Hatchett
using NUnit.Framework;

namespace SoftWx.Numerics.Tests {
    /// <summary></summary>
    [TestFixture]
    public class TestMath {
        [Test]
        public void AbsU0ShouldReturn0() {
            Assert.AreEqual(0, ((sbyte)0).AbsU(), "sbyte");
            Assert.AreEqual(0, ((short)0).AbsU(), "short");
            Assert.AreEqual(0, 0.AbsU(), "int");
            Assert.AreEqual(0, ((long)0).AbsU(), "long");
        }

        [Test]
        public void AbsU1ShouldReturn1() {
            Assert.AreEqual(1, ((sbyte)1).AbsU(), "sbyte");
            Assert.AreEqual(1, ((short)1).AbsU(), "short");
            Assert.AreEqual(1, 1.AbsU(), "int");
            Assert.AreEqual(1, ((long)1).AbsU(), "long");
        }

        [Test]
        public void AbsUNegative1ShouldReturn1() {
            Assert.AreEqual(1, ((sbyte)-1).AbsU(), "sbyte");
            Assert.AreEqual(1, ((short)-1).AbsU(), "short");
            Assert.AreEqual(1, (-1).AbsU(), "int");
            Assert.AreEqual(1, ((long)-1).AbsU(), "long");
        }

        [Test]
        public void AbsUMaxValueShouldReturnMaxValue() {
            Assert.AreEqual(sbyte.MaxValue, sbyte.MaxValue.AbsU(), "sbyte");
            Assert.AreEqual(short.MaxValue, short.MaxValue.AbsU(), "short");
            Assert.AreEqual(int.MaxValue, int.MaxValue.AbsU(), "int");
            Assert.AreEqual(long.MaxValue, long.MaxValue.AbsU(), "long");
        }

        [Test]
        public void AbsUMinValueShouldReturnNegMinValue() {
            Assert.AreEqual(-(sbyte.MinValue+1), sbyte.MinValue.AbsU()-1, "sbyte");
            Assert.AreEqual(-(short.MinValue+1), short.MinValue.AbsU()-1, "short");
            Assert.AreEqual(-(int.MinValue+1), int.MinValue.AbsU()-1, "int");
            Assert.AreEqual(-(long.MinValue+1), long.MinValue.AbsU()-1, "long");
        }

        [Test]
        public void AbsUsByteShouldMatchWider() {
            sbyte v = sbyte.MinValue;
            while (true) {
                byte expected = v.AbsU();
                Assert.AreEqual((ushort)expected, ((short)v).AbsU(), "short=>" + v);
                Assert.AreEqual((uint)expected, ((int)v).AbsU(), "int=>" + v);
                Assert.AreEqual((ulong)expected, ((long)v).AbsU(), "long=>" + v);
                if (v == sbyte.MaxValue) break;
                v++;
            }
        }
    }
}