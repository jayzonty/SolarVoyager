using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechManager : MonoBehaviour
{
    public int maxRecordingLength = 60;

    private AudioClip recordingClip = null;

    private int minFrequency;
    private int maxFrequency;

    public void StartRecording()
    {
        recordingClip = Microphone.Start( null, false, maxRecordingLength, maxFrequency );

        if( recordingClip == null )
        {
            Debug.Log( "AAAAAHHHH Something is wrong with the recording clip!" );
        }
    }

    public void StopRecording()
    {
        Microphone.End( null );
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
        Microphone.GetDeviceCaps( null, out minFrequency, out maxFrequency );
    }

    // Update is called once per frame
    void Update()
    {
    }
}
