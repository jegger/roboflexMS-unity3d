using UnityEngine;
using System.Collections;

//"Import" scripts
[RequireComponent (typeof (toucher))]
[RequireComponent (typeof (webrequest))]


public class GUI2 : MonoBehaviour {
	//Public
	public GUIStyle selection_button;
	public GUIStyle background_style;
	public GUIStyle cube_style;
	
	public Texture typ1;
	public Texture typ2;
	public Texture typ3;
	public Texture typ4;
	public Texture typ5;
	public Texture typ6;
	public Texture typ7;
	public Texture typ8;
	public Texture typ9;
	public Texture typ10;
	public Texture typ11;
	public Texture typ12;
	public Texture typ13;
	
	public Transform cube1;
	public Transform cube2;
	public Transform cube3;
	public Transform cube4;
	public Transform cube5;
	public Transform cube6;
	public Transform cube7;
	public Transform cube8;
	public Transform cube9;
	public Transform cube10;
	public Transform cube11;
	public Transform cube12;
	public Transform cube13;
	
	//privates
	private toucher TOucher; 
	private webrequest WEbrequest;
	
	private bool cube_active=false;
	private GameObject active_cube;
	
	private bool cube_selection_is_open = false;
	
	
	void Start(){
		//This function get called when the game starts, 
		//here it maps the external scripts
		TOucher = this.gameObject.GetComponent<toucher>();
		WEbrequest = this.gameObject.GetComponent<webrequest>();
	}
	
	
	void OnGUI(){
		//Read valies from other files
		cube_active= TOucher.cube_active;
		active_cube= TOucher.active_cube;
		
		if (cube_selection_is_open==false){
			//Cube selection menu is closed
			//Display only one button			
			if(GUI.Button(new Rect(Screen.width-170,20,150,50), "Wurfel hinzufugen", selection_button)) {
				cube_selection_is_open=true;
			}
		}else{
			//Display Cube selection
			
			//draw background
			GUI.Box(new Rect(0,0,100,Screen.height), "", background_style);
			GUI.Box(new Rect(Screen.width-100, 0, Screen.width-100, Screen.height), "", background_style);
			
			//close button
			if(GUI.Button(new Rect(Screen.width-30,0,30,30), "X", selection_button)) {
				cube_selection_is_open=false;
			}
			
			//cubes button
			
			// Left side
			GUILayout.BeginArea (new Rect (0,0,100,Screen.height));
			GUILayout.BeginHorizontal();
			GUILayout.BeginVertical();
			if (GUILayout.Button(typ1, cube_style)) {
				Instantiate(cube1, new Vector3(0, 0.5f, 0), Quaternion.identity);
			}
			
			if (GUILayout.Button(typ2, cube_style)) {
				Instantiate(cube2, new Vector3(0, 0.5f, 0), Quaternion.identity);
			}
			
			if (GUILayout.Button(typ3, cube_style)) {
				Instantiate(cube3, new Vector3(0, 0.5f, 0), Quaternion.identity);
			}
			
			if (GUILayout.Button(typ4, cube_style)) {
				Instantiate(cube4, new Vector3(0, 0.5f, 0), Quaternion.identity);
			}
			
			if (GUILayout.Button(typ5, cube_style)) {
				Instantiate(cube5, new Vector3(0, 0.5f, 0), Quaternion.identity);
			}
			
			if (GUILayout.Button(typ6, cube_style)) {
				Instantiate(cube6, new Vector3(0, 0.5f, 0), Quaternion.identity);
			}
			
			if (GUILayout.Button(typ7, cube_style)) {
				Instantiate(cube7, new Vector3(0, 0.5f, 0), Quaternion.identity);
			}
			
			if (GUILayout.Button(typ8, cube_style)) {
				Instantiate(cube8, new Vector3(0, 0.5f, 0), Quaternion.identity);
			}
			
			if (GUILayout.Button(typ9, cube_style)) {
				Instantiate(cube9, new Vector3(0, 0.5f, 0), Quaternion.identity);
			}
			GUILayout.EndVertical();
			GUILayout.EndHorizontal();
			GUILayout.EndArea();
			
			// right side
			GUILayout.BeginArea (new Rect (Screen.width-100,0,100,Screen.height));
			GUILayout.BeginHorizontal();
			GUILayout.BeginVertical();
			if (GUILayout.Button(typ10, cube_style)) {
				Instantiate(cube10, new Vector3(0, 0.5f, 0), Quaternion.identity);
			}
			
			if (GUILayout.Button(typ11, cube_style)) {
				Instantiate(cube11, new Vector3(0, 0.5f, 0), Quaternion.identity);
			}
			
			if (GUILayout.Button(typ12, cube_style)) {
				Instantiate(cube12, new Vector3(0, 0.5f, 0), Quaternion.identity);
			}
			
			if (GUILayout.Button(typ13, cube_style)) {
				Instantiate(cube13, new Vector3(0, 0.5f, 0), Quaternion.identity);
			}
			
			GUILayout.EndVertical();
			GUILayout.EndHorizontal();
			GUILayout.EndArea();
			
			
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
		TOucher.cube_selection_is_open=cube_selection_is_open;
	}
}
