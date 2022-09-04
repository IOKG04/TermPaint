using System;

namespace TermPaint.Base;

//TODO: Add pixoid getter and setter
//TODO: Add pixoid_data updater (resizes pixoid_data without losing data)

/// <summary>Collection of pixoids</summary>
public class Layer{
	/// <summary>Width of the layer</summary>
	public int Width {get{return Width;} private set{Width = value;}}
	/// <summary>Height of the layer</summary>
	public int Height {get{return Height;} private set{Height = value;}}
	/// <summary>Array containing the pixoid data</summary>
	private Pixoid[,] pixoid_data;

	/// <summary>Creates a layer with the specified height and width</summary>
	/// <param name="_Height">Height of the layer</param>
	/// <param name="_Width">Width of the layer</param>
	public Layer(int _Height, int _Width){
		pixoid_data = new Pixoid[_Width, _Height];
		Width = _Width;
		Height = _Height;
	}
	/// <summary>Creates a layer with the specified pixoid data</summary>
	/// <param name="_pixoid_data">Pixoid data of the layer</param>
	public Layer(Pixoid[,] _pixoid_data){
		pixoid_data = _pixoid_data;
		Width = _pixoid_data.GetLength(0);
		Height = _pixoid_data.GetLength(1);
	}
	/// <summary>Creates an empty layer</summary>
	public Layer(){
		pixoid_data = new Pixoid[0, 0];
		Width = 0;
		Height = 0;
	}

	/// <summary>Null equivalent for layers</summary>
	public static Layer Empty = new Layer();
}
