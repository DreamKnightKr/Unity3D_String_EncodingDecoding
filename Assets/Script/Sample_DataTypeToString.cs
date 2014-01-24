using UnityEngine;
using System.Collections;

public class Sample_DataTypeToString : MonoBehaviour {
	public enum DataType
	{
		Byte,
		Char,
		Int,
		Float,

		Max
	}

	public GUIText txtOut;

	byte bByteVal = 0x41;	// ASCII = 'A'
	char chCharVal = 'A';
	int nIntVal = 0xAC00;	// UTF-8 = '가'
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
	}

	string GetToStringDemos(object val, DataType type)
	{
		string strRet = "";
		switch(type)
		{
		case DataType.Byte:
			strRet = GetToStringDemosByte((byte)val);
			break;
		}

		return strRet;
	}

	string GetToStringDemosByte(byte val)
	{
		string strRet = "";
		string strTemp = "";
		char chTemp = ' ';
		int nTemp = 0;

		// fot Normal Use
		strRet += ("> String(digit)/revert Value : \n"
	           + (strTemp = val.ToString()))
				+ "/"
				+ byte.Parse(strTemp);
		strRet += "\n\n";

		// fot Converting Char
		strRet += ("> Char/revert Value : \n" 
		           + (chTemp = (char)val).ToString()
					+ "/"
					+ "(No Support)");
		strRet += "\n\n";

		// for Check Hex Code
		byte[] buff = System.BitConverter.GetBytes(val);
		strTemp = System.BitConverter.ToString(buff).Replace("-", string.Empty);
		nTemp = System.BitConverter.ToInt16( GetHexStringToByte(strTemp), 0);
		{
			// if Need...
			//if (System.BitConverter.IsLittleEndian)
			//	System.Array.Reverse(buff);
		}
		strRet += ("> Encoding ByteSteam string(Hex+IsLittleEndian)/revert Value : \n" 
		           + (strTemp = System.BitConverter.ToString(buff).Replace("-", string.Empty))
		           + "/"
		           + ((byte)nTemp).ToString());
		return strRet;
	}

	public static byte[] GetHexStringToByte(string hexString)
	{
		int bytesCount = (hexString.Length) / 2;
		byte[] bytes = new byte[bytesCount];
		for (int x = 0; x < bytesCount; ++x)
		{
			bytes[x] = System.Convert.ToByte(hexString.Substring(x * 2, 2), 16);
		}
		
		return bytes;
	}
}
