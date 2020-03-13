using PolyPattern.Utils;
using System;
using System.Collections;

namespace PolyPattern.Channels
{
	public class ChannelSimple
	{
		public static readonly int ChannelNoteLengthSize = 4; // Length of a channel can be max 16 (4-bit integer).

		public byte Length { get; }
		public bool[] Notes { get; }

		public ChannelSimple(byte length)
		{
			Length = length;
			Notes = new bool[length];
		}

		public void Set(int index, bool note)
		{
			if (index >= Notes.Length)
				throw new ArgumentException($"Index '{index}' was outside of array bounds '{Notes.Length}'.", nameof(index));

			Notes[index] = note;
		}

		public BitArray HeaderToBits()
		{
			BitArray array = new BitArray(ChannelNoteLengthSize);
			int arrayPos = 0;
			array.SetBits(ref arrayPos, (byte)(Length - 1), ChannelNoteLengthSize);
			return array;
		}
	}
}