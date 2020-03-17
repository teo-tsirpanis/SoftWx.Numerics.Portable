// Copyright ©2015 SoftWx, Inc.
// Released under the MIT License the text of which appears at the end of this file.
// <authors> Steve Hatchett
using NUnit.Framework;

namespace SoftWx.Numerics.Tests {
    /// <summary></summary>
    [TestFixture]
    public class TestBitMath {

        [Test]
        public void LowBit0ShouldReturn0() {
            Assert.AreEqual(0, ((byte)0).LowBit(), "byte");
            Assert.AreEqual(0, ((sbyte)0).LowBit(), "sbyte");
            Assert.AreEqual(0, ((ushort)0).LowBit(), "ushort");
            Assert.AreEqual(0, ((short)0).LowBit(), "short");
            Assert.AreEqual(0, ((uint)0).LowBit(), "uint");
            Assert.AreEqual(0, 0.LowBit(), "int");
            Assert.AreEqual(0, ((ulong)0).LowBit(), "ulong");
            Assert.AreEqual(0, ((long)0).LowBit(), "long");
            Assert.AreEqual(UInt128.Zero, ((UInt128)0).LowBit(), "UInt128");
        }

        [Test]
        public void LowBit1ShouldReturn1() {
            Assert.AreEqual(1, ((byte)1).LowBit(), "byte");
            Assert.AreEqual(1, ((sbyte)1).LowBit(), "sbyte");
            Assert.AreEqual(1, ((ushort)1).LowBit(), "ushort");
            Assert.AreEqual(1, ((short)1).LowBit(), "short");
            Assert.AreEqual(1, ((uint)1).LowBit(), "uint");
            Assert.AreEqual(1, 1.LowBit(), "int");
            Assert.AreEqual(1, ((ulong)1).LowBit(), "ulong");
            Assert.AreEqual(1, ((long)1).LowBit(), "long");
            Assert.AreEqual(UInt128.One, ((UInt128)1).LowBit(), "UInt128");
        }

        [Test]
        public void LowBitAllBitsSetShouldReturn1() {
            Assert.AreEqual(1, ((byte)0xff).LowBit(), "byte");
            Assert.AreEqual(1, ((sbyte)-1).LowBit(), "sbyte");
            Assert.AreEqual(1, ((ushort)0xffff).LowBit(), "ushort");
            Assert.AreEqual(1, ((short)-1).LowBit(), "short");
            Assert.AreEqual(1, 0xffffffff.LowBit(), "uint");
            Assert.AreEqual(1, (-1).LowBit(), "int");
            Assert.AreEqual(1, 0xffffffffffffffff.LowBit(), "ulong");
            Assert.AreEqual(1, ((long)-1).LowBit(), "long");
            Assert.AreEqual(UInt128.One, UInt128.MaxValue.LowBit());
        }

        [Test]
        public void LowBitAllBitsMinus1ShouldReturn2() {
            Assert.AreEqual(2, ((byte)0xfe).LowBit(), "byte");
            Assert.AreEqual(2, ((sbyte)-2).LowBit(), "sbyte");
            Assert.AreEqual(2, ((ushort)0xfffe).LowBit(), "ushort");
            Assert.AreEqual(2, ((short)-2).LowBit(), "short");
            Assert.AreEqual(2, 0xfffffffe.LowBit(), "uint");
            Assert.AreEqual(2, (-2).LowBit(), "int");
            Assert.AreEqual(2, 0xfffffffffffffffe.LowBit(), "ulong");
            Assert.AreEqual(2, ((long)-2).LowBit(), "long");
            Assert.AreEqual((UInt128)2, new UInt128(0xffffffffffffffff, 0xfffffffffffffffe).LowBit(), "UInt128");
        }

        [Test]
        public void LowBitByteShouldMatchWider() {
            byte v = byte.MinValue;
            while(true) {
                byte expected = v.LowBit();
                Assert.AreEqual(unchecked((sbyte)expected), unchecked((sbyte)v).LowBit(), "sbyte");
                Assert.AreEqual((ushort)expected, ((ushort)v).LowBit(), "ushort");
                Assert.AreEqual((short)expected, ((short)v).LowBit(), "short");
                Assert.AreEqual((uint)expected, ((uint)v).LowBit(), "uint");
                Assert.AreEqual((int)expected, ((int)v).LowBit(), "int");
                Assert.AreEqual((ulong)expected, ((ulong)v).LowBit(), "ulong");
                Assert.AreEqual((long)expected, ((long)v).LowBit(), "long");
                Assert.AreEqual((UInt128)expected, ((UInt128)v).LowBit(), "UInt128");
                if (v == byte.MaxValue) break;
                v++;
            }
        }

