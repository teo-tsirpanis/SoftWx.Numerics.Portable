// Copyright ©2015-2016 SoftWx, Inc.
// Released under the MIT License the text of which appears at the end of this file.
// <authors> Steve Hatchett

//#define StandaloneUInt128 // if StandaloneUInt128 is defined UInt128 can operate standalone, without needing other classes in SoftWx.Numerics
// comment the #define if UInt128 is used as part of the SoftWx.Numerics library

using System;

namespace SoftWx.Numerics
{
    /// <summary>Represents a 128-bit unsigned integer.</summary>
    /// <remarks>The UInt128 struct is immutable.</remarks>
#if NETSTANDARD2_0
    [Serializable]
#endif
    public readonly struct UInt128 : IEquatable<UInt128>, IComparable, IComparable<UInt128>
    {
        /// <summary>Gets a value that represents the number 0 (zero).</summary>
        public static readonly UInt128 Zero = new UInt128(0, 0);

        /// <summary>Gets a value that represents the number 1 (one).</summary>
        public static readonly UInt128 One = new UInt128(0, 1);

        /// <summary>Represents the largest possible value of UInt128.</summary>
        public static readonly UInt128 MaxValue = new UInt128(ulong.MaxValue, ulong.MaxValue);

        /// <summary>Represents the smallest possible value of UInt128.</summary>
        public static readonly UInt128 MinValue = Zero;

        /// <summary>Computes the 128 bit product of two 64 bit unsigned integers.</summary>
        /// <param name="left">The value to be multiplied by right.</param>
        /// <param name="right">The value to be multiplied by left.</param>
        /// <returns>The UInt128 product of multiplying left and right.</returns>
        public static UInt128 Multiply(ulong left, ulong right)
        {
            unchecked
            {
                var lhi = left >> 32;
                var rhi = right >> 32;
                if ((lhi | rhi) == 0) return left * right;
                var hi = lhi * rhi;
                ulong llo = (uint) left;
                var mid = llo * rhi;
                ulong rlo = (uint) right;
                var lo = llo * rlo;
                var mid2 = rlo * lhi;
                mid += mid2;
                hi += mid >> 32;
                if (mid < mid2) hi += 1ul << 32;
                mid <<= 32;
                lo += mid;
                if (lo < mid) hi++;
                return new UInt128(hi, lo);
            }
        }

        /// <summary>Computes the 128 bit product of squaring a 64 bit unsigned integer.</summary>
        /// <param name="value">The value to be squared (multiplied by itself).</param>
        /// <returns>The UInt128 product of squaring the specified value.</returns>
        public static UInt128 Square(ulong value)
        {
            if (value == (uint) value) return value * value;
            unchecked
            {
                var hi = value >> 32;
                ulong lo = (uint) value;
                var mid = lo * hi;
                hi *= hi;
                lo *= lo;
                // mid is 2*lo*hi, but we skipped multiplying by 2 and account for it in the
                // off by one shifts below so we wouldn't lose data by shifting off a bit doing *2
                hi += mid >> 31;
                mid <<= 33;
                lo += mid;
                if (lo < mid) hi++;
                return new UInt128(hi, lo);
            }
        }

        /// <summary>Creates an instance of UInt128.</summary>
        /// <param name="high">The most significant 64 bits.</param>
        /// <param name="low">The least significant 64 bits.</param>
        public UInt128(ulong high, ulong low)
        {
            High = high;
            Low = low;
        }

        /// <summary>Determines if the UInt128 value is less than or equal to UInt64.MaxValue.</summary>
        public bool IsULong => High == 0;

        /// <summary>Returns the lower 64 bits of the UInt128 value.</summary>
        public ulong Low { get; }

        /// <summary>Returns the upper 64 bits of the UInt128 value.</summary>
        public ulong High { get; }

        /// <summary>Returns a value indicating whether this instance is equal to a specified value.</summary>
        /// <param name="other">The value to compare to this instance.</param>
        /// <returns>True if value equals the this instance; otherwise, false.</returns>
        public bool Equals(UInt128 other)
        {
            return this == other;
        }

        /// <summary>Returns a value indicating whether this instance is equal to a specified ulong value.</summary>
        /// <param name="other">The value to compare to this instance.</param>
        /// <returns>True if value equals the this instance; otherwise, false.</returns>
        public bool Equals(ulong other)
        {
            return this == other;
        }

        /// <summary>Returns a value indicating whether this instance is equal to a specified object.</summary>
        /// <param name="obj">An object to compare to this instance.</param>
        /// <returns>True if obj is an instance of UInt128 and equals the value of this instance; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is UInt128))
                return false;
            return this == (UInt128) obj;
        }

