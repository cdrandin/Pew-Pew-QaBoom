using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TextureTesting : MonoBehaviour 
{
	public Texture2D tex;
	public List<Color> _dif_colors;
	public GameObject background;
	public GameObject ground_platform;

	public enum COLLISION_COLOR_PRORITY
	{
		LIGHT, DARK
	}

	public COLLISION_COLOR_PRORITY collision_color;

	private CC _player;

	void Awake()
	{
		_player = GameObject.FindObjectOfType<CC>();
	}

	void Start()
	{
		int width = tex.width, height = tex.height;
		_dif_colors = new List<Color>();

		Color[] pic = tex.GetPixels();

		// Count different colors in texture
		foreach(Color color in pic)
			DifferentTypesColors(color);

		//background = GameObject.CreatePrimitive(PrimitiveType.Quad);
		background.renderer.material.mainTexture = tex;

		/*
		// Adjust background texture size to the size of the viewport
		float obj_width, obj_height;

		Vector3 left_bot  = camera.ViewportToWorldPoint(new Vector3(0.0f, 0.0f, camera.nearClipPlane));
		Vector3 right_top = camera.ViewportToWorldPoint(new Vector3(1.0f, 1.0f, camera.nearClipPlane));

		obj_width = (right_top.x - left_bot.x);
		obj_height = (right_top.y - left_bot.y);

		background.transform.localScale = new Vector3(obj_width, obj_height, 0.1f);
		*/
	}

	void Update()
	{
		SetNewPositionOnCollision();
	}

	void DifferentTypesColors( Color color )
	{
		if(!_dif_colors.Contains(color))
		{
			_dif_colors.Add(color);
		}
	}

	void SetNewPositionOnCollision()
	{
		if(_player.is_moving || !_player.is_grounded)
		{
			Vector3 world_pos  = _player.transform.GetChild(0).position;
			Vector3 screen_pos = Camera.main.WorldToScreenPoint(world_pos);
			
			if(CheckBlackWhite(Camera.main.ScreenPointToRay(screen_pos), collision_color))
			{
				ground_platform.transform.position = world_pos;
			}
		}
	}

	bool CheckBlackWhite(Ray castThis, COLLISION_COLOR_PRORITY color)
	{// http://answers.unity3d.com/questions/189998/2d-collisions-on-a-texture2d-with-transparent-area.html

		// TODO: Understand why this works

		RaycastHit hit;
		if(Physics.Raycast(castThis, out hit))
		{
			// Just in case, also make sure the collider also has a renderer
			// material and texture. Also we should ignore primitive colliders.
			Renderer hitRender = hit.collider.renderer;
			MeshCollider meshCollider = hit.collider as MeshCollider;

			if (hitRender == null || hitRender.sharedMaterial == null ||
			    hitRender.sharedMaterial.mainTexture == null || meshCollider == null)
				return false;

			Texture2D hitTex = hitRender.material.mainTexture as Texture2D;
			Vector2 pixelUV  = hit.textureCoord;
			pixelUV          = new Vector2(pixelUV.x * tex.width, pixelUV.y * tex.height);
			
			// this is the important bit!
			// Make sure that the texture has 'isReadable' set to true, or this won't work.
			Color pixelValue = hitTex.GetPixel((int)pixelUV.x, (int)pixelUV.y);

			// returns true if the colour hit is the color priority, false if it's the other
			return (collision_color == COLLISION_COLOR_PRORITY.LIGHT)?(pixelValue.grayscale > 0.5f):(pixelValue.grayscale < 0.5f);
		}
		else
			return false;
	}
}