        [Test]
        public void HighBit0ShouldReturn0() {
            Assert.AreEqual(0, ((byte)0).HighBit(), "byte");
            Assert.AreEqual(0, ((sbyte)0).HighBit(), "sbyte");
            Assert.AreEqual(0, ((ushort)0).HighBit(), "ushort");
            Assert.AreEqual(0, ((short)0).HighBit(), "short");
            Assert.AreEqual(0, ((uint)0).HighBit(), "uint");
            Assert.AreEqual(0, 0.HighBit(), "int");
            Assert.AreEqual(0, ((ulong)0).HighBit(), "ulong");
            Assert.AreEqual(0, ((long)0).HighBit(), "long");
            Assert.AreEqual(UInt128.Zero, ((UInt128)0).HighBit(), "UInt128");
        }

        [Test]
        public void HighBit1ShouldReturn1() {
            Assert.AreEqual(1, ((byte)1).HighBit(), "byte");
            Assert.AreEqual(1, ((sbyte)1).HighBit(), "sbyte");
            Assert.AreEqual(1, ((ushort)1).HighBit(), "ushort");
            Assert.AreEqual(1, ((short)1).HighBit(), "short");
            Assert.AreEqual(1, ((uint)1).HighBit(), "uint");
            Assert.AreEqual(1, 1.HighBit(), "int");
            Assert.AreEqual(1, ((ulong)1).HighBit(), "ulong");
            Assert.AreEqual(1, ((long)1).HighBit(), "long");
            Assert.AreEqual(UInt128.One, ((UInt128)1).HighBit(), "UInt128");
        }

        [Test]
        public void HighBitAllBitsSetShouldReturnMsb() {
            Assert.AreEqual(0x80, ((byte)0xff).HighBit(), "byte");
            Assert.AreEqual(sbyte.MinValue, ((sbyte)-1).HighBit(), "sbyte");
            Assert.AreEqual(0x8000, ((ushort)0xffff).HighBit(), "ushort");
            Assert.AreEqual(short.MinValue, ((short)-1).HighBit(), "short");
            Assert.AreEqual(0x80000000, 0xffffffff.HighBit(), "uint");
            Assert.AreEqual(int.MinValue, (-1).HighBit(), "int");
            Assert.AreEqual(0x8000000000000000, 0xffffffffffffffff.HighBit(), "ulong");
            Assert.AreEqual(long.MinValue, ((long)-1).HighBit(), "long");
            Assert.AreEqual(new UInt128(0x8000000000000000, 0), UInt128.MaxValue.HighBit());
        }

        [Test]
        public void HighBitByteShouldMatchWider() {
            byte v = byte.MinValue;
            while (true) {
                byte expected = v.HighBit();
                Assert.AreEqual(unchecked((sbyte)expected), unchecked((sbyte)v).HighBit(), "sbyte");
                Assert.AreEqual((ushort)expected, ((ushort)v).HighBit(), "ushort");
                Assert.AreEqual((short)expected, ((short)v).HighBit(), "short");
                Assert.AreEqual((uint)expected, ((uint)v).HighBit(), "uint");
                Assert.AreEqual((int)expected, ((int)v).HighBit(), "int");
                Assert.AreEqual((ulong)expected, ((ulong)v).HighBit(), "ulong");
                Assert.AreEqual((long)expected, ((long)v).HighBit(), "long");
                Assert.AreEqual((UInt128)expected, ((UInt128)v).HighBit(), "UInt128");
                if (v == byte.MaxValue) break;
                v++;
            }
        }

        [Test]
        public void LowBitPosition0ShouldReturnNeg1() {
            Assert.AreEqual(-1, ((byte)0).LowBitPosition(), "byte");
            Assert.AreEqual(-1, ((sbyte)0).LowBitPosition(), "sbyte");
            Assert.AreEqual(-1, ((ushort)0).LowBitPosition(), "ushort");
            Assert.AreEqual(-1, ((short)0).LowBitPosition(), "short");
            Assert.AreEqual(-1, ((uint)0).LowBitPosition(), "uint");
            Assert.AreEqual(-1, 0.LowBitPosition(), "int");
            Assert.AreEqual(-1, ((ulong)0).LowBitPosition(), "ulong");
            Assert.AreEqual(-1, ((long)0).LowBitPosition(), "long");
            Assert.AreEqual(-1, ((UInt128)0).LowBitPosition(), "UInt128");
        }

