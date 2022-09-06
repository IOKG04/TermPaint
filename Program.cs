using System;
using TermPaint.Base;
using System.Drawing;

namespace TermPaint;

class Program{
	static void Main(string[] args){
		string str = "\x00\x00Me\x00ow\x00";
		Console.WriteLine(str.Length);
		Console.WriteLine('\n');
		for(int i = 0; i < str.Length; i++){
			Console.WriteLine(str[i]);
		}
	}
}
