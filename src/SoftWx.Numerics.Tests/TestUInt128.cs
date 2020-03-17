// Copyright ©2016 SoftWx, Inc.
// Released under the MIT License the text of which appears at the end of this file.
// <authors> Steve Hatchett

using FsCheck;
using NUnit.Framework;

// ReSharper disable EqualExpressionComparison

namespace SoftWx.Numerics.Tests
{
    public class UInt128Generator
    {
        [JetBrains.Annotations.UsedImplicitly]
        public static Arbitrary<UInt128> GenerateUInt128() => Arb.Generate<ulong>()
            .Two()
            .Select(x => new UInt128(x.Item1, x.Item2))
            .ToArbitrary();
    }

    /// <summary></summary>
    [TestFixture]
    public class TestUInt128
    {
        [FsCheck.NUnit.Property(Arbitrary = new[] {typeof(UInt128Generator)})]
        public bool AdditionIsCommutative(UInt128 x1, UInt128 x2) => x1 + x2 == x2 + x1;

        [FsCheck.NUnit.Property(Arbitrary = new[] {typeof(UInt128Generator)})]
        public bool AdditionIsAssociative(UInt128 x1, UInt128 x2, UInt128 x3) => (x1 + x2) + x3 == x1 + (x2 + x3);

        [FsCheck.NUnit.Property(Arbitrary = new[] {typeof(UInt128Generator)})]
        public bool AdditionHasZeroAsIdentityElement(UInt128 x) => x + UInt128.Zero == x;

        [FsCheck.NUnit.Property(Arbitrary = new[] {typeof(UInt128Generator)})]
        public bool MultiplicationIsCommutative(UInt128 x1, UInt128 x2) => x1 * x2 == x2 * x1;

        [FsCheck.NUnit.Property(Arbitrary = new[] {typeof(UInt128Generator)})]
        public bool MultiplicationIsAssociative(UInt128 x1, UInt128 x2, UInt128 x3) => (x1 * x2) * x3 == x1 * (x2 * x3);

        [FsCheck.NUnit.Property(Arbitrary = new[] {typeof(UInt128Generator)})]
        public bool MultiplicationIsDistributiveOverAddition(UInt128 x1, UInt128 x2, UInt128 x3) =>
            (x1 + x2) * x3 == x1 * x3 + x2 * x3;

        [FsCheck.NUnit.Property(Arbitrary = new[] {typeof(UInt128Generator)})]
        public bool MultiplicationHasOneAsIdentityElement(UInt128 x) => x * UInt128.One == x;

        [FsCheck.NUnit.Property(Arbitrary = new[] {typeof(UInt128Generator)})]
        public bool MultiplicationHasZeroAsAnnihilatingElement(UInt128 x) => x * UInt128.Zero == UInt128.Zero;

        [FsCheck.NUnit.Property(Arbitrary = new[] {typeof(UInt128Generator)})]
        public bool TwosComplementShouldWork(UInt128 x) => -x + x == UInt128.Zero;

        [Test]
        public void MultiplyMax()
        {
            Assert.AreEqual(new UInt128(ulong.MaxValue << 1, 1), UInt128.Multiply(ulong.MaxValue, ulong.MaxValue));
        }

        [FsCheck.NUnit.Property(Arbitrary = new[] {typeof(UInt128Generator)})]
        public bool SquareShouldEqualMultiplySame(ulong x) => UInt128.Square(x) == UInt128.Multiply(x, x);

        [Test]
        public void RoundTripUlongCastShouldReturnOriginal()
        {
            const ulong expected = ulong.MaxValue / 3;
            UInt128 intermediate = expected;
            var final = (ulong) intermediate;
            Assert.AreEqual(expected, final);
        }

        [Test]
        public void EqualOperatorUInt128ToUInt128()
        {
            Assert.IsTrue(UInt128.Zero == new UInt128(0, 0));
            Assert.IsTrue(UInt128.One == new UInt128(0, 1));
            Assert.IsTrue(new UInt128(1234, 5678) == new UInt128(1234, 5678));
            Assert.IsFalse(UInt128.Zero == UInt128.One);
        }

        [FsCheck.NUnit.Property(Arbitrary = new[] {typeof(UInt128Generator)})]
        public bool EqualOperatorUInt128ToULong(ulong x) => new UInt128(0, x) == x;

        [Test]
        public void NotEqualOperatorUInt128ToUInt128()
        {
            Assert.IsTrue(UInt128.Zero != new UInt128(1, 1));
            Assert.IsTrue(UInt128.One != new UInt128(1, 0));
            Assert.IsTrue(new UInt128(1234, 5678) != new UInt128(1230, 5678));
            Assert.IsFalse(UInt128.Zero != new UInt128(0, 0));
        }

