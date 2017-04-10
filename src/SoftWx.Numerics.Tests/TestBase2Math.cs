// Copyright ©2015 SoftWx, Inc.
// Released under the MIT License the text of which appears at the end of this file.
// <authors> Steve Hatchett

using NUnit.Framework;

namespace SoftWx.Numerics.Tests {
    /// <summary></summary>
    [TestFixture]
    public class TestBase2Math {

        [Test]
        public void Log2UnsignedOf0ShouldReturnMaxValue() {
            Assert.AreEqual(byte.MaxValue, ((byte)0).Log2(), "byte");
            Assert.AreEqual(ushort.MaxValue, ((ushort)0).Log2(), "ushort");
            Assert.AreEqual(uint.MaxValue, ((uint)0).Log2(), "uint");
            Assert.AreEqual(ulong.MaxValue, ((ulong)0).Log2(), "ulong");
            Assert.AreEqual(UInt128.MaxValue, ((UInt128)0).Log2(), "UInt128");
        }

        [Test]
        public void Log2SignedOf0ShouldReturnNeg1() {
            Assert.AreEqual(-1, ((sbyte)0).Log2(), "sbyte");
            Assert.AreEqual(-1, ((short)0).Log2(), "short");
            Assert.AreEqual(-1, 0.Log2(), "int");
            Assert.AreEqual(-1, ((long)0).Log2(), "long");
        }

        [Test]
        public void Log2SignedOfNegativeShouldReturnNeg1() {
            Assert.AreEqual(-1, ((sbyte)-10).Log2(), "sbyte");
            Assert.AreEqual(-1, ((short)-10).Log2(), "short");
            Assert.AreEqual(-1, (-10).Log2(), "int");
            Assert.AreEqual(-1, ((long)-10).Log2(), "long");
        }

        [Test]
        public void Log2Of1ShouldReturn0() {
            Assert.AreEqual(0, ((byte)1).Log2(), "byte");
            Assert.AreEqual(0, ((sbyte)1).Log2(), "sbyte");
            Assert.AreEqual(0, ((ushort)1).Log2(), "ushort");
            Assert.AreEqual(0, ((short)1).Log2(), "short");
            Assert.AreEqual(0, ((uint)1).Log2(), "uint");
            Assert.AreEqual(0, 1.Log2(), "int");
            Assert.AreEqual(0, ((ulong)1).Log2(), "ulong");
            Assert.AreEqual(0, ((long)1).Log2(), "long");
            Assert.AreEqual(UInt128.Zero, ((UInt128)1).Log2(), "UInt128");
        }

        [Test]
        public void Log2Of2ShouldReturn1() {
            Assert.AreEqual(1, ((byte)2).Log2(), "byte");
            Assert.AreEqual(1, ((sbyte)2).Log2(), "sbyte");
            Assert.AreEqual(1, ((ushort)2).Log2(), "ushort");
            Assert.AreEqual(1, ((short)2).Log2(), "short");
            Assert.AreEqual(1, ((uint)2).Log2(), "uint");
            Assert.AreEqual(1, 2.Log2(), "int");
            Assert.AreEqual(1, ((ulong)2).Log2(), "ulong");
            Assert.AreEqual(1, ((long)2).Log2(), "long");
            Assert.AreEqual((UInt128)1, ((UInt128)2).Log2(), "UInt128");
        }

        [Test]
        public void Log2Of10ShouldReturn3() {
            Assert.AreEqual(3, ((byte)10).Log2(), "byte");
            Assert.AreEqual(3, ((sbyte)10).Log2(), "sbyte");
            Assert.AreEqual(3, ((ushort)10).Log2(), "ushort");
            Assert.AreEqual(3, ((short)10).Log2(), "short");
            Assert.AreEqual(3, ((uint)10).Log2(), "uint");
            Assert.AreEqual(3, 10.Log2(), "int");
            Assert.AreEqual(3, ((ulong)10).Log2(), "ulong");
            Assert.AreEqual(3, ((long)10).Log2(), "long");
            Assert.AreEqual((UInt128)3, ((UInt128)10).Log2(), "UInt128");
        }

