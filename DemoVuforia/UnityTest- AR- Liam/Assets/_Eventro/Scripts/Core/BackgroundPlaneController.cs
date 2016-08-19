using UnityEngine;
using System.Collections;

public class BackgroundPlaneController : MonoBehaviour {

	[Tooltip("The the Child of the ARCamer's -> StereoCameraLeft's -> Background plane Object here")]
	public GameObject backgroundPlane;

	// Use this for initialization
	IEnumerator Start () {
		yield return new WaitForSeconds (1);
		backgroundPlane.transform.localPosition = new Vector3 (
			backgroundPlane.transform.position.x,
			backgroundPlane.transform.position.y,
			// This is my custom value /// You may need to find your own by checking the same first in Editor  
			// THen write the same here .. // OR you can set the range variation etc etc 
			1292

		);
	}
	
	// Update is called once per frame
	void Update () {

		// Just for custom test..
		if (Input.GetKeyDown(KeyCode.A)){
			StartCoroutine( Start());
		} 
	}
}
