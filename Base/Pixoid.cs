using System;
using System.Drawing;
using SHA256 = System.Security.Cryptography.SHA256;
using Pastel;

namespace TermPaint.Base;

/// <summary>Smallest amount of renderable data, a collection of a character and two colors</summary>
public struct Pixoid{
	/// <summary>Character of the pixoid</summary>
	public char character;
	/// <summary>Color of the charcter</summary>
	public Color text_color;
	/// <summary>Color of the background</summary>
	public Color background_color;

	/// <summary>Returns the pixoid as a string colored using Pastel</summary>
	public override string ToString(){
		return character.ToString().Pastel(text_color).PastelBg(background_color);
	}
	/// <summary>Get's a unique hascode for the pixoid</summary>
	public override int GetHashCode(){
		return BitConverter.ToInt32(SHA256.HashData(BitConverter.GetBytes((int)character ^ text_color.GetHashCode() ^ (background_color.R ^ background_color.GetHashCode()))));
	}
	/// <summary>Returns true if type and hash are equal</summary>
	public override bool Equals(object? obj){
		return obj != null && obj.GetType() == this.GetType() && obj.GetHashCode() == this.GetHashCode();
	}

	/// <summary>Creates an empty pixoid</summary>
	public Pixoid(){
		character = '\x00';
		text_color = Color.Empty;
		background_color = Color.Empty;
	}
	/// <summary>Creates a pixoid using the given values</summary>
	/// <param name="_character">Character of the pixoid</param>
	/// <param name="_text_color">Color of the character</param>
	/// <param name="_background_color">Color of the background</param>
	public Pixoid(char _character, Color _text_color, Color _background_color){
		character = _character;
		text_color = _text_color;
		background_color = _background_color;
	}

	/// <summary>Return true if character, text_color and background_color are equa</summary>
	public static bool operator ==(Pixoid a, Pixoid b){
		return a.character == b.character && a.text_color == b.text_color && a.background_color == b.background_color;
	}
	/// <summary>Returns false if character, text_color and background_color are equal</summary>
	public static bool operator !=(Pixoid a, Pixoid b){
		return !(a == b);
	}

	/// <summary>Null equivalent for pixoids</summary>
	public static Pixoid Empty = new Pixoid();
}