        /// <summary>Returns the hash code for this instance.</summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override int GetHashCode()
        {
            return High.GetHashCode() ^ Low.GetHashCode();
        }

        /// <summary>Returns the string representation of this instance.</summary>
        /// <returns>The string representation of this instance.</returns>
        public override string ToString()
        {
            var loStr = Low.ToString("X");
            if (High == 0) return loStr;
            loStr = new string('0', 64 / 4 - loStr.Length) + loStr;
            return High.ToString("X") + ' ' + loStr;
        }

        /// <summary>
        ///     Compares the current instance with another object of the same type and
        ///     returns an integer that indicates whether the current instance precedes, follows,
        ///     or occurs in the same position in the sort order as the other object.
        /// </summary>
        /// <param name="other">An object to compare with this instance.</param>
        /// <returns>
        ///     A value that indicates the relative order of the itmes being compared.
        ///     The return value has these meanings: -1 This is less than other, 0 this equals
        ///     other, 1 this is greater than other.
        /// </returns>
        public int CompareTo(object other)
        {
            if (other == null) return 1;
            if (!(other is UInt128)) throw new ArgumentException("Argument must be UInt128");
            var num = (UInt128) other;
            if (this > num) return 1;
            return this == num ? 0 : -1;
        }

        /// <summary>
        ///     Compares the current instance with another object of the same type and
        ///     returns an integer that indicates whether the current instance precedes, follows,
        ///     or occurs in the same position in the sort order as the other object.
        /// </summary>
        /// <param name="other">An object to compare with this instance.</param>
        /// <returns>
        ///     A value that indicates the relative order of the itmes being compared.
        ///     The return value has these meanings: -1 This is less than other, 0 this equals
        ///     other, 1 this is greater than other.
        /// </returns>
        public int CompareTo(UInt128 other)
        {
            if (this > other) return 1;
            return this == other ? 0 : -1;
        }

        /// <summary>Defines an explicit conversion of a UInt128 value to an unsigned long value.</summary>
        /// <remarks>Only the lower 64 bits is returned, which may result in loss of data.</remarks>
        /// <param name="value"></param>
        public static explicit operator ulong(UInt128 value)
        {
            return value.Low;
        }

        /// <summary>Defines an implicit conversion of an unsigned long value to a UInt128 value.</summary>
        public static implicit operator UInt128(ulong value)
        {
            return new UInt128(0, value);
        }

        /// <summary>Returns a value that indicates whether the two UInt128 are equal.</summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>True if the values are equal, otherwise, false.</returns>
        public static bool operator ==(UInt128 left, UInt128 right)
        {
            return left.High == right.High && left.Low == right.Low;
        }

        /// <summary>Returns a value that indicates whether the UInt128 is equal to a ulong value.</summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>True if the values are equal, otherwise, false.</returns>
        public static bool operator ==(UInt128 left, ulong right)
        {
            return left.High == 0 && left.Low == right;
        }

        /// <summary>Returns a value that indicates whether the ulong value is equal to a UInt128.</summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>True if the values are equal, otherwise, false.</returns>
        public static bool operator ==(ulong left, UInt128 right)
        {
            return right.High == 0 && right.Low == left;
        }

        /// <summary>Returns a value that indicates whether the two UInt128 are not equal.</summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>True if the values are not equal, otherwise, false.</returns>
        public static bool operator !=(UInt128 left, UInt128 right)
        {
            return left.High != right.High || left.Low != right.Low;
        }

        /// <summary>Returns a value that indicates whether the a UInt128 is not equal to a ulong value.</summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>True if the values are not equal, otherwise, false.</returns>
        public static bool operator !=(UInt128 left, ulong right)
        {
            return left.High != 0 || left.Low != right;
        }

        /// <summary>Returns a value that indicates whether the a ulong value is not equal to a UInt128.</summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>True if the values are not equal, otherwise, false.</returns>
        public static bool operator !=(ulong left, UInt128 right)
        {
            return right.High != 0 || right.Low != left;
        }

        /// <summary>Returns a value that indicates whether a UInt128 value is greater than another UInt128 value.</summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>True if the left is greater than right, otherwise, false.</returns>
        public static bool operator >(UInt128 left, UInt128 right)
        {
            if (left.High > right.High) return true;
            if (left.High < right.High) return false;
            return left.Low > right.Low;
        }

