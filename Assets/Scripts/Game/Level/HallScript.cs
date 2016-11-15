using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class HallScript : MonoBehaviour {

	public CameraScript cameraScript;

	// Use this for initialization
	void Start () {
//		this.cameraScript = GameObject.Find("Main Camera").GetComponent<CameraScript>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D coll){
		if (coll.gameObject.tag == "Player"){
			this.cameraScript.CurrentRoom = this.gameObject;
		}
	}
}
