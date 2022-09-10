using System;
using System.Drawing;
using TermPaint.Base;
using TermPaint.Complete;

namespace TermPaint.IO;

/// <summary>For converting images, layers and pixoids to bytes and reverse</summary>
public static class File{
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
			currentInformation[i] = BitConverter.GetBytes(l.name[i])[0];
			currentInformation[i + 1] = BitConverter.GetBytes(l.name[i])[1];
		}
		for(int i = 0; i < l.name.Length * 2; i++){
			bytes[i + 21] = currentInformation[i];
		}

		return bytes;
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
