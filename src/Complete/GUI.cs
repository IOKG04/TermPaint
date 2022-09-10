using System;
using System.Drawing;
using Pastel;
using TermPaint.Low;
using TermPaint.Base;
using TermPaint.Complete;

//TODO: Add information bits: Toolselection(when-created)

namespace TermPaint.Complete;

/// <summary>GraphicalUserInterface showing layers, layerorder and more</summary>
public class GUI{
	private Image? _Img;
	/// <summary>Pointer to the image the information is taken from</summary>
	public Image Img {get {return _Img;} set {_Img = value;}}
	/// <summary>Pixoids used for painting</summary>
	private Pixoid brush;
	/// <summary>Size of the GUI</summary>
	public Vec2 dimensions;
	/// <summary>Start height of the layer-order information bit</summary>
	public int layerOrderStart;
	/// <summary>Length of the layer-order information bit</summary>
	public int layerOrderLength;
	/// <summary>Object in Img.Layers to start with</summary>
	public int layerOrderListStart;

	public void SetBrush(Pixoid _brush){
		brush = _brush;
	}

	/// <summary>Returns a 1d representation of the GUI</summary>
	public override string ToString(){
		string str =  "";
		string[] strs = ToStrings();
		for(int i = 0; i < strs.Length; i++){
			str += strs[i];
			str += "\n";
		}
		return str;
	}
	/// <summary>Returns a 1*1d representation of the GUI</summary>
	public string[] ToStrings(){
		string[,] strss = ToStringss();
		string[] strs = new string[strss.GetLength(1)];
		for(int y = 0; y < strss.GetLength(1); y++){
			strs[y] = "";
			for(int x = 0; x < strss.GetLength(0); x++){
				strs[y] += strss[x, y];
			}
		}
		return strs;
	}
	/// <summary>Returns a 2*1d representation of the GUI</summary>
	public string[,] ToStringss(){
		//TODO: Add new code as needed for more information bits
		
		string[,] strss = new string[dimensions.x, dimensions.y];

		//Add brush information
		strss[0, 0] = brush.ToString();
		if(strss.GetLength(0) > 0) strss[1, 0] = " ".PastelBg(Color.DarkGray).Pastel(Color.White);
		string colorString = ("#" + brush.text_color.R.ToString("x2") + brush.text_color.G.ToString("x2") + brush.text_color.B.ToString("x2") + " #" + brush.background_color.R.ToString("x2") + brush.background_color.G.ToString("x2") + brush.background_color.B.ToString("x2"));
		for(int x = 2; x < strss.GetLength(0) /*&& x - 2 < colorString.Length*/; x++){
			try{
			strss[x, 0] = colorString[x - 2].ToString().Pastel(Color.White).PastelBg(Color.DarkGray);
			}
			catch{
				strss[x, 0] = " ".Pastel(Color.White).PastelBg(Color.DarkGray);
			}
		}

		//Add layer-order
		for(int x = 0; x < strss.GetLength(0); x++){
			strss[x, layerOrderStart] = "-".Pastel(Color.White).PastelBg(Color.DarkGray);
		}
		for(int y = 1; y < layerOrderLength && y + layerOrderListStart <= Img.Layers.Count; y++){
			for(int x = 0; x < strss.GetLength(0); x++){
				try{
					strss[x, y + layerOrderStart] = Img.Layers[y + layerOrderListStart - 1].name[x].ToString().Pastel(Color.White).PastelBg(Img.Layers[y + layerOrderListStart - 1].visible ? Color.DarkGray : Color.LightGray);
				}
				catch{
					strss[x, y + layerOrderStart] = " ".Pastel(Color.White).PastelBg(Img.Layers[y + layerOrderListStart - 1].visible ? Color.DarkGray : Color.LightGray);
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
		layerOrderStart = 1;
		layerOrderLength = dimensions.y - layerOrderStart;
		layerOrderListStart = 0;
	}
	/// <summary>Creates a new GUI without any further information</summary>
	/// <param name="_dimensions">Size of the GUI</param>
	public GUI(Vec2 _dimensions){
		dimensions = _dimensions;
		layerOrderStart = 1;
		layerOrderLength = dimensions.y - layerOrderStart;
		layerOrderListStart = 0;
	}
	/// <summary>Creates a new GUI using the specified image</summary>
	/// <param name="_Img">Image to use the data of</param>
	public GUI(Image _Img){
		Img = _Img;
		dimensions = new Vec2(0, 2);
		layerOrderStart = 1;
		layerOrderLength = dimensions.y - layerOrderStart;
		layerOrderListStart = 0;
	}
	/// <summary>Creates a new GUI without any further information</summary>
	public GUI(){
		dimensions = new Vec2(0, 2);
		layerOrderStart = 1;
		layerOrderLength = dimensions.y - layerOrderStart;
		layerOrderListStart = 0;
	}
}
