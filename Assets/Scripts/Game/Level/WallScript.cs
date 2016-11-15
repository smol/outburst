using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WallScript : MonoBehaviour {
	public GameObject sprite;

	public Vector2 size {
		get;
		private set;
	}

	private Vector3 tile_size;


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private IEnumerator InstantiateWall(Rect rect){
//		Debug.LogFormat("Instantiate Wall {0}", rect);
		for (int x = 0; x < rect.width; ++x){
			for (int y = 0; y < rect.height; ++y){
				
				GameObject copy = Instantiate(this.sprite, Vector3.zero, Quaternion.identity) as GameObject;

				copy.transform.position = new Vector3(((x - (rect.width / 2)) * this.tile_size.x) + (this.tile_size.x / 2), (y - (rect.height / 2)) * this.tile_size.y + (this.tile_size.y / 2), 0);
				copy.transform.SetParent(this.transform, false);

				yield return null;
			}
		}

		yield break;
	}

	public IEnumerator SetRect(Rect rect, Vector2 roomSize, Transform roomTransform){
		this.tile_size = this.sprite.GetComponent<SpriteRenderer>().bounds.size;

		this.size = new Vector2((rect.width * this.tile_size.x), (rect.height * this.tile_size.y));

		rect.x *= this.tile_size.x;
		rect.y *= this.tile_size.y;

		this.transform.position = new Vector3(rect.x - ((roomSize.x - this.size.x) / 2), rect.y - ((roomSize.y - this.size.y) / 2), 0);

		this.transform.SetParent(roomTransform, false);

		this.GetComponent<BoxCollider2D>().size = this.size;
//
		yield return this.InstantiateWall(rect);
	}
}
