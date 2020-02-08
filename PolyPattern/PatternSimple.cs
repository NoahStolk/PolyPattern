using System.Collections;

namespace PolyPattern
{
	public class PatternSimple
	{
		public static readonly int ChannelCountSize = 2; // The maximum amount of limbs that can be used by a human is 4 (2-bit integer).

		public ChannelSimple[] Channels { get; }

		public PatternSimple(params ChannelSimple[] channels)
		{
			Channels = channels;
		}

		public BitArray ToBitArray()
		{
			BitArray bitArray = new BitArray(ChannelCountSize);
			int arrayPos = 0;
			bitArray.SetBits(ref arrayPos, (byte)(Channels.Length - 1), ChannelCountSize);

			for (int i = 0; i < Channels.Length; i++)
				bitArray = bitArray.Append(Channels[i].HeaderToBits());
			for (int i = 0; i < Channels.Length; i++)
				bitArray = bitArray.Append(Channels[i].NotesToBits());

			return bitArray;
		}
	}
}