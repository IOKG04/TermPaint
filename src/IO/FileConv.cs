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
	/// <summary>Byte[] -> layer</summary>
	/// <param name="b">Bytes to convert</param>
	public static Layer ToLayer(byte[] b){
		Layer l = new Layer();

		if(b.LongLength < 21) throw new Exception("Way too not enough arguments");

		l.Dimensions = new Vec2(BitConverter.ToInt32(b, 0), BitConverter.ToInt32(b, 4));
		l.position = new Vec2(BitConverter.ToInt32(b, 8), BitConverter.ToInt32(b, 12));
		l.visible = b[16] > 127 ? true : false;
		int nameLength = BitConverter.ToInt32(b, 17);
		string name = "";
		for(int i = 0; i < nameLength; i++){
			name += BitConverter.ToChar(b, (i * 2) + 21);
		}
		l.name = name;

		if(b.LongLength < 21 + (l.name.Length * 2) + (l.Dimensions.x * l.Dimensions.y * 8)) throw new Exception("Still not enough arguments");

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

	/// <summary>Imgage -> byte[]</summary>
	/// <param name="img">Image to convert</param>
	public static byte[] ToData(Image img){
		long length = 12;
		for(int i = 0; i < img.Layers.Count; i++){
			length += 8;
			length += ToData(img.Layers[i]).LongLength;
		}
		byte[] bytes = new byte[length];

		//Add metadata
		bytes[0] = BitConverter.GetBytes(img.dimensions.x)[0];
		bytes[1] = BitConverter.GetBytes(img.dimensions.x)[1];
		bytes[2] = BitConverter.GetBytes(img.dimensions.x)[2];
		bytes[3] = BitConverter.GetBytes(img.dimensions.x)[3];
		bytes[4] = BitConverter.GetBytes(img.dimensions.y)[0];
		bytes[5] = BitConverter.GetBytes(img.dimensions.y)[1];
		bytes[6] = BitConverter.GetBytes(img.dimensions.y)[2];
		bytes[7] = BitConverter.GetBytes(img.dimensions.y)[3];
		bytes[8] = BitConverter.GetBytes(img.Layers.Count)[0];
		bytes[9] = BitConverter.GetBytes(img.Layers.Count)[1];
		bytes[10] = BitConverter.GetBytes(img.Layers.Count)[2];
		bytes[11] = BitConverter.GetBytes(img.Layers.Count)[3];

		//Add layerinformations
		long currentmin = 12;
		for(int i = 0; i < img.Layers.Count; i++){
			byte[] layerData = ToData(img.Layers[i]);
			bytes[currentmin + 0] = BitConverter.GetBytes(layerData.LongLength)[0];
			bytes[currentmin + 1] = BitConverter.GetBytes(layerData.LongLength)[1];
			bytes[currentmin + 2] = BitConverter.GetBytes(layerData.LongLength)[2];
			bytes[currentmin + 3] = BitConverter.GetBytes(layerData.LongLength)[3];
			bytes[currentmin + 4] = BitConverter.GetBytes(layerData.LongLength)[4];
			bytes[currentmin + 5] = BitConverter.GetBytes(layerData.LongLength)[5];
			bytes[currentmin + 6] = BitConverter.GetBytes(layerData.LongLength)[6];
			bytes[currentmin + 7] = BitConverter.GetBytes(layerData.LongLength)[7];
			currentmin += 8;
			for(long l = 0; l < layerData.LongLength; l++){
				bytes[currentmin + l] = layerData[l];
			}
			currentmin += layerData.LongLength;
		}

		return bytes;
	}
	/// <summary>Byte[] -> image</summary>
	/// <param name="b">Bytes to convert</param>
	public static Image ToImage(byte[] b){
		if(b.LongLength < 12) throw new Exception("Not enough bytes in b");

		Image img = new Image();

		//Add metadata
		img.dimensions.x = BitConverter.ToInt32(b, 0);
		img.dimensions.y = BitConverter.ToInt32(b, 4);
		int layer_amount = BitConverter.ToInt32(b, 8);

		//Return if layer_amount is 0
		if(layer_amount == 0) return img;

		if(b.LongLength < 12 + (layer_amount * 8)) throw new Exception("Still not enough arguments");

		//Add layers
		long currentmin = 12;
		for(int i = 0; i < layer_amount; i++){
			long l = BitConverter.ToInt64(b, (int)currentmin);
			byte[] layerdata = new byte[l];
			currentmin += 8;
			for(int j = 0; j < l; j++){
				layerdata[i] = b[currentmin + i];
			}
			currentmin += l;
			img.AddLayer(ToLayer(layerdata));
		}

		return img;
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
