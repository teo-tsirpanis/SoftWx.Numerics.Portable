
// Released under the MIT License the text of which appears at the end of this file.
// <authors> Steve Hatchett
using NUnit.Framework;

namespace SoftWx.Numerics.Tests {
    /// <summary></summary>
    [TestFixture]
    public class TestPrimeMath {
        [Test]
        public void Gcd0And0ShouldReturn0() {
            Assert.AreEqual(0, ((uint)0).Gcd(0), "uint");
            Assert.AreEqual(0, 0.Gcd(0), "int");
            Assert.AreEqual(0, ((ulong)0).Gcd(0), "ulong");
            Assert.AreEqual(0, ((long)0).Gcd(0), "long");
            Assert.AreEqual(UInt128.Zero, ((UInt128)0).Gcd(0), "UInt128");
        }

        [Test]
        public void GcdNon0And0ShouldReturnNon0() {
            Assert.AreEqual(9, ((uint)0).Gcd(9), "uint");
            Assert.AreEqual(9, 0.Gcd(9), "int");
            Assert.AreEqual(9, ((ulong)0).Gcd(9), "ulong");
            Assert.AreEqual(9, ((long)0).Gcd(9), "long");
            Assert.AreEqual(9, ((uint)9).Gcd(0), "uint");
            Assert.AreEqual(9, 9.Gcd(0), "int");
            Assert.AreEqual(9, ((ulong)9).Gcd(0), "ulong");
            Assert.AreEqual(9, ((long)9).Gcd(0), "long");
            Assert.AreEqual((UInt128)9, ((UInt128)9).Gcd(0), "UInt128");
        }

        [Test]
        public void GcdTwoPrimesShouldReturn1() {
            Assert.AreEqual(1, ((uint)23).Gcd(101), "uint");
            Assert.AreEqual(1, 23.Gcd(101), "int");
            Assert.AreEqual(1, ((ulong)23).Gcd(101), "ulong");
            Assert.AreEqual(1, ((long)23).Gcd(101), "long");
            Assert.AreEqual(UInt128.One, ((UInt128)23).Gcd(101), "UInt128");
        }

        [Test]
        public void GcdNegativesShouldReturnSameAsPositive() {
            Assert.AreEqual(20, 100.Gcd(80), "int pos");
            Assert.AreEqual(20, ((long)100).Gcd(80), "long pos");
            Assert.AreEqual(20, (-100).Gcd(-80), "int neg");
            Assert.AreEqual(20, ((long)-100).Gcd(-80), "long neg");
            Assert.AreEqual(20, 100.Gcd(-80), "int pos neg");
            Assert.AreEqual(20, ((long)100).Gcd(-80), "long pos neg");
            Assert.AreEqual(20, (-100).Gcd(80), "int neg pos");
            Assert.AreEqual(20, ((long)-100).Gcd(80), "long neg pos");
        }

        [Test]
        public void IsCoPrime0And0ShouldReturnFalse() {
            Assert.AreEqual(false, ((uint)0).IsCoprime(0), "uint");
            Assert.AreEqual(false, 0.IsCoprime(0), "int");
            Assert.AreEqual(false, ((ulong)0).IsCoprime(0), "ulong");
            Assert.AreEqual(false, ((long)0).IsCoprime(0), "long");
            Assert.AreEqual(false, ((UInt128)0).IsCoprime(0), "UInt128");
        }

        [Test]
        public void IsCoPrime0And1ShouldReturnTrue() {
            Assert.AreEqual(true, ((uint)0).IsCoprime(1), "uint");
            Assert.AreEqual(true, 0.IsCoprime(1), "int");
            Assert.AreEqual(true, ((ulong)0).IsCoprime(1), "ulong");
            Assert.AreEqual(true, ((long)0).IsCoprime(1), "long");
            Assert.AreEqual(true, ((UInt128)0).IsCoprime(1), "UInt128");
        }

        [Test]
        public void IsCoPrime0AndNeg1ShouldReturnTrue() {
            Assert.AreEqual(true, 0.IsCoprime(-1), "int");
            Assert.AreEqual(true, ((long)0).IsCoprime(-1), "long");
        }

        [Test]
        public void IsCoPrime1And1ShouldReturnTrue() {
            // 1/-1 are the only number coprime to itself
            Assert.AreEqual(true, ((uint)1).IsCoprime(1), "uint");
            Assert.AreEqual(true, 1.IsCoprime(1), "int");
            Assert.AreEqual(true, ((ulong)1).IsCoprime(1), "ulong");
            Assert.AreEqual(true, ((long)1).IsCoprime(1), "long");
            Assert.AreEqual(true, ((UInt128)1).IsCoprime(1), "UInt128");
        }

        [Test]
        public void IsCoPrime1AndNeg1ShouldReturnTrue() {
            // 1/-1 are the only number coprime to itself
            Assert.AreEqual(true, 1.IsCoprime(-1), "int");
            Assert.AreEqual(true, ((long)1).IsCoprime(-1), "long");
        }

