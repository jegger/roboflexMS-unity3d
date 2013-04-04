/*
 * In this file is the Webrequest done for sending the cubes to the server. 
 * This is done over POST.
 * 
 * 
 */

using UnityEngine;
using System.Collections;

public class webrequest : MonoBehaviour{
	
	void Start(){
		;
	}
	
	public void send_cubes(){
		//This function gets coorindates of the cubes from the scene.
		//Then it sends it to the server.
		
		//get ammount of cubes
		GameObject[] objs = GameObject.FindGameObjectsWithTag("cuboro-cube");
		var ammount_of_cubes = 0;
		foreach (GameObject go in objs) {
			ammount_of_cubes=ammount_of_cubes+1;
		}
				
		//leave if no cubes
		if (ammount_of_cubes==0){
			return;
		}
		
		//Create JSON by hand with string addition
		//Start JSON with: [
		var json_cubes = "[";
		
		//Loop trough cubes
		var i = 0;
		foreach (GameObject go in objs) {
			//count i up
			i=i+1;
			
			//Create cube string
			var j_x = go.transform.position.x;
			var j_y = go.transform.position.y-0.5;
			var j_z = go.transform.position.z;
			var j_typ = go.name;
			var j_rot = go.transform.rotation.eulerAngles.y;
			var json_cube = "{\"x\":"+j_x+", \"y\":"+j_y+", \"z\":"+j_z+", \"typ\":\""+j_typ+"\", \"rot\":"+j_rot+"}";
			
			//Add comma, except the last one 
			if (i!=ammount_of_cubes){
				json_cube = json_cube+",";
			}
			
			//String addition
			json_cubes=json_cubes+json_cube;
		}
		
		//Finish JSON string with ]
		json_cubes=json_cubes+"]";
		
		//Send JSON string via POST
		string url = "http://192.168.50.236/process";
        WWWForm form = new WWWForm();
        form.AddField("data", json_cubes);
        WWW www = new WWW(url, form);
        StartCoroutine(WaitForRequest(www));
	}

	
    IEnumerator WaitForRequest(WWW www){
		//This function actuelly sends the request and check it for errors.
        yield return www;
 
        // check for errors
        if (www.error == null)
        {
            Debug.Log("RESPONSE: " + www.text);
        } else {
            Debug.Log("WWW Error: "+ www.error);
        }    
    }
}
