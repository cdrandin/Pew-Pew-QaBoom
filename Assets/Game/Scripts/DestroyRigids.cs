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

	void Update()
	{
		bool clip = (continous_delete || Input.GetMouseButton(0))?true:false;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, 100.0f))
		{
			if(clip && hit.transform.gameObject.layer == LayerMask.NameToLayer("Dynamic Objects"))
			{
				Destroy(hit.transform.gameObject);
			}
		}
	}
}