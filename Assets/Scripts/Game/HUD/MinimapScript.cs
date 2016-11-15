using UnityEngine;
using System.Collections;

public class MinimapScript : MonoBehaviour {

	public Vector2 unitSize;

	public GameObject Offset;
	public GameObject tile;

	public float StartPosition;

	// Use this for initialization
	void Start () {
	}

	void Awake() {
	}

	public void SetTile(Rect rect) {
		GameObject tempTile = Instantiate(this.tile) as GameObject;
		RectTransform rectTransform = tempTile.GetComponent<RectTransform>();

		rectTransform.position = new Vector3(rect.x * this.unitSize.x, rect.y * this.unitSize.y, 1);
		rectTransform.transform.SetParent(this.Offset.transform, false);

		rectTransform.sizeDelta = new Vector2(rect.width * this.unitSize.x, rect.height * this.unitSize.y);
	}

	public void UpdateHeroePosition(Vector3 heroePosition) {
		RectTransform rectTransform = this.Offset.GetComponent<RectTransform>();
		RectTransform rootTransform = this.GetComponent<RectTransform>();

		Vector2 position = new Vector2();

		heroePosition.x = Mathf.Round(heroePosition.x);
		heroePosition.y = Mathf.Round(heroePosition.y);

		position.x = -(heroePosition.x * this.unitSize.x) - this.StartPosition;
		position.y = -(heroePosition.y * this.unitSize.y);

		position.x -= (rootTransform.sizeDelta.x + this.unitSize.x) / 2;
		position.y -= (rootTransform.sizeDelta.y + this.unitSize.y) / 2;

		rectTransform.localPosition = new Vector3(position.x, position.y, 0);
	}

	public void SetSize(Vector2 size) {
		RectTransform rectTransform = this.Offset.GetComponent<RectTransform>();

		rectTransform.sizeDelta = new Vector2(size.x * this.unitSize.x, size.y * this.unitSize.y);
	}
}
