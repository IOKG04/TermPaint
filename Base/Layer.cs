using System;
using SHA256 = System.Security.Cryptography.SHA256;
using TermPaint.Low;

namespace TermPaint.Base;

//TODO: Add equals and getHashCode

/// <summary>Collection of pixoids</summary>
public class Layer{
	/// <summary>Dimensions of the layer</summary>
	public Vec2 Dimensions {get{return Dimensions;} private set{Dimensions = value; UpdatePixoid_data();}}
	/// <summary>Array containing the pixoid data</summary>
	private Pixoid[,] pixoid_data;

	/// <summary>Set's pixoid data to new dimensions</summary>
	private void UpdatePixoid_data(){
		Pixoid[,] new_pixoid_data = new Pixoid[Dimensions.x, Dimensions.y];
		for(int x = 0; x < new_pixoid_data.GetLength(0) && x < pixoid_data.GetLength(0); x++){
			for(int y = 0; y < new_pixoid_data.GetLength(1) && y < pixoid_data.GetLength(1); y++){
				new_pixoid_data[x, y] = pixoid_data[x, y];
			}
		}
		pixoid_data = new_pixoid_data;
	}	
	/// <summary>Returns the pixoid at the specified position</summary>
	/// <param name="x">X position of the pixoid gotten</param>
	/// <param name="y">Y position of the pixoid gotten</param>
	public Pixoid GetPixoid(int x, int y){
		return pixoid_data[x, y];
	}
	/// <summary>Returns the pixoid at the specified position</summary>
	/// <param name="position">Position of the pixoid gotten</param>
	public Pixoid GetPixoid(Vec2 position){
		return pixoid_data[position.x, position.y];
	}
	/// <summary>Set's the Pixoid at the specified position</summary>
	/// <param name="x">X position of the pixoid changed</param>
	/// <param name="y">Y position of the pixoid changed</param>
	/// <param name="p">New value of the pixoid changed</param>
	public void SetPixoid(int x, int y, Pixoid p){
		pixoid_data[x, y] = p;
	}
	/// <summary>Set's the Pixoid at the specified position</summary>
	/// <param name="position">Position of the pixoid changed</param>
        /// <param name="p">New value of the pixoid changed</param>
	public void SetPixoid(Vec2 position, Pixoid p){
		pixoid_data[position.x, position.y] = p;
	}

	/// <summary>Returns the hash of this layer</summary>
	public override int GetHashCode(){
		return BitConverter.ToInt32(SHA256.HashData(BitConverter.GetBytes(Dimensions.GetHashCode() ^ pixoid_data.GetHashCode())));
	}
	/// <summary>Returns true if type and hash are equal</summary>
	/// <param name="obj">Object to be compared against</param>
	public override bool Equals(object? obj){
		return obj != null && obj.GetType() == this.GetType() && obj.GetHashCode() == this.GetHashCode();
	}

	/// <summary>Creates a layer with the specified height and width</summary>
	/// <param name="_Width">Width of the layer</param>
	/// <param name="_Height">Height of the layer</param>
	public Layer(int _Width, int _Height){
		pixoid_data = new Pixoid[_Width, _Height];
		Dimensions = new Vec2(_Width, _Height);
	}
	/// <summary>Creates a layer with the specified dimensions</summary>
	/// <param name="_Dimensions">Dimensions of the layer</param>
	public Layer(Vec2 _Dimensions){
		pixoid_data = new Pixoid[_Dimensions.x, _Dimensions.y];
		Dimensions = _Dimensions;
	}
	/// <summary>Creates a layer with the specified pixoid data</summary>
	/// <param name="_pixoid_data">Pixoid data of the layer</param>
	public Layer(Pixoid[,] _pixoid_data){
		pixoid_data = _pixoid_data;
		Dimensions = new Vec2(pixoid_data.GetLength(0), pixoid_data.GetLength(1));
	}
	/// <summary>Creates an empty layer</summary>
	public Layer(){
		pixoid_data = new Pixoid[0, 0];
		Dimensions = new Vec2();
	}

	/// <summary>Null equivalent for layers</summary>
	public static Layer Empty = new Layer();
}
