using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {
	public float direction = 1f;
	public float Damage = 10f;
	public ParticleSystem Particles;

	private bool isDestroyed = false;
	private Camera mainCamera;

	void Awake(){
		this.mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
	}

	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {
		if (!this.isDestroyed){

			Vector3 translation = new Vector3(20f * direction * Time.deltaTime,0,0);

			StartCoroutine(this.DetectCollision(translation));

//			if (this.transform.position.x < this.mainCamera.rect.min.x || this.transform.position.x > this.mainCamera.rect.max.x)
//				Destroy(this.gameObject);

			this.transform.Translate(translation);
		}
	}

	private IEnumerator DetectCollision(Vector3 translation) {
		Vector3 end = this.transform.localScale;
		end.x += translation.x;
		RaycastHit2D hit = Physics2D.Raycast(this.transform.position, end, 0f, 1 << LayerMask.NameToLayer("Room"));

		if (hit.collider != null){
			this.Explode();
		}

		yield return null;
	}
	
	public void Explode(){
		this.isDestroyed = true;

		this.GetComponent<SpriteRenderer>().enabled = false;

		this.Particles.Play();

	}

	public void OnBecameInvisible(){
		Destroy(this.gameObject, 1);
	}

	void OnTriggerEnter2D(Collider2D coll){
		if (coll.tag == "Room" || coll.tag == "Enemy"){
			this.Explode();
		}
	}
}