        [Test]
        public void LowBitPosition1ShouldReturn0() {
            Assert.AreEqual(0, ((byte)1).LowBitPosition(), "byte");
            Assert.AreEqual(0, ((sbyte)1).LowBitPosition(), "sbyte");
            Assert.AreEqual(0, ((ushort)1).LowBitPosition(), "ushort");
            Assert.AreEqual(0, ((short)1).LowBitPosition(), "short");
            Assert.AreEqual(0, ((uint)1).LowBitPosition(), "uint");
            Assert.AreEqual(0, 1.LowBitPosition(), "int");
            Assert.AreEqual(0, ((ulong)1).LowBitPosition(), "ulong");
            Assert.AreEqual(0, ((long)1).LowBitPosition(), "long");
            Assert.AreEqual(0, ((UInt128)1).LowBitPosition(), "UInt128");
        }

        [Test]
        public void LowBitPositionAllBitsSetShouldReturn0() {
            Assert.AreEqual(0, ((byte)0xff).LowBitPosition(), "byte");
            Assert.AreEqual(0, ((sbyte)-1).LowBitPosition(), "sbyte");
            Assert.AreEqual(0, ((ushort)0xffff).LowBitPosition(), "ushort");
            Assert.AreEqual(0, ((short)-1).LowBitPosition(), "short");
            Assert.AreEqual(0, 0xffffffff.LowBitPosition(), "uint");
            Assert.AreEqual(0, (-1).LowBitPosition(), "int");
            Assert.AreEqual(0, 0xffffffffffffffff.LowBitPosition(), "ulong");
            Assert.AreEqual(0, ((long)-1).LowBitPosition(), "long");
            Assert.AreEqual(0, UInt128.MaxValue.LowBitPosition());
        }

        [Test]
        public void LowBitPositionAllBitsMinus11ShouldReturn1() {
            Assert.AreEqual(1, ((byte)0xfe).LowBitPosition(), "byte");
            Assert.AreEqual(1, ((sbyte)-2).LowBitPosition(), "sbyte");
            Assert.AreEqual(1, ((ushort)0xfffe).LowBitPosition(), "ushort");
            Assert.AreEqual(1, ((short)-2).LowBitPosition(), "short");
            Assert.AreEqual(1, 0xfffffffe.LowBitPosition(), "uint");
            Assert.AreEqual(1, (-2).LowBitPosition(), "int");
            Assert.AreEqual(1, 0xfffffffffffffffe.LowBitPosition(), "ulong");
            Assert.AreEqual(1, ((long)-2).LowBitPosition(), "long");
            Assert.AreEqual(1, new UInt128(0xffffffffffffffff, 0xfffffffffffffffe).LowBitPosition(), "UInt128");
        }

        [Test]
        public void LowBitPositionByteShouldMatchWider() {
            byte v = byte.MinValue;
            while (true) {
                int expected = v.LowBitPosition();
                Assert.AreEqual(expected, unchecked((sbyte)v).LowBitPosition(), "sbyte");
                Assert.AreEqual(expected, ((ushort)v).LowBitPosition(), "short");
                Assert.AreEqual(expected, ((short)v).LowBitPosition(), "short");
                Assert.AreEqual(expected, ((uint)v).LowBitPosition(), "uint");
                Assert.AreEqual(expected, ((int)v).LowBitPosition(), "int");
                Assert.AreEqual(expected, ((ulong)v).LowBitPosition(), "ulong");
                Assert.AreEqual(expected, ((long)v).LowBitPosition(), "long");
                Assert.AreEqual(expected, ((UInt128)v).LowBitPosition(), "UInt128");
                if (v == byte.MaxValue) break;
                v++;
            }
        }

        [Test]
        public void HighBitPosition0ShouldReturnNeg1() {
            Assert.AreEqual(-1, ((byte)0).HighBitPosition(), "byte");
            Assert.AreEqual(-1, ((sbyte)0).HighBitPosition(), "sbyte");
            Assert.AreEqual(-1, ((ushort)0).HighBitPosition(), "ushort");
            Assert.AreEqual(-1, ((short)0).HighBitPosition(), "short");
            Assert.AreEqual(-1, ((uint)0).HighBitPosition(), "uint");
            Assert.AreEqual(-1, 0.HighBitPosition(), "int");
            Assert.AreEqual(-1, ((ulong)0).HighBitPosition(), "ulong");
            Assert.AreEqual(-1, ((long)0).HighBitPosition(), "long");
            Assert.AreEqual(-1, ((UInt128)0).HighBitPosition(), "UInt128");
        }