        [Test]
        public void Log2UnsignedByteShouldMatchWider() {
            byte v = 1;
            while (true) {
                byte expected = v.Log2();
                Assert.AreEqual((ushort)expected, ((ushort)v).Log2(), "ushort=>" + v);
                Assert.AreEqual((short)expected, ((short)v).Log2(), "short=>" + v);
                Assert.AreEqual((uint)expected, ((uint)v).Log2(), "uint=>" + v);
                Assert.AreEqual((int)expected, ((int)v).Log2(), "int=>" + v);
                Assert.AreEqual((ulong)expected, ((ulong)v).Log2(), "ulong=>" + v);
                Assert.AreEqual((long)expected, ((long)v).Log2(), "long=>" + v);
                Assert.AreEqual((UInt128)expected, ((UInt128)v).Log2(), "UInt128=>" + v);
                if (v == byte.MaxValue) break;
                v++;
            }
        }

        [Test]
        public void Log2SignedByteShouldMatchWider() {
            sbyte v = sbyte.MinValue;
            while (true) {
                sbyte expected = v.Log2();
                Assert.AreEqual((short)expected, ((short)v).Log2(), "short");
                Assert.AreEqual((int)expected, ((int)v).Log2(), "int");
                Assert.AreEqual((long)expected, ((long)v).Log2(), "long");
                if (v == sbyte.MaxValue) break;
                v++;
            }
        }

        [Test]
        public void IsPowerOf2SignedOfNegativeShouldReturnFalse() {
            Assert.AreEqual(false, ((sbyte)-10).IsPowerOf2(), "sbyte");
            Assert.AreEqual(false, ((short)-10).IsPowerOf2(), "short");
            Assert.AreEqual(false, (-10).IsPowerOf2(), "int");
            Assert.AreEqual(false, ((long)-10).IsPowerOf2(), "long");
        }

        [Test]
        public void IsPowerOf2Of0ShouldReturnFalse() {
            Assert.AreEqual(false, ((byte)0).IsPowerOf2(), "byte");
            Assert.AreEqual(false, ((sbyte)0).IsPowerOf2(), "sbyte");
            Assert.AreEqual(false, ((ushort)0).IsPowerOf2(), "ushort");
            Assert.AreEqual(false, ((short)0).IsPowerOf2(), "short");
            Assert.AreEqual(false, ((uint)0).IsPowerOf2(), "uint");
            Assert.AreEqual(false, 0.IsPowerOf2(), "int");
            Assert.AreEqual(false, ((ulong)0).IsPowerOf2(), "ulong");
            Assert.AreEqual(false, ((long)0).IsPowerOf2(), "long");
            Assert.AreEqual(false, ((UInt128)0).IsPowerOf2(), "UInt128");
        }

        [Test]
        public void IsPowerOf2Of1ShouldReturnTrue() {
            Assert.AreEqual(true, ((byte)1).IsPowerOf2(), "byte");
            Assert.AreEqual(true, ((sbyte)1).IsPowerOf2(), "sbyte");
            Assert.AreEqual(true, ((ushort)1).IsPowerOf2(), "ushort");
            Assert.AreEqual(true, ((short)1).IsPowerOf2(), "short");
            Assert.AreEqual(true, ((uint)1).IsPowerOf2(), "uint");
            Assert.AreEqual(true, 1.IsPowerOf2(), "int");
            Assert.AreEqual(true, ((ulong)1).IsPowerOf2(), "ulong");
            Assert.AreEqual(true, ((long)1).IsPowerOf2(), "long");
            Assert.AreEqual(true, ((UInt128)1).IsPowerOf2(), "UInt128");
        }

        [Test]
        public void IsPowerOf2Of2ShouldReturnTrue() {
            Assert.AreEqual(true, ((byte)2).IsPowerOf2(), "byte");
            Assert.AreEqual(true, ((sbyte)2).IsPowerOf2(), "sbyte");
            Assert.AreEqual(true, ((ushort)2).IsPowerOf2(), "ushort");
            Assert.AreEqual(true, ((short)2).IsPowerOf2(), "short");
            Assert.AreEqual(true, ((uint)2).IsPowerOf2(), "uint");
            Assert.AreEqual(true, 2.IsPowerOf2(), "int");
            Assert.AreEqual(true, ((ulong)2).IsPowerOf2(), "ulong");
            Assert.AreEqual(true, ((long)2).IsPowerOf2(), "long");
            Assert.AreEqual(true, ((UInt128)2).IsPowerOf2(), "UInt128");
        }