        [Test]
        public void IsCoPrimeNeg1AndNeg1ShouldReturnTrue() {
            // 1/-1 are the only number coprime to itself
            Assert.AreEqual(true, (-1).IsCoprime(-1), "int");
            Assert.AreEqual(true, ((long)-1).IsCoprime(-1), "long");
        }

        [Test]
        public void IsCoPrimeEqualNon1ValuesShouldReturnFalse() {
            Assert.AreEqual(false, ((uint)11).IsCoprime(11), "uint");
            Assert.AreEqual(false, 11.IsCoprime(11), "int");
            Assert.AreEqual(false, ((ulong)11).IsCoprime(11), "ulong");
            Assert.AreEqual(false, ((long)11).IsCoprime(11), "long");
            Assert.AreEqual(false, ((UInt128)11).IsCoprime(11), "UInt128");
        }

        [Test]
        public void IsCoPrimeTwoPrimesShouldReturnTrue() {
            Assert.AreEqual(true, ((uint)11).IsCoprime(13), "uint");
            Assert.AreEqual(true, 11.IsCoprime(13), "int");
            Assert.AreEqual(true, ((ulong)11).IsCoprime(13), "ulong");
            Assert.AreEqual(true, ((long)11).IsCoprime(13), "long");
            Assert.AreEqual(true, ((UInt128)11).IsCoprime(13), "UInt128");
        }

        [Test]
        public void IsCoPrimeTwoEvenValuesShouldReturnFalse() {
            Assert.AreEqual(false, ((uint)10).IsCoprime(12), "uint");
            Assert.AreEqual(false, 10.IsCoprime(12), "int");
            Assert.AreEqual(false, ((ulong)10).IsCoprime(12), "ulong");
            Assert.AreEqual(false, ((long)10).IsCoprime(12), "long");
            Assert.AreEqual(false, ((UInt128)10).IsCoprime(12), "UInt128");
        }

        [Test]
        public void IsCoPrimeEqualNegValuesShouldReturnTrue() {
            Assert.AreEqual(false, (-11).IsCoprime(-11), "int");
            Assert.AreEqual(false, ((long)-11).IsCoprime(-11), "long");
        }

        [Test]
        public void IsCoPrimeEqualButOppositeSignValuesShouldReturnTrue() {
            Assert.AreEqual(false, (-11).IsCoprime(11), "int");
            Assert.AreEqual(false, ((long)-11).IsCoprime(11), "long");
        }

        [Test]
        public void IsCoPrimeSwappedArgOrderShouldReturnSameResult() {
            for (int i = 10; i < 20; i++) {
                for (int j = 20; j < 30; j++) {
                    Assert.AreEqual(((uint)i).IsCoprime((uint)j), ((uint)j).IsCoprime((uint)i), "uint");
                    Assert.AreEqual(i.IsCoprime(j), j.IsCoprime(i), "int");
                    Assert.AreEqual(((ulong)i).IsCoprime((ulong)j), ((ulong)j).IsCoprime((ulong)i), "ulong");
                    Assert.AreEqual(((long)i).IsCoprime(j), ((long)j).IsCoprime(i), "long");
                    Assert.AreEqual(((UInt128)(ulong)i).IsCoprime((ulong)j), ((UInt128)(ulong)j).IsCoprime((ulong)i), "UInt128");
                }
            }
        }

        [Test]
        public void NearestCoprimeFloor0And0UnsignedShouldReturn0() {
            Assert.AreEqual(0, ((uint)0).NearestCoprimeFloor(0), "uint");
            Assert.AreEqual(0, ((ulong)0).NearestCoprimeFloor(0), "ulong");
            Assert.AreEqual(UInt128.Zero, ((UInt128)0).NearestCoprimeFloor(0), "UInt128");
        }

        [Test]
        public void NearestCoprimeFloor0And0SignedShouldReturnNeg1() {
            Assert.AreEqual(-1, 0.NearestCoprimeFloor(0), "int");
            Assert.AreEqual(-1, ((long)0).NearestCoprimeFloor(0), "long");
        }

        [Test]
        public void NearestCoprimeFloorNeg2And0ShouldReturn0() {
            Assert.AreEqual(0, (-2).NearestCoprimeFloor(0), "int");
            Assert.AreEqual(0, ((long)-2).NearestCoprimeFloor(0), "long");
        }

        [Test]
        public void NearestCoprimeFloor1And1ShouldReturn1() {
            Assert.AreEqual(1, ((uint)1).NearestCoprimeFloor(1), "uint");
            Assert.AreEqual(1, 1.NearestCoprimeFloor(1), "int");
            Assert.AreEqual(1, ((ulong)1).NearestCoprimeFloor(1), "ulong");
            Assert.AreEqual(1, ((long)1).NearestCoprimeFloor(1), "long");
            Assert.AreEqual(UInt128.One, ((UInt128)1).NearestCoprimeFloor(1), "UInt128");
        }

