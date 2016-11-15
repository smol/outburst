using UnityEngine;
using System.Collections;

public class FirstScreenScript : MonoBehaviour {

	public GameObject CreateGameScreen;
	public GameObject JoinGameScreen;

	// Use this for initialization
	void Start () {
	
	}

	public void CreateGame(){
		this.gameObject.SetActive(false);
		this.CreateGameScreen.SetActive(true);
	}

	public void JoinGame(){
		this.gameObject.SetActive(false);
		this.JoinGameScreen.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
