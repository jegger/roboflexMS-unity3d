using UnityEngine;
using System.Collections;

public class toucher : MonoBehaviour {
	
	//Publics
	public float camera_speed=3f;
	public int ammount_of_cubes=15;
	
	public Material active_cube_material;
	public Material normal_cube_material;
	
	//Privates for inspector
	[System.NonSerialized] public bool cube_active=false;
	[System.NonSerialized] public GameObject active_cube=null;
	[System.NonSerialized] public bool cube_selection_is_open = false;
	[System.NonSerialized] public Transform new_cube_typ;
	[System.NonSerialized] public bool new_cube_get_placed;
	
	//Privates
	private int c_ammount_typ1; //current ammount of cubes in the scene
	private int c_ammount_typ2;
	private int c_ammount_typ3;
	private int c_ammount_typ4;
	private int c_ammount_typ5;
	private int c_ammount_typ6;
	private int c_ammount_typ7;
	private int c_ammount_typ8;
	private int c_ammount_typ9;
	private int c_ammount_typ10;
	private int c_ammount_typ11;
	private int c_ammount_typ12;
	private int c_ammount_typ13;
	private int c_ammount_of_cubes;
	
	private int ammount_typ1=46;
	private int ammount_typ2=4;
	private int ammount_typ3=7;
	private int ammount_typ4=2;
	private int ammount_typ5=4;
	private int ammount_typ6=4;
	private int ammount_typ7=6;
	private int ammount_typ8=6;
	private int ammount_typ9=4;
	private int ammount_typ10=4;
	private int ammount_typ11=12;
	private int ammount_typ12=5;
	private int ammount_typ13=2;
	
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
				if (active_cube!=null){
					active_cube.renderer.material=normal_cube_material;
				}
				active_cube=null;
				cube_active=false;
				return;
			}
		}else{
			if (touch.x<100 | touch.x>Screen.width-100){
				if (active_cube!=null){
					active_cube.renderer.material=normal_cube_material;
				}
				active_cube=null;
				cube_active=false;
				return;
			}
		}
		
		//ignore touch on cube rotation gui element
		if (cube_active & (active_cube!=null)){
			float cx = camera.WorldToScreenPoint(active_cube.transform.position).x;
			float cy = camera.WorldToScreenPoint(active_cube.transform.position).y;
			//Right left
			if (touch.x>cx-50 & touch.x<cx+50){
				if (touch.y>cy-30 & touch.y<cy){
					return;
				}
			}
			//delete
			if (touch.x>cx-15 & touch.x<cx+15){
				if (touch.y>cy+30 & touch.y<cy+60){
					return;
				}
			}
		}
		
		//detect if touch is on camera or cube
		detect_camera_or_cube(touch);
	}
	
	
	void detect_camera_or_cube(Vector2 touch){
		//Create raycasts to detect if touch is over cube
		Ray ray = Camera.main.ScreenPointToRay(touch);
		RaycastHit hit;
		if(Physics.Raycast(ray, out hit, Mathf.Infinity)){
			if (hit.collider.gameObject.tag=="cuboro-cube"){
				//check for cube
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
			//Move camera (dind't hit any objects)
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
		
		//Check if a new cube gets placed in the scene
		if (new_cube_get_placed){
			if (draginfo.pos.x>100 & draginfo.pos.x<Screen.width-100){
				Ray ray = Camera.main.ScreenPointToRay(draginfo.pos);
				RaycastHit hit;
				if(Physics.Raycast(ray, out hit, Mathf.Infinity)){
					//if the max of cubes is reached, dont create cube
					if (c_ammount_of_cubes>=ammount_of_cubes | is_max_of_ammount_reached(new_cube_typ)){
						new_cube_get_placed=false;
						return;
					}
					
					//create new cube in scene
					MoveCube(hit.point, new_cube_typ);
					new_cube_get_placed=false;
					//detect_camera_or_cube(draginfo.pos);
					c_ammount_of_cubes++;
					update_ammount(new_cube_typ, true);
				}
			}
		}
		
		//Check if cube gets draged
		if (cube_active & dragging_camera==false){
			Ray ray = Camera.main.ScreenPointToRay(draginfo.pos);
			RaycastHit hit;
			if(Physics.Raycast(ray, out hit, Mathf.Infinity)){
				MoveCube(hit.point, null);
			}
		}
		
		//Check if camera get rotated
		if (dragging_camera){
			Camera.mainCamera.transform.RotateAround(Vector3.zero, Vector3.up, draginfo.delta.x/10);
		}
	}
		
	void MoveCube(Vector3 point, Transform cube_typ){
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
		
		if (new_cube_y>4.5f | new_cube_x>3 | new_cube_z>3){
			return;
		}
		
		//move cube
		if (cube_typ==null){
			if (active_cube!=null){
				active_cube.transform.position=new Vector3(new_cube_x, new_cube_y, new_cube_z);
			}
		}else{
			Instantiate(cube_typ, new Vector3(new_cube_x, new_cube_y, new_cube_z), Quaternion.identity);
			//find created cube
			GameObject[] objs = GameObject.FindGameObjectsWithTag("cuboro-cube");
			foreach (GameObject go in objs) {
				CubeBehavior CB = (CubeBehavior) go.GetComponent(typeof(CubeBehavior));
				if (CB.number==-1){
					//we found the instantated cube
					active_cube=go;
					cube_active=true;
					go.renderer.material=active_cube_material;
					//reset the number in cube
					CB.number=1;
				}
			}
		}
	}
	
	public void remove_cube(){
		update_ammount(active_cube.gameObject.transform, false);
		Destroy(active_cube.gameObject);
		cube_active=false;
		active_cube=null;
		c_ammount_of_cubes--;
	}
	
	void update_ammount(Transform go, bool up){
		//Get cube typ
		CubeBehavior CB = (CubeBehavior) new_cube_typ.GetComponent(typeof(CubeBehavior));
		int typ = CB.typ;
		
		//Update correct typ
		if (typ==1){if (up){c_ammount_typ1++;}else{c_ammount_typ1--;};}
		if (typ==2){if (up){c_ammount_typ2++;}else{c_ammount_typ2--;};}
		if (typ==3){if (up){c_ammount_typ3++;}else{c_ammount_typ3--;};}
		if (typ==4){if (up){c_ammount_typ4++;}else{c_ammount_typ4--;};}
		if (typ==5){if (up){c_ammount_typ5++;}else{c_ammount_typ5--;};}
		if (typ==6){if (up){c_ammount_typ6++;}else{c_ammount_typ6--;};}
		if (typ==7){if (up){c_ammount_typ7++;}else{c_ammount_typ7--;};}
		if (typ==8){if (up){c_ammount_typ8++;}else{c_ammount_typ8--;};}
		if (typ==9){if (up){c_ammount_typ9++;}else{c_ammount_typ9--;};}
		if (typ==10){if (up){c_ammount_typ10++;}else{c_ammount_typ10--;};}
		if (typ==11){if (up){c_ammount_typ11++;}else{c_ammount_typ11--;};}
		if (typ==12){if (up){c_ammount_typ12++;}else{c_ammount_typ12--;};}
		if (typ==13){if (up){c_ammount_typ13++;}else{c_ammount_typ13--;};}
	}
	
	bool is_max_of_ammount_reached(Transform go){
		//Get cube typ
		CubeBehavior CB = (CubeBehavior) new_cube_typ.GetComponent(typeof(CubeBehavior));
		int typ = CB.typ;
				
		//Check ammount
		if (typ==1){if (c_ammount_typ1<ammount_typ1){return false;}else{return true;}}
		if (typ==2){if (c_ammount_typ2<ammount_typ2){return false;}else{return true;}}
		if (typ==3){if (c_ammount_typ3<ammount_typ3){return false;}else{return true;}}
		if (typ==4){if (c_ammount_typ4<ammount_typ4){return false;}else{return true;}}
		if (typ==5){if (c_ammount_typ5<ammount_typ5){return false;}else{return true;}}
		if (typ==6){if (c_ammount_typ6<ammount_typ6){return false;}else{return true;}}
		if (typ==7){if (c_ammount_typ7<ammount_typ7){return false;}else{return true;}}
		if (typ==8){if (c_ammount_typ8<ammount_typ8){return false;}else{return true;}}
		if (typ==9){if (c_ammount_typ9<ammount_typ9){return false;}else{return true;}}
		if (typ==10){if (c_ammount_typ10<ammount_typ10){return false;}else{return true;}}
		if (typ==11){if (c_ammount_typ11<ammount_typ11){return false;}else{return true;}}
		if (typ==12){if (c_ammount_typ12<ammount_typ12){return false;}else{return true;}}
		if (typ==13){if (c_ammount_typ13<ammount_typ13){return false;}else{return true;}}
		return false;
	}
} 
