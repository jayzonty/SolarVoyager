using System.IO;
using System.Text;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Networking;

public class TextToSpeech : MonoBehaviour
{
	private AudioClip recentClip = null;
	
	public AudioClip RecentClip
	{
		get
		{
			return recentClip;
		}
	}
	
	public enum VoiceType
	{
		Male, Female
	}
	
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
	
	public string bingSpeechApiKey = "e54ef2dca66a41f88c92d8cd4b592c13";
	
	public static readonly string SSML_TEMPLATE = "<speak version='1.0' xml:lang='en-us'><voice xml:lang='{0}' xml:gender='{1}' name='{2}'>{3}</voice></speak>";
	
	public string locale = "en-US";
	public VoiceType voiceType = VoiceType.Female;
	public string voiceName = "Microsoft Server Speech Text to Speech Voice (en-US, ZiraRUS)";
	
	private string accessToken;
	
	private string testText = "Hello! My name is Cortana.";
	
	public void DebugTextToSpeech()
	{
		//StartCoroutine( RetrieveAuthToken() );
		StartCoroutine( SynthesizeText( testText ) );
	}
	
	public void Synthesize( string text )
	{
		StartCoroutine( SynthesizeText( text ) );
	}
	
	// Use this for initialization
	void Start()
	{
	}
	
	// Update is called once per frame
	void Update()
	{	
	}
	
	IEnumerator RetrieveAuthToken()
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
	
	IEnumerator SynthesizeText( string text )
	{
		yield return RetrieveAuthToken();
		
		Debug.Log( "Connecting to speech service..." );
		
		//UnityWebRequest request = new UnityWebRequest( SERVICE_URL );
		UnityWebRequest request = UnityWebRequest.GetAudioClip( SERVICE_URL, AudioType.WAV );
		request.method = UnityWebRequest.kHttpVerbPOST;
		
		string content = string.Format( SSML_TEMPLATE, locale, voiceType.ToString(), voiceName, text );
		
		UploadHandler uploadHandler = new UploadHandlerRaw( Encoding.ASCII.GetBytes( content ) );
		uploadHandler.contentType = "application/ssml+xml";
		request.uploadHandler = uploadHandler;
		
		//request.downloadHandler = new DownloadHandlerBuffer();
		
		request.SetRequestHeader( OUTPUT_FORMAT_HEADER_KEY, "riff-16khz-16bit-mono-pcm" );
		request.SetRequestHeader( APP_ID_HEADER_KEY, "0A00E2D44CD541999CF622B7C8B1D036" );
		request.SetRequestHeader( CLIENT_ID_HEADER_KEY, SystemInfo.deviceUniqueIdentifier );
		request.SetRequestHeader( "Authorization", "Bearer " + accessToken );
		//request.SetRequestHeader( USER_AGENT_HEADER_KEY, "SolarVoyager" );
		
		yield return request.Send();
		
		if( request.isError )
		{
			Debug.LogError( request.error );
		}
		else
		{
			Debug.Log( "Response code: " + request.responseCode );
			Debug.Log( "Content-Type: " + request.GetResponseHeader( "Content-Type" ) );
			
			recentClip = DownloadHandlerAudioClip.GetContent( request );
			
			AudioSource audioSource = GameObject.FindObjectOfType<AudioSource>();
            audioSource.clip = recentClip;
            audioSource.Play();
		}
	}
}
