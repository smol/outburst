using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {

	public GameObject character;

	private Character script;
	private float totalWidth = 30;

	// Use this for initialization
	void Start () {
		this.script = this.character.GetComponent<Character>();
		this.totalWidth = (this.transform as RectTransform).rect.width;
		Debug.Log(this.totalWidth);
	}
	
	// Update is called once per frame
	void Update () {
		(this.transform as RectTransform).sizeDelta = new Vector2(this.script.currentHealth / this.script.Health * this.totalWidth, 3);
	}
}
