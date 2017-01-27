using System;
using System.IO;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Networking;

public class NetManager : MonoBehaviour
{
	private string response;
	private string error;
	
	public string Response
	{
		get
		{
			return response;
		}
	}
	
	public string Error
	{
		get
		{
			return error;
		}
	}
	
	void Start()
	{
	}
	
	void Update()
	{
	}
	
	public void DebugSendQuery()
	{
		AudioClip clip = GameObject.FindObjectOfType<SpeechManager>().GetRecentClip();
		Debug.Log( clip.length );
		
		SendSpeechQuery( clip );
	}
	
	public void SendSpeechQuery( AudioClip clip )
	{
		StartCoroutine( SendAudioQuery( clip ) );
	}
	
	private IEnumerator SendAudioQuery( AudioClip clip )
	{
		/*Byte[] clipBytes = AudioUtil.GetBytes( clip );
		
		List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
		formData.Add( new MultipartFormDataSection( "query", "none" ) );
		formData.Add( new MultipartFormFileSection( "query.wav", clipBytes ) );
		
		UnityWebRequest request = UnityWebRequest.Post( Constants.SPEECH_QUERY_URL, formData );
		
		request.downloadHandler = new DownloadHandlerBuffer();
		
		yield return request.Send();
		
		if( request.isError )
		{
			Debug.LogError( request.error );
		}
		else
		{
			Debug.Log( request.downloadHandler.text );
		}*/
		
		string tempFile = Application.temporaryCachePath + "/temp_speech.wav";
		AudioUtil.SaveAudioClipToFile( clip, tempFile );
		
		WWW localFile = new WWW( "file://" + tempFile );
		yield return localFile;
		
		if( !string.IsNullOrEmpty( localFile.error ) )
		{
			Debug.LogError( "RAWR " + localFile.error );
		}
		
		Dictionary<string, string> fields = new Dictionary<string, string>();
		fields[Constants.QUERY_KEY] = "none";
		
		Dictionary<string, byte[]> data = new Dictionary<string, byte[]>();
		data[Constants.WAV_FILE_KEY] = localFile.bytes;
		
		SendQuery( Constants.SPEECH_QUERY_URL, fields, data );
	}
	
	public void SendQuery( string url, Dictionary<string, string> fieldsDictionary )
	{
		SendQuery( url, fieldsDictionary, null );
	}
	
	public void SendQuery( string url, Dictionary<string, string> fieldsDictionary, Dictionary<string, byte[]> dataDictionary )
	{
		WWWForm form = new WWWForm();
		if( fieldsDictionary != null )
		{
			foreach( KeyValuePair<string, string> field in fieldsDictionary )
			{
				form.AddField( field.Key, field.Value );
			}				
		}
		
		if( dataDictionary != null )
		{
			foreach( KeyValuePair<string, byte[]> data in dataDictionary )
			{
				form.AddBinaryData( data.Key, data.Value, "speech.wav" );
			}
		}
		
		StartCoroutine( SendForm( url, form ) );
	}
	
	private IEnumerator SendForm( string url, WWWForm form )
	{
		WWW conn = new WWW( url, form );
		
		yield return conn;
		
		if( !string.IsNullOrEmpty( conn.error ) )
		{
			Debug.LogError( "Connection error: " + conn.error );
		}
		else
		{
			Debug.Log( "Response: " + conn.text );
			
			response = conn.text;
			error = conn.error;
			
			QueryResponse q = JsonUtility.FromJson<QueryResponse>( response );
			if( q.response == null )
			{
				Debug.Log( "WTF" );
			}
			
			TextToSpeech t = GameObject.FindObjectOfType<TextToSpeech>();
			if( t != null )
			{
				t.Synthesize( q.response );
			}
			else
			{
				Debug.Log( "RAWR" );
			}
		}
		
		
	}
}
