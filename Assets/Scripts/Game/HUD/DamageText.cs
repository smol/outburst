using UnityEngine;
using System.Collections;

public class DamageText : MonoBehaviour {
	public float TTL = 1;
	public float Speed = 1;
	private float currentTime = 0;


	// Use this for initialization
	void Start () {
		this.transform.localScale = Vector3.one;
	}
	
	// Update is called once per frame
	void Update () {
		if (this.currentTime >= this.TTL){


			Destroy(this.gameObject);
		}
		this.currentTime += Time.deltaTime;

		this.transform.Translate(0, this.Speed * Time.deltaTime, 0);
	}
}
