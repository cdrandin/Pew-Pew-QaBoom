using UnityEngine;
using System.Collections;

public class CC : MonoBehaviour 
{
	public float max_speed      = 10.0f;
	public float jump_height     = 6.0f;
	public float fall_speed     = 15.0f;
	public Transform ground_check;
	public LayerMask what_is_ground;
	
	private bool face_right     = true;
	private bool grounded       = false;
	private float ground_radius = 0.2f;
	private bool is_moving      = false;

	private Vector3 _direction;
	private float _vertical_speed;
	private CharacterController _cc;
	private bool is_controllable;

	void Start() 
	{
		_cc            = GetComponent<CharacterController>();
		_direction     = Vector3.zero;
		is_controllable = true;
	}
	
	void FixedUpdate() 
	{
		if(!is_controllable)
		{
			Input.ResetInputAxes();
		}

		// Character left/right movement
		float move = Input.GetAxis("Horizontal");
		is_moving = Mathf.Abs(move) > 0.1f;

		Move(move);
		ApplyGravity();
		Jump();

		Vector3 movement = _direction * max_speed;
		movement.y += _vertical_speed;
		movement *= Time.fixedDeltaTime;

		_cc.Move(movement);

		ImageFace(move);
	}

	void Move(float move)
	{
		_direction.x = move;
	}

	void ApplyGravity()
	{
		_vertical_speed = (_cc.isGrounded)?0.0f:(_vertical_speed - fall_speed);
	}

	void Jump()
	{
		if(_cc.isGrounded)
		{
			if(Input.GetKeyDown(KeyCode.Space))
			{
				_vertical_speed = Mathf.Sqrt(2 * jump_height * fall_speed);
			}
		}
	}

	void ImageFace(float move)
	{
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
