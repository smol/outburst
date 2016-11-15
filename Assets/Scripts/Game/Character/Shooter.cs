using UnityEngine;
using System.Collections;

public class Shooter : MonoBehaviour {
	public Vector3 startPosition = Vector3.zero;

	public GameObject shotGameObject;
	public int AttackSpeed = 250;
	public bool isShooting = false;

	private float lastShot = 0;

	private Animator animator;

	void Awake(){
		this.animator = this.GetComponent<Animator>();
	}

	void OnDrawGizmosSelected(){
		Gizmos.color = new Color(2, 0, 0, 1.0f);
		Gizmos.DrawCube(this.startPosition + this.transform.position, new Vector3(0.2f, 0.2f, 1));
	}

	// Update is called once per frame
	void Update () {
		this.animator.SetBool("IsShooting", this.isShooting);

		this.Shoot(1);
	}

	private void Shoot(float translation){
		this.isShooting = Input.GetButton("Fire1");

		if (this.isShooting && this.lastShot >= this.AttackSpeed){
			this.lastShot = 0;

			Vector3 position = this.startPosition;
			position.x *= this.transform.localScale.x;

			GameObject shoot = Instantiate(this.shotGameObject, position + this.transform.position, Quaternion.identity) as GameObject;
			shoot.transform.localScale = this.transform.localScale;
			shoot.GetComponent<Shoot>().direction = 1f;
		}

		this.lastShot += Time.deltaTime * 1000;
	}
}
