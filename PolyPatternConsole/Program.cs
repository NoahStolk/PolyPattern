using PolyPattern;
using System;
using System.Collections;
using System.IO;

namespace PolyPatternConsole
{
	internal static class Program
	{
		private static void Main()
		{
			// Create pattern.
			ChannelSimple channel1 = new ChannelSimple(7);
			channel1.Set(1, true);
			channel1.Set(3, true);
			channel1.Set(5, true);

			ChannelSimple channel2 = new ChannelSimple(4);
			channel2.Set(0, true);
			channel2.Set(3, true);

			ChannelSimple channel3 = new ChannelSimple(3);
			channel3.Set(2, true);

			PatternSimple patternOut = new PatternSimple(channel1, channel2, channel3);

			// Write pattern bits.
			Console.WriteLine(patternOut);
			File.WriteAllBytes("Test", patternOut.ToBitArray().ToBytes());

			// Read pattern bits.
			PatternSimple patternIn = PatternSimple.FromBitArray(new BitArray(File.ReadAllBytes("Test")));
			Console.WriteLine(patternIn);
		}
	}
}