        [Test]
        public void IsPowerOf2Of3ShouldReturnFalse() {
            Assert.AreEqual(false, ((byte)3).IsPowerOf2(), "byte");
            Assert.AreEqual(false, ((sbyte)3).IsPowerOf2(), "sbyte");
            Assert.AreEqual(false, ((ushort)3).IsPowerOf2(), "ushort");
            Assert.AreEqual(false, ((short)3).IsPowerOf2(), "short");
            Assert.AreEqual(false, ((uint)3).IsPowerOf2(), "uint");
            Assert.AreEqual(false, 3.IsPowerOf2(), "int");
            Assert.AreEqual(false, ((ulong)3).IsPowerOf2(), "ulong");
            Assert.AreEqual(false, ((long)3).IsPowerOf2(), "long");
            Assert.AreEqual(false, ((UInt128)3).IsPowerOf2(), "UInt128");
        }

        [Test]
        public void IsPowerOf2UnsignedByteShouldMatchWider() {
            byte v = byte.MinValue;
            while (true) {
                bool expected = v.IsPowerOf2();
                Assert.AreEqual(expected, ((ushort)v).IsPowerOf2(), "ushort=>" + v);
                Assert.AreEqual(expected, ((short)v).IsPowerOf2(), "short=>" + v);
                Assert.AreEqual(expected, ((uint)v).IsPowerOf2(), "uint=>" + v);
                Assert.AreEqual(expected, ((int)v).IsPowerOf2(), "int=>" + v);
                Assert.AreEqual(expected, ((ulong)v).IsPowerOf2(), "ulong=>" + v);
                Assert.AreEqual(expected, ((long)v).IsPowerOf2(), "long=>" + v);
                Assert.AreEqual(expected, ((UInt128)v).IsPowerOf2(), "UInt128=>" + v);
                if (v == byte.MaxValue) break;
                v++;
            }
        }

        [Test]
        public void IsPowerOf2SignedByteShouldMatchWider() {
            sbyte v = sbyte.MinValue;
            while (true) {
                bool expected = v.IsPowerOf2();
                Assert.AreEqual(expected, ((short)v).IsPowerOf2(), "short=>" + v);
                Assert.AreEqual(expected, ((int)v).IsPowerOf2(), "int=>" + v);
                Assert.AreEqual(expected, ((long)v).IsPowerOf2(), "long=>" + v);
                if (v == sbyte.MaxValue) break;
                v++;
            }
        }

        [Test]
        public void PowerOf2FloorOf0ShouldReturn0() {
            Assert.AreEqual(0, ((byte)0).PowerOf2Floor(), "byte");
            Assert.AreEqual(0, ((sbyte)0).PowerOf2Floor(), "sbyte");
            Assert.AreEqual(0, ((ushort)0).PowerOf2Floor(), "ushort");
            Assert.AreEqual(0, ((short)0).PowerOf2Floor(), "short");
            Assert.AreEqual(0, ((uint)0).PowerOf2Floor(), "uint");
            Assert.AreEqual(0, 0.PowerOf2Floor(), "int");
            Assert.AreEqual(0, ((ulong)0).PowerOf2Floor(), "ulong");
            Assert.AreEqual(0, ((long)0).PowerOf2Floor(), "long");
            Assert.AreEqual(UInt128.Zero, ((UInt128)0).PowerOf2Floor(), "UInt128");
        }

        [Test]
        public void PowerOf2FloorOfNegativeShouldReturn0() {
            Assert.AreEqual(0, ((sbyte)-10).PowerOf2Floor(), "sbyte");
            Assert.AreEqual(0, ((short)-10).PowerOf2Floor(), "short");
            Assert.AreEqual(0, (-10).PowerOf2Floor(), "int");
            Assert.AreEqual(0, ((long)-10).PowerOf2Floor(), "long");
        }

        [Test]
        public void PowerOf2FloorOf1ShouldReturn1() {
            Assert.AreEqual(1, ((byte)1).PowerOf2Floor(), "byte");
            Assert.AreEqual(1, ((sbyte)1).PowerOf2Floor(), "sbyte");
            Assert.AreEqual(1, ((ushort)1).PowerOf2Floor(), "ushort");
            Assert.AreEqual(1, ((short)1).PowerOf2Floor(), "short");
            Assert.AreEqual(1, ((uint)1).PowerOf2Floor(), "uint");
            Assert.AreEqual(1, 1.PowerOf2Floor(), "int");
            Assert.AreEqual(1, ((ulong)1).PowerOf2Floor(), "ulong");
            Assert.AreEqual(1, ((long)1).PowerOf2Floor(), "long");
            Assert.AreEqual((UInt128)1, ((UInt128)1).PowerOf2Floor(), "UInt128");
        }

