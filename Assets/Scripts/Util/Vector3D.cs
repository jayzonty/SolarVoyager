using UnityEngine;

using System;

[System.Serializable]
public class Vector3D
{
	public double x;
	public double y;
	public double z;
	
	public Vector3D() : this( 0.0, 0.0, 0.0 )
	{}
	
	public Vector3D( Vector3D v ) : this( v.x, v.y, v.z )
	{}
	
	public Vector3D( double x, double y, double z )
	{
		this.x = x;
		this.y = y;
		this.z = z;
	}
	
	public Vector3D( Vector3 v )
	{
		this.x = (double)v.x;
		this.y = (double)v.y;
		this.z = (double)v.z;
	}
	
	public double Magnitude
	{
		get
		{
			return Math.Sqrt( x * x + y * y + z * z );
		}
	}
	
	public void Normalize()
	{
		double magnitude = this.Magnitude;
		
		x /= magnitude;
		y /= magnitude;
		z /= magnitude;
	}
	
	public Vector3D Normalized
	{
		get
		{
			Vector3D ret = new Vector3D( this );
			ret.Normalize();
			
			return ret;
		}
	}
	
	public static Vector3D operator+( Vector3D a, Vector3D b )
	{
		return new Vector3D( a.x + b.x, a.y + b.y, a.z + b.z );
	}
	
	public static Vector3D operator-( Vector3D a, Vector3D b )
	{
		return new Vector3D( a.x - b.x, a.y - b.y, a.z - b.z );
	}
	
	public static Vector3D operator-( Vector3D a )
	{
		return new Vector3D( -a.x, -a.y, -a.z );
	}
	
	public static Vector3D operator*( Vector3D v, double s )
	{
		return new Vector3D( v.x * s, v.y * s, v.z * s );
	}
	
	public static Vector3D operator/( Vector3D v, double s )
	{
		return new Vector3D( v.x / s, v.y / s, v.z / s );
	}
	
	public override string ToString()
	{
		return String.Format( "{0}, {1}, {2}", x, y, z );
	}
}
