using UnityEngine;
using System.Collections;
using Eventro.Testapp.Core;
public class BackgroundPlaneController : MonoBehaviour {

	[Tooltip("The the Child of the ARCamer's -> StereoCameraLeft's -> Background plane Object here")]
	public GameObject backgroundPlane;
	private Vector3 initailPosition; 
	// Use this for initialization
	IEnumerator Start () {
		initailPosition = backgroundPlane.transform.position;
		yield return new WaitForSeconds (0.75f);
		SetBackgroundPos ();
	}
	
	// Update is called once per frame
	void Update () {

		// Just for custom test..
		if (Input.GetKeyDown(KeyCode.A)){
			StartCoroutine( Start());
		} 
	}

	private void SetBackgroundPos(){
		backgroundPlane.transform.localPosition = new Vector3 (
			backgroundPlane.transform.position.x,
			backgroundPlane.transform.position.y,
			// This is my custom value /// You may need to find your own by checking the same first in Editor  
			// THen write the same here .. // OR you can set the range variation etc etc 
			3636
		);

	}

	internal void SetBackgroundPosDelayStereo(){
		StartCoroutine (Start());
	}
	internal void SetBackgroundPosDelayMono(){
		backgroundPlane.transform.position = initailPosition;
	}
}
