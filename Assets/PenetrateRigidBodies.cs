using UnityEngine;
using System.Collections;

public class PenetrateRigidBodies : MonoBehaviour 
{
	public float force;
	public float sqrd_velocity_threshold = .1f; // When to stop desotrying objects ("At what speed" to stop)
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
			rigidbody.AddForce((mousePos - transform.position).normalized * force);
		}
	}

	// Rigidbody works best when mass is 100kg
	void OnCollisionExit(Collision collision)
	{
		Debug.Log(Vector3.SqrMagnitude(rigidbody.velocity));
		float amount = Vector3.SqrMagnitude(rigidbody.velocity);
		if(collision.gameObject.layer == LayerMask.NameToLayer("Dynamic Objects") && (amount > sqrd_velocity_threshold))
		{
			rigidbody.velocity *= 1.0f/8.0f;
			Destroy(collision.gameObject);
		}

		if(amount <= sqrd_velocity_threshold)
		{
			rigidbody.velocity = Vector3.zero;
		}
	}
}
