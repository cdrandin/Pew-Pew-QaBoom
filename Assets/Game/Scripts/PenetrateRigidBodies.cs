using UnityEngine;
using System.Collections;

public class PenetrateRigidBodies : MonoBehaviour 
{
	public float force;
	public float sqrd_velocity_threshold = .1f; // When to stop desotrying objects ("At what speed" to stop)
	public float deterrence = 8.0f; 

	void Start()
	{
	}

	void FixedUpdate()
	{
		Vector3 mousePos = Input.mousePosition;
		mousePos.z = Mathf.Abs(0.0f - Camera.main.transform.position.z);
		mousePos = Camera.main.ScreenToWorldPoint(mousePos);

		if(Input.GetKeyDown(KeyCode.Space))
		{
			rigidbody.AddForce((mousePos - transform.position).normalized * force * rigidbody.mass);
		}
	}

	void OnCollisionExit(Collision collision)
	{
		float amount = Vector3.SqrMagnitude(rigidbody.velocity);
		Debug.Log(amount);
		if(collision.gameObject.layer == LayerMask.NameToLayer("Dynamic Objects") && (amount > sqrd_velocity_threshold))
		{
			Debug.Log("Destroy");
			rigidbody.velocity *= 1.0f/deterrence;
			Destroy(collision.gameObject);
		}

		if(amount <= sqrd_velocity_threshold)
		{
			Debug.Log("Stop");
			rigidbody.isKinematic = true;
		}
	}
}
