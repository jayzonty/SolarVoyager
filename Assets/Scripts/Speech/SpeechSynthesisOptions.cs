using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct SpeechSynthesisOptions
{
	public enum VoiceType
	{
		Male, Female
	}
	
	// Voice type of the speech synthesizer
	public VoiceType voiceType;
	
	// Language of the speech synthesizer
	public string locale;
	
	// Name of the voice pack to use
	//public string voiceName;
}