        [Test]
        public void PowerOf2FloorOf3ShouldReturn2() { 
            Assert.AreEqual(2, ((byte)3).PowerOf2Floor(), "byte");
            Assert.AreEqual(2, ((sbyte)3).PowerOf2Floor(), "sbyte");
            Assert.AreEqual(2, ((ushort)3).PowerOf2Floor(), "ushort");
            Assert.AreEqual(2, ((short)3).PowerOf2Floor(), "short");
            Assert.AreEqual(2, ((uint)3).PowerOf2Floor(), "uint");
            Assert.AreEqual(2, 3.PowerOf2Floor(), "int");
            Assert.AreEqual(2, ((ulong)3).PowerOf2Floor(), "ulong");
            Assert.AreEqual(2, ((long)3).PowerOf2Floor(), "long");
            Assert.AreEqual((UInt128)2, ((UInt128)3).PowerOf2Floor(), "UInt128");
        }

        [Test]
        public void PowerOf2FloorUnsignedByteShouldMatchWider() {
            byte v = byte.MinValue;
            while (true) {
                byte expected = v.PowerOf2Floor();
                Assert.AreEqual(expected, ((ushort)v).PowerOf2Floor(), "ushort=>" + v);
                Assert.AreEqual(expected, ((short)v).PowerOf2Floor(), "short=>" + v);
                Assert.AreEqual(expected, ((uint)v).PowerOf2Floor(), "uint=>" + v);
                Assert.AreEqual(expected, ((int)v).PowerOf2Floor(), "int=>" + v);
                Assert.AreEqual(expected, ((ulong)v).PowerOf2Floor(), "ulong=>" + v);
                Assert.AreEqual(expected, ((long)v).PowerOf2Floor(), "long=>" + v);
                Assert.AreEqual((UInt128)expected, ((UInt128)v).PowerOf2Floor(), "UInt128=>" + v);
                if (v == byte.MaxValue) break;
                v++;
            }
        }

        [Test]
        public void PowerOf2FloorSignedByteShouldMatchWider() {
            sbyte v = sbyte.MinValue;
            while (true) {
                sbyte expected = v.PowerOf2Floor();
                Assert.AreEqual(expected, ((short)v).PowerOf2Floor(), "short=>" + v);
                Assert.AreEqual(expected, ((int)v).PowerOf2Floor(), "int=>" + v);
                Assert.AreEqual(expected, ((long)v).PowerOf2Floor(), "long=>" + v);
                if (v == sbyte.MaxValue) break;
                v++;
            }
        }

        [Test]
        public void PowerOf2CeilingOf0ShouldReturn1() {
            Assert.AreEqual(1, ((byte)0).PowerOf2Ceiling(), "byte");
            Assert.AreEqual(1, ((sbyte)0).PowerOf2Ceiling(), "sbyte");
            Assert.AreEqual(1, ((ushort)0).PowerOf2Ceiling(), "ushort");
            Assert.AreEqual(1, ((short)0).PowerOf2Ceiling(), "short");
            Assert.AreEqual(1, ((uint)0).PowerOf2Ceiling(), "uint");
            Assert.AreEqual(1, 0.PowerOf2Ceiling(), "int");
            Assert.AreEqual(1, ((ulong)0).PowerOf2Ceiling(), "ulong");
            Assert.AreEqual(1, ((long)0).PowerOf2Ceiling(), "long");
            Assert.AreEqual((UInt128)1, ((UInt128)0).PowerOf2Ceiling(), "UInt128");
        }

        [Test]
        public void PowerOf2CeilingOfNegativeShouldReturn1() {
            Assert.AreEqual(1, ((sbyte)-10).PowerOf2Ceiling(), "sbyte");
            Assert.AreEqual(1, ((short)-10).PowerOf2Ceiling(), "short");
            Assert.AreEqual(1, (-10).PowerOf2Ceiling(), "int");
            Assert.AreEqual(1, ((long)-10).PowerOf2Ceiling(), "long");
        }