        /// <summary>Returns a value that indicates whether a UInt128 value is less than another UInt128 value.</summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>True if the left is less than right, otherwise, false.</returns>
        public static bool operator <(UInt128 left, UInt128 right)
        {
            if (left.High < right.High) return true;
            if (left.High > right.High) return false;
            return left.Low < right.Low;
        }

        /// <summary>Returns a value that indicates whether a UInt128 value is greater than or equal to another UInt128 value.</summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>True if the left is greater than or equal to right, otherwise, false.</returns>
        public static bool operator >=(UInt128 left, UInt128 right)
        {
            if (left.High > right.High) return true;
            if (left.High < right.High) return false;
            return left.Low >= right.Low;
        }

        /// <summary>Returns a value that indicates whether a UInt128 value is less than or equal to another UInt128 value.</summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>True if the left is less than or equal to right, otherwise, false.</returns>
        public static bool operator <=(UInt128 left, UInt128 right)
        {
            if (left.High < right.High) return true;
            if (left.High > right.High) return false;
            return left.Low <= right.Low;
        }

        /// <summary>Returns the bitwise one's complement of a UInt128 value.</summary>
        /// <param name="value">The value to be complemented.</param>
        /// <returns>The bitwise one's complement of value.</returns>
        public static UInt128 operator ~(UInt128 value)
        {
            return new UInt128(~value.High, ~value.Low);
        }

        /// <summary>Returns the bitwise two's complement of a UInt128 value.</summary>
        /// <param name="value">The value to be complemented.</param>
        /// <returns>The bitwise two's complement of value.</returns>
        public static UInt128 operator -(UInt128 value) => ~value + One;

        /// <summary>Performs a bitwise And operation on two values.</summary>
        /// <param name="left">The first value.</param>
        /// <param name="right">The second value.</param>
        /// <returns>The result of the bitwise And operation.</returns>
        public static UInt128 operator &(UInt128 left, UInt128 right)
        {
            return new UInt128(left.High & right.High, left.Low & right.Low);
        }

        /// <summary>Performs a bitwise And operation on two values.</summary>
        /// <param name="left">The first value.</param>
        /// <param name="right">The second value.</param>
        /// <returns>The result of the bitwise And operation.</returns>
        public static UInt128 operator &(UInt128 left, ulong right)
        {
            return new UInt128(0, left.Low & right);
        }

        /// <summary>Performs a bitwise And operation on two values.</summary>
        /// <param name="left">The first value.</param>
        /// <param name="right">The second value.</param>
        /// <returns>The result of the bitwise And operation.</returns>
        public static UInt128 operator &(ulong left, UInt128 right)
        {
            return new UInt128(0, left & right.Low);
        }

        /// <summary>Performs a bitwise Or operation on two values.</summary>
        /// <param name="left">The first value.</param>
        /// <param name="right">The second value.</param>
        /// <returns>The result of the bitwise Or operation.</returns>
        public static UInt128 operator |(UInt128 left, UInt128 right)
        {
            return new UInt128(left.High | right.High, left.Low | right.Low);
        }

        /// <summary>Performs a bitwise Or operation on two values.</summary>
        /// <param name="left">The first value.</param>
        /// <param name="right">The second value.</param>
        /// <returns>The result of the bitwise Or operation.</returns>
        public static UInt128 operator |(UInt128 left, ulong right)
        {
            return new UInt128(left.High, left.Low | right);
        }

        /// <summary>Performs a bitwise Or operation on two values.</summary>
        /// <param name="left">The first value.</param>
        /// <param name="right">The second value.</param>
        /// <returns>The result of the bitwise Or operation.</returns>
        public static UInt128 operator |(ulong left, UInt128 right)
        {
            return new UInt128(right.High, left | right.Low);
        }

        /// <summary>Performs a bitwise Xor operation on two values.</summary>
        /// <param name="left">The first value.</param>
        /// <param name="right">The second value.</param>
        /// <returns>The result of the bitwise Xor operation.</returns>
        public static UInt128 operator ^(UInt128 left, UInt128 right)
        {
            return new UInt128(left.High ^ right.High, left.Low ^ right.Low);
        }

        /// <summary>Performs a bitwise Xor operation on two values.</summary>
        /// <param name="left">The first value.</param>
        /// <param name="right">The second value.</param>
        /// <returns>The result of the bitwise Xor operation.</returns>
        public static UInt128 operator ^(UInt128 left, ulong right)
        {
            return new UInt128(left.High, left.Low ^ right);
        }

