using UnityEngine;
using System.Collections;

public class PenetrateRigidBodies : MonoBehaviour 
{
	public float force;

	void Start()
	{
	}

	void Update()
	{
		Vector3 mousePos = Input.mousePosition;
		mousePos.z = Mathf.Abs(0.0f - Camera.main.transform.position.z);
		mousePos = Camera.main.ScreenToWorldPoint(mousePos);

		if(Input.GetKeyDown(KeyCode.Space))
		{
			rigidbody.AddForce((mousePos - transform.position).normalized * force);
		}
	}

	// Rigidbody works best when mass is 100kg
	void OnCollisionExit(Collision collision)
	{
		Debug.Log(Vector3.SqrMagnitude(rigidbody.velocity));
		float amount = Vector3.SqrMagnitude(rigidbody.velocity);
		if(collision.gameObject.layer == LayerMask.NameToLayer("Dynamic Objects") && (amount > .1f))
		{
			Destroy(collision.gameObject);
		}

		if(amount <= .1f)
		{
			rigidbody.velocity = Vector3.zero;
		}
	}
}
