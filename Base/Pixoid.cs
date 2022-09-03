using System;
using System.Drawing;
using Pastel;

namespace TermPaint.Base;

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

	/// <summary>Creates an empty pixoid</summary>
	public Pixoid(){
		character = ' ';
		text_color = Color.FromArgb(0, 0, 0, 0);
		background_color = Color.FromArgb(0, 0, 0, 0);
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
}
