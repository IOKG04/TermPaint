using System;
using System.Drawing;
using System.IO;
using Pastel;
using TermPaint.Low;
using TermPaint.Base;
using TermPaint.Complete;
using TermPaint.IO;

namespace TermPaint.Complete;

/*
Keybinds:
	Key			Action

#	[Space]			Set pixoid to brush
#	x / [Backspace]		Erase pixoid

#	j / [Left]		Move cursor left
#	k / [Down]		Move cursor down
#	l / [Up]		Move cursor up
#	[Semicolon] / [Right]	Move cursor right
#	[Single Quote]		Toggle cursor visible

#	b-c			Set brush character
#	b-t			Set brush text color
#	b-b			Set brush background color
#	c			Set pixoid as brush

#	i / [End]		Move layer cursor down
#	o / [Home]		Move layer cursor up
#	u / [Del]		Move layer down in hirachy (less impactful)
#	p / [Pg-dn]		Move layer up in hirachy (more impactful)
*	7 / [1]			Move layer left
*	8 / [2]			Move layer down
*	9 / [5]			Move layer up
*	0 / [3]			Move layer right
#	v			Toggle layer visiblity
#	n			Rename layer using Console.ReadLine()
#	t / [Ins]		Create new layer with highest priority
#	g / [Pg-up]		Deletes layer

*	r-l			Resize layer using Console.ReadLine()
*	r-i			Resize image and all layers using Console.ReadLine()
	
*	s			Save as file using Console.ReadLine()
*	r			Open file using Console.ReadLine()

#	q			Quit program
*/

/// <summary>Part of the program exposed to the user</summary>
public class Shell{
	/// <summary>Image manipulated by this shell</summary>
	private Image img;
	/// <summary>GUI manipulated by this shell</summary>
	private GUI gui;
	/// <summary>Current cursor position</summary>
	private Vec2 cursorPosition;
	/// <summary>Current brush</summary>
	private Pixoid brush;
	/// <summary>Current layer cursor position</summary>
	private int layerCursorPosition;
	/// <summary>
	/// 0: Normal mode
	/// 1: b pressed prev
	/// 2: r pressed prev
	/// </summary>
	private int mode;
	/// <summary>GeneralBackgroundColor</summary>
	private Color gbc;

