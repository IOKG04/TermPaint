using System;
using System.Drawing;
using TermPaint.Base;
using TermPaint.Low;
using TermPaint.Complete;

namespace TermPaint;

class Program{
	static void Main(string[] args){
		b classb = new b();
		classb.classa = new a();
		classb.classa.i = 8;
		b classb2 = classb;
		classb.classa.i = 4;
		Console.WriteLine(classb.classa.i);
		Console.WriteLine(classb2.classa.i);
	}
}
class a{
	public int i;
}
class b{
	public a classa;
}