        [Test]
        public void PowerOf2CeilingOf1ShouldReturn1() {
            Assert.AreEqual(1, ((byte)1).PowerOf2Ceiling(), "byte");
            Assert.AreEqual(1, ((sbyte)1).PowerOf2Ceiling(), "sbyte");
            Assert.AreEqual(1, ((ushort)1).PowerOf2Ceiling(), "ushort");
            Assert.AreEqual(1, ((short)1).PowerOf2Ceiling(), "short");
            Assert.AreEqual(1, ((uint)1).PowerOf2Ceiling(), "uint");
            Assert.AreEqual(1, 1.PowerOf2Ceiling(), "int");
            Assert.AreEqual(1, ((ulong)1).PowerOf2Ceiling(), "ulong");
            Assert.AreEqual(1, ((long)1).PowerOf2Ceiling(), "long");
            Assert.AreEqual((UInt128)1, ((UInt128)1).PowerOf2Ceiling(), "UInt128");
        }

        [Test]
        public void PowerOf2CeilingOf3ShouldReturn4() {
            Assert.AreEqual(4, ((byte)3).PowerOf2Ceiling(), "byte");
            Assert.AreEqual(4, ((sbyte)3).PowerOf2Ceiling(), "sbyte");
            Assert.AreEqual(4, ((ushort)3).PowerOf2Ceiling(), "ushort");
            Assert.AreEqual(4, ((short)3).PowerOf2Ceiling(), "short");
            Assert.AreEqual(4, ((uint)3).PowerOf2Ceiling(), "uint");
            Assert.AreEqual(4, 3.PowerOf2Ceiling(), "int");
            Assert.AreEqual(4, ((ulong)3).PowerOf2Ceiling(), "ulong");
            Assert.AreEqual(4, ((long)3).PowerOf2Ceiling(), "long");
            Assert.AreEqual((UInt128)4, ((UInt128)3).PowerOf2Ceiling(), "UInt128");
        }

        [Test]
        public void PowerOf2CeilingOfMaxValueShouldReturn0() {
            Assert.AreEqual(0, byte.MaxValue.PowerOf2Ceiling(), "byte");
            Assert.AreEqual(0, sbyte.MaxValue.PowerOf2Ceiling(), "sbyte");
            Assert.AreEqual(0, ushort.MaxValue.PowerOf2Ceiling(), "ushort");
            Assert.AreEqual(0, short.MaxValue.PowerOf2Ceiling(), "short");
            Assert.AreEqual(0, uint.MaxValue.PowerOf2Ceiling(), "uint");
            Assert.AreEqual(0, int.MaxValue.PowerOf2Ceiling(), "int");
            Assert.AreEqual(0, ulong.MaxValue.PowerOf2Ceiling(), "ulong");
            Assert.AreEqual(0, long.MaxValue.PowerOf2Ceiling(), "long");
            Assert.AreEqual(UInt128.Zero, UInt128.MaxValue.PowerOf2Ceiling(), "UInt128");
        }

        [Test]
        public void PowerOf2CeilingUnsignedByteShouldMatchWider() {
            byte v = byte.MinValue;
            while (true) {
                byte expected = v.PowerOf2Ceiling();
                Assert.AreEqual(expected, ((ushort)v).PowerOf2Ceiling(), "ushort=>" + v);
                Assert.AreEqual(expected, ((short)v).PowerOf2Ceiling(), "short=>" + v);
                Assert.AreEqual(expected, ((uint)v).PowerOf2Ceiling(), "uint=>" + v);
                Assert.AreEqual(expected, ((int)v).PowerOf2Ceiling(), "int=>" + v);
                Assert.AreEqual(expected, ((ulong)v).PowerOf2Ceiling(), "ulong=>" + v);
                Assert.AreEqual(expected, ((long)v).PowerOf2Ceiling(), "long=>" + v);
                Assert.AreEqual((UInt128)expected, ((UInt128)v).PowerOf2Ceiling(), "UInt128=>" + v);
                if (v == byte.MaxValue >> 1) break;
                v++;
            }
        }

        [Test]
        public void PowerOf2CeilingSignedByteShouldMatchWider() {
            sbyte v = sbyte.MinValue;
            while (true) {
                sbyte expected = v.PowerOf2Ceiling();
                Assert.AreEqual(expected, ((short)v).PowerOf2Ceiling(), "short=>" + v);
                Assert.AreEqual(expected, ((int)v).PowerOf2Ceiling(), "int=>" + v);
                Assert.AreEqual(expected, ((long)v).PowerOf2Ceiling(), "long=>" + v);
                if (v == sbyte.MaxValue >> 1) break;
                v++;
            }
        }
    }
}