	public void Loop(){
		bool running = true;
		bool cursorVisible = true;

		while(running){
			//Update gui
			gui.SetBrush(brush);
			gui.selectedLayer = layerCursorPosition;

			//Print current state
			string[] imgStr = img.ToStrings();
			string[] guiStr = gui.ToStrings();
			for(int i = 0; i < img.dimensions.y; i++){
				Console.SetCursorPosition(0, i);
				Console.Write(imgStr[i]);
				Console.SetCursorPosition(img.dimensions.x, i);
				Console.Write(guiStr[i]);
			}

			if(cursorVisible) Console.SetCursorPosition(cursorPosition.x, cursorPosition.y);

			//Get input
			ConsoleKeyInfo ckInfo = Console.ReadKey(true);

			//Normal mode
			if(mode == 0){
				switch(ckInfo.Key){
					//Paint
					case ConsoleKey.Spacebar:
						img.Layers[layerCursorPosition].SetPixoid(cursorPosition, brush);
						break;

					//Erease
					case ConsoleKey.X:
						img.Layers[layerCursorPosition].SetPixoid(cursorPosition, layerCursorPosition == 0 ? new Pixoid(' ', Color.Black, gbc) : Pixoid.Empty);
						break;
					case ConsoleKey.Backspace:
						img.Layers[layerCursorPosition].SetPixoid(cursorPosition, layerCursorPosition == 0 ? new Pixoid(' ', Color.Black, gbc) : Pixoid.Empty);
						break;
	
					//Move cursor left
					case ConsoleKey.J:
						cursorPosition.x--;
						if(cursorPosition.x < 0) cursorPosition.x = img.dimensions.x - 1;
						break;
					case ConsoleKey.LeftArrow:
						cursorPosition.x--;
						if(cursorPosition.x < 0) cursorPosition.x = img.dimensions.x - 1;
						break;

					//Move cursor down
					case ConsoleKey.K:
						cursorPosition.y++;
						if(cursorPosition.y >= img.dimensions.y) cursorPosition.y = 0;
						break;
					case ConsoleKey.DownArrow:
						cursorPosition.y++;
						if(cursorPosition.y >= img.dimensions.y) cursorPosition.y = 0;
						break;

					//Move cursor up
					case ConsoleKey.L:
						cursorPosition.y--;
						if(cursorPosition.y < 0) cursorPosition.y = img.dimensions.y - 1;
						break;
					case ConsoleKey.UpArrow:
						cursorPosition.y--;
						if(cursorPosition.y < 0) cursorPosition.y = img.dimensions.y - 1;
						break;

					//Move cursor right
					//-> default
					case ConsoleKey.RightArrow:
						cursorPosition.x++;
						if(cursorPosition.x >= img.dimensions.x) cursorPosition.x = 0;
						break;

					//Toggle cursor visible
					//-> default

					//Enable b mode
					case ConsoleKey.B:
						mode = 1;
						break;

					//Set pixoid to brush
					case ConsoleKey.C:
						brush = img.Layers[layerCursorPosition].GetPixoid(cursorPosition);
						break;

					//Move layer cursor down
					case ConsoleKey.I:
						layerCursorPosition++;
						if(layerCursorPosition >= img.Layers.Count) layerCursorPosition = 0;
						break;
					case ConsoleKey.End:
						layerCursorPosition++;
						if(layerCursorPosition >= img.Layers.Count) layerCursorPosition = 0;
						break;

					//Move layer cursor up
					case ConsoleKey.O:
						layerCursorPosition--;
						if(layerCursorPosition < 0) layerCursorPosition = img.Layers.Count - 1;
						break;
					case ConsoleKey.Home:
						layerCursorPosition--;
						if(layerCursorPosition < 0) layerCursorPosition = img.Layers.Count - 1;
						break;

					//Move layer down in hirachy (less impactful)
					case ConsoleKey.U:
						if(layerCursorPosition <= 0) break;
						img.SwitchLayer(layerCursorPosition, layerCursorPosition - 1);
						layerCursorPosition--;
						break;
					case ConsoleKey.Delete:
						if(layerCursorPosition <= 0) break;
						img.SwitchLayer(layerCursorPosition, layerCursorPosition - 1);
						layerCursorPosition--;
						break;

					//Move layer up in hirachy (more impactful)
					case ConsoleKey.P:
						if(layerCursorPosition >= img.Layers.Count - 1) break;
						img.SwitchLayer(layerCursorPosition, layerCursorPosition + 1);
						layerCursorPosition++;
						break;
					case ConsoleKey.PageDown:
						if(layerCursorPosition >= img.Layers.Count - 1) break;
						img.SwitchLayer(layerCursorPosition, layerCursorPosition + 1);
						layerCursorPosition++;
						break;

					//Toggle layer visibility
					case ConsoleKey.V:
						img.Layers[layerCursorPosition].visible = ! img.Layers[layerCursorPosition].visible;
						break;

					//Rename layer using Console.ReadLine()
					case ConsoleKey.N:
						Console.Write("New name: (No input to not rename) ".Pastel(Color.White).PastelBg(Color.Black));
						string newName = Console.ReadLine();
						if(newName == "") break;
						img.Layers[layerCursorPosition].name = newName;
						break;

					//Create new layer at highest priority
					case ConsoleKey.T:
						img.AddLayer(new Layer(img.dimensions));
						break;
					case ConsoleKey.Insert:
						img.AddLayer(new Layer(img.dimensions));
						break;
					
					//Deletes layer
					case ConsoleKey.G:
						img.Layers.RemoveAt(layerCursorPosition);
						layerCursorPosition = 0;
						Console.Clear();
						break;
					case ConsoleKey.PageUp:
						img.Layers.RemoveAt(layerCursorPosition);
						layerCursorPosition = 0;
						Console.Clear();
						break;
					
					/*
					//Save as file using Console.ReadLine()
					case ConsoleKey.S:
						Console.Write("Save as: (No input to not save) ".Pastel(Color.White).PastelBg(Color.Black));
						string filename_s = Console.ReadLine();
						if(filename_s == "") break;
						File.WriteAllBytes(filename_s, FileConv.ToData(img));
						break;

					//Open file using Console.ReadLine()
					case ConsoleKey.R:
						Console.Write("Open: (No input to not open) ".Pastel(Color.White).PastelBg(Color.Black));
						string filename_o = Console.ReadLine();
						if(filename_o == "") break;
						img = FileConv.ToImage(File.ReadAllBytes(filename_o));
						cursorPosition = new Vec2(0, 0);
						layerCursorPosition = 0;
						break;
					*/

					//Quit
					case ConsoleKey.Q:
						Console.Write("Are you sure? Y/n ".Pastel(Color.White).PastelBg(Color.Black));
						string str = Console.ReadLine().ToLower();
						if(str == "" || str == "y" || str == "yes") running = false;
						break;

					//Keys without ConsoleKey representation
					default:
						//Move cursor right
						if(ckInfo.KeyChar == ';' || ckInfo.KeyChar == ':'){
							cursorPosition.x++;
							if(cursorPosition.x >= img.dimensions.x) cursorPosition.x = 0;
						}

						//Toggle cursor visible
						if(ckInfo.KeyChar == '\'' || ckInfo.KeyChar == '\"'){
							cursorVisible = !cursorVisible;
						}

						break;
				}
			}
			//b mode
			else if(mode == 1){
				mode = 0;
				switch(ckInfo.Key){
					case ConsoleKey.C:
						brush.character = Console.ReadKey(true).KeyChar;
						break;
					case ConsoleKey.T:
						Console.Write("New color: (leave empty to not change) ".Pastel(Color.White).PastelBg(Color.Black));
						string color_text = Console.ReadLine();
						if(color_text == "") break;
						int argb_color_text = int.Parse(color_text, System.Globalization.NumberStyles.HexNumber);
						brush.text_color = Color.FromArgb(argb_color_text);
						break;
					case ConsoleKey.B:
						Console.Write("New color: (leave empty to not change) ".Pastel(Color.White).PastelBg(Color.Black));
						string color_bckg = Console.ReadLine();
						if(color_bckg == "") break;
						int argb_color_bckg = int.Parse(color_bckg, System.Globalization.NumberStyles.HexNumber);
						brush.background_color = Color.FromArgb(argb_color_bckg);
						break;
				}
			}
			//r mode
			else if(mode == 2){
				mode = 0;
			}
		}
	}
	
	/// <summary>Creates a new shell</summary>
	/// <param name="dimensions">Size of the shell ; Set to (0,0) for automatic</param>
	/// <param name="backgroundLayer">Whether to add a white background layer</param>
	public Shell(Vec2 dimensions, bool backgroundLayer = true){
		if(dimensions == new Vec2(0, 0)){
			dimensions = new Vec2((Console.WindowWidth * 2) / 3, Console.WindowHeight);
		}
		img = new Image(dimensions);
		gui = new GUI(img, new Vec2(dimensions.x / 2, dimensions.y));
		gbc = Color.White;

		if(backgroundLayer){
			Layer bl = new Layer(dimensions);
			for(int x = 0; x < bl.Dimensions.x; x++){
				for(int y = 0; y < bl.Dimensions.y; y++){
					bl.SetPixoid(x, y, new Pixoid(' ', Color.Black, gbc));
				}
			}
			bl.name = "Background";
			img.AddLayer(new Layer(bl));
		}
		img.AddLayer(new Layer(dimensions));

		cursorPosition = new Vec2(0, 0);
		brush = new Pixoid('|', Color.White, Color.Black);
		layerCursorPosition = img.Layers.Count - 1;
		mode = 0;
	}
}
