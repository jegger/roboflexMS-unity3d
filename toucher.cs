using UnityEngine;
using System.Collections;

public class toucher : MonoBehaviour {
	
	//Publics
	public float camera_speed=3f;
	public Material active_cube_material;
	public Material normal_cube_material;
	
	//Privates for inspector
	[System.NonSerialized] public bool cube_active=false;
	[System.NonSerialized] public GameObject active_cube;
	[System.NonSerialized] public bool cube_selection_is_open = false;
	
	//Privates
	private float new_cube_x;
	private float new_cube_y;
	private float new_cube_z;
	
	private bool dragging_camera = false;
	private Transform cubes;
	private float zoomSpeed;
	private float dist;
	private Vector3 center=Vector3.zero;
		
	void OnEnable(){
		Gesture.onDraggingE += OnDrag;
		Gesture.onTouchUpE += OnTouchUp;
		Gesture.onMouse1UpE += OnTouchUp;
		Gesture.onTouchDownE +=OnTouchDown;
		Gesture.onMouse1DownE +=OnTouchDown;
		Gesture.onPinchE +=onPinch;
	}
	
	void OnDisable(){
		Gesture.onDraggingE -= OnDrag;
		Gesture.onTouchUpE -= OnTouchUp;
		Gesture.onMouse1UpE -= OnTouchUp;
		Gesture.onTouchDownE -=OnTouchDown;
		Gesture.onMouse1DownE -=OnTouchDown;
	}

	void OnTouchUp(Vector2 touch){
		dragging_camera=false;
		zoomSpeed=0.0f;
	}
	
	void Update () {
		//calculate the distance from center based on zoomSpeed
		dist+=Time.deltaTime*zoomSpeed*0.01f;
		//clamp the distance so it's within permmited range
		dist=Mathf.Clamp(dist, -15, -3);
		
		//set the camera rotation back to center, so we can calculate things from this point
		transform.position=center;
		//apply the distance
		transform.position=transform.TransformPoint(new Vector3(0, 0, dist));
		
		//use mouse scroll wheel to simulate pinch, sorry I sort of cheated here
		zoomSpeed+=Input.GetAxis("Mouse ScrollWheel")*500*2;
	}
	
	void OnTouchDown(Vector2 touch){
		//This function get called whenever a touch recives the screeen
		
		//ignore touches on GUI elements
		if (cube_selection_is_open==false){
			if (touch.x<Screen.width-20 & touch.x>Screen.width-170 & touch.y>Screen.height-70 & touch.y<Screen.height-20){
				return;
			}
		}else{
			if (touch.x<100 | touch.x>Screen.width-100){
				return;
			}
		}
		
		//ignore touch on cube rotation gui element
		if (cube_active){
			if (touch.y<camera.WorldToScreenPoint(active_cube.transform.position).y & touch.x>camera.WorldToScreenPoint(active_cube.transform.position).x){
				if (touch.y>camera.WorldToScreenPoint(active_cube.transform.position).y-20 & touch.x<camera.WorldToScreenPoint(active_cube.transform.position).x+50){
					return;
				}
			}
		}
		
		//Create raycasts
		Ray ray = Camera.main.ScreenPointToRay(touch);
		RaycastHit hit;
		if(Physics.Raycast(ray, out hit, Mathf.Infinity)){
			if (hit.collider.gameObject.tag=="cuboro-cube"){
				//check if another cube is obove
				Vector3 position = new Vector3(hit.collider.gameObject.transform.position.x, hit.collider.gameObject.transform.position.y+1.0f, hit.collider.gameObject.transform.position.z);
				GameObject[] objs = GameObject.FindGameObjectsWithTag("cuboro-cube");
				bool found = false;
			    foreach (GameObject go in objs) {
					if (go!=hit.collider.gameObject){
		      			if (go.transform.position == position) {
		        		found=true;
		      			}
					}
			    }	
				if (found==false){
					if (active_cube!=hit.collider.gameObject){
						//we touched another cube than before
						if (cube_active){
							if (active_cube!=null){
								active_cube.renderer.material=normal_cube_material;
							}
						}
						hit.collider.gameObject.renderer.material=active_cube_material;
					}
					cube_active=true;
					active_cube=hit.collider.gameObject;
				}
			}else{
				if (hit.collider.gameObject.tag=="ground"){
					dragging_camera=true;
					cube_active=false;
					if (active_cube!=null){
						active_cube.renderer.material=normal_cube_material;
					}
					active_cube=null;
				}
			}			
		}else{
			//Move camera (din't hit any objects)
			dragging_camera=true;
			cube_active=false;
			if (active_cube!=null){
				active_cube.renderer.material=normal_cube_material;
			}
			active_cube=null;
		}
	}
	
	
	void onPinch(PinchInfo pinfo){
		//This function get called when a pinch is done on the screen -> zoom
		zoomSpeed-=pinfo.magnitude*3;
	}
	
	
	void OnDrag(DragInfo draginfo){	
		//This function get called whenever a touch get draged on the screen.
		if (cube_active & dragging_camera==false){
			//Cube dragging
			Ray ray = Camera.main.ScreenPointToRay(draginfo.pos);
			RaycastHit hit;
			if(Physics.Raycast(ray, out hit, Mathf.Infinity)){
				MoveCube(hit.point);
			}
		}
		if (dragging_camera){
			//camera rotating
			Camera.mainCamera.transform.RotateAround(Vector3.zero, Vector3.up, draginfo.delta.x/10);
		}
	}
		
	void MoveCube(Vector3 point){
		//This function moves a selected cube arround in the scene.
		
		//round coordinates -> Cube gets into grid
		new_cube_x=Mathf.Round(point.x);
		new_cube_y=0.5f;
		new_cube_z=Mathf.Round(point.z);
		
		//Check if a cube is already at the position we wana locate the cube
		//When we found another cube on that position => Place selected cube obove cube which is targeted by the finger
		bool found=true;
		int exit=0;
		while (found==true){
			Vector3 position = new Vector3(new_cube_x,new_cube_y,new_cube_z);
			GameObject[] objs = GameObject.FindGameObjectsWithTag("cuboro-cube");
			bool found_round=false;
		    foreach (GameObject go in objs) {
				if (go!=active_cube){
	      			if (go.transform.position == position) {
						found_round=true;
					}
				}
		    }
			if (found_round){
				found=true;
				new_cube_y=new_cube_y+1.0f;
			}else{
				found=false;
			}
			exit=exit+1;
		}
		
		//move cube
		active_cube.transform.position=new Vector3(new_cube_x, new_cube_y, new_cube_z);
	}
} 