        /// <summary>Performs a bitwise Xor operation on two values.</summary>
        /// <param name="left">The first value.</param>
        /// <param name="right">The second value.</param>
        /// <returns>The result of the bitwise Xor operation.</returns>
        public static UInt128 operator ^(ulong left, UInt128 right)
        {
            return new UInt128(right.High, left ^ right.Low);
        }

        /// <summary>Shifts a UInt128 value a specified number of bits to the left.</summary>
        /// <param name="value">The value whose bits are to be shifted.</param>
        /// <param name="shift">The number of bits to shift value to the left.</param>
        /// <returns>A value that has been shifted to the left by the specified number of bits.</returns>
        public static UInt128 operator <<(UInt128 value, int shift)
        {
            if (shift == 0) return value;
            return shift >= 64
                ? new UInt128(value.Low << (shift - 64), 0UL)
                : new UInt128((value.High << shift) | (value.Low >> (64 - shift)), value.Low << shift);
        }

        /// <summary>Shifts a UInt128 value a specified number of bits to the right.</summary>
        /// <param name="value">The value whose bits are to be shifted.</param>
        /// <param name="shift">The number of bits to shift value to the right.</param>
        /// <returns>A value that has been shifted to the right by the specified number of bits.</returns>
        public static UInt128 operator >>(UInt128 value, int shift)
        {
            if (shift == 0) return value;
            return shift >= 64
                ? new UInt128(0UL, value.High >> (shift - 64))
                : new UInt128(value.High >> shift, (value.Low >> shift) | (value.High << (64 - shift)));
        }

        /// <summary>Adds two specified UInt128 values.</summary>
        /// <param name="left">The first value to add.</param>
        /// <param name="right">The second value to add.</param>
        /// <returns>The sum of left and right.</returns>
        public static UInt128 operator +(UInt128 left, UInt128 right)
        {
            var newLo = unchecked(left.Low + right.Low);
            var newHi = left.High + right.High;
            if (newLo < left.Low) newHi++;
            return new UInt128(newHi, newLo);
        }

        /// <summary>Adds a ulong value to an UInt128 value.</summary>
        /// <param name="left">The first value to add.</param>
        /// <param name="right">The second value to add.</param>
        /// <returns>The sum of left and right.</returns>
        public static UInt128 operator +(UInt128 left, ulong right)
        {
            var newLo = unchecked(left.Low + right);
            var newHi = left.High;
            if (newLo < left.Low) newHi++;
            return new UInt128(newHi, newLo);
        }

        /// <summary>Adds a UInt128 value to a ulong value.</summary>
        /// <param name="left">The first value to add.</param>
        /// <param name="right">The second value to add.</param>
        /// <returns>The sum of left and right.</returns>
        public static UInt128 operator +(ulong left, UInt128 right)
        {
            return right + left;
        }

        /// <summary>Subtracts a UInt128 value from another UInt128 value.</summary>
        /// <param name="left">The value to subtract from.</param>
        /// <param name="right">The value to subtract.</param>
        /// <returns>The value resulting from subtracting right from left.</returns>
        public static UInt128 operator -(UInt128 left, UInt128 right)
        {
            return new UInt128(left.High - right.High - (left.Low < right.Low ? 1UL : 0UL),
                unchecked(left.Low - right.Low));
        }

        /// <summary>Subtracts a ulong value from a UInt128 value.</summary>
        /// <param name="left">The value to subtract from.</param>
        /// <param name="right">The value to subtract.</param>
        /// <returns>The value resulting from subtracting right from left.</returns>
        public static UInt128 operator -(UInt128 left, ulong right)
        {
            return new UInt128(left.High - (left.Low < right ? 1UL : 0UL), unchecked(left.Low - right));
        }

        /// <summary>Increments a value by 1.</summary>
        /// <param name="value">The value to be incremented.</param>
        /// <returns>The value of the parameter incremented by 1.</returns>
        public static UInt128 operator ++(UInt128 value)
        {
            var newLo = unchecked(value.Low + 1);
            return new UInt128(newLo != 0 ? value.High : value.High + 1, newLo);
        }

        /// <summary>Decrements a value by 1.</summary>
        /// <param name="value">The value to be decremented.</param>
        /// <returns>The value of the parameter decremented by 1.</returns>
        public static UInt128 operator --(UInt128 value)
        {
            return new UInt128(value.Low != 0 ? value.High : value.High - 1, unchecked(value.Low - 1));
        }

