using System;
using System.Drawing;
using TermPaint.Base;
using TermPaint.Low;
using TermPaint.Complete;

namespace TermPaint;

class Program{
	static void Main(string[] args){
		Image img = new Image();
		Layer l1 = new Layer(10, 10, _name: "Layer 1");
		Layer l2 = new Layer(6, 6, _name: "Layer 2");
		Layer l3 = new Layer(3, 3, _name: "Layer 3");
		for(int x = 0; x < 10; x++){
			for(int y = 0; y < 10; y++){
				try{
					l1.SetPixoid(x, y, new Pixoid('\\', Color.White, Color.Red));
					l2.SetPixoid(x, y, new Pixoid('|', Color.White, Color.Red));
					l3.SetPixoid(x, y, new Pixoid('/', Color.White, Color.Red));
				}
				catch{
				}
			}
		}
		l3.visible = false;
		img.AddLayer(l1);
		img.AddLayer(l2);
		img.AddLayer(l3);
		img.Layers[2].visible = false;

		Console.WriteLine(img.ToString());

		GUI g = new GUI(img, new Vec2(32, 10));
		g.SetBrush (new Pixoid('/', Color.Tan, Color.FromArgb(0x45, 0x33, 0x01)));
		string[,] strss = g.ToStringss();
		for(int y = 0; y < strss.GetLength(1); y++){
			for(int x = 0; x < strss.GetLength(0); x++){
				Console.Write(strss[x, y]);
			}
			Console.WriteLine();
		}
	}
}
