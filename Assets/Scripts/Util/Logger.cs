using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Logger
{
	private static string logFileName;
	
	static Logger()
	{
		logFileName = Application.persistentDataPath + "/" + string.Format( "{0}.log", System.DateTime.UtcNow.ToString( "yyyy-MM-dd" ) );
	}
	
	public static void Write( string rawResponse )
	{
		string timeStamp = System.DateTime.UtcNow.ToString( "yyyy-MM-ddTHH:mm:ss" );
		
		using( System.IO.StreamWriter writer = System.IO.File.AppendText( logFileName ) )
		{
			writer.Write( string.Format( "[{0}] {1}\n", timeStamp, rawResponse ) );
			writer.Flush();
		}
	}
}
