using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Runtime.CompilerServices;

[RequireComponent(typeof(Shooter))]
public class Player : MonoBehaviour {
	public LevelScript levelScript;


	void Awake() {
	}

	// Use this for initialization
	void Start () {
	}

	private void InputMovement(){
		
//		this.Shoot(translation);

		StartCoroutine(this.SetAnimation());
	}

	private IEnumerator SetAnimation() {
		yield return null;
	}

	// Update is called once per frame
	void Update () {
		this.InputMovement();
	}


}