        /// <summary>Multiplies two specified UInt128 values.</summary>
        /// <param name="left">The first value to multiply.</param>
        /// <param name="right">The second value to multiply.</param>
        /// <returns>The product of multiplying left and right.</returns>
        public static UInt128 operator *(UInt128 left, UInt128 right)
        {
            var result = Multiply(left.Low, right.Low);
            return new UInt128(result.High + left.Low * right.High + left.High * right.Low, result.Low);
        }

        /// <summary>Multiplies a UInt128 value and a ulong value.</summary>
        /// <param name="left">The first value to multiply.</param>
        /// <param name="right">The second value to multiply.</param>
        /// <returns>The product of multiplying left and right.</returns>
        public static UInt128 operator *(UInt128 left, ulong right)
        {
            var result = Multiply(left.Low, right);
            return new UInt128(result.High + left.High * right, result.Low);
        }

        /// <summary>Multiplies a ulong value and a UInt128 value.</summary>
        /// <param name="left">The first value to multiply.</param>
        /// <param name="right">The second value to multiply.</param>
        /// <returns>The product of multiplying left and right.</returns>
        public static UInt128 operator *(ulong left, UInt128 right)
        {
            return right * left;
        }

        /// <summary>Divides a UInt128 value by another UInt128 value using integer division.</summary>
        /// <param name="left">The value to be divided.</param>
        /// <param name="right">The value to divide by.</param>
        /// <returns>The integral result of the division.</returns>
        public static UInt128 operator /(UInt128 left, UInt128 right)
        {
            if (right.IsULong) return left / right.Low;
            if (left < right) return Zero;
            //if (right.IsPowerOf2()) return left >> right.HighBitPosition();
            return left.Divide(right);
        }

        /// <summary>Divides a UInt128 value by an unsigned long value using integer division.</summary>
        /// <param name="left">The value to be divided.</param>
        /// <param name="right">The value to divide by.</param>
        /// <returns>The integral result of the division.</returns>
        public static UInt128 operator /(UInt128 left, ulong right)
        {
            if (right == (uint) right) return left / (uint) right;
            if (left < right) return Zero;
            if (right.IsPowerOf2()) return left >> right.HighBitPosition();
            return left.Divide(right);
        }

        /// <summary>Divides a UInt128 value by an unsigned long value using integer division.</summary>
        /// <param name="left">The value to be divided.</param>
        /// <param name="right">The value to divide by.</param>
        /// <returns>The integral result of the division.</returns>
        public static UInt128 operator /(UInt128 left, uint right)
        {
            if (right == 0) throw new DivideByZeroException();
            if (left < right) return Zero;
            if (left.High == 0) return left.Low / right;
            return left.Divide(right);
        }

        /// <summary>Returns the remainder that results from dividing one UInt128 value by another.</summary>
        /// <param name="left">The value to be divided.</param>
        /// <param name="right">The value to divide by.</param>
        /// <returns>The remainder that results from the division.</returns>
        public static UInt128 operator %(UInt128 left, UInt128 right)
        {
            if (right.IsULong) return left % right.Low;
            if (left < right) return left;
            if (right.IsPowerOf2()) return left & (right - 1);
            return left.Mod(right);
        }

        /// <summary>Returns the remainder that results from dividing a UInt128 value by an unsigned long value.</summary>
        /// <param name="left">The value to be divided.</param>
        /// <param name="right">The value to divide by.</param>
        /// <returns>The remainder that results from the division.</returns>
        public static ulong operator %(UInt128 left, ulong right)
        {
            if (right == (uint) right) return left % (uint) right;
            if (left < right) return left.Low;
            if (right.IsPowerOf2()) return left.Low & (right - 1);
            return left.Mod(right);
        }

        /// <summary>Returns the remainder that results from dividing a UInt128 value by an unsigned int value.</summary>
        /// <param name="left">The value to be divided.</param>
        /// <param name="right">The value to divide by.</param>
        /// <returns>The remainder that results from the division.</returns>
        public static uint operator %(UInt128 left, uint right)
        {
            if (right == 0) throw new DivideByZeroException();
            if (left < right) return (uint) left.Low;
            if (left.High == 0) return (uint) (left.Low % right);
            if (right.IsPowerOf2()) return (uint) left.Low & (right - 1);
            return left.Mod(right);
        }

