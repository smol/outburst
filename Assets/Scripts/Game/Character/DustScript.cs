using UnityEngine;
using System.Collections;

public class DustScript : MonoBehaviour {
	public Vector2 direction;
	public float TTL;
	public float speed;

	public AnimationCurve curve;

	private float startPosition;
	private float time;
	private SpriteRenderer spriteRenderer;

	void Awake(){
		this.direction = Vector2.zero;
		this.TTL = 0.0f;
		this.speed = 1.0f;
		this.time = 0.0f;

		this.spriteRenderer = this.GetComponent<SpriteRenderer>();
	}

	// Use this for initialization
	void Start () {
		this.startPosition = this.transform.position.y;
		this.transform.localScale = new Vector3(0, 0, 1);
	}

	// Update is called once per frame
	void Update () {
		if (this.time >= this.TTL){
			Destroy(this.gameObject);
			return;
		}

		this.transform.position = new Vector3(
			(this.direction.x * Time.deltaTime * -this.speed) + this.transform.position.x, 
			this.curve.Evaluate(this.time * this.TTL) + this.startPosition, 0
		);

		this.transform.localScale = new Vector3(this.time + 0.2f, this.time + 0.2f, 1);
		this.spriteRenderer.color = new Color(1, 1, 1, this.TTL / this.time);

		this.time += Time.deltaTime;
	}
}
