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
		if(_continous_delete && collision.gameObject.layer == LayerMask.NameToLayer("Dynamic Objects"))
		{
			Destroy(collision.gameObject);
		}
	}
}