        // denominator must be <= this and > ulong.MaxValue
        private ulong Divide(UInt128 denominator)
        {
            var remainder = this;
            ulong result = 0;
            var denHiBit = denominator.High.HighBitPosition();
            do
            {
                var remHiBit = remainder.High.HighBitPosition();
                var diff = remHiBit - denHiBit;
                if (diff <= 3)
                {
                    // div by subtraction
                    do
                    {
                        result++;
                        remainder -= denominator;
                    } while (remainder >= denominator);

                    return result;
                }

                ulong resLo;
                if (denHiBit >= 18 && diff <= 24)
                {
                    // div by divide high ulongs
                    resLo = remainder.High /
                            (denominator.High +
                             1
                            ); // we don't have to worry about denominator.hi being MaxValue and overflowing, because that case handled above
                }
                else
                {
                    // div by shifted divide
                    var denShift = denHiBit + 1;
                    var den = (denominator >> denShift).Low;
                    if (den > remainder.High)
                    {
                        // we already know division quotient will fit in a ulong
                        den++;
                        if (den == 0)
                        {
                            // handle the rare case where the shifted den + 1 overflows
                            denShift++;
                            den = 1UL << 31;
                        }

                        resLo = DivUnchecked(remainder.High, remainder.Low, den);
                        resLo >>= denShift;
                    }
                    else
                    {
                        // ensure division quotient will fit in a ulong
                        den++; // den + 1 can't overflow, because it's less then remainder.hi
                        var resHi = remainder.High / den;
                        var remHi = remainder.High - remainder.High / den * den;
                        var tempResult = new UInt128(resHi, DivUnchecked(remHi, remainder.Low, den));
                        tempResult >>= denShift;
                        resLo = tempResult.Low;
                    }
                }

                result += resLo;
                remainder -= denominator * resLo;
            } while (remainder >= denominator);

            return result;
        }

        // denominator must be > uint.MaxValue
        private UInt128 Divide(ulong denominator)
        {
            ulong resHi = 0;
            var remHi = High;
            //ensure we can divide a UInt128 by a ulong, and have the quotient fit in a ulong
            if (remHi >= denominator)
            {
                resHi = remHi / denominator;
                remHi -= resHi * denominator;
            }

            return new UInt128(resHi, DivUnchecked(remHi, Low, denominator));
        }

        // denominator must be > uint.MaxValue and > the high parameter
        private static ulong DivUnchecked(ulong high, ulong low, ulong denominator)
        {
            unchecked
            {
                ulong hiShLo, denHi, denLo, quotHi, quotLo, mid, shLoMid, shLo, rhat, left, right;

                if (denominator >> 63 != 0)
                {
                    mid = high;
                    shLo = low;
                }
                else
                {
                    // unwind first few iterations of loop to count leading zero bits but use
                    // bit fiddle if more than 3 to avoid time consuming count when there are
                    // many leading zero bits. Doing a quick test of the 4 most significant
                    // bit positions covers about 94% of possible ulong denominator values.
                    int s;
                    if (denominator >> 62 != 0) s = 1;
                    else if (denominator >> 61 != 0) s = 2;
                    else if (denominator >> 60 != 0) s = 3;
                    else s = ((uint) (denominator >> 32)).LeadingZeroBits();
                    denominator <<= s;
                    mid = (high << s) | (low >> (64 - s));
                    shLo = low << s;
                }

                shLoMid = mid << 32;
                hiShLo = shLo >> 32;
                shLoMid += hiShLo;
                denLo = (uint) denominator;
                denHi = denominator >> 32;
                quotHi = mid / denHi;
                if (quotHi != 0)
                {
                    rhat = mid - mid / denHi * denHi;
                    right = (rhat << 32) | hiShLo;
                    left = quotHi * denLo;
                    while (quotHi != (uint) quotHi || left > right)
                    {
                        quotHi--;
                        rhat += denHi;
                        if (rhat != (uint) rhat) break;
                        right = (rhat << 32) | hiShLo;
                        left -= denLo;
                    }

                    shLoMid -= quotHi * denominator;
                    quotHi <<= 32;
                }

                quotLo = shLoMid / denHi;
                rhat = shLoMid - shLoMid / denHi * denHi;
                right = (rhat << 32) | (uint) shLo;
                left = quotLo * denLo;
                while (quotLo != (uint) quotLo || left > right)
                {
                    quotLo--;
                    rhat += denHi;
                    if (rhat != (uint) rhat) break;
                    right = (rhat << 32) | (uint) shLo;
                    left -= denLo;
                }

                return quotHi | quotLo;
            }
        }

