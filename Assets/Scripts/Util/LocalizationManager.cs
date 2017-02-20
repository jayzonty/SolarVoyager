using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class LocalizationManager : MonoBehaviour
{
	public string locale = "en-US";
	
	private Dictionary<string, Messages> messagesCache;
	
	private Messages messagesList;
	public Messages MessagesList
	{
		get
		{
			return messagesList;
		}
	}
	
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
				messagesList = JsonUtility.FromJson<Messages>( textAsset.text );
			}
		}
	}
	
	void Awake()
	{
		messagesCache = new Dictionary<string, Messages>();
		
		LoadLocale( locale );
	}
}
