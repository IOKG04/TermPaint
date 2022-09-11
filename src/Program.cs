using System;
using System.Drawing;
using TermPaint.Base;
using TermPaint.Low;
using TermPaint.Complete;
using TermPaint.IO;

namespace TermPaint;

public class Program{
	static void Main(string[] args){
		Console.Clear();
		Shell sh = new Shell(new Vec2(0, 0));
		sh.Loop();
		Console.Clear();
	}
}
