using System;
using System.Drawing;
using TermPaint.Low;
using TermPaint.Base;
using TermPaint.Complete;

namespace TermPaint.Complete;

/*
Keybinds:
	Key			Action

	v / [Space]		Set pixoid to brush
	x / [Backspace]		Erase pixoid

	j / [Left]		Move cursor left
	k / [Down]		Move cursor down
	l / [Up]		Move cursor up
	[Semicolon] / [Right]	Move cursor right

	b-c			Set brush character
	b-t			Set brush text color
	b-b			Set brush background color
	c			Set pixoid as brush

	i / [End]		Move layer cursor down
	o / [Home]		Move layer cursor up
	u / [Del]		Move layer down in hirachy
	p / [Pg-dn]		Move layer up in hirachy
	7 / [1]			Move layer left
	8 / [2]			Move layer down
	9 / [5]			Move layer up
	0 / [3]			Move layer right
	v			Toggle layer visiblity
	n			Rename layer using Console.ReadLine()

	r-l			Resize layer using Console.ReadLine()
	r-i			Resize image and all layers using Console.ReadLine()
	
	s			Save as file using Console.ReadLine()
	o			Open file using Console.ReadLine()
*/

/// <summary>Part of the program exposed to the user</summary>
public class Shell{
	/// <summary>Image manipulated by this shell</summary>
	private Image img;
}
