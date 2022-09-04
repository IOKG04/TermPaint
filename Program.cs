using System;
using TermPaint.Base;
using System.Drawing;

namespace TermPaint;

class Program{
	static void Main(string[] args){
		Pixoid p0 = new Pixoid('I', Color.FromArgb(255, 16, 255, 16), Color.FromArgb(255, 255, 0, 32));
		Pixoid p1 = new Pixoid('J', Color.FromArgb(255, 16, 255, 16), Color.FromArgb(255, 255, 0, 32));
		Pixoid p2 = new Pixoid('I', Color.FromArgb(255, 16, 254, 16), Color.FromArgb(255, 255, 0, 32));
		Pixoid p3 = new Pixoid('I', Color.FromArgb(255, 16, 255, 16), Color.FromArgb(255, 255, 1, 32));

		Console.WriteLine(p0.ToString() + " : " + p0.GetHashCode());
		Console.WriteLine(p1.ToString() + " : " + p1.GetHashCode());
		Console.WriteLine(p2.ToString() + " : " + p2.GetHashCode());
		Console.WriteLine(p3.ToString() + " : " + p3.GetHashCode());
	}
}
