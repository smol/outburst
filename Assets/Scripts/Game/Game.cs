using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

	public GameObject playerPrefab;
	public GameObject camera; 
	public GameObject heroe;

	// Use this for initialization
	void Start () {
		Debug.Log("start");

//		GameObject heroe = Instantiate(this.playerPrefab, new Vector3(0f,0f,0f), Quaternion.identity) as GameObject;
		this.camera.GetComponent<CameraScript>().Heroe = heroe;


	//	SpawnPlayer();
	}

	void OnServerInitialized()
	{
		//Debug.Log("Server Initialized");

	}

	void OnPlayerConnected(NetworkPlayer player) {
//		Debug.Log("PlayerConnected");
//		SpawnPlayer();
	}

	void OnConnectedToServer()
	{

	}

	//[RPC]
	private void SpawnPlayer()
	{
		GameObject player = Network.Instantiate(playerPrefab, new Vector3(0f, 5f, 0f), Quaternion.identity, 0) as GameObject;
		if (player != null && player.GetComponent<NetworkView>().isMine){
			this.camera.GetComponent<CameraScript>().Heroe = player;
		}
	}

	
	// Update is called once per frame
	void Update () {
	
	}


}
