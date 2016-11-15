using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class JoinGameScreen : MonoBehaviour {
	public GameObject Game;

	public GameObject rowButton;
	
	private HostData[] hosts;

	// Use this for initialization
	void Start () {
//		Network.InitializeServer(3,NetworkManager.PORT,!Network.HavePublicAddress());
		MasterServer.RequestHostList(NetworkManager.GAME_NAME);
	}

	public void OnMasterServerEvent(MasterServerEvent mse){
		Debug.Log(mse);
		if (mse == MasterServerEvent.RegistrationSucceeded){
//			this.OnConnectedToServer();
		} else if (mse == MasterServerEvent.HostListReceived){
			this.gameObject.SetActive(true);
			
//			this.TextLog("Games received", false);
			
			this.hosts =  MasterServer.PollHostList();
			
			int i = 0;
			foreach (HostData prime in this.hosts){
				GameObject row = Instantiate(this.rowButton) as GameObject;
				int temp = i;

				row.GetComponentInChildren<Text>().text = "NAME :" + prime.gameName + " " + prime.connectedPlayers + "/" + prime.playerLimit;
				row.GetComponent<Button>().onClick.AddListener(() => {
					Debug.Log("click join");
					
					Network.Connect(this.hosts[temp]);
				});
				
				row.transform.SetParent(this.transform);
				row.transform.localScale = Vector3.one;
				i++;
			}
		}
	}

	void OnServerInitialized() {
		Debug.Log("Server initialized and ready");
	}
	
	void OnDisconnectedFromServer(NetworkDisconnection info) {
		if (Network.isServer)
			Debug.Log("Local server connection disconnected");
		else
			if (info == NetworkDisconnection.LostConnection)
				Debug.Log("Lost connection to the server");
		else
			Debug.Log("Successfully diconnected from the server");
	}
	
	void OnFailedToConnect(NetworkConnectionError error) {
		Debug.Log("Could not connect to server: " + error);
	}
	
	void OnConnectedToServer(){
		Debug.Log("Connected");

		this.transform.parent.gameObject.SetActive(false);
		this.Game.SetActive(true);
	}

	public void JoinGame(int i){

	}

	// Update is called once per frame
	void Update () {
	
	}
}
