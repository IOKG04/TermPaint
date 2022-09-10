using System;
using System.Drawing;
using TermPaint.Base;
using TermPaint.Low;
using TermPaint.Complete;
using TermPaint.IO;

namespace TermPaint;

public class Program{
	static void Main(string[] args){
		Layer l1 = new Layer(10, 10, _name: "Layer 1");
		Layer l2 = new Layer(6, 6, _name: "LLLLLLLLLLLLLLLLLLLLLLLLLLLLLL");
		for(int x = 0; x < 10; x++){
			for(int y = 0; y < 10; y++){
				try{
					l1.SetPixoid(x, y, new Pixoid('\\', Color.White, Color.Red));
					l2.SetPixoid(x, y, new Pixoid('L', Color.Indigo, Color.MidnightBlue));
				}
				catch{
				}
			}
		}
		l2.position = new Vec2(0, 1);
		l1.SetPixoid(3, 8, new Pixoid('A', Color.Crimson, Color.DarkSalmon));

		Image img = new Image(new Vec2(10, 10));
		img.AddLayer(l1);
		img.AddLayer(l2);

		Console.WriteLine(img.ToString());
		byte[] bytes = FileConv.ToData(img);
	}
}
