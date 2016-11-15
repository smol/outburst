using UnityEngine;
using System.Collections;

[RequireComponent(typeof(DustManagerScript))]
public class Moveable : MonoBehaviour {
	public float Speed = 10.0f;
	public bool isMoving = false;
	public bool isJumping = false;

	private bool canJump = true;
	private Rigidbody2D rigidbody2d;
	private Animator animator;
	private DustManagerScript dustManagerScript;

	void Awake(){
		this.rigidbody2d = this.GetComponent<Rigidbody2D>();
		this.animator = this.GetComponent<Animator>();

		this.dustManagerScript = this.GetComponent<DustManagerScript>();
	}

	// Use this for initialization
	void Start () {
//		this.dustManagerScript.Walk();
	}
	
	// Update is called once per frame
	void Update () {
		float horizontal = Input.GetAxis("Horizontal");

		float translation = Mathf.Round((horizontal * this.Speed * Time.deltaTime) * 100) / 100;

		Vector2 start = new Vector2(this.transform.position.x, this.transform.position.y);
		Vector2 end = new Vector2(translation, 0);

		RaycastHit2D hit = Physics2D.Raycast(start, end, 0.3f, 1 << LayerMask.NameToLayer("Room"));

		if (hit.collider == null){
			this.Move(translation);
		}


		this.Jump();

		if (this.isMoving && !this.isJumping)
			this.dustManagerScript.Walk();

		this.animator.SetBool("IsMoving", this.isMoving);
		this.animator.SetBool("IsJumping", this.isJumping);
	}

	private void Move(float translation){
		transform.Translate(Mathf.Abs(translation), 0, 0);

		this.isMoving = translation != 0;

		if (translation < 0)
			this.transform.localScale = new Vector3(-1f,1f,1f); //(c# code)
		else if (translation > 0)
			this.transform.localScale = new Vector3(1f,1f,1f); //(c# code)

		//		if translation != 0 {
//		if (translation != 0) {
//			this.levelScript.UpdateHeroePosition(this.transform.position);
//		}
		//		}
	}

	private void Jump(){
		if (Input.GetButton("Jump") && this.canJump){
			this.rigidbody2d.AddForce(new Vector2(0,12), ForceMode2D.Impulse);
		}

		float velocity_y = this.rigidbody2d.velocity.y;

		if (this.isJumping && velocity_y == 0){
			this.dustManagerScript.Jump();
		}

		this.canJump = velocity_y <= 0;
		this.isJumping = velocity_y != 0;
	}
}
