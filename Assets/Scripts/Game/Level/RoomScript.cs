using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

[RequireComponent(typeof(ProceduralRoomScript)),ExecuteInEditMode]
public class RoomScript : MonoBehaviour {
	public float Unit = 5f;

	public GameObject Minimap;

	public Vector2 unitSize;
	public Vector2 unitPosition;

	private Vector2 roomSize;
	private Vector3 tileSize;

	private List<Rect> holes;

	private CameraScript cameraScript;
	private ProceduralRoomScript proceduralRoomScript;

	public GameObject Background;
	public GameObject tile;

	private bool activated = false;

	// Use this for initialization
	void Start () {
		this.proceduralRoomScript = this.GetComponent<ProceduralRoomScript>();
		this.cameraScript = GameObject.Find("Main Camera").GetComponent<CameraScript>();
	}

	public IEnumerator New(Vector2 size, Vector2 position, List<Rect> holes) {
		this.unitSize = size;
		this.unitPosition = position;

		this.tileSize = this.tile.GetComponent<Renderer>().bounds.size;

		this.roomSize.x = size.x * this.tileSize.x * this.Unit;
		this.roomSize.y = size.y * this.tileSize.y * this.Unit;

		this.transform.position = new Vector3(
			(position.x * (this.Unit * this.tileSize.x)) + (this.roomSize.x / 2), 
			(position.y * (this.Unit * this.tileSize.y)) + (this.roomSize.y / 2),
			0
		);

		this.GetComponent<BoxCollider2D>().size = new Vector2(this.roomSize.x, this.roomSize.y);

		GameObject temp = Instantiate(this.Background, Vector3.zero, Quaternion.identity) as GameObject;
		Bounds bounds = temp.GetComponent<Renderer>().bounds;

		temp.transform.localScale = new Vector3((this.roomSize.x / bounds.size.x) + 0.1f, this.roomSize.y / bounds.size.y + 0.1f, 1f);
		temp.transform.position = new Vector3(0, 0, 2f);
		temp.transform.SetParent(this.transform, false);

		this.proceduralRoomScript.TileSize = this.tileSize;

		StartCoroutine(this.proceduralRoomScript.StartGeneration(size, position, holes));

		yield return null;

//		while (true){
//			if (!coroutine.MoveNext())
//				yield break;
//
//			object yielded = coroutine.Current;
//
//			if (yielded.GetType() == typeof(Rect))
//				StartCoroutine(this.proceduralTileScript.CreateWall((Rect)yielded));
//
//			yield return null;
//		}
	}



	void OnTriggerEnter2D(Collider2D coll){
		if (coll.gameObject.tag == "Player"){
			Bounds bounds = this.gameObject.GetComponent<BoxCollider2D>().bounds;

			this.cameraScript.CurrentRoom = this.gameObject;

			if (!this.activated){
//				this.levelScript.MinimapScript.SetTile(new Rect(this.unitPosition, this.unitSize));
				this.activated = true;
			}

		}
	}


	// Update is called once per frame
	void Update () {

	}
}
