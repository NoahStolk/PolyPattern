using Commons.Music.Midi;
using PolyPattern.Patterns;
using System.IO;

namespace PolyPattern.Midi
{
	public static class MidiWriter
	{
		public static void Write(PatternSimple patternSimple)
		{
			SmfWriter smfWriter = new SmfWriter(new FileStream(@"C:\Users\NOAH\Desktop\test.mid", FileMode.Create));

			MidiTrack track = new MidiTrack();
			track.Messages.Add(new MidiMessage(1, new MidiEvent(2)));

			MidiMusic music = new MidiMusic();
			music.AddTrack(track);

			smfWriter.WriteMusic(music);
		}
	}
}