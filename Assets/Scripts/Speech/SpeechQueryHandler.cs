using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Networking;

using SimpleJSON;

public delegate void OnSpeechQueryFinished( string rawTextResponse, SpeechQueryResponse response );

public class SpeechQueryHandler
{
	public event OnSpeechQueryFinished SpeechQueryFinished;
	
	private const string TEMP_FILE_NAME = "speech_temp.wav";
	
	private AudioClip speechQueryClip;
	private SpeechQueryParams speechQueryParams;
	
	public SpeechQueryHandler( AudioClip speechQueryClip, SpeechQueryParams speechQueryParams )
	{
		this.speechQueryClip = speechQueryClip;
		this.speechQueryParams = speechQueryParams;
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
		form.AddField( Constants.QUERY_KEY, JsonUtility.ToJson( speechQueryParams ) );
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
			response = new SpeechQueryResponse();
			
			Debug.Log( request.text );
			
			rawTextResponse = request.text;
			
			JSONNode node = JSON.Parse( request.text );
			
			response.transcription = node["transcription"].Value;
			response.answer = node["response"]["answer"].Value;
			response.queryType = node["response"]["qa_class"].Value;
			response.targetPlanet = node["response"]["planet"].Value;
		}
		
		if( SpeechQueryFinished != null )
		{
			SpeechQueryFinished( rawTextResponse, response );
		}
	}
}
