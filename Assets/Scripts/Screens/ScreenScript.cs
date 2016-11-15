using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScreenScript : MonoBehaviour {
	public Color SelectedColor;
	public Color NormalColor;
	public GameObject[] MenuEntry;

	private int index = 0;
	private float TTL = 250;


	// Use this for initialization
	void Start () {
		for (int i = 0; i < this.MenuEntry.Length; i++){
			this.MenuEntry[i].GetComponentInChildren<Text>().color = this.NormalColor;
		}

		this.MenuEntry[this.index].GetComponentInChildren<Text>().color = this.SelectedColor;
	}
	
	// Update is called once per frame
	void Update () {

		float vert = Input.GetAxis("Vertical");

		if (this.TTL >= 250){
			if (vert < 0) {
				this.TTL = 0;
				this.MenuEntry[this.index].GetComponentInChildren<Text>().color = this.NormalColor;
				this.index++;

				if (this.index >= this.MenuEntry.Length)
					this.index = 0;

				this.MenuEntry[this.index].GetComponentInChildren<Text>().color = this.SelectedColor;
			} else if (vert > 0) {
				this.TTL = 0;
				this.MenuEntry[this.index].GetComponentInChildren<Text>().color = this.NormalColor;
				this.index--;
				
				if (this.index < 0)
					this.index = this.MenuEntry.Length - 1;
				
				this.MenuEntry[this.index].GetComponentInChildren<Text>().color = this.SelectedColor;
			} 
		}

		if (Input.GetButton("Submit"))
			this.MenuEntry[this.index].GetComponent<Button>().onClick.Invoke();

//		Debug.Log(Time.unscaledDeltaTime);
		this.TTL += Time.deltaTime * 1000;

	}
}