        [Test]
        public void HighBitPosition1ShouldReturn0() {
            Assert.AreEqual(0, ((byte)1).HighBitPosition(), "byte");
            Assert.AreEqual(0, ((sbyte)1).HighBitPosition(), "sbyte");
            Assert.AreEqual(0, ((ushort)1).HighBitPosition(), "ushort");
            Assert.AreEqual(0, ((short)1).HighBitPosition(), "short");
            Assert.AreEqual(0, ((uint)1).HighBitPosition(), "uint");
            Assert.AreEqual(0, 1.HighBitPosition(), "int");
            Assert.AreEqual(0, ((ulong)1).HighBitPosition(), "ulong");
            Assert.AreEqual(0, ((long)1).HighBitPosition(), "long");
            Assert.AreEqual(0, ((UInt128)1).HighBitPosition(), "UInt128");
        }

        [Test]
        public void HighBitPositionAllBitsSetShouldReturnMsbPos() {
            Assert.AreEqual(7, ((byte)0xff).HighBitPosition(), "byte");
            Assert.AreEqual(7, ((sbyte)-1).HighBitPosition(), "sbyte");
            Assert.AreEqual(15, ((ushort)0xffff).HighBitPosition(), "ushort");
            Assert.AreEqual(15, ((short)-1).HighBitPosition(), "short");
            Assert.AreEqual(31, 0xffffffff.HighBitPosition(), "uint");
            Assert.AreEqual(31, (-1).HighBitPosition(), "int");
            Assert.AreEqual(63, 0xffffffffffffffff.HighBitPosition(), "ulong");
            Assert.AreEqual(63, ((long)-1).HighBitPosition(), "long");
            Assert.AreEqual(127, new UInt128(0xffffffffffffffff, 0xffffffffffffffff).HighBitPosition(), "UInt128");
        }

        [Test]
        public void HighBitPositionByteShouldMatchWider() {
            byte v = byte.MinValue;
            while (true) {
                int expected = v.HighBitPosition();
                Assert.AreEqual(expected, unchecked((sbyte)v).HighBitPosition(), "sbyte");
                Assert.AreEqual(expected, ((ushort)v).HighBitPosition(), "ushort");
                Assert.AreEqual(expected, ((short)v).HighBitPosition(), "short");
                Assert.AreEqual(expected, ((uint)v).HighBitPosition(), "uint");
                Assert.AreEqual(expected, ((int)v).HighBitPosition(), "int");
                Assert.AreEqual(expected, ((ulong)v).HighBitPosition(), "ulong");
                Assert.AreEqual(expected, ((long)v).HighBitPosition(), "long");
                Assert.AreEqual(expected, ((UInt128)v).HighBitPosition(), "UInt128");
                if (v == byte.MaxValue) break;
                v++;
            }
        }

        [Test]
        public void TrailingZeroBits0ShouldReturnBitWidth() {
            Assert.AreEqual(8, ((byte)0).TrailingZeroBits(), "byte");
            Assert.AreEqual(8, ((sbyte)0).TrailingZeroBits(), "sbyte");
            Assert.AreEqual(16, ((ushort)0).TrailingZeroBits(), "ushort");
            Assert.AreEqual(16, ((short)0).TrailingZeroBits(), "short");
            Assert.AreEqual(32, ((uint)0).TrailingZeroBits(), "uint");
            Assert.AreEqual(32, 0.TrailingZeroBits(), "int");
            Assert.AreEqual(64, ((ulong)0).TrailingZeroBits(), "ulong");
            Assert.AreEqual(64, ((long)0).TrailingZeroBits(), "long");
            Assert.AreEqual(128, ((UInt128)0).TrailingZeroBits(), "UInt128");
        }

        [Test]
        public void TrailingZeroBits1ShouldReturn0() {
            Assert.AreEqual(0, ((byte)1).TrailingZeroBits(), "byte");
            Assert.AreEqual(0, ((sbyte)1).TrailingZeroBits(), "sbyte");
            Assert.AreEqual(0, ((ushort)1).TrailingZeroBits(), "ushort");
            Assert.AreEqual(0, ((short)1).TrailingZeroBits(), "short");
            Assert.AreEqual(0, ((uint)1).TrailingZeroBits(), "uint");
            Assert.AreEqual(0, 1.TrailingZeroBits(), "int");
            Assert.AreEqual(0, ((ulong)1).TrailingZeroBits(), "ulong");
            Assert.AreEqual(0, ((long)1).TrailingZeroBits(), "long");
            Assert.AreEqual(0, ((UInt128)1).TrailingZeroBits(), "UInt128");
        }

