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
		for(int x = 0; x < 10; x++){
			for(int y = 0; y < 10; y++){
				try{
					l1.SetPixoid(x, y, new Pixoid('\\', Color.White, Color.Red));
				}
				catch{
				}
			}
		}
		l1.SetPixoid(3, 7, new Pixoid('A', Color.Crimson, Color.DarkSalmon));

		Console.WriteLine(l1.name);
		Console.Write(l1.ToString());
		Console.WriteLine(l1.visible);
		Console.WriteLine(l1.Dimensions.x);
		Console.WriteLine(l1.Dimensions.y);
		Console.WriteLine();

		byte[] b = FileConv.ToData(l1);
		Layer l2 = FileConv.ToLayer(b);

		Console.WriteLine(l2.name);
		Console.WriteLine(l2.ToString());
		Console.WriteLine(l2.visible);
		Console.WriteLine(l2.Dimensions.x);
		Console.WriteLine(l2.Dimensions.y);
	}
}
