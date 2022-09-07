using System;
using System.Collections.Generic;
using TermPaint.Base;

//TODO: Add rendering with ToString() ToStrings()
//TODO: Add MoveLayer(int, int)

namespace TermPaint.Complete;

/// <summary>Collection of layers with the ablility to render</summary>
public class Image{
	private List<Layer>? _Layers;
	/// <summary>Collection of layers</summary>
	public List<Layer> Layers {get{return _Layers;} private set{_Layers = value;}}

	/// <summary>Merges this image with the specified image</summary>
	/// <param name="img">Image to be merged with</param>
	/// <param name="newOnTop">Whether or not to put img at the top of this image</param>
	public void MergeWith(Image img, bool newOnTop = true){
		if(newOnTop) this.Layers = Image.Merge(this, img).Layers;
		else this.Layers = Image.Merge(img, this).Layers;
	}
	/// <summary>Adds an empty layer</summary>
	public void AddLayer(){
		Layers.Add(new Layer());
	}
	/// <summary>Adds a layer to this image</summary>
	/// <param name="l">Layer to be added</param>
	public void AddLayer(Layer l){
		Layers.Add(l);
	}

	/// <summary>Deleted the specified layer</summary>
	/// <param name="i">Index of the layer to be deleted</param>
	public void DeleteLayer(int i){
		Layers.RemoveAt(i);
	}
	/// <summary>Switches the specified layers</summary>
	/// <param name="a">Index of the first layer</param>
	/// <param name="b">Index of the second layer</param>
	public void SwitchLayer(int a, int b){
		Layer l = new Layer(Layers[a]);
		Layers[a] = new Layer(Layers[b]);
		Layers[b] = l;
		
	}
	public void MoveLayer(int position, int newPosition){
		if(position > newPosition){
			for(int i = position; i > newPosition; i--){
				SwitchLayer(i, i - 1);
			}
		}
		else{
			for(int i = position; i < newPosition; i++){
				SwitchLayer(i, i + 1);
			}
		}
	}

	/// <summary>Creates a new  based on an array of layers</summary>
	/// <param name="original">Array of layers copied from</param>
	public Image(List<Layer> original){
		Layers = original;
	}
	/// <summary>Creates a new image with a specified number of layers</summary>
	/// <param name="i">Number of layers the image containes</param>
	public Image(int i){
		Layers = new List<Layer>();
		for(int j = 0; j < i; j++){
			Layers.Add(Layer.Empty);
		}
	}
	/// <summary>Creates a copy of the image given</summary>
	/// <param name="original">Image to be copied from</param>
	public Image(Image original){
		Layers = original.Layers;
	}
	/// <summary>Creates an empty image</summary>
	public Image(){
		Layers = new List<Layer>();
	}

	/// <summary>Null equivalent for images</summary>
	public static Image Empty {get {return new Image();}}

	/// <summary>Merges two images into one</summary>
	/// <param name="a">Lower image</param>
	/// <param name="b">Higher image</param>
	public static Image Merge(Image a, Image b){
		Image img = new Image(a);
		for(int i = 0; i < b.Layers.Count; i++){
			img.AddLayer(b.Layers[i]);
		}
		return img;
	}
}
