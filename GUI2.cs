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
	private bool new_cube_get_placed;
	private Transform new_cube_typ;
	
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
		new_cube_typ = TOucher.new_cube_typ;
		new_cube_get_placed = TOucher.new_cube_get_placed;
		
		//GUI Creation
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
			if (GUILayout.RepeatButton(typ1, cube_style)) {
				new_cube_typ=cube1;
				new_cube_get_placed=true;
			}
			
			if (GUILayout.RepeatButton(typ2, cube_style)) {
				new_cube_typ=cube2;
				new_cube_get_placed=true;
			}
			
			if (GUILayout.RepeatButton(typ3, cube_style)) {
				new_cube_typ=cube3;
				new_cube_get_placed=true;
			}
			
			if (GUILayout.RepeatButton(typ4, cube_style)) {
				new_cube_typ=cube4;
				new_cube_get_placed=true;
			}
			
			if (GUILayout.RepeatButton(typ5, cube_style)) {
				new_cube_typ=cube5;
				new_cube_get_placed=true;
			}
			
			if (GUILayout.RepeatButton(typ6, cube_style)) {
				new_cube_typ=cube6;
				new_cube_get_placed=true;
			}
			
			if (GUILayout.RepeatButton(typ7, cube_style)) {
				new_cube_typ=cube7;
				new_cube_get_placed=true;
			}
			
			if (GUILayout.RepeatButton(typ8, cube_style)) {
				new_cube_typ=cube8;
				new_cube_get_placed=true;
			}
			
			if (GUILayout.RepeatButton(typ9, cube_style)) {
				new_cube_typ=cube9;
				new_cube_get_placed=true;
			}
			GUILayout.EndVertical();
			GUILayout.EndHorizontal();
			GUILayout.EndArea();
			
			// right side
			GUILayout.BeginArea (new Rect (Screen.width-100,0,100,Screen.height));
			GUILayout.BeginHorizontal();
			GUILayout.BeginVertical();
			if (GUILayout.RepeatButton(typ10, cube_style)) {
				new_cube_typ=cube10;
				new_cube_get_placed=true;
			}
			
			if (GUILayout.RepeatButton(typ11, cube_style)) {
				new_cube_typ=cube11;
				new_cube_get_placed=true;
			}
			
			if (GUILayout.RepeatButton(typ12, cube_style)) {
				new_cube_typ=cube12;
				new_cube_get_placed=true;
			}
			
			if (GUILayout.RepeatButton(typ13, cube_style)) {
				new_cube_typ=cube13;
				new_cube_get_placed=true;
			}
			
			if(GUILayout.Button("Send \n cubes")) {
				WEbrequest.send_cubes();
			}
			
			if(GUILayout.Button("Clear \n all")) {
				TOucher.clear_stage();
			}
			
			GUILayout.EndVertical();
			GUILayout.EndHorizontal();
			GUILayout.EndArea();
		}
		
		//Additional Buttons (only visible when some cube is active / selected)
		if (cube_active & (active_cube!=null)){
			float x = camera.WorldToScreenPoint(active_cube.transform.position).x;
			float y = Screen.height-camera.WorldToScreenPoint(active_cube.transform.position).y;
			
			if(GUI.Button(new Rect(x-15,y-50,30,30), "X")) {
				if (active_cube!=null){
					TOucher.remove_cube();
				}
			}
			if(GUI.Button(new Rect(x,y,50,30), "->")) {
				active_cube.transform.Rotate(Vector3.up * -90);
			}
			if(GUI.Button(new Rect(x-50,y,50,30), "<-")) {
				active_cube.transform.Rotate(Vector3.up * +90);
			}
		}
		
		//Set external variables
		TOucher.cube_selection_is_open=cube_selection_is_open;
		TOucher.new_cube_typ=new_cube_typ;
		TOucher.new_cube_get_placed=new_cube_get_placed;
	}
}
