using UnityEngine;
using System.Collections;

public class Sample_DataTypeToString : MonoBehaviour {
	public enum DataType
	{
		Byte,
		Char,
		CharUTF8,
		Int,
		Float,

		Max
	}

	public GUIText txtOut;

	byte bByteVal = 0x41;	// ASCII = 'A'
	char chCharVal = '핳'; 	//'A' : 2byte '핳' : 2byte
	int nIntVal = 0xAC00;	// Unicode = '가'
	float fFloatVal = 7.00101f;

	// Use this for initialization
	void Start () {
	
	}
	
	void OnGUI()
	{
		float fPosX = 300, fPosY = 50, fYInterval = 40;
		int nYPosCount = 0;
		string strOut = "";		
		
		nYPosCount = 0;
		if (GUI.Button(new Rect(fPosX, fPosY + (fYInterval * nYPosCount), 200, 30), "Byte Test"))
		{
			txtOut.text = "";

			strOut = ("Start Testing Byte -> String -> Byte\n" +
			               "Original Data : " + bByteVal.ToString());
			strOut += "\n\n";
			strOut += GetToStringDemos(bByteVal, DataType.Byte);
			Debug.Log(strOut);
			txtOut.text += strOut;
		}

		nYPosCount++;
		if (GUI.Button(new Rect(fPosX, fPosY + (fYInterval * nYPosCount), 200, 30), "Char Test(Default)"))
		{
			txtOut.text = "";
			
			strOut = ("Start Testing Char -> String(Sys. Default = Unicode) -> Char\n" +
			          "Original Data : " + chCharVal.ToString());
			strOut += "\n\n";
			strOut += GetToStringDemos(chCharVal, DataType.Char);
			Debug.Log(strOut);
			txtOut.text += strOut;
		}

		nYPosCount++;
		if (GUI.Button(new Rect(fPosX, fPosY + (fYInterval * nYPosCount), 200, 30), "Char Test(UTF-8)"))
		{
			txtOut.text = "";
			
			strOut = ("Start Testing Char -> String(UTF-8) -> Char\n" +
			          "Original Data : " + chCharVal.ToString());
			strOut += "\n\n";
			strOut += GetToStringDemos(chCharVal, DataType.CharUTF8);
			Debug.Log(strOut);
			txtOut.text += strOut;
		}

		nYPosCount++;
		if (GUI.Button(new Rect(fPosX, fPosY + (fYInterval * nYPosCount), 200, 30), "Int Test"))
		{
			txtOut.text = "";
			
			strOut = ("Start Testing Int -> String(UTF-8) -> Int\n" +
			          "Original Data : " + nIntVal.ToString());
			strOut += "\n\n";
			strOut += GetToStringDemos(nIntVal, DataType.Int);
			Debug.Log(strOut);
			txtOut.text += strOut;
		}

		nYPosCount++;
		if (GUI.Button(new Rect(fPosX, fPosY + (fYInterval * nYPosCount), 200, 30), "Float Test"))
		{
			txtOut.text = "";
			
			strOut = ("Start Testing Float -> String(UTF-8) -> Float\n" +
			          "Original Data : " + fFloatVal.ToString());
			strOut += "\n\n";
			strOut += GetToStringDemos(fFloatVal, DataType.Float);
			Debug.Log(strOut);
			txtOut.text += strOut;
		}
	}

	string GetToStringDemos(object val, DataType type)
	{
		string strRet = "";
		switch(type)
		{
		case DataType.Byte:
			strRet = GetToStringDemosByte((byte)val);
			break;
		case DataType.Char:
			strRet = GetToStringDemosChar((char)val);
			break;
		case DataType.CharUTF8:
			strRet = GetToStringDemosCharUTF8((char)val);
			break;
		case DataType.Int:
			strRet = GetToStringDemosInt((int)val);
			break;
		case DataType.Float:
			strRet = GetToStringDemosFloat((float)val);
			break;
		}

		return strRet;
	}

	string GetToStringDemosFloat(float val)
	{
		string strRet = "";
		string strTemp = "";

		// fot Normal Use
		strRet += ("> String(digit)/revert Value : \n"
		           + (strTemp = val.ToString())
		           + "/"
		           + float.Parse(strTemp));
		strRet += "\n\n";

		// for Check Hex Code
		byte[] buff = System.BitConverter.GetBytes(val);
		strTemp = System.BitConverter.ToString(buff).Replace("-", string.Empty);
		double fTemp = System.BitConverter.ToDouble( GetHexStringToByte(strTemp, 8), 0);	//!! float를 double로 변화하는 상화이상 값이 이상해짐.
		strRet += ("> Encoding ByteSteam string(Hex+IsLittleEndian)/revert Value(double) : \n" 
		           + strTemp
		           + "/"
		           + (fTemp).ToString());

		return strRet;
	}

