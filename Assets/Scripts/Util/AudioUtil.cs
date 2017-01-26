using System;

using UnityEngine;

public class AudioUtil
{
	// Wrapper function for the Save method in SavWav.cs
	public static void SaveAudioClipToFile( AudioClip clip, string fileName )
	{
		SavWav.Save( fileName, clip );
	}
}
