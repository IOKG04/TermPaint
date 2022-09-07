using System;
using System.Drawing;
using TermPaint.Base;
using TermPaint.Low;
using TermPaint.Complete;

namespace TermPaint;

class Program{
	static void Main(string[] args){
		Image img = new Image(4);
		for(int i = 0; i < img.Layers.Count; i++){
			img.Layers[i].name = i.ToString();
		}
		for(int i = 0; i < img.Layers.Count; i++){
			Console.WriteLine(img.Layers[i].name);
		}
		img.MoveLayer(3, 1);
		for(int i = 0; i < img.Layers.Count; i++){
			Console.WriteLine(img.Layers[i].name);
		}
		img.MoveLayer(1, 3);
		for(int i = 0; i < img.Layers.Count; i++){
			Console.WriteLine(img.Layers[i].name);
		}
	}
}
