using System.IO;
using System.Text;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Networking;

public delegate void OnSpeechSynthesisFinished( string transcription, AudioClip clip );

public class SpeechSynthesisHandler
{
	public event OnSpeechSynthesisFinished SpeechSynthesisFinished;
	
	private static readonly string AUTH_TOKEN_URL = "https://api.cognitive.microsoft.com/sts/v1.0/issueToken";
	private static readonly string SERVICE_URL = "https://speech.platform.bing.com/synthesize";
	
	// Header keys
	public static readonly string SUBSCRIPTION_HEADER_KEY = "Ocp-Apim-Subscription-Key";
	public static readonly string OUTPUT_FORMAT_HEADER_KEY = "X-Microsoft-OutputFormat";
	public static readonly string APP_ID_HEADER_KEY = "X-Search-AppId";
	public static readonly string CLIENT_ID_HEADER_KEY = "X-Search-ClientId";
	public static readonly string USER_AGENT_HEADER_KEY = "User-Agent";
	
	// Parameter keys
	public static readonly string VOICE_TYPE_PARAM_KEY = "VoiceType";
	public static readonly string VOICE_NAME_PARAM_KEY = "VoiceName";
	public static readonly string LOCALE_PARAM_KEY = "Locale";
	public static readonly string OUTPUT_FORMAT_PARAM_KEY = "OutputFormat";
	public static readonly string AUTH_TOKEN_PARAM_KEY = "AuthorizationToken";
	public static readonly string TEXT_PARAM_KEY = "Text";
	
	public static readonly string SERVICE_NAME_TEMPLATE = "Microsoft Server Speech Text to Speech Voice ({0})";
	
	public string bingSpeechApiKey = "e54ef2dca66a41f88c92d8cd4b592c13";
	
	public static readonly string SSML_TEMPLATE = "<speak version=\"1.0\" xml:lang=\"en-US\"><voice xml:lang=\"{0}\" xml:gender=\"{1}\" name=\"{2}\">{3}</voice></speak>";
	
	private SpeechSynthesisOptions synthesisOptions;
	
	private string accessToken;
	
	public SpeechSynthesisHandler( SpeechSynthesisOptions options )
	{
		synthesisOptions = options;
	}
	
	public IEnumerator Synthesize( string text )
	{
		yield return RetrieveAuthToken();
		
		Debug.Log( "Connecting to speech service..." );
		
		//UnityWebRequest request = new UnityWebRequest( SERVICE_URL );
		UnityWebRequest request = UnityWebRequest.GetAudioClip( SERVICE_URL, AudioType.WAV );
		request.method = UnityWebRequest.kHttpVerbPOST;
		
		string voiceName = GetServiceName();
		
		string content = string.Format( SSML_TEMPLATE, synthesisOptions.locale, synthesisOptions.voiceType.ToString(), voiceName, text );
		
		UploadHandler uploadHandler = new UploadHandlerRaw( Encoding.UTF8.GetBytes( content ) );
		uploadHandler.contentType = "application/ssml+xml";
		request.uploadHandler = uploadHandler;
		
		request.SetRequestHeader( OUTPUT_FORMAT_HEADER_KEY, "riff-16khz-16bit-mono-pcm" );
		request.SetRequestHeader( APP_ID_HEADER_KEY, "0A00E2D44CD541999CF622B7C8B1D036" );
		request.SetRequestHeader( CLIENT_ID_HEADER_KEY, SystemInfo.deviceUniqueIdentifier );
		request.SetRequestHeader( "Authorization", "Bearer " + accessToken );
		//request.SetRequestHeader( USER_AGENT_HEADER_KEY, "SolarVoyager" );
		
		yield return request.Send();
		
		AudioClip speechClip = null;
		
		if( request.isError )
		{
			Debug.LogError( request.error );
		}
		else
		{
			Debug.Log( "Response code: " + request.responseCode );
			Debug.Log( "Content-Type: " + request.GetResponseHeader( "Content-Type" ) );
			
			speechClip = DownloadHandlerAudioClip.GetContent( request );
		}
		
		if( SpeechSynthesisFinished != null )
		{
			SpeechSynthesisFinished( text, speechClip );
		}
	}
	
	private IEnumerator RetrieveAuthToken()
	{
		UnityWebRequest request = new UnityWebRequest( AUTH_TOKEN_URL );
		request.method = UnityWebRequest.kHttpVerbPOST;
		request.SetRequestHeader( SUBSCRIPTION_HEADER_KEY, bingSpeechApiKey );
		request.downloadHandler = new DownloadHandlerBuffer();
		
		yield return request.Send();
		
		if( request.isError )
		{
			Debug.LogError( request.error );
		}
		else
		{
			Debug.Log( "Successfully received token!" );
			
			accessToken = request.downloadHandler.text;
		}
	}
	
	private string GetServiceName()
	{
		// TODO: Make this more elegant, like a JSON file for the different
		// service names.
		string param = synthesisOptions.locale;
		
		if( synthesisOptions.locale.Equals( "en-US" ) )
		{
			param += ", ";
			if( synthesisOptions.voiceType == SpeechSynthesisOptions.VoiceType.Male )
			{
				param += "BenjaminRUS";
			}
			else
			{
				param += "ZiraRUS";
			}
		}
		else if( synthesisOptions.locale.Equals( "ja-JP" ) )
		{
			param += ", ";
			if( synthesisOptions.voiceType == SpeechSynthesisOptions.VoiceType.Male )
			{
				param += "Ichiro, Apollo";
			}
			else
			{
				param += "Ayumi, Apollo";
			}
		}
		
		string serviceName = string.Format( SERVICE_NAME_TEMPLATE, param );
		return serviceName;
	}
}
