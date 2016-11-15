using UnityEngine;

using System.Collections;

public class Character : MonoBehaviour {
	public float Health = 100f;
	public float currentHealth {
		get;
		protected set;
	}

	public GameObject DamageText;

	// Use this for initialization
	void Start () {
		Debug.Log("temp");
		this.currentHealth = this.Health;
	}


	

	// Update is called once per frame
	void Update () {
	
	}
}
