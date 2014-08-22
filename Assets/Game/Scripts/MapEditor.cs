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
		_obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		_obj.AddComponent<Rigidbody>();
		_obj.rigidbody.freezeRotation = true;
		_obj.rigidbody.drag = 0.08f;
		_obj.rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
		_obj.rigidbody.useGravity = false;
		_obj.rigidbody.isKinematic = false;
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

	void OnCollisionEnter(Collision collision)
	{
		if(continous_delete && collision.gameObject.layer == LayerMask.NameToLayer("Dynamic Objects"))
		{
			Destroy(collision.gameObject);
		}
	}
}
