using System;
using System.Drawing;
using TermPaint.Base;
using TermPaint.Low;
using TermPaint.Complete;

namespace TermPaint;

class Program{
	static void Main(string[] args){
		Image img = new Image(new Vec2(10, 5));
		Layer l0 = new Layer(new Vec2(11, 6));
		Layer l1 = new Layer(new Vec2(3, 2));
		for(int x = 0; x < l0.Dimensions.x; x++){
			for(int y = 0; y < l0.Dimensions.y; y++){
				l0.SetPixoid(x, y, new Pixoid('/', Color.Gold, Color.Black));
			}
		}
		for(int x = 0; x < l1.Dimensions.x; x++){
			for(int y = 0; y < l1.Dimensions.y; y++){
				l1.SetPixoid(x, y, new Pixoid('\\', Color.DarkViolet, Color.White));
			}
		}
		l1.position = new Vec2(2, 1);
		img.AddLayer(l0);
		img.AddLayer(l1);

		Console.WriteLine(img.ToString());
	}
}
