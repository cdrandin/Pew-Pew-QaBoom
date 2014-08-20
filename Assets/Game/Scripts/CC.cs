using UnityEngine;
using System.Collections;

public class CC : MonoBehaviour 
{
	public float max_speed       = 10.0f;
	public float jump_height     = 6.0f;
	public float fall_speed      = 15.0f;

	public Transform ground_check;
	public LayerMask what_is_ground;

	private bool _face_right     = true;
	private bool _grounded       = false;
	private bool is_moving       = false;
	private float ground_radius  = 0.2f;

	private Animator _anim;
	private Vector3 _move_direction;
	private float _vertical_speed;
	private CharacterController _cc;
	private bool is_controllable;

	void Start() 
	{
		_anim            = GetComponent<Animator>();
		_cc             = GetComponent<CharacterController>();
		_move_direction = Vector3.zero;
		is_controllable = true;
	}

	void Update()
	{
		if(_grounded && Input.GetKeyDown(KeyCode.Space))
		{
			_anim.SetBool("Ground", false);
		}
		
		if(Input.GetKeyDown(KeyCode.P))
		{
			_anim.SetTrigger("Summon");
		}
		
		if(Input.GetKeyDown(KeyCode.O))
		{
			_anim.SetTrigger("Attack");
		}
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

		// Set variable through the animator
		_anim.SetBool("Ground", _grounded);
		_anim.SetFloat("vSpeed", _vertical_speed);
		_anim.SetFloat("Speed", Mathf.Abs(move));

		Vector3 movement = _move_direction * max_speed;
		movement.y      += _vertical_speed;
		movement        *= Time.fixedDeltaTime;

		_cc.Move(movement);
		_grounded = Physics.OverlapSphere(ground_check.position, ground_radius, what_is_ground).Length > 0;

		ImageFace(move);
	}

	void Move(float move)
	{
		_move_direction.x = move;
	}

	void ApplyGravity()
	{
		_vertical_speed = (_grounded)?0.0f:(_vertical_speed - fall_speed);
	}

	void Jump()
	{
		if(_grounded)
		{
			if(Input.GetKeyDown(KeyCode.Space))
			{
				_vertical_speed = Mathf.Sqrt(2 * jump_height * fall_speed);
			}
		}
	}

	// Decide when to flip image
	void ImageFace(float move)
	{
		// Determine when to flip image
		if(move > 0.0f && !_face_right)
		{
			ImageFlip();
		}
		else if(move < 0.0f && _face_right)
		{
			ImageFlip();
		}
	}

	// Flips sprite image, reflective along the y-axis
	void ImageFlip()
	{
		_face_right = !_face_right;
		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}
}
