using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(ProceduralTileScript))]
public class ProceduralRoomScript : MonoBehaviour {
	public float Unit = 5f;

	public Vector2 TileSize = Vector2.zero;
	public GameObject Hole = null;

	private List<Rect> holes;
	private Vector2 unitSize;
	private Vector2 roomSize;

	private ProceduralTileScript proceduralTileScript;

	void Awake(){
		this.proceduralTileScript = this.GetComponent<ProceduralTileScript>();
	}

	public IEnumerator StartGeneration(Vector2 size, Vector2 position, List<Rect> holes) {
		this.holes = holes;

		// la taille de la salle en unité
		this.unitSize = size;

		this.roomSize.x = size.x * this.TileSize.x * this.Unit;
		this.roomSize.y = size.y * this.TileSize.y * this.Unit;

		this.proceduralTileScript.Init(this.roomSize);

		for (int i = 0; i < this.holes.Count; ++i){

			// get Rect d'une porte
			Rect rect = this.holes[i];

			rect.x -= position.x;
			rect.y -= position.y;

			rect.x *= this.Unit;
			rect.y *= this.Unit;

			rect.width *= this.Unit - 1;
			rect.height *= this.Unit - 1;

			rect.width = Mathf.Round(rect.width);
			rect.height = Mathf.Round(rect.height);

			rect.x = Mathf.Round(rect.x + 1);
			rect.y = Mathf.Round(rect.y + 1);

			this.holes[i] = rect;
		}

		GameObject Hole = Instantiate(this.Hole) as GameObject;
		HoleScript holeScript = Hole.GetComponent<HoleScript>();

		holeScript.StartGeneration(new Rect(1, 1, (this.unitSize.x * this.Unit) - 2, (this.unitSize.y * this.Unit) - 2));

//		Hole.transform.position = new Vector3(-(this.roomSize.x / 2), -(this.roomSize.y / 2), 0);
		Hole.transform.position = Vector3.zero;
		Hole.transform.SetParent(this.transform, false);

		this.holes.Add(holeScript.Rect);

		IEnumerator coroutine = this.Through(new Rect(0, 0, 0, 0), 0, 0);

		while (true) {
			if (!coroutine.MoveNext())
				yield break;

			yield return coroutine.Current;
		}




//		yield return 10;
	}

	private IEnumerator Through(Rect rect, int x, int y){
		// taille de la room en unite
		float width = this.unitSize.x * this.Unit;
		float height = this.unitSize.y * this.Unit;

		rect.width = 1;

		for (x = 0; x < width; ++x){
			rect.y = 0;
			rect.x = x;

			for (y = 0; y <= height; ++y) {
				rect.height = y - rect.y;


				bool doorFound = true;

				while (doorFound) {
					doorFound = false;

					for (int i = 0; i < this.holes.Count; i++){
						Rect doorRect = this.holes[i];

						// si le mur en creation croise une porte, on peut le creer
						if (doorRect.Overlaps(rect)){
							// si tout s'est bien passe on cree le mur

							rect.height -= 1;

							StartCoroutine(this.proceduralTileScript.CreateWall(rect));

							rect.x = x;
							rect.y = doorRect.height + doorRect.y;
							y = (int)rect.y;

							rect.height = 0; 

							doorFound = true;

							break;
						}
					}
				}

				StartCoroutine(this.proceduralTileScript.CreateWall(rect));
			}
		}

		yield break;
	}


}