        [Test]
        public void NotEqualOperatorUInt128ToULong()
        {
            Assert.IsTrue(UInt128.Zero != 1UL);
            Assert.IsTrue(UInt128.One != 0UL);
            Assert.IsTrue(0UL != UInt128.One);
            Assert.IsTrue(new UInt128(1234, 5678) != 5678UL);
            Assert.IsFalse(UInt128.Zero != 0UL);
        }

        [Test]
        public void GreaterThanOperator()
        {
            Assert.IsTrue(UInt128.One > UInt128.Zero);
            Assert.IsTrue(new UInt128(123, 456) > new UInt128(123, 450));
            Assert.IsTrue(new UInt128(123, 456) > new UInt128(122, 999));
            Assert.IsFalse(UInt128.One > new UInt128(0, 1));
            Assert.IsFalse(UInt128.Zero > UInt128.One);
        }

        [Test]
        public void GreaterThanOrEqualOperator()
        {
            Assert.IsTrue(UInt128.One >= UInt128.Zero);
            Assert.IsTrue(new UInt128(123, 456) >= new UInt128(123, 450));
            Assert.IsTrue(new UInt128(123, 456) >= new UInt128(122, 999));
            Assert.IsTrue(new UInt128(123, 456) >= new UInt128(123, 456));
            Assert.IsTrue(UInt128.One >= new UInt128(0, 1));
            Assert.IsFalse(UInt128.Zero >= UInt128.One);
        }

        [Test]
        public void LessThanOperator()
        {
            Assert.IsTrue(UInt128.Zero < UInt128.One);
            Assert.IsTrue(new UInt128(123, 450) < new UInt128(123, 456));
            Assert.IsTrue(new UInt128(122, 999) < new UInt128(123, 456));
            Assert.IsFalse(UInt128.One < new UInt128(0, 1));
            Assert.IsFalse(UInt128.One < UInt128.Zero);
        }

        [Test]
        public void LessThanOrEqualOperator()
        {
            Assert.IsTrue(UInt128.Zero <= UInt128.One);
            Assert.IsTrue(new UInt128(123, 450) <= new UInt128(123, 456));
            Assert.IsTrue(new UInt128(122, 999) <= new UInt128(123, 456));
            Assert.IsTrue(new UInt128(123, 456) <= new UInt128(123, 456));
            Assert.IsTrue(UInt128.One <= new UInt128(0, 1));
            Assert.IsFalse(UInt128.One <= UInt128.Zero);
        }

        [Test]
        public void AddOperatorShouldHandleULongOverflow()
        {
            var expected = new UInt128(1, 9);
            var actual = new UInt128(0, ulong.MaxValue) + new UInt128(0, 10);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void AddOperatorShouldHandleOverflow()
        {
            var expected = new UInt128(0, 9);
            var actual = new UInt128(ulong.MaxValue, ulong.MaxValue) + new UInt128(0, 10);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SubtractOperatorShouldHandleULongUnderflow()
        {
            var expected = new UInt128(0, ulong.MaxValue);
            var actual = new UInt128(1, 9) - new UInt128(0, 10);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SubtractOperatorShouldHandleUnderflow()
        {
            var expected = new UInt128(ulong.MaxValue, ulong.MaxValue);
            var actual = new UInt128(0, 9) - new UInt128(0, 10);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void MultiplyOperatorShouldReturn0With0()
        {
            var expected = UInt128.Zero;
            var actual = new UInt128(0, 0) * UInt128.MaxValue;
            Assert.AreEqual(expected, actual);
            actual = new UInt128(0, 0) * UInt128.Zero;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void MultiplyOperatorShouldReturnSameWith1()
        {
            var expected = UInt128.MaxValue;
            var actual = new UInt128(0, 1) * UInt128.MaxValue;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void MultiplyOperatorShouldHandleOverflow()
        {
            var expected = UInt128.One;
            var actual = UInt128.MaxValue * UInt128.MaxValue;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void DivideOperatorShouldReturnSameWith1()
        {
            var expected = UInt128.MaxValue;
            var actual = UInt128.MaxValue / new UInt128(0, 1);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void DivideOperatorShouldReturn1WhenDivideBySelf()
        {
            var expected = UInt128.One;
            var actual = UInt128.MaxValue / UInt128.MaxValue;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void DivideOperatorShouldReturn0WithLargerDenominator()
        {
            var expected = UInt128.Zero;
            var actual = (UInt128.MaxValue - 1) / UInt128.MaxValue;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void DivideOperatorShouldReturn2WithShiftedNumerator()
        {
            var expected = new UInt128(0, 2);
            var actual = (new UInt128(0, ulong.MaxValue) << 1) / new UInt128(0, ulong.MaxValue);
            Assert.AreEqual(expected, actual);
        }
    }
}