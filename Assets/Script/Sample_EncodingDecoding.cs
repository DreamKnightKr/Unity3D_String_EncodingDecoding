﻿using UnityEngine;
using System.Collections;

/*
 * !!!![Condition]This File Save As UTF-8(Default)
*/
public class Sample_EncodingDecoding : MonoBehaviour {

	// for Test Datas..
	string strTestData = "ABCD\n가나다\n最初から始める\n~!@#$%^&*(*)(";
	byte[] bytesDefaultTestData;
	byte[] bytesUTF8TestData;
	byte[] bytesUTF16TestData;


	public GUIText txtOut;

	// Use this for initialization
	void Start () {
	}

	string GetDataEncodingDemos(byte[] buff)
	{
		string strRet = "";
		// for BinaryData(like savefile etc.) Transfer
		strRet += ("> Show Binary(Hex) string : \n" 
		          + System.BitConverter.ToString(buff).Replace("-", string.Empty));
		strRet += "\n\n";

		// for Print BinaryData that have strig.
		strRet += ("> Encoding System Default string : \n" 
		          + System.Text.Encoding.Default.GetString(buff));
		strRet += "\n\n";

		// for Print BinaryData that have strig.
		strRet += ("> Encoding UTF-8 string : \n" 
		          + System.Text.Encoding.UTF8.GetString(buff));
		strRet += "\n\n";

		// for Web requesting Data(When Sarver must use UTF-8.)
		string strUTF8 = System.Text.Encoding.UTF8.GetString(buff);	// return to UTF-8
		byte[] bytesUTF8 = System.Text.Encoding.UTF8.GetBytes(strUTF8);
		strRet +=("> Encoding Web request(UTF8 > Base64) string : \n" 
		          + System.Convert.ToBase64String(bytesUTF8));
		strRet += "\n\n";

		// for Demo Others...
		strRet += ("> Encoding Unicode string : \n" 
		          + System.Text.Encoding.Unicode.GetString(buff));
		strRet += "\n\n";

		return strRet;
	}

	void OnGUI()
	{
		float fPosX = 20, fPosY = 50, fYInterval = 40;
		int nYPosCount = 0;
		string strOut = "";		
		// Read...
		nYPosCount = 0;
		if (GUI.Button(new Rect(fPosX, fPosY + (fYInterval * nYPosCount), 200, 30), "System Default"))
		{
			txtOut.text = ("Start Testing System default Encording.\n" +
			               "Original Data : " + strTestData);
			txtOut.text += "\n\n";

			bytesDefaultTestData = System.Text.Encoding.Default.GetBytes(strTestData);
			strOut = GetDataEncodingDemos(bytesDefaultTestData);

			Debug.Log(strOut);
			txtOut.text += strOut;
		}

		// Read...
		nYPosCount++;
		if (GUI.Button(new Rect(fPosX, fPosY + (fYInterval * nYPosCount), 200, 30), "UTF-8"))
		{
			txtOut.text = ("Start Testing System default Encording.\n" +
			               "Original Data : " + strTestData);
			txtOut.text += "\n\n";

			bytesDefaultTestData = System.Text.Encoding.UTF8.GetBytes(strTestData);
			strOut = GetDataEncodingDemos(bytesDefaultTestData);
			
			Debug.Log(strOut);
			txtOut.text += strOut;
		}
	}
}
