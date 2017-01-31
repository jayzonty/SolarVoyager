using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class SpeechRecording
{
	// Max recording length in seconds.
    private const int MAX_RECORDING_LENGTH = 5;
	
	// Do not record beyond the max recording length.
	private const bool SHOULD_RECORDING_LOOP = false;
	
	// Due to the limitation of the speech recognition system,
	// recording frequency is set to 16000.
	private static int recordingFrequency = 16000;
	
	// Just in case, keep the minimum and maximum
	// possible recording frequencies.
	private static int minRecordingFrequency;
	private static int maxRecordingFrequency;
	
	// Keep track of recently recorded audio clip.
    private static AudioClip recordingClip = null;
	
	// Initially, just use the default microphone,
	// which is represented by a null.
	private static string deviceName = null;
	
	// Static constructor
	static SpeechRecording()
	{
		// Get the properties of the current microphone device.
		Microphone.GetDeviceCaps( deviceName, out minRecordingFrequency, out maxRecordingFrequency );
	}

	// Start recording using the current recording device.
	// Calls Microphone.Start()
    public static void StartRecording()
    {
		// Try to start recording using the parameters.
		// If recordingClip is null, something went wrong.
		recordingClip = Microphone.Start( deviceName, SHOULD_RECORDING_LOOP, MAX_RECORDING_LENGTH, recordingFrequency );
        if( recordingClip == null )
        {
            Debug.LogError( "Recording clip is null!" );
        }
    }

	// Stop recording from the current device.
	// Calls Microphone.End()
    public static void StopRecording()
    {
        Microphone.End( deviceName );
		if( Microphone.IsRecording( deviceName ) )
		{
			Debug.LogWarning( "Microphone is still recording even after Microphone.End()." );
		}
    }
	
	// Returns true if the microphone is still recording.
	public static bool IsRecording()
	{
		return Microphone.IsRecording( deviceName );
	}

	// Gets the most recent recording.
    public static AudioClip GetMostRecentClip()
    {
        return recordingClip;
    }
}
