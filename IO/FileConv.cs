using System;
using System.Drawing;
using TermPaint.Low;
using TermPaint.Base;
using TermPaint.Complete;

namespace TermPaint.IO;

/// <summary>For converting images, layers and pixoids to bytes and reverse</summary>
public static class FileConv{
	/// <summary>Pixoid -> byte[]</summary>
	/// <param name="p">Pixoid to convert</param>
	public static byte[] ToData(Pixoid p){
		byte[] bytes = new byte[8];

		bytes[0] = BitConverter.GetBytes(p.character)[0];
		bytes[1] = BitConverter.GetBytes(p.character)[1];
		bytes[2] = p.text_color.R;
		bytes[3] = p.text_color.G;
		bytes[4] = p.text_color.B;
		bytes[5] = p.background_color.R;
		bytes[6] = p.background_color.G;
		bytes[7] = p.background_color.B;

		return bytes;
	}
	/// <summary>Byte[] -> Pixoid</summary>
	/// <param name="b">Byte[] to convert</param>
	public static Pixoid ToPixoid(byte[] b){
		if(b.Length < 8) throw new Exception("byte[] doesn't contain enough data");
		Pixoid p = new Pixoid();

		p.character = BitConverter.ToChar(b, 0);
		p.text_color = Color.FromArgb(255, b[2], b[3], b[4]);
		p.background_color = Color.FromArgb(255, b[5], b[6], b[7]);

		return p;
	}

	/// <summary>Layer -> byte[]</summary>
	/// <param name="l">Layer to convert</param>
	public static byte[] ToData(Layer l){
		//TODO: Finsish
		byte[] bytes = new byte[21 + (l.name.Length * 2) + (l.Dimensions.x * l.Dimensions.y * 8)];

		//Add Metadata
		byte[] currentInformation = new byte[21];
		currentInformation[0] = BitConverter.GetBytes(l.Dimensions.x)[0];
		currentInformation[1] = BitConverter.GetBytes(l.Dimensions.x)[1];
		currentInformation[2] = BitConverter.GetBytes(l.Dimensions.x)[2];
		currentInformation[3] = BitConverter.GetBytes(l.Dimensions.x)[3];
		currentInformation[4] = BitConverter.GetBytes(l.Dimensions.y)[0];
		currentInformation[5] = BitConverter.GetBytes(l.Dimensions.y)[1];
		currentInformation[6] = BitConverter.GetBytes(l.Dimensions.y)[2];
		currentInformation[7] = BitConverter.GetBytes(l.Dimensions.y)[3];
		currentInformation[8] = BitConverter.GetBytes(l.position.x)[0];
		currentInformation[9] = BitConverter.GetBytes(l.position.x)[1];
		currentInformation[10] = BitConverter.GetBytes(l.position.x)[2];
		currentInformation[11] = BitConverter.GetBytes(l.position.x)[3];
		currentInformation[12] = BitConverter.GetBytes(l.position.y)[0];
		currentInformation[13] = BitConverter.GetBytes(l.position.y)[1];
		currentInformation[14] = BitConverter.GetBytes(l.position.y)[2];
		currentInformation[15] = BitConverter.GetBytes(l.position.y)[3];
		currentInformation[16] = l.visible ? (byte)255 : (byte)0;
		currentInformation[17] = BitConverter.GetBytes(l.name.Length)[0];
		currentInformation[18] = BitConverter.GetBytes(l.name.Length)[1];
		currentInformation[19] = BitConverter.GetBytes(l.name.Length)[2];
		currentInformation[20] = BitConverter.GetBytes(l.name.Length)[3];
		for(int i = 0; i < 21; i++){
			bytes[i] = currentInformation[i];
		}

		//Add l.name
		currentInformation = new byte[l.name.Length * 2];
		for(int i = 0; i < l.name.Length; i++){
			currentInformation[(2 * i)] = BitConverter.GetBytes(l.name[i])[0];
			currentInformation[(2 * i) + 1] = BitConverter.GetBytes(l.name[i])[1];
		}
		for(int i = 0; i < l.name.Length * 2; i++){
			bytes[i + 21] = currentInformation[i];
		}

		//Add pixoid_data
		currentInformation = new byte[0];
		for(int y = 0; y < l.Dimensions.y; y++){
			for(int x = 0; x < l.Dimensions.x; x++){
				currentInformation = Join(currentInformation, ToData(l.GetPixoid(x, y)));
			}
		}
		for(int i = 0; i < currentInformation.Length; i++){
			bytes[i + 21 + (l.name.Length * 2)] = currentInformation[i];
		}

		return bytes;
	}
	public static Layer ToLayer(byte[] b){
		Layer l = new Layer();

		if(b.Length < 21) throw new Exception("Way too not enough arguments");

		l.Dimensions = new Vec2(BitConverter.ToInt32(b, 0), BitConverter.ToInt32(b, 4));
		l.position = new Vec2(BitConverter.ToInt32(b, 8), BitConverter.ToInt32(b, 12));
		l.visible = b[16] > 127 ? true : false;
		int nameLength = BitConverter.ToInt32(b, 17);
		string name = "";
		for(int i = 0; i < nameLength; i++){
			name += BitConverter.ToChar(b, (i * 2) + 21);
		}
		l.name = name;

		if(b.Length < 21 + (l.name.Length * 2) + (l.Dimensions.x * l.Dimensions.y * 8)) throw new Exception("Still not enough arguments");

		for(int y = 0; y < l.Dimensions.y; y++){
			for(int x = 0; x < l.Dimensions.x; x++){
				byte[] pixoidBytes = new byte[8];
				for(int i = 0; i < 8; i++){
					pixoidBytes[i] = b[(y * l.Dimensions.x * 8) + (x * 8) + i + 21 + l.name.Length * 2];
				}
				l.SetPixoid(x, y, ToPixoid(pixoidBytes));
			}
		}

		return l;
	}

	private static byte[] Join(byte[] a, byte[] b){
		byte[] bytes = new byte[a.Length + b.Length];

		for(int i = 0; i < a.Length; i++){
			bytes[i] = a[i];
		}
		for(int i = 0; i < b.Length; i++){
			bytes[i + a.Length] = b[i];
		}

		return bytes;
	}
}
