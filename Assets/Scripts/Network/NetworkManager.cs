using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NetworkManager : MonoBehaviour {
	public static int PORT = 23466;
	public static string GAME_NAME = "NETWORK_OUTBURST_1337";
	
	public GameObject listPanel;
	public GameObject createGamePanel;

	public GameObject gameNameText;

	public GameObject debugPanel;
	public GameObject debugText;

	private int playerCount = 0;

	// Use this for initialization
	void Start () {
		MasterServer.ipAddress = "5.39.88.144";
	
	}

	private void TextLog(string text, bool isError){
		GameObject debug = Instantiate(this.debugText) as GameObject;
		debug.GetComponent<Text>().text = text;

		if (isError)
			debug.GetComponent<Text>().color = Color.red;

		debug.transform.SetParent(this.debugPanel.transform);

		debug.transform.position = new Vector3(debug.transform.position.x,debug.transform.position.y,0);
		debug.transform.localScale = Vector3.one;
	}

	public void OnServerInitialized(){
		this.TextLog("Server Initialized", false);
	}

//	public void OnMasterServerEvent(MasterServerEvent mse){
//		if (mse == MasterServerEvent.RegistrationSucceeded){
//			this.OnConnectedToServer();
//		} 
//		else if (mse == MasterServerEvent.HostListReceived){
//			this.listPanel.SetActive(true);
//
//			this.TextLog("Games received", false);
//
//			this.hosts =  MasterServer.PollHostList();
//
//			int i = 0;
//			foreach (HostData prime in this.hosts){
//				GameObject row = Instantiate(this.rowButton) as GameObject;
//				int temp = i;
//				row.GetComponentInChildren<Text>().text = "NAME :" + prime.gameName + " " + prime.connectedPlayers + "/" + prime.playerLimit;
//				row.GetComponent<Button>().onClick.AddListener(() => {
//					this.JoinGame(temp);
//				});
//
//				row.transform.SetParent(this.listPanel.transform);
//				row.transform.localScale = Vector3.one;
//				i++;
//			}
//		}
//	}

	void OnFailedToConnect(NetworkConnectionError error) {
		this.TextLog("Could not connect to server: " + error, true);
	}


	void OnPlayerConnected(NetworkPlayer player) {
		Debug.Log("Player " + playerCount + " connected from " + player.ipAddress + ":" + player.port);
		this.TextLog("Player " + playerCount++ + " connected from " + player.ipAddress + ":" + player.port, false);
	}

	// Update is called once per frame
	void Update () {
	
	}
}