        [Test]
        public void TrailingZeroBitsAllBitsSetShouldReturn0() {
            Assert.AreEqual(0, ((byte)0xff).TrailingZeroBits(), "byte");
            Assert.AreEqual(0, ((sbyte)-1).TrailingZeroBits(), "sbyte");
            Assert.AreEqual(0, ((ushort)0xffff).TrailingZeroBits(), "ushort");
            Assert.AreEqual(0, ((short)-1).TrailingZeroBits(), "short");
            Assert.AreEqual(0, 0xffffffff.TrailingZeroBits(), "uint");
            Assert.AreEqual(0, (-1).TrailingZeroBits(), "int");
            Assert.AreEqual(0, 0xffffffffffffffff.TrailingZeroBits(), "ulong");
            Assert.AreEqual(0, ((long)-1).TrailingZeroBits(), "long");
            Assert.AreEqual(0, new UInt128(0xffffffffffffffff, 0xffffffffffffffff).TrailingZeroBits(), "UInt128");
        }

        [Test]
        public void TrailingZeroBitsAllBitsMinus1ShouldReturn1() {
            Assert.AreEqual(1, ((byte)0xfe).TrailingZeroBits(), "byte");
            Assert.AreEqual(1, ((sbyte)-2).TrailingZeroBits(), "sbyte");
            Assert.AreEqual(1, ((ushort)0xfffe).TrailingZeroBits(), "ushort");
            Assert.AreEqual(1, ((short)-2).TrailingZeroBits(), "short");
            Assert.AreEqual(1, 0xfffffffe.TrailingZeroBits(), "uint");
            Assert.AreEqual(1, (-2).TrailingZeroBits(), "int");
            Assert.AreEqual(1, 0xfffffffffffffffe.TrailingZeroBits(), "ulong");
            Assert.AreEqual(1, ((long)-2).TrailingZeroBits(), "long");
            Assert.AreEqual(1, new UInt128(0xffffffffffffffff, 0xfffffffffffffffe).TrailingZeroBits(), "UInt128");
        }

        [Test]
        public void TrailingZeroBitsByteShouldMatchWider() {
            byte v = byte.MinValue;
            while (true) {
                Assert.AreEqual(v.TrailingZeroBits(), unchecked((sbyte)v).TrailingZeroBits(), "sbyte");
                if (v == 0) {
                    v++;
                    continue; // the result for 0 is type dependant
                }
                Assert.AreEqual(v.TrailingZeroBits(), ((ushort)v).TrailingZeroBits(), "short");
                Assert.AreEqual(v.TrailingZeroBits(), ((short)v).TrailingZeroBits(), "short");
                Assert.AreEqual(v.TrailingZeroBits(), ((uint)v).TrailingZeroBits(), "uint");
                Assert.AreEqual(v.TrailingZeroBits(), ((int)v).TrailingZeroBits(), "int");
                Assert.AreEqual(v.TrailingZeroBits(), ((ulong)v).TrailingZeroBits(), "ulong");
                Assert.AreEqual(v.TrailingZeroBits(), ((long)v).TrailingZeroBits(), "long");
                Assert.AreEqual(v.TrailingZeroBits(), ((UInt128)v).TrailingZeroBits(), "UInt128");
                if (v == byte.MaxValue) break;
                v++;
            }
        }

        [Test]
        public void LeadingZeroBits0ShouldReturnBitWidth() {
            Assert.AreEqual(8, ((byte)0).LeadingZeroBits(), "byte");
            Assert.AreEqual(8, ((sbyte)0).LeadingZeroBits(), "sbyte");
            Assert.AreEqual(16, ((ushort)0).LeadingZeroBits(), "ushort");
            Assert.AreEqual(16, ((short)0).LeadingZeroBits(), "short");
            Assert.AreEqual(32, ((uint)0).LeadingZeroBits(), "uint");
            Assert.AreEqual(32, 0.LeadingZeroBits(), "int");
            Assert.AreEqual(64, ((ulong)0).LeadingZeroBits(), "ulong");
            Assert.AreEqual(64, ((long)0).LeadingZeroBits(), "long");
            Assert.AreEqual(128, ((UInt128)0).LeadingZeroBits(), "UInt128");
        }

