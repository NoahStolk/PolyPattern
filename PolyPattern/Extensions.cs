using System;
using System.Collections;

namespace PolyPattern
{
	public static class Extensions
	{
		public static void SetBits(this BitArray bitArray, ref int bitArrayPosition, byte value, int valueBitCount)
		{
			if (valueBitCount > 8)
				throw new ArgumentException("Value was outside of 8-bit integer bit count.", nameof(valueBitCount));

			int pow = (int)Math.Pow(2, valueBitCount - 1);
			for (int i = 0; i < valueBitCount; i++)
			{
				bitArray.Set(bitArrayPosition + i, (value & pow) != 0);
				pow >>= 1;
			}

			bitArrayPosition += valueBitCount;
		}

		public static BitArray Prepend(this BitArray current, BitArray before)
		{
			bool[] bools = new bool[current.Count + before.Count];
			before.CopyTo(bools, 0);
			current.CopyTo(bools, before.Count);
			return new BitArray(bools);
		}

		public static BitArray Append(this BitArray current, BitArray after)
		{
			bool[] bools = new bool[current.Count + after.Count];
			current.CopyTo(bools, 0);
			after.CopyTo(bools, current.Count);
			return new BitArray(bools);
		}

		public static byte[] ToBytes(this BitArray bitArray)
		{
			byte[] bytes = new byte[bitArray.Length / 8 + (bitArray.Length % 8 == 0 ? 0 : 1)]; // Due to integer division we need to allocate one more byte should there be any remaining bits.
			bitArray.CopyTo(bytes, 0);
			return bytes;
		}
	}
}