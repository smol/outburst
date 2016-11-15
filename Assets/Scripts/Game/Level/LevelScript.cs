using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(ProceduralLevelScript))]
//[ExecuteInEditMode]
public class LevelScript : MonoBehaviour, IProceduralLevelScriptDelegate  {
	public GameObject LoadingScreen;
	public GameObject Minimap;

	public MinimapScript MinimapScript {
		get; private set;
	}

	private ProceduralLevelScript proceduralLevelScript;

	void Awake(){
		this.MinimapScript = this.Minimap.GetComponent<MinimapScript>();
		this.proceduralLevelScript = this.GetComponent<ProceduralLevelScript>();
		this.proceduralLevelScript.Delegate = this;
	}

	// Use this for initialization
	void Start () {


//		this.MinimapScript.StartPosition = this.start_position;

//		// init list of doors
//		this.doors = new List<Rect>();
//
//		// on ajoute un porte principale pour rentrer dans le laryrinthe
//		this.doors.Add(new Rect(-0.5f, 0f, 1f, 0.5f));


//		Debug.Log("END");
	}

	public void Loaded(){
		DestroyImmediate(this.LoadingScreen);
	}

	public void UpdateHeroePosition(Vector3 heroePosition){
		heroePosition.x /= 5;
		heroePosition.y /= 5;

		this.MinimapScript.UpdateHeroePosition(heroePosition);
	}

//	public void RoomCreated(Rect rect) {
//		this.MinimapScript.SetTile(rect);
//	}


	
	// Update is called once per frame
	void Update () {
	
	}
}
