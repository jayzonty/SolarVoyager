using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants
{
	public static readonly string[] PLANET_KEYS = { "mercury", "venus", "earth", "mars", "jupiter", "saturn", "uranus", "neptune" };
	public static readonly string[] ATTRIBUTE_KEYS = { "temperature", "size", "rotation", "revolution" };
	
	// Speech related constants
	public static readonly string SPEECH_QUERY_URL = "ahcclm04:5000/";
	
	public static readonly string WAV_FILE_KEY = "wavfile";
	public static readonly string QUERY_KEY = "query";
	
	public static readonly int RECORDING_FREQ = 16000;
}
