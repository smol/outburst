using UnityEngine;
using System.Collections;

public class ProceduralTileScript : MonoBehaviour {
	public GameObject[] Tiles;
	public GameObject Wall;

	private Vector2 roomSize;

	public void Init(Vector2 roomSize){
		this.roomSize = roomSize;
	}

	public IEnumerator CreateWall(Rect rect){
		if (rect.width <= 0 || rect.height <= 0){
			yield break;
		} else {
			GameObject wall = Instantiate(this.Wall) as GameObject;

			WallScript script = wall.GetComponent<WallScript>();
			//			Debug.LogFormat("roomSize {0}", this.roomSize);
			StartCoroutine(script.SetRect(rect, this.roomSize, this.transform));
		}

		yield return null;
	}
}