        [Test]
        public void LeadingZeroBits1ShouldReturnBitWidthLessOne() {
            Assert.AreEqual(7, ((byte)1).LeadingZeroBits(), "byte");
            Assert.AreEqual(7, ((sbyte)1).LeadingZeroBits(), "sbyte");
            Assert.AreEqual(15, ((ushort)1).LeadingZeroBits(), "ushort");
            Assert.AreEqual(15, ((short)1).LeadingZeroBits(), "short");
            Assert.AreEqual(31, ((uint)1).LeadingZeroBits(), "uint");
            Assert.AreEqual(31, 1.LeadingZeroBits(), "int");
            Assert.AreEqual(63, ((ulong)1).LeadingZeroBits(), "ulong");
            Assert.AreEqual(63, ((long)1).LeadingZeroBits(), "long");
            Assert.AreEqual(127, ((UInt128)1).LeadingZeroBits(), "UInt128");
        }

        [Test]
        public void LeadingZeroBitsAllBitsSetShouldReturn0() {
            Assert.AreEqual(0, ((byte)0xff).LeadingZeroBits(), "byte");
            Assert.AreEqual(0, ((sbyte)-1).LeadingZeroBits(), "sbyte");
            Assert.AreEqual(0, ((ushort)0xffff).LeadingZeroBits(), "ushort");
            Assert.AreEqual(0, ((short)-1).LeadingZeroBits(), "short");
            Assert.AreEqual(0, 0xffffffff.LeadingZeroBits(), "uint");
            Assert.AreEqual(0, (-1).LeadingZeroBits(), "int");
            Assert.AreEqual(0, 0xffffffffffffffff.LeadingZeroBits(), "ulong");
            Assert.AreEqual(0, ((long)-1).LeadingZeroBits(), "long");
            Assert.AreEqual(0, new UInt128(0xffffffffffffffff, 0xffffffffffffffff).LeadingZeroBits(), "UInt128");
        }

        [Test]
        public void SignificantBits0ShouldReturn0() {
            Assert.AreEqual(0, ((byte)0).SignificantBits(), "byte");
            Assert.AreEqual(0, ((sbyte)0).SignificantBits(), "sbyte");
            Assert.AreEqual(0, ((ushort)0).SignificantBits(), "ushort");
            Assert.AreEqual(0, ((short)0).SignificantBits(), "short");
            Assert.AreEqual(0, ((uint)0).SignificantBits(), "uint");
            Assert.AreEqual(0, 0.SignificantBits(), "int");
            Assert.AreEqual(0, ((ulong)0).SignificantBits(), "ulong");
            Assert.AreEqual(0, ((long)0).SignificantBits(), "long");
            Assert.AreEqual(0, ((UInt128)0).SignificantBits(), "UInt128");
        }

        [Test]
        public void SignificantBits1ShouldReturn1() {
            Assert.AreEqual(1, ((byte)1).SignificantBits(), "byte");
            Assert.AreEqual(1, ((sbyte)1).SignificantBits(), "sbyte");
            Assert.AreEqual(1, ((ushort)1).SignificantBits(), "ushort");
            Assert.AreEqual(1, ((short)1).SignificantBits(), "short");
            Assert.AreEqual(1, ((uint)1).SignificantBits(), "uint");
            Assert.AreEqual(1, 1.SignificantBits(), "int");
            Assert.AreEqual(1, ((ulong)1).SignificantBits(), "ulong");
            Assert.AreEqual(1, ((long)1).SignificantBits(), "long");
            Assert.AreEqual(1, ((UInt128)1).SignificantBits(), "UInt128");
        }

        [Test]
        public void SignificantBitsAllBitsSetShouldReturnBitWidth() {
            Assert.AreEqual(8, ((byte)0xff).SignificantBits(), "byte");
            Assert.AreEqual(8, ((sbyte)-1).SignificantBits(), "sbyte");
            Assert.AreEqual(16, ((ushort)0xffff).SignificantBits(), "ushort");
            Assert.AreEqual(16, ((short)-1).SignificantBits(), "short");
            Assert.AreEqual(32, 0xffffffff.SignificantBits(), "uint");
            Assert.AreEqual(32, (-1).SignificantBits(), "int");
            Assert.AreEqual(64, 0xffffffffffffffff.SignificantBits(), "ulong");
            Assert.AreEqual(64, ((long)-1).SignificantBits(), "long");
            Assert.AreEqual(128, new UInt128(0xffffffffffffffff, 0xffffffffffffffff).SignificantBits(), "UInt128");
        }

