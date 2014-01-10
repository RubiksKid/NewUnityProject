using UnityEngine;
using System.Collections;

public class CatController : MonoBehaviour {

	public float maxSpeed = 5f;
	public float airDamping = 25f;
	public float floorDamping = 10f;
	public float jumpHeight = 30f;

	bool facingRight = true;
	public bool locked = false;
	Animator animr;

	bool grounded = false;
	public Transform groundCheck;

	float groundRadius = 0.1f;
	public LayerMask whatIsGround;

	// Use this for initialization
	void Start () 
	{
		animr = GetComponent<Animator>();
	}

	// Use FixedUpdate for physics due to fixed deltaTime.
	void FixedUpdate () 
	{
		grounded = Physics2D.OverlapCircle(groundCheck.position,groundRadius,whatIsGround);

		float horz  = Input.GetAxis("Horizontal");
		animr.SetFloat("vSpeed",rigidbody2D.velocity.y);
		animr.SetBool("Ground",grounded);
		animr.SetFloat("Speed",Mathf.Abs(horz));
		animr.SetBool("locked",locked);
		if(!grounded || locked)
		return;
		rigidbody2D.velocity = new Vector2(horz * maxSpeed,rigidbody2D.velocity.y);
		///rigidbody2D.AddForce(new Vector2(horz*20,-.85f*rigidbody2D.mass*Physics2D.gravity.y));
		if(horz > 0 && !facingRight)
			Flip ();
		else if(horz < 0 && facingRight)
			Flip ();


	}
	//called every frame
	void Update()
	{
		if(grounded && Input.GetAxis("Jump") > 0)
		{
			animr.SetBool("Ground",false);
			rigidbody2D.AddForce(new Vector2(0,130));
		}
	}

	void Flip()
	{
		//modify flag
		facingRight = !facingRight;

		//flips image by inverting x-scale: 
		//done this way specifically to avoid the inability to modify a property.
		Vector3 newScale = transform.localScale;
		newScale.x *= -1;
		transform.localScale = newScale;

	}


}