        internal UInt128 Divide(uint denominator)
        {
            var hihi = (uint) (High >> 32);
            var reshihi = hihi == 0 ? 0 : hihi / denominator;
            var remainder = hihi == 0
                ? (uint) High
                : ((ulong) (hihi - hihi / denominator * denominator) << 32) | (uint) High;
            var reshilo = (uint) (remainder / denominator);
            var remHi = remainder - reshilo * denominator;
            remainder = (remHi << 32) | (uint) (Low >> 32);
            var reslohi = (uint) (remainder / denominator);
            remHi = remainder - reslohi * denominator;
            remainder = (remHi << 32) | (uint) Low;
            var reslolo = (uint) (remainder / denominator);
            return new UInt128(((ulong) reshihi << 32) | reshilo, ((ulong) reslohi << 32) | reslolo);
        }

        // denominator must be <= this and > ulong.MaxValue
        internal UInt128 Mod(UInt128 denominator)
        {
            var remainder = this;
            var denHiBit = denominator.High.HighBitPosition();
            do
            {
                var remHiBit = remainder.High.HighBitPosition();
                var diff = remHiBit - denHiBit;
                if (diff <= 3)
                {
                    // mod by subtraction
                    do
                    {
                        remainder -= denominator;
                    } while (remainder >= denominator);

                    return remainder;
                }

                ulong resLo, den;
                if (denHiBit >= 18 && diff <= 24)
                {
                    // mod by divide high ulongs
                    resLo = remainder.High /
                            (denominator.High +
                             1
                            ); // we don't have to worry about denominator.hi being MaxValue and overflowing, because that case handled above
                }
                else
                {
                    // mod by shifted divide
                    var denShift = denHiBit + 1;
                    den = (denominator >> denShift).Low;
                    if (den > remainder.High)
                    {
                        // we already know division quotient will fit in a ulong
                        den++;
                        if (den == 0)
                        {
                            // handle the rare case where the shifted den + 1 overflows
                            denShift++;
                            den = 1UL << 31;
                        }

                        resLo = DivUnchecked(remainder.High, remainder.Low, den);
                        resLo >>= denShift;
                    }
                    else
                    {
                        // ensure division quotient will fit in a ulong
                        den++; // den + 1 can't overflow, because it's less then remainder.hi
                        var resHi = remainder.High / den;
                        var remHi = remainder.High - remainder.High / den * den;
                        var tempResult = new UInt128(resHi, DivUnchecked(remHi, remainder.Low, den));
                        tempResult >>= denShift;
                        resLo = tempResult.Low;
                    }
                }

                remainder -= denominator * resLo;
            } while (remainder >= denominator);

            return remainder;
        }

        // denominator must be > uint.MaxValue
        internal ulong Mod(ulong denominator)
        {
            unchecked
            {
                var high = High;
                var low = Low;
                //ensure we can divide a UInt128 by a ulong, and have the quotient fit in a ulong
                if (high >= denominator) high -= high / denominator * denominator;
                if (high == 0) return low - low / denominator * denominator;

                ulong hiShLo, denHi, denLo, quotHi, quotLo, mid, shLoMid, shLo, rhat, left, right;
                int shift;
                if (denominator >> 63 != 0)
                {
                    mid = high;
                    shLo = low;
                    shift = 0;
                }
                else
                {
                    // unwind first few iterations of loop to count leading zero bits but use
                    // bit fiddle if more than 3 to avoid time consuming count when there are
                    // many leading zero bits. Doing a quick test of the 4 most significant
                    // bit positions covers about 94% of possible ulong denominator values.
                    if (denominator >> 62 != 0) shift = 1;
                    else if (denominator >> 61 != 0) shift = 2;
                    else if (denominator >> 60 != 0) shift = 3;
                    else shift = ((uint) (denominator >> 32)).LeadingZeroBits();
                    denominator <<= shift;
                    mid = (high << shift) | (low >> (64 - shift));
                    shLo = low << shift;
                }

                shLoMid = mid << 32;
                hiShLo = shLo >> 32;
                shLoMid += hiShLo;
                denLo = (uint) denominator;
                denHi = denominator >> 32;
                quotHi = mid / denHi;
                if (quotHi != 0)
                {
                    rhat = mid - mid / denHi * denHi;
                    right = (rhat << 32) | hiShLo;
                    left = quotHi * denLo;
                    while (quotHi != (uint) quotHi || left > right)
                    {
                        quotHi--;
                        rhat += denHi;
                        if (rhat != (uint) rhat) break;
                        right = (rhat << 32) | hiShLo;
                        left -= denLo;
                    }

                    shLoMid -= quotHi * denominator;
                }

                quotLo = shLoMid / denHi;
                rhat = shLoMid - shLoMid / denHi * denHi;
                right = (rhat << 32) | (uint) shLo;
                left = quotLo * denLo;
                while (quotLo != (uint) quotLo || left > right)
                {
                    quotLo--;
                    rhat += denHi;
                    if (rhat != (uint) rhat) break;
                    right = (rhat << 32) | (uint) shLo;
                    left -= denLo;
                }

                return ((shLoMid << 32) + ((uint) shLo - quotLo * denominator)) >> shift;
            }
        }

