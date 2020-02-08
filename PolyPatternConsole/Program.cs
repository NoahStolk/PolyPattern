using PolyPattern;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace PolyPatternConsole
{
	internal static class Program
	{
		private static void Main()
		{
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
			OutputPattern(patternOut);

			BitArray bitArray = patternOut.ToBitArray();
			for (int i = 0; i < bitArray.Length; i++)
			{
				int hOffset = i - 2;
				if (i == 2 || (hOffset % 4 == 0 && hOffset / 4 < patternOut.Channels.Length))
					Console.Write(" ");

				List<int> noteSeparators = new List<int> { 0 };
				int sep = 0;
				foreach (ChannelSimple channel in patternOut.Channels)
				{
					sep += channel.Notes.Length;
					noteSeparators.Add(sep);
				}
				int nOffset = hOffset - patternOut.Channels.Length * 4;
				if (noteSeparators.Any(s => s == nOffset))
					Console.Write("|");

				Console.Write(bitArray.Get(i) ? "1" : "0");
			}

			Console.WriteLine();
		}

		private static void OutputPattern(PatternSimple patternIn)
		{
			Console.WriteLine(string.Join("\n", patternIn.Channels.Select(c => $"{c.Length}: {string.Join("", c.Notes.Select(n => n ? "x" : "-"))}")));
			Console.WriteLine();
		}
	}
}