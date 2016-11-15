using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CreateGameScreen : MonoBehaviour {
	public GameObject Game;

	public Text gameNameText;

	// Use this for initialization
	void Start () {
	
	}

	public void StartNewGame(){
		Network.InitializeServer(3,NetworkManager.PORT,!Network.HavePublicAddress());
		MasterServer.RegisterHost(NetworkManager.GAME_NAME, this.gameNameText.text, "THE MUST HAVE");

		this.transform.parent.gameObject.SetActive(false);
		this.Game.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
