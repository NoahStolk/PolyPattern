using PolyPattern.Channels;
using PolyPattern.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PolyPattern.Patterns
{
	public class PatternSimple
	{
		public static readonly int ChannelCountSize = 2; // The maximum amount of limbs that can be used by a human is 4 (2-bit integer).

		public ChannelSimple[] Channels { get; }

		public PatternSimple(params ChannelSimple[] channels)
		{
			Channels = channels;
		}

		public static PatternSimple FromBitArray(BitArray bitArray)
		{
			int arrayPos = 0;
			byte channelCount = (byte)(bitArray.GetBits(ref arrayPos, 2) + 1);

			ChannelSimple[] channels = new ChannelSimple[channelCount];
			for (int i = 0; i < channelCount; i++)
				channels[i] = new ChannelSimple((byte)(bitArray.GetBits(ref arrayPos, 4) + 1));

			for (int i = 0; i < channelCount; i++)
				for (int j = 0; j < channels[i].Length; j++)
					channels[i].Notes[j] = bitArray.Get(arrayPos++);

			return new PatternSimple(channels);
		}

		public BitArray ToBitArray()
		{
			BitArray bitArray = new BitArray(ChannelCountSize);
			int arrayPos = 0;
			bitArray.SetBits(ref arrayPos, (byte)(Channels.Length - 1), ChannelCountSize);

			for (int i = 0; i < Channels.Length; i++)
				bitArray = bitArray.Append(Channels[i].HeaderToBits());
			for (int i = 0; i < Channels.Length; i++)
				bitArray = bitArray.Append(new BitArray(Channels[i].Notes));

			return bitArray;
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder(string.Join("\n", Channels.Select(c => $"{c.Length}: {string.Join("", c.Notes.Select(n => n ? "x" : "-"))}")));
			sb.AppendLine();

			BitArray bitArray = ToBitArray();
			for (int i = 0; i < bitArray.Length; i++)
			{
				int hOffset = i - 2;
				if (i == 2 || (hOffset % 4 == 0 && hOffset / 4 < Channels.Length))
					sb.Append(" ");

				List<int> noteSeparators = new List<int> { 0 };
				int sep = 0;
				foreach (ChannelSimple channel in Channels)
				{
					sep += channel.Notes.Length;
					noteSeparators.Add(sep);
				}
				int nOffset = hOffset - Channels.Length * 4;
				if (noteSeparators.Any(s => s == nOffset))
					sb.Append("|");

				sb.Append(bitArray.Get(i) ? "1" : "0");
			}

			return sb.ToString();
		}
	}
}