        [Test]
        public void NearestCoprimeFloor10And15ShouldReturn8() {
            Assert.AreEqual(8, ((uint)10).NearestCoprimeFloor(15), "uint");
            Assert.AreEqual(8, 10.NearestCoprimeFloor(15), "int");
            Assert.AreEqual(8, ((ulong)10).NearestCoprimeFloor(15), "ulong");
            Assert.AreEqual(8, ((long)10).NearestCoprimeFloor(15), "long");
            Assert.AreEqual((UInt128)8, ((UInt128)10).NearestCoprimeFloor(15), "UInt128");
        }

        [Test]
        public void NearestCoprimeFloor10AndNeg15ShouldReturn8() {
            Assert.AreEqual(8, 10.NearestCoprimeFloor(-15), "int");
            Assert.AreEqual(8, ((long)10).NearestCoprimeFloor(-15), "long");
        }

        [Test]
        public void NearestCoprimeFloorNeg10And15ShouldReturnNeg11() {
            Assert.AreEqual(-11, (-10).NearestCoprimeFloor(15), "int");
            Assert.AreEqual(-11, ((long)-10).NearestCoprimeFloor(15), "long");
        }

        [Test]
        public void NearestCoprimeFloorNeg10AndNeg15ShouldReturnNeg11() {
            Assert.AreEqual(-11, (-10).NearestCoprimeFloor(-15), "int");
            Assert.AreEqual(-11, ((long)-10).NearestCoprimeFloor(-15), "long");
        }

        [Test]
        public void NearestCoprimeCeiling0And0ShouldReturn1() {
            Assert.AreEqual(1, ((uint)0).NearestCoprimeCeiling(0), "uint");
            Assert.AreEqual(1, 0.NearestCoprimeCeiling(0), "int");
            Assert.AreEqual(1, ((ulong)0).NearestCoprimeCeiling(0), "ulong");
            Assert.AreEqual(1, ((long)0).NearestCoprimeCeiling(0), "long");
            Assert.AreEqual(UInt128.One, ((UInt128)0).NearestCoprimeCeiling(0), "UInt128");
        }

        [Test]
        public void NearestCoprimeCeiling2And0ShouldReturn0() {
            Assert.AreEqual(0, PrimeMath.NearestCoprimeCeiling(2, (uint)0), "uint");
            Assert.AreEqual(0, 2.NearestCoprimeCeiling(0), "int");
            Assert.AreEqual(0, ((ulong)2).NearestCoprimeCeiling(0), "ulong");
            Assert.AreEqual(0, ((long)2).NearestCoprimeCeiling(0), "long");
            Assert.AreEqual(UInt128.Zero, ((UInt128)2).NearestCoprimeCeiling(0), "UInt128");
        }

        [Test]
        public void NearestCoprimeCeiling1And1ShouldReturn1() {
            Assert.AreEqual(1, ((uint)1).NearestCoprimeCeiling(1), "uint");
            Assert.AreEqual(1, 1.NearestCoprimeCeiling(1), "int");
            Assert.AreEqual(1, ((ulong)1).NearestCoprimeCeiling(1), "ulong");
            Assert.AreEqual(1, ((long)1).NearestCoprimeCeiling(1), "long");
            Assert.AreEqual(UInt128.One, ((UInt128)1).NearestCoprimeCeiling(1), "UInt128");
        }

        [Test]
        public void NearestCoprimeCeiling10And15ShouldReturn11() {
            Assert.AreEqual(11, ((uint)10).NearestCoprimeCeiling(15), "uint");
            Assert.AreEqual(11, 10.NearestCoprimeCeiling(15), "int");
            Assert.AreEqual(11, ((ulong)10).NearestCoprimeCeiling(15), "ulong");
            Assert.AreEqual(11, ((long)10).NearestCoprimeCeiling(15), "long");
            Assert.AreEqual((UInt128)11, ((UInt128)10).NearestCoprimeCeiling(15), "UInt128");
        }

        [Test]
        public void NearestCoprimeCeiling10AndNeg15ShouldReturn11() {
            Assert.AreEqual(11, 10.NearestCoprimeCeiling(-15), "int");
            Assert.AreEqual(11, ((long)10).NearestCoprimeCeiling(-15), "long");
        }

        [Test]
        public void NearestCoprimeCeilingNeg10And15ShouldReturnNeg8() {
            Assert.AreEqual(-8, (-10).NearestCoprimeCeiling(15), "int");
            Assert.AreEqual(-8, ((long)-10).NearestCoprimeCeiling(15), "long");
        }

        [Test]
        public void NearestCoprimeCeilingNeg10AndNeg15ShouldReturnNeg8() {
            Assert.AreEqual(-8, (-10).NearestCoprimeCeiling(-15), "int");
            Assert.AreEqual(-8, ((long)-10).NearestCoprimeCeiling(-15), "long");
        }
    }
}
