using UnityEngine;
using System.Collections;

public class HoleScript : MonoBehaviour {
	private Rect rect;
	public Rect Rect {
		get { return this.rect; }
	}

	public GameObject Floor;

	private Vector2 tileSize;

	void Awake(){
		this.tileSize = this.Floor.GetComponent<SpriteRenderer>().bounds.size;
	}

	// Use this for initialization
	void Start () {
	}

	public void StartGeneration(Rect rect){
		this.rect = rect;

		StartCoroutine(this.Generate());
	}

	private IEnumerator Generate(){
//		for (int x = (int)this.rect.x; x < (int)this.rect.width; x++) {
////			for (int y = (int)this.rect.y; y < (int)this.rect.height; y++) {
////				if (((y - this.rect.y) % 3) == 0){
//					GameObject temp = Instantiate(this.Floor) as GameObject;
////
//			temp.transform.position = new Vector3(x * this.tileSize.x, 0, 0);
//			temp.transform.SetParent(this.transform, false);
////				}
////			}
//		}

//		Debug.Log("Je passe la");

//		GameObject temp = Instantiate(this.Floor) as GameObject;
//		temp.transform.position = Vector3.zero;
//		temp.transform.SetParent(this.transform, false);


		yield return null;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