        internal uint Mod(uint denominator)
        {
            var hihi = (uint) (High >> 32);
            var remainder = hihi == 0
                ? (uint) High
                : ((ulong) (hihi - hihi / denominator * denominator) << 32) | (uint) High;
            var remHi = (remainder % denominator) << 32;
            remainder = remHi | (uint) (Low >> 32);
            remHi = (remainder % denominator) << 32;
            remainder = remHi | (uint) Low;
            return (uint) (remainder % denominator);
        }
    }

#if StandaloneUInt128
    internal static class SoftWxNumerics
    {
        private static readonly byte[] DeBruijnLsBsSet =
        {
            0, 9, 1, 10, 13, 21, 2, 29, 11, 14, 16, 18, 22, 25, 3, 30,
            8, 12, 20, 28, 15, 17, 24, 7, 19, 27, 23, 6, 26, 5, 4, 31
        };

        /// <summary>Returns the most significant set bit position of the specified value,
        /// or -1 if no bits were set. The least significant bit position is 0.</summary>
        /// <remarks>Example: HighBitPosition(10) returns 3, i.e. high bit of 00001010 is position 3.</remarks>
        /// <param name="value">The value whose most significant bit position is desired.</param>
        /// <returns>The value parameter's most significant bit position.</returns>
        public static int HighBitPosition(this uint value)
        {
            if (value == 0) return -1;
            value |= value >> 1;
            value |= value >> 2;
            value |= value >> 4;
            value |= value >> 8;
            return DeBruijnLsBsSet[unchecked((value | value >> 16) * 0x07c4acddu) >> 27];
        }

        /// <summary>Returns the most significant set bit position of the specified value,
        /// or -1 if no bits were set. The least significant bit position is 0.</summary>
        /// <remarks>Example: HighBitPosition(10) returns 3, i.e. high bit of 00001010 is position 3.</remarks>
        /// <param name="value">The value whose most significant bit position is desired.</param>
        /// <returns>The value parameter's most significant bit position.</returns>
        public static int HighBitPosition(this ulong value)
        {
            uint high = (uint) (value >> 32);
            return high != 0 ? 32 + HighBitPosition(high) : HighBitPosition((uint) value);
        }

        /// <summary>Returns the count of leading zero bits in the specified value.</summary>
        /// <remarks>Example: LeadingZeroBits(10) returns 4, i.e. 00001010 has 4 leading 0 bits.</remarks>
        /// <param name="value">The value whose leading zero bit count is desired.</param>
        /// <returns>The count of the value parameter's leading zero bits.</returns>
        public static int LeadingZeroBits(this uint value)
        {
            return unchecked(31 - HighBitPosition(value));
        }

        /// <summary>Determines if the specified value is a power of 2.</summary>
        /// <param name="value">The value to be tested as a power of 2.</param>
        /// <returns>True if the value is a power of 2, otherwise false.</returns>
        public static bool IsPowerOf2(this uint value)
        {
            return (value & unchecked(value - 1)) == 0 && value != 0;
        }

        /// <summary>Determines if the specified value is a power of 2.</summary>
        /// <param name="value">The value to be tested as a power of 2.</param>
        /// <returns>True if the value is a power of 2, otherwise false.</returns>
        public static bool IsPowerOf2(this ulong value)
        {
            return (value & unchecked(value - 1UL)) == 0 && value != 0;
        }

        /// <summary>Determines if the specified value is a power of 2.</summary>
        /// <param name="value">The value to be tested as a power of 2.</param>
        /// <returns>True if the value is a power of 2, otherwise false.</returns>
        public static bool IsPowerOf2(this UInt128 value)
        {
            return (value & value - 1) == 0 && value != 0;
        }
    }

#endif
}
/*
Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/