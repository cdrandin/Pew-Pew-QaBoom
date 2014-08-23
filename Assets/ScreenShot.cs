using UnityEngine;
using System.Collections;
using System.IO;

public class ScreenShot : MonoBehaviour 
{
	public string file_name;
	public enum PICTURE_FORMAT
	{
		PNG, JPEG
	}
	public PICTURE_FORMAT picture_format;

	// Take a shot immediately
	void Start () 
	{		
		StartCoroutine(UploadPNG ());
	}
	
	IEnumerator UploadPNG ()
	{
		yield return new WaitForEndOfFrame();

		int width = Screen.width;
		int height = Screen.height;

		Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);

		tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
		tex.Apply();

		byte[] bytes = (picture_format == PICTURE_FORMAT.PNG)?tex.EncodeToPNG():tex.EncodeToJPG();

		Destroy(tex);

		System.IO.File.WriteAllBytes(string.Format("{0}.{1}", file_name, (picture_format == PICTURE_FORMAT.PNG)?"png":"jpeg"), bytes);
	}
}