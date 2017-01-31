using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Networking;

public delegate void OnSpeechQueryFinished( string rawTextResponse, SpeechQueryResponse response );

public class SpeechQueryHandler
{
	public event OnSpeechQueryFinished SpeechQueryFinished;
	
	private const string TEMP_FILE_NAME = "speech_temp.wav";
	
	private AudioClip speechQueryClip;
	
	public SpeechQueryHandler( AudioClip speechQueryClip )
	{
		this.speechQueryClip = speechQueryClip;
	}
	
	public IEnumerator PerformQuery()
	{
		string tempFile = Application.temporaryCachePath + "/" + TEMP_FILE_NAME;
		AudioUtil.SaveAudioClipToFile( speechQueryClip, tempFile );
		
		WWW localFile = new WWW( "file://" + tempFile );
		yield return localFile;
		
		if( !string.IsNullOrEmpty( localFile.error ) )
		{
			Debug.LogError( "RAWR " + localFile.error );
		}
		
		WWWForm form = new WWWForm();
		form.AddField( Constants.QUERY_KEY, "none" );
		form.AddBinaryData( Constants.WAV_FILE_KEY, localFile.bytes, "speech.wav" );
		
		WWW request = new WWW( Constants.SPEECH_QUERY_URL, form );
		yield return request;
		
		SpeechQueryResponse response = null;
		string rawTextResponse = null;
		
		if( !string.IsNullOrEmpty( request.error ) )
		{
			Debug.LogError( request.error );
		}
		else
		{
			Debug.Log( request.text );
			
			rawTextResponse = request.text;
			
			response = JsonUtility.FromJson<SpeechQueryResponse>( rawTextResponse );
		}
		
		if( SpeechQueryFinished != null )
		{
			SpeechQueryFinished( rawTextResponse, response );
		}
	}
}
