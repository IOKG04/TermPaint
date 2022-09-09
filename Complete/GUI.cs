using System;
using System.Drawing;
using Pastel;
using TermPaint.Low;
using TermPaint.Base;
using TermPaint.Complete;

//TODO: Add information bits: Layer-order Brush(when-created) Toolselection(when-created)
//TODO: Add information bit size parameters

namespace TermPaint.Complete;

/// <summary>GraphicalUserInterface showing layers, layerorder and more</summary>
public class GUI{
	private Image? _Img;
	/// <summary>Pointer to the image the information is taken from</summary>
	public Image Img {get {return _Img;} set {_Img = value; /*Following Tasks*/}}
	/// <summary>Size of the GUI</summary>
	public Vec2 dimensions;
	/// <summary>Start height of the layer-order information bit</summary>
	public int layerOrderStart;
	/// <summary>Length of the layer-order information bit</summary>
	public int layerOrderLength;
	/// <summary>Object in Img.Layers to start with</summary>
	public int layerOrderListStart;

	public string[,] ToStringss(){
		//TODO: Add new code as needed for more information bits
		
		string[,] strss = new string[dimensions.x, dimensions.y];

		//Add Layer-order
		for(int x = 0; x < strss.GetLength(0); x++){
			strss[x, layerOrderStart] = "-".Pastel(Color.White).PastelBg(Color.DarkGray);
		}
		for(int y = 1; y < layerOrderLength && y + layerOrderListStart <= Img.Layers.Count; y++){
			for(int x = 0; x < strss.GetLength(0); x++){
				try{
					strss[x, y + layerOrderListStart] = Img.Layers[y + layerOrderListStart - 1].name[x].ToString().Pastel(Color.White).PastelBg(Img.Layers[y + layerOrderListStart - 1].visible ? Color.DarkGray : Color.LightGray);
				}
				catch{
					strss[x, y + layerOrderListStart] = " ".Pastel(Color.White).PastelBg(Img.Layers[y + layerOrderListStart - 1].visible ? Color.DarkGray : Color.LightGray);
				}
			}
		}
		
		return strss;
	}

	/// <summary>Creates a new GUI using the specified image</summary>
	/// <param name="_Img">Image to use the data of</param>
	/// <param name="_dimensions">Size of the GUI</param>
	public GUI(Image _Img, Vec2 _dimensions){
		Img = _Img;
		dimensions = _dimensions;
		layerOrderStart = 0;
		layerOrderLength = dimensions.y;
		layerOrderListStart = 0;
	}
	/// <summary>Creates a new GUI without any further information</summary>
	/// <param name="_dimensions">Size of the GUI</param>
	public GUI(Vec2 _dimensions){
		dimensions = _dimensions;
		layerOrderStart = 0;
		layerOrderLength = dimensions.y;
		layerOrderListStart = 0;
	}
	/// <summary>Creates a new GUI using the specified image</summary>
	/// <param name="_Img">Image to use the data of</param>
	public GUI(Image _Img){
		Img = _Img;
		dimensions = new Vec2(0, 0);
		layerOrderStart = 0;
		layerOrderLength = dimensions.y;
		layerOrderListStart = 0;
	}
	/// <summary>Creates a new GUI without any further information</summary>
	public GUI(){
		dimensions = new Vec2(0, 0);
		layerOrderStart = 0;
		layerOrderLength = dimensions.y;
		layerOrderListStart = 0;
	}
}
