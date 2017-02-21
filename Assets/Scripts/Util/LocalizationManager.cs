using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using SimpleJSON;

public class LocalizationManager
{
	private static string currentLocale = "ja-JP";
	
	private static Dictionary<string, JSONNode> messagesCache;
	private static JSONNode messagesList;
	
	static LocalizationManager()
	{
		messagesCache = new Dictionary<string, JSONNode>();
		
		LoadLocale( currentLocale );
	}
	
	public static string Locale
	{
		get
		{
			return currentLocale;
		}
	}
	
	public static void LoadLocale( string locale )
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
				
				currentLocale = locale;
			}
		}
	}
	
	// Returns the associated message if the key exists in the localization file.
	// Otherwise, returns the key itself.
	public static string GetString( string key )
	{
		if( messagesList[key] != null )
		{
			return messagesList[key];
		}
		
		return key;
	}
}