        [Test]
        public void SignificantBitsByteShouldMatchWider() {
            byte v = byte.MinValue;
            while (true) {
                int expected = v.SignificantBits();
                Assert.AreEqual(expected, unchecked((sbyte)v).SignificantBits(), "sbyte");
                Assert.AreEqual(expected, ((ushort)v).SignificantBits(), "ushort");
                Assert.AreEqual(expected, ((short)v).SignificantBits(), "short");
                Assert.AreEqual(expected, ((uint)v).SignificantBits(), "uint");
                Assert.AreEqual(expected, ((int)v).SignificantBits(), "int");
                Assert.AreEqual(expected, ((ulong)v).SignificantBits(), "ulong");
                Assert.AreEqual(expected, ((long)v).SignificantBits(), "long");
                Assert.AreEqual(expected, ((UInt128)v).SignificantBits(), "UInt128");
                if (v == byte.MaxValue) break;
                v++;
            }
        }

        [Test]
        public void BitCount0ShouldReturn0() {
            Assert.AreEqual(0, ((byte)0).BitCount(), "byte");
            Assert.AreEqual(0, ((sbyte)0).BitCount(), "sbyte");
            Assert.AreEqual(0, ((ushort)0).BitCount(), "ushort");
            Assert.AreEqual(0, ((short)0).BitCount(), "short");
            Assert.AreEqual(0, ((uint)0).BitCount(), "uint");
            Assert.AreEqual(0, 0.BitCount(), "int");
            Assert.AreEqual(0, ((ulong)0).BitCount(), "ulong");
            Assert.AreEqual(0, ((long)0).BitCount(), "long");
            Assert.AreEqual(0, ((UInt128)0).BitCount(), "UInt128");
        }

        [Test]
        public void BitCount1ShouldReturn1() {
            Assert.AreEqual(1, ((byte)1).BitCount(), "byte");
            Assert.AreEqual(1, ((sbyte)1).BitCount(), "sbyte");
            Assert.AreEqual(1, ((ushort)1).BitCount(), "ushort");
            Assert.AreEqual(1, ((short)1).BitCount(), "short");
            Assert.AreEqual(1, ((uint)1).BitCount(), "uint");
            Assert.AreEqual(1, 1.BitCount(), "int");
            Assert.AreEqual(1, ((ulong)1).BitCount(), "ulong");
            Assert.AreEqual(1, ((long)1).BitCount(), "long");
            Assert.AreEqual(1, ((UInt128)1).BitCount(), "UInt128");
        }

        [Test]
        public void BitCountAllBitsSetShouldReturnBitWidth() {
            Assert.AreEqual(8, ((byte)0xff).BitCount(), "byte");
            Assert.AreEqual(8, ((sbyte)-1).BitCount(), "sbyte");
            Assert.AreEqual(16, ((ushort)0xffff).BitCount(), "ushort");
            Assert.AreEqual(16, ((short)-1).BitCount(), "short");
            Assert.AreEqual(32, 0xffffffff.BitCount(), "uint");
            Assert.AreEqual(32, (-1).BitCount(), "int");
            Assert.AreEqual(64, 0xffffffffffffffff.BitCount(), "ulong");
            Assert.AreEqual(64, ((long)-1).BitCount(), "long");
            Assert.AreEqual(128, new UInt128(0xffffffffffffffff, 0xffffffffffffffff).BitCount(), "UInt128");
        }

        [Test]
        public void BitCountMsbSetShouldReturn1() {
            Assert.AreEqual(1, ((byte)0x80).BitCount(), "byte");
            Assert.AreEqual(1, sbyte.MinValue.BitCount(), "sbyte");
            Assert.AreEqual(1, ((ushort)0x8000).BitCount(), "ushort");
            Assert.AreEqual(1, short.MinValue.BitCount(), "short");
            Assert.AreEqual(1, 0x80000000.BitCount(), "uint");
            Assert.AreEqual(1, int.MinValue.BitCount(), "int");
            Assert.AreEqual(1, 0x8000000000000000.BitCount(), "ulong");
            Assert.AreEqual(1, long.MinValue.BitCount(), "long");
            Assert.AreEqual(1, new UInt128(0x8000000000000000, 0).BitCount(), "UInt128");
        }

        [Test]
        public void BitCountByteShouldMatchWider() {
            byte v = byte.MinValue;
            while (true) {
                Assert.AreEqual(v.BitCount(), unchecked((sbyte)v).BitCount(), "sbyte");
                Assert.AreEqual(v.BitCount(), ((ushort)v).BitCount(), "ushort");
                Assert.AreEqual(v.BitCount(), ((short)v).BitCount(), "short");
                Assert.AreEqual(v.BitCount(), ((uint)v).BitCount(), "uint");
                Assert.AreEqual(v.BitCount(), ((int)v).BitCount(), "int");
                Assert.AreEqual(v.BitCount(), ((ulong)v).BitCount(), "ulong");
                Assert.AreEqual(v.BitCount(), ((long)v).BitCount(), "long");
                Assert.AreEqual(v.BitCount(), ((UInt128)v).BitCount(), "UInt128");
                if (v == byte.MaxValue) break;
                v++;
            }
        }

