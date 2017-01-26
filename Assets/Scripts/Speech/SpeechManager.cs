using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class SpeechManager : MonoBehaviour
{
    public int maxRecordingLength = 60;
	public bool loopRecording = false;

	private int minFrequency;
    private int maxFrequency;
	
    private AudioClip recordingClip = null;
	
	private string deviceName = null;

    public void StartRecording()
    {
        recordingClip = Microphone.Start( deviceName, loopRecording, maxRecordingLength, maxFrequency );
        if( recordingClip == null )
        {
            Debug.LogError( "Recording clip is null!" );
        }
    }

    public void StopRecording()
    {
        Microphone.End( deviceName );
    }

    public AudioClip GetRecentClip()
    {
        return recordingClip;
    }

    public void PlayRecentClip()
    {
        if( recordingClip != null )
        {
            AudioSource audioSource = GameObject.FindObjectOfType<AudioSource>();
            audioSource.clip = recordingClip;
            audioSource.Play();
        }
    }

    // Use this for initialization
    void Start()
    {
        Microphone.GetDeviceCaps( deviceName, out minFrequency, out maxFrequency );
    }

    // Update is called once per frame
    void Update()
    {
    }
}
