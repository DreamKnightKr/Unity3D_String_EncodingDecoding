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
	float fFloatVal = 1.00101f;

	// Use this for initialization
	void Start () {
	
	}
	
	void OnGUI()
	{
		float fPosX = 20, fPosY = 50, fYInterval = 40;
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
		}

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
		           + GetStringFromUint(nTemp)
		           );
		strRet += "\n\n";

		// for Check Hex Code
		strRet += ("> Encoding ByteSteam string(Hex+IsLittleEndian)/revert Value : \n" 
		           + strUTF8Hex
		           + "/"
		           + System.Text.Encoding.UTF8.GetString(bytesUTF8));

		return strRet;
	}

	string GetStringFromUint(uint val)
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
		           + (strTemp = System.BitConverter.ToString(buff).Replace("-", string.Empty))
		           + "/"
		           + ((byte)nTemp).ToString());
		return strRet;
	}

	public static byte[] GetHexStringToByte(string hexString, int nPaddingSize = 0)
	{
		// Is LittleEndian type

		int bytesCount = (hexString.Length) / 2;
		int paddingCount = 0;
		if(nPaddingSize > 0)
			paddingCount += (hexString.Length) % nPaddingSize;
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
