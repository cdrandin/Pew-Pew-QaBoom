using UnityEngine;
using System.Collections;

public class CharacterMotor : MonoBehaviour 
{
	public float jump_force     = 700.0f;
	public float max_speed      = 10.0f;
	public Transform ground_check;
	public LayerMask what_is_ground;

	private bool face_right     = true;
	private Animator anim;
	private bool grounded       = false;
	private float ground_radius = 0.2f;

	void Start() 
	{
		anim = GetComponent<Animator>();
	}

	void Update()
	{
		if(grounded && Input.GetKeyDown(KeyCode.Space))
		{
			anim.SetBool("Ground", false);
			rigidbody2D.AddForce(new Vector2(0.0f, jump_force));
		}

		if(Input.GetKeyDown(KeyCode.P))
		{
			anim.SetTrigger("Summon");
		}

		if(Input.GetKeyDown(KeyCode.O))
		{
			anim.SetTrigger("Attack");
		}
	}

	void FixedUpdate() 
	{
		// Check if on ground
		grounded = Physics2D.OverlapCircle(ground_check.position, ground_radius, what_is_ground);

		// Set variable through the animator
		anim.SetBool("Ground", grounded);
		anim.SetFloat("vSpeed", rigidbody2D.velocity.y);

		// Character left/right movement
		float move = Input.GetAxis("Horizontal");

		// Set variable through the animator
		anim.SetFloat("Speed", Mathf.Abs(move));

		// Movement of character
		rigidbody2D.velocity = new Vector2(move * max_speed, rigidbody2D.velocity.y);

		// Determine when to flip image
		if(move > 0.0f && !face_right)
		{
			ImageFlip();
		}
		else if(move < 0.0f && face_right)
		{
			ImageFlip();
		}
	}

	void ImageFlip()
	{
		face_right = !face_right;
		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}
}