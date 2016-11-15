using UnityEngine;
using System.Collections;

public class DustManagerScript : MonoBehaviour {
	public GameObject dust;
	public Vector3 startPosition;

	private float walkDelta = 0.0f;

	public AnimationCurve[] curves;

	void OnDrawGizmosSelected(){
		Gizmos.color = new Color(2, 2, 0, 1.0f);
		Gizmos.DrawCube(this.startPosition + this.transform.position, new Vector3(0.2f, 0.2f, 0f));
	}

	public void Jump(){
		GameObject newDust = Instantiate(this.dust) as GameObject;

		DustScript dustScript = newDust.GetComponent<DustScript>();
		dustScript.curve = this.curves[1];
		dustScript.direction = new Vector2(-1, this.transform.localScale.y);
		dustScript.TTL = Random.Range(0.5f, 1.0f);


		newDust.transform.position = new Vector3(
			this.transform.position.x + this.startPosition.x,
			this.transform.position.y + this.startPosition.y,
			-1.0f
		);

		newDust = Instantiate(this.dust) as GameObject;

		dustScript = newDust.GetComponent<DustScript>();
		dustScript.curve = this.curves[1];
		dustScript.direction = new Vector2(1, this.transform.localScale.y);
		dustScript.TTL = Random.Range(0.5f, 1.0f);


		newDust.transform.position = new Vector3(
			this.transform.position.x + this.startPosition.x,
			this.transform.position.y + this.startPosition.y,
			-1.0f
		);
	}

	public void Walk(){
		if (this.walkDelta >= 0.02f){
			this.walkDelta = 0;

			GameObject newDust = Instantiate(this.dust) as GameObject;

			DustScript dustScript = newDust.GetComponent<DustScript>();
			dustScript.curve = this.curves[1];
			dustScript.direction = new Vector2(this.transform.localScale.x, this.transform.localScale.y);
			dustScript.TTL = Random.Range(0.5f, 1.0f);


			newDust.transform.position = new Vector3(
				this.transform.position.x + this.startPosition.x,
				this.transform.position.y + this.startPosition.y,
				-1.0f
			);
		}

		this.walkDelta += Time.deltaTime;

//		newDust.transform.SetParent(this.transform, false);
	}
}
