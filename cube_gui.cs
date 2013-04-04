/*
 * This file contains the GUI elements.
 * 
 * 
 */

using UnityEngine;
using System.Collections;

//"Import" scripts
[RequireComponent (typeof (toucher))]
[RequireComponent (typeof (webrequest))]


public class cube_gui : MonoBehaviour {
	
	//Publics
	public GUIStyle background_style;
	public GUIStyle cube_style;
	public Texture typ1;
	public Texture typ2;
	public Texture typ3;
	
	public Transform cube1;
	public Transform cube2;
	public Transform cube3;
	public Transform cube4;
	public Transform cube5;
	public Transform cube6;
	public Transform cube7;
	public Transform cube8;
	public Transform cube9;
	
	//Privates
	private toucher TOucher; 
	private webrequest WEbrequest;
	
	private bool cube_active=false;
	private GameObject active_cube;
	
	
	void Start(){
		//This function get called when the game starts, 
		//here it maps the external scripts
		TOucher = this.gameObject.GetComponent<toucher>();
		WEbrequest = this.gameObject.GetComponent<webrequest>();
	}
		
	void OnGUI () {
		//fetch variables from toucher script
		cube_active= TOucher.cube_active;
		active_cube= TOucher.active_cube;
		
		// Make a background box
		GUI.Box(new Rect(2,Screen.height-98,Screen.width-4,98), "Cube Selection", background_style);
		
		//Create cube buttons
		if(GUI.Button(new Rect(10,Screen.height-80,100,65), typ1, cube_style)) {
			Instantiate(cube1, new Vector3(0, 0.5f, 0), Quaternion.identity);
		}

		if(GUI.Button(new Rect(110,Screen.height-80,100,65), typ2, cube_style)) {
			Instantiate(cube2, new Vector3(0, 0.5f, 0), Quaternion.identity);
		}
		
		if(GUI.Button(new Rect(210,Screen.height-80,100,65), typ1, cube_style)) {
			Instantiate(cube3, new Vector3(0, 0.5f, 0), Quaternion.identity);
		}
		
		if(GUI.Button(new Rect(310,Screen.height-80,100,65), typ1, cube_style)) {
			Instantiate(cube4, new Vector3(0, 0.5f, 0), Quaternion.identity);
		}
		
		if(GUI.Button(new Rect(410,Screen.height-80,100,65), typ1, cube_style)) {
			Instantiate(cube5, new Vector3(0, 0.5f, 0), Quaternion.identity);
		}
		
		if(GUI.Button(new Rect(510,Screen.height-80,100,65), typ1, cube_style)) {
			Instantiate(cube6, new Vector3(0, 0.5f, 0), Quaternion.identity);
		}
		
		if(GUI.Button(new Rect(610,Screen.height-80,100,65), typ1, cube_style)) {
			Instantiate(cube7, new Vector3(0, 0.5f, 0), Quaternion.identity);
		}
		
		if(GUI.Button(new Rect(710,Screen.height-80,100,65), typ1, cube_style)) {
			Instantiate(cube8, new Vector3(0, 0.5f, 0), Quaternion.identity);
		}
		
		if(GUI.Button(new Rect(810,Screen.height-80,100,65), typ1, cube_style)) {
			Instantiate(cube9, new Vector3(0, 0.5f, 0), Quaternion.identity);
		}
		
		//Additional Buttons (only visible when some cube is active / selected)
		if (cube_active){
			if(GUI.Button(new Rect(Screen.width-50,Screen.height-80,50,65), "Delete")) {
				if (active_cube!=null){
					Destroy(active_cube.gameObject);
					TOucher.cube_active=false;
				}
			}
			if(GUI.Button(new Rect(Screen.width-100,Screen.height-80,50,65), "Send-cubes")) {
				if (active_cube!=null){
					WEbrequest.send_cubes();
				}
			}
			Debug.Log(camera.WorldToScreenPoint(active_cube.transform.position));
			if(GUI.Button(new Rect(camera.WorldToScreenPoint(active_cube.transform.position).x,Screen.height-camera.WorldToScreenPoint(active_cube.transform.position).y,50,20), "->")) {
				active_cube.transform.Rotate(Vector3.up * -90);
			}
		}
		
	}
}
