using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Camera)), ExecuteInEditMode]
public class CameraScript : MonoBehaviour {
	public GameObject Heroe;
	public GameObject Level;
	public Shader shader;

	public GameObject CurrentRoom {
		set {
			this.roomBounds = value.GetComponent<BoxCollider2D>().bounds;

			this.roomChildren = new List<Transform>();

			foreach (Transform child in value.transform) {
				this.roomChildren.Add(child);
			}
		}
	}

	private Bounds roomBounds;
	private List<Transform> roomChildren;

	private new Camera camera;

	void Awake(){
		this.camera = this.GetComponent<Camera>();
//		this.transform.position = this.Heroe.transform.position;
	}

	// Use this for initialization
	void Start () {
//		this.transform.position = this.Heroe.transform.position;
//		this.Update();
//		this.camera.SetReplacementShader(this.shader, "TVOverlay");
//		StartCoroutine(this.Parallax());
	}

	void Parallax(){
//		foreach (Transform child in this.roomChildren){
//			float x = child.position.x + this.transform.position.x;
//			float y = child.position.y + this.transform.position.y;
//
//
//			Debug.LogFormat("child {0}", child);
//			child.position = new Vector3(x * child.position.z, y * child.position.z, child.position.z);
//		}
//
	}

	void Shake(){
		
	}

	// Update is called once per frame
	void Update () {
//		transform.position = new Vector3(this.Heroe.transform.position.x, this.Heroe.transform.position.y, this.transform.position.z);
		if (this.Heroe != null){

			float camVertExtent = this.camera.orthographicSize;
			float camHorzExtent = this.camera.aspect * camVertExtent;

			float camX = 0, camY = 0;

			if ((camHorzExtent * 2) <= this.roomBounds.size.x) {
				float leftBound = this.roomBounds.min.x + camHorzExtent;
				float rightBound = this.roomBounds.max.x - camHorzExtent;

				camX = Mathf.Clamp(this.Heroe.transform.position.x, leftBound, rightBound);
			} else {
				camX = this.roomBounds.center.x;
			}

			if ((camVertExtent * 2) <= this.roomBounds.size.y) {
				float bottomBound = this.roomBounds.min.y + camVertExtent;
				float topBound = this.roomBounds.max.y - camVertExtent;

				camY = Mathf.Clamp(this.Heroe.transform.position.y, bottomBound, topBound);
			} else {
				camY = this.roomBounds.center.y;
			}

			this.transform.position = new Vector3(camX, camY, this.transform.position.z);
		}

//		Parallax();
	}
}
