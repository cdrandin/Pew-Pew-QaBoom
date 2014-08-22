using UnityEngine;
using System.Collections;

public class DestroyRigids : MonoBehaviour 
{
	private bool _continous_delete;
	public bool continous_delete
	{
		get
		{
			return _continous_delete;
		}

		set
		{
			_continous_delete = value;
		}
	}
	void OnCollisionEnter(Collision collision)
	{
		bool clip = (continous_delete || Input.GetMouseButton(0))?true:false;

		if(clip && collision.gameObject.layer == LayerMask.NameToLayer("Dynamic Objects"))
		{
			Destroy(collision.gameObject);
		}
	}
}
