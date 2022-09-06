using System;
using System.Drawing;
using TermPaint.Base;
using TermPaint.Low;

namespace TermPaint;

class Program{
	static void Main(string[] args){
		Layer l = new Layer(8, 4);
		int i = 40;
		for(int y = 0; y < l.Dimensions.y; y++){
			for(int x = 0; x < l.Dimensions.x; x++){
				i++;
				l.SetPixoid(x, y, new Pixoid(' ', Color.White, Color.FromKnownColor((KnownColor)i)));
			}
		}
		for(int j = 0; j < l.ToStrings().Length; j++){
			Console.WriteLine(l.ToStrings()[j]);
		}
	}
}
