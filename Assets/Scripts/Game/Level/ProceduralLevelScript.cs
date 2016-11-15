using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IProceduralLevelScriptDelegate {
	void Loaded();
}

//[ExecuteInEditMode]
public class ProceduralLevelScript : MonoBehaviour {
	public IProceduralLevelScriptDelegate Delegate;

	public bool enabled = true;

	public enum Orientation {
		HORIZONTAL, VERTICAL
	};

	private List<Rect> doors;
	private float startPosition = 0;

	public GameObject RoomPrefab;
	public Vector2 Size;

	void Awake(){
		// init doors list
		this.doors = new List<Rect>();

		// on ajoute un porte principale pour rentrer dans le laryrinthe
		this.doors.Add(new Rect(-0.5f, 0f, 1f, 0.5f));

		// Get Renderer of the hall
		BoxCollider2D hallRenderer = GameObject.Find("Hall").GetComponent<BoxCollider2D>();

		// Get tail of HALL for start the maze
		this.startPosition = hallRenderer.bounds.size.x / 2;

		if (this.enabled)
			// start maze generation
			StartCoroutine("Launch");
	}

	private IEnumerator Launch(){
		// get orientation of maze
		Orientation orientation = this.GetOrientation((int)(this.Size.x), (int)(this.Size.y));

		Debug.Log("Start Generation");

		IEnumerator coroutine = this.Divide(0,0, (int)(this.Size.x), (int)(this.Size.y), orientation);

//		this.MinimapScript.SetSize(this.Size);

		while (true) {
			if (!coroutine.MoveNext()){
				this.Delegate.Loaded();
				Debug.LogFormat("End generation {0}", Time.realtimeSinceStartup);
				yield break;
			}

			yield return null;
		}



		yield return null;
	}

	/// <summary>
	/// Divide the specified width, height and orientation. Start from x and y
	/// </summary>
	/// <param name="x">The x coordinate to start</param>
	/// <param name="y">The y coordinate to start</param>
	/// <param name="width">Width to divide</param>
	/// <param name="height">Height to divide</param>
	/// <param name="orientation">Orientation</param>
	public IEnumerator Divide(int x, int y, int width, int height, Orientation orientation){
		yield return null;

		// if we cannot divide, we create the room
		if (width <= (4 + Random.value * 8) && height <= 8){
			StartCoroutine(this.CreateRoom(x, y, width, height));
			yield break;
		}

		// get coord of the separator
		int separator_x = orientation == Orientation.VERTICAL ? (int)Mathf.Round(Random.value * ((float)width - 2)) + 3: 0;
		int	separator_y = orientation == Orientation.HORIZONTAL ? (int)Mathf.Round(Random.value * ((float)height - 2)) + 3 : 0;

		int diff_width = orientation == Orientation.HORIZONTAL ? width : width - separator_x;
		int diff_height = orientation == Orientation.VERTICAL ? height : height - separator_y;

		int rest_width = orientation == Orientation.HORIZONTAL ? width : width - diff_width;
		int rest_height = orientation == Orientation.VERTICAL ? height : height - diff_height;

		if (diff_width > 0 && diff_height > 0){
			Rect door = this.CalculateDoorCoord(separator_x + x, separator_y + y, diff_width, diff_height, orientation);
			this.doors.Add(door);

			Orientation child_orientation = this.GetOrientation(diff_width,diff_height);
			StartCoroutine(this.Divide(separator_x + x, separator_y + y, diff_width, diff_height, child_orientation));
		}

		if (rest_width > 0 && rest_height > 0){
			Orientation rest_orientation = this.GetOrientation(rest_width,rest_height);
			StartCoroutine(this.Divide(x,y, rest_width, rest_height,rest_orientation));
		}

		//		yield return new WaitForSeconds(2f);
	}

	private IEnumerator CreateRoom(int x, int y, int width, int height){
		GameObject room = Instantiate(this.RoomPrefab) as GameObject;
		room.transform.SetParent(this.transform);

		Vector2 roomSize = new Vector2(width, height);

		yield return null;

		List<Rect> doors = this.FindDoors(x, y, width, height);

		if (doors.Count > 0){
			StartCoroutine(room.GetComponent<RoomScript>().New(roomSize, new Vector2(x, y), doors));

			room.transform.position = new Vector3(this.startPosition + room.transform.position.x, room.transform.position.y - 0.3f, 0);
		}

	}

	private List<Rect> FindDoors(int x, int y, float width, float height){
		List<Rect> doors = new List<Rect>();

		for (int i = 0; i < this.doors.Count; ++i){
			Rect door = this.doors[i];

			if (door.Overlaps(new Rect(x, y, width, height)))
				doors.Add(door);
		}

		return doors;
	}

	private Rect CalculateDoorCoord(int x,int y,int width, int height, Orientation orientation){
		float door_size = 0.5f;

		Rect obj = new Rect(){
			x = x, y = y,
			width = 1f, height = 1f
		};

		if (orientation == Orientation.HORIZONTAL){
			//			obj.x += (float);
			obj.y -= 0.5f;
			//			obj.x += 0;
			obj.width = door_size;
			obj.x += Mathf.Round(Random.value * ((float)width - obj.width));
		} else {// if orientation == "VERTICAL"
			obj.x -= 0.5f;
			obj.height = door_size;
			obj.y += Mathf.Round(Random.value * ((float)height - obj.height));
		}

		return obj;
	}

	private Orientation GetOrientation(int width, int height){
		if (width < height)
			return Orientation.HORIZONTAL;
		else if (height < width)
			return Orientation.VERTICAL;


		//		return Orientation.VERTICAL;
		return Mathf.Floor(Random.value * 2) == 1 ? Orientation.HORIZONTAL : Orientation.VERTICAL;
	}
}
