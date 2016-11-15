using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Enemy : Character {

	public float Speed = 10.0f;

	public ParticleSystem particles;
	public GameObject BloodSmudge;
	public Renderer SpriteRenderer;

	private float direction = 1f;

	private float changeDirection = 0f;

	// Use this for initialization
	void Start () {
		this.currentHealth = this.Health;
	}
	
	// Update is called once per frame
	void Update () {
		this.changeDirection += Time.deltaTime;

		if (this.changeDirection >= 3){
			this.changeDirection = 0;
			this.direction = this.direction * -1f;
//			this.transform.FindChild("Enemy_Sprite").transform.localScale = new Vector3(this.direction, 1f,1f);
		}
	}

	public void Damage(float value){
		this.currentHealth -= value;
		
		GameObject text = Instantiate(this.DamageText, Vector3.zero, Quaternion.identity) as GameObject;
		
		text.GetComponent<Text>().text = "-" + value.ToString();
		(text.transform as RectTransform).SetParent(this.GetComponentInChildren<Canvas>().transform);
		(text.transform as RectTransform).anchoredPosition = new Vector3(0,0,0);
	}



	void OnTriggerEnter2D(Collider2D coll){
		if (coll.gameObject.tag == "Shot"){
			this.Damage(coll.gameObject.GetComponent<Shoot>().Damage);

			if (this.currentHealth <= 0){
				this.particles.Play();
				GameObject heroe = GameObject.Find("Player1");
//				heroe.GetComponent<Player>().XP = 50;
				GameObject bloodSmudgeCopy = Instantiate(this.BloodSmudge);

				Renderer bloodSmudgeRenderer = bloodSmudgeCopy.GetComponent<Renderer>();

				Vector3 bloodSmudgePosition = this.transform.position;

				bloodSmudgePosition.y -= (this.SpriteRenderer.bounds.size.y + bloodSmudgeRenderer.bounds.size.y) / 2;
				bloodSmudgePosition.y += this.SpriteRenderer.transform.localPosition.y;

				bloodSmudgeCopy.transform.SetParent(this.transform.parent);
				bloodSmudgeCopy.transform.position = bloodSmudgePosition;


				Destroy(this.gameObject);
			}
		}
	}


//	void OnCollisionEnter2D(Collision2D coll){
//		Debug.Log(coll.collider.tag);
//	}
//
//
//
//
//	void OnTriggerEnter2D(Collider2D coll){
//		Debug.Log(coll.tag);
//		if (coll.gameObject.tag == "Shot"){
//			if (this.GetComponent<Character>().currentHealth <= 0){
//				this.particles.Play();
//			}
//		}
//
////
////		if (coll.gameObject.tag == "Shot"){
//////			this.currentHealth -= coll.gameObject.GetComponent<Shoot>().Damage;
////
////		}
//	}

}
