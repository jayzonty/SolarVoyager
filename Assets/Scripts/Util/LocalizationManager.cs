using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using SimpleJSON;

public class LocalizationManager : MonoBehaviour
{
	public string locale = "en-US";
	
	private Dictionary<string, JSONNode> messagesCache;
	private JSONNode messagesList;
	
	private static LocalizationManager instance = null;
	public static LocalizationManager Instance
	{
		get
		{
			if( instance == null )
			{
				instance = GameObject.FindObjectOfType<LocalizationManager>();
				if( instance == null )
				{
					GameObject go = new GameObject( "LocalizationManager" );
					instance = go.AddComponent<LocalizationManager>();
				}
			}
			
			return instance;
		}
	}
	
	public void LoadLocale( string locale )
	{
		if( messagesCache.ContainsKey( locale ) )
		{
			messagesList = messagesCache[locale];
		}
		else
		{
			TextAsset textAsset = Resources.Load( "Data/Locales/" + locale ) as TextAsset;
			if( textAsset == null )
			{
				Debug.Log( "[LocalizationManager] Cannot find locale " + locale );
			}
			else
			{
				messagesList = JSON.Parse( textAsset.text );
				messagesCache.Add( locale, messagesList );
			}
		}
	}
	
	public string GetString( string key )
	{
		return messagesList[key].Value;
	}
	
	void Awake()
	{
		messagesCache = new Dictionary<string, JSONNode>();
		
		LoadLocale( locale );
	}
}
