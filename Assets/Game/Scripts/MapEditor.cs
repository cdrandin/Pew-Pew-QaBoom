using UnityEngine;
using System.Collections;

public class MapEditor : MonoBehaviour 
{
	public float radius;
	public bool continous_delete;

	private GameObject _obj;

	// Use this for initialization
	void Start () 
	{
		_obj = gameObject;
		_obj.AddComponent<DestroyRigids>();
		_obj.GetComponent<DestroyRigids>().continous_delete = continous_delete;
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 mousePos = Input.mousePosition;
		mousePos.z = Mathf.Abs(0.0f - Camera.main.transform.position.z);
		mousePos = Camera.main.ScreenToWorldPoint(mousePos);

		_obj.transform.position = mousePos;
		_obj.transform.localScale = new Vector3(radius, radius, 0.0f);
		_obj.GetComponent<DestroyRigids>().continous_delete = continous_delete;
	}
}
