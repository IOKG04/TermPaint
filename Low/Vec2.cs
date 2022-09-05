using System;
using SHA256 = System.Security.Cryptography.SHA256;

namespace TermPaint.Low;

/// <summary>Two dimensional vector</summary>
public struct Vec2{
	/// <summary>X size of the vec2</summary>
	public int x;
	/// <summary>Y size of the vec2</summary>
	public int y;
	/// <summary>Magnitude of the vec2</summary>
	public double Magnitude {get{return Math.Sqrt(x * x + y * y);}}

	/// <summary>Returns the hashcode for this vec2</summary>
	public override int GetHashCode(){
		return BitConverter.ToInt32(SHA256.HashData(BitConverter.GetBytes(x ^ y + ((x << 3) * (y << -2)))));
	}
	/// <summary>Returns true if the type and hash are equal</summary>
	/// <param name="obj">The object to be compaared against</param>
	public override bool Equals(object? obj){
		return obj != null && obj.GetType() == this.GetType() && obj.GetHashCode() == this.GetHashCode();
	}

	/// <summary>Creates a vec2 with the specified sizes</summary>
	public Vec2(int _x, int _y){
		x = _x;
		y = _y;
	}
	/// <summary>Creates a vec2 with the sizes 0, 0</summary>
	public Vec2(){
		x = 0;
		y = 0;
	}

	public static bool operator ==(Vec2 a, Vec2 b){
		return a.x == b.x && a.y == b.y;
	}
	public static bool operator !=(Vec2 a, Vec2 b){
		return !(a == b);
	}
}