	string GetToStringDemosInt(int val)
	{
		string strRet = "";
		string strTemp = "";

		// fot Normal Use
		strRet += ("> String(digit)/revert Value : \n"
		           + (strTemp = val.ToString())
		           + "/"
		           + int.Parse(strTemp));
		strRet += "\n\n";

		// fot Converting Char
		strRet += ("> Char/revert Value : \n" 
		           + ((char)val).ToString()
		           + "/"
		           + "(No Support)");
		strRet += "\n\n";

		// for Check Hex Code
		byte[] buff = System.BitConverter.GetBytes(val);
		strTemp = System.BitConverter.ToString(buff).Replace("-", string.Empty);
		int nTemp = System.BitConverter.ToInt32( GetHexStringToByte(strTemp, 4), 0);
		strRet += ("> Encoding ByteSteam string(Hex+IsLittleEndian)/revert Value : \n" 
		           + strTemp
		           + "/"
		           + (nTemp).ToString());

		return strRet;
	}

	string GetToStringDemosByte(byte val)
	{
		string strRet = "";
		string strTemp = "";
		int nTemp = 0;
		
		// fot Normal Use
		strRet += ("> String(digit)/revert Value : \n"
		           + (strTemp = val.ToString())
		           + "/"
		           + byte.Parse(strTemp));
		strRet += "\n\n";
		
		// fot Converting Char
		strRet += ("> Char/revert Value : \n" 
		           + ((char)val).ToString()
		           + "/"
		           + "(No Support)");
		strRet += "\n\n";
		
		// for Check Hex Code
		byte[] buff = System.BitConverter.GetBytes(val);
		strTemp = System.BitConverter.ToString(buff).Replace("-", string.Empty);
		nTemp = System.BitConverter.ToInt16( GetHexStringToByte(strTemp), 0);
		strRet += ("> Encoding ByteSteam string(Hex+IsLittleEndian)/revert Value : \n" 
		           + strTemp
		           + "/"
		           + ((byte)nTemp).ToString());
		return strRet;
	}

	string GetToStringDemosCharUTF8(char val)
	{
		string strRet = "";
		byte[] bytesUTF8 = System.Text.Encoding.UTF8.GetBytes(val.ToString());
		string strUTF8Hex = System.BitConverter.ToString(bytesUTF8).Replace("-", string.Empty);
		uint nTemp = System.BitConverter.ToUInt32( GetHexStringToByte(strUTF8Hex, 4), 0);

		// fot Normal Use
		strRet += ("> String(digit)/revert Value : \n"
		           + nTemp.ToString()
		           + "/"
		           + GetStringFromUTF8Uint(nTemp)
		           );
		strRet += "\n\n";

		// for Check Hex Code
		strRet += ("> Encoding ByteSteam string(Hex+IsLittleEndian)/revert Value : \n" 
		           + strUTF8Hex
		           + "/"
		           + System.Text.Encoding.UTF8.GetString(bytesUTF8));

		return strRet;
	}

	string GetStringFromUTF8Uint(uint val)
	{
		// for protected 0 include case : ex) 0A 0B 0c 00

		byte[] buff = System.BitConverter.GetBytes(val);

		int nLengNew = 0;
		while(nLengNew < buff.Length)
		{
			if(0 == buff[nLengNew])
				break;
			nLengNew++;
		}

		byte[] buffNew = new byte[nLengNew];
		System.Array.Copy(buff, 0, buffNew, 0, nLengNew);

		return System.Text.Encoding.UTF8.GetString(buffNew);
	}

	public static byte[] GetHexStringToByte(string hexString, int nPaddingSize = 0)
	{
		// Is LittleEndian type

		int bytesCount = (hexString.Length) / 2;
		int paddingCount = 0;
		if(nPaddingSize > 0)
		{
			if(bytesCount > nPaddingSize)
				paddingCount += bytesCount % nPaddingSize;
			else
				paddingCount = nPaddingSize - bytesCount;
		}
		byte[] bytes = new byte[bytesCount + paddingCount];

		int x = 0;
		for (; x < bytesCount; ++x)
		{
			bytes[x] = System.Convert.ToByte(hexString.Substring(x * 2, 2), 16);
		}

		// Padding
		for(; x < (bytesCount+paddingCount); ++x)
		{
			bytes[x] = 0;
		}
		return bytes;
	}


	string GetToStringDemosChar(char val)
	{
		string strRet = "";
		string strTemp = "";
		uint nTemp = 0;
		
		// fot Normal Use
		strRet += ("> String(digit)/revert Value : \n"
		           + (nTemp = (uint)val).ToString()
					+ "/"
		           + ((char)nTemp).ToString());
		strRet += "\n\n";
		
		// for Check Hex Code
		byte[] buff = System.BitConverter.GetBytes(val);
		strTemp = System.BitConverter.ToString(buff).Replace("-", string.Empty);
		nTemp = System.BitConverter.ToUInt16( GetHexStringToByte(strTemp, 2), 0);
		strRet += ("> Encoding ByteSteam string(Hex+IsLittleEndian)/revert Value : \n" 
		           + (strTemp = System.BitConverter.ToString(buff).Replace("-", string.Empty))
		           + "/"
		           + ((char)nTemp).ToString());

		return strRet;
	}
}