        [Test]
        public void ReverseBits0ShouldReturn0() {
            Assert.AreEqual(0, ((byte)0).ReverseBits(), "byte");
            Assert.AreEqual(0, ((sbyte)0).ReverseBits(), "sbyte");
            Assert.AreEqual(0, ((ushort)0).ReverseBits(), "ushort");
            Assert.AreEqual(0, ((short)0).ReverseBits(), "short");
            Assert.AreEqual(0, ((uint)0).ReverseBits(), "uint");
            Assert.AreEqual(0, 0.ReverseBits(), "int");
            Assert.AreEqual(0, ((ulong)0).ReverseBits(), "ulong");
            Assert.AreEqual(0, ((long)0).ReverseBits(), "long");
            Assert.AreEqual(UInt128.Zero, ((UInt128)0).ReverseBits(), "UInt128");
        }

        [Test]
        public void ReverseBits1ShouldReturnMsb() {
            Assert.AreEqual(0x80, ((byte)1).ReverseBits(), "byte");
            Assert.AreEqual(sbyte.MinValue, ((sbyte)1).ReverseBits(), "sbyte");
            Assert.AreEqual(0x8000, ((ushort)1).ReverseBits(), "ushort");
            Assert.AreEqual(short.MinValue, ((short)1).ReverseBits(), "short");
            Assert.AreEqual(0x80000000, ((uint)1).ReverseBits(), "uint");
            Assert.AreEqual(int.MinValue, 1.ReverseBits(), "int");
            Assert.AreEqual(0x8000000000000000, ((ulong)1).ReverseBits(), "ulong");
            Assert.AreEqual(long.MinValue, ((long)1).ReverseBits(), "long");
            Assert.AreEqual(new UInt128(0x8000000000000000, 0), ((UInt128)1).ReverseBits(), "UInt128");
        }

        [Test]
        public void ReverseBitsAllBitsSetShouldReturnAllBitsSet() {
            Assert.AreEqual(0xff, ((byte)0xff).ReverseBits(), "byte");
            Assert.AreEqual(-1, ((sbyte)-1).ReverseBits(), "sbyte");
            Assert.AreEqual(0xffff, ((ushort)0xffff).ReverseBits(), "ushort");
            Assert.AreEqual(-1, ((short)-1).ReverseBits(), "short");
            Assert.AreEqual(0xffffffff, 0xffffffff.ReverseBits(), "uint");
            Assert.AreEqual(-1, (-1).ReverseBits(), "int");
            Assert.AreEqual(0xffffffffffffffff, 0xffffffffffffffff.ReverseBits(), "ulong");
            Assert.AreEqual(-1, ((long)-1).ReverseBits(), "long");
            Assert.AreEqual(new UInt128(0xffffffffffffffff, 0xffffffffffffffff), new UInt128(0xffffffffffffffff, 0xffffffffffffffff).ReverseBits(), "UInt128");
        }

        [Test]
        public void ReverseBitsMsbSetShouldReturn1() {
            Assert.AreEqual(1, ((byte)0x80).ReverseBits(), "byte");
            Assert.AreEqual(1, sbyte.MinValue.ReverseBits(), "sbyte");
            Assert.AreEqual(1, ((ushort)0x8000).ReverseBits(), "ushort");
            Assert.AreEqual(1, short.MinValue.ReverseBits(), "short");
            Assert.AreEqual(1, 0x80000000.ReverseBits(), "uint");
            Assert.AreEqual(1, int.MinValue.ReverseBits(), "int");
            Assert.AreEqual(1, 0x8000000000000000.ReverseBits(), "ulong");
            Assert.AreEqual(1, long.MinValue.ReverseBits(), "long");
            Assert.AreEqual(UInt128.One, new UInt128(0x8000000000000000,0).ReverseBits(), "UInt128");
        }

        [FsCheck.NUnit.Property(Arbitrary = new[] {typeof(UInt128Generator)})]
        public bool ReverseBitsByteTwiceShouldReturnOriginal(UInt128 x) => x.ReverseBits().ReverseBits() == x;
    }
}