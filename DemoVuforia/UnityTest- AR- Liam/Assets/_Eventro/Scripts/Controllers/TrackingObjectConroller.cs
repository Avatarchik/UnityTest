using UnityEngine;
using System.Collections;
using Eventro.Testapp.Common;

public class TrackingObjectConroller : MonoBehaviour
{

	public float speed = 2;

	[Tooltip ("Place the prefab of the cube from the Project vide" +
	"It will get replaced with the tracked cube ")] 
	public GameObject dublicateCube;

	[Tooltip ("Place the cube which is child of the tracker. " +
	"Duplciate cube will get replaced with this cube's position ")] 
	public GameObject trackerCube;


	private bool foundStatus = false, moveCube = false;
	private GameObject instantiatedCube;
	private MediaPlayerCtrl meadiaCntrl;


	#region Init

	// Use this for initialization
	void Start ()
	{
	
	}

	private void OnEnable ()
	{
		Delegate (true);
	}

	private void OnDisable ()
	{
		Delegate (false);
	}

	private void Delegate (bool state)
	{
		if (state) {
//			Vuforia.DefaultTrackableEventHandler.TrackerState += DefaultTrackableEventHandler_TrackerState;
			TrackableEventHandler.TrackerState += DefaultTrackableEventHandler_TrackerState;
		} else {
//			Vuforia.DefaultTrackableEventHandler.TrackerState -= DefaultTrackableEventHandler_TrackerState;
			TrackableEventHandler.TrackerState -= DefaultTrackableEventHandler_TrackerState;
		}
	}

	#endregion

	#region Object Font And Lost

	void DefaultTrackableEventHandler_TrackerState (bool obj)
	{
		if (obj) { // Object Found
			print ("Tracker found");
			ResetOldCube ();
			foundStatus = true;
			HandleCube ();
		} else { // Object Lost
			foundStatus = false;
			ReEnableCube ();
		}
	}

	#endregion

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.Y))
			DefaultTrackableEventHandler_TrackerState (true);


		if (moveCube) {
			print ("Cube upwards");
			instantiatedCube.transform.Translate (Vector3.up * speed * Time.deltaTime);
		} 
	}


	/* THis method will handle following casees. i.e. if the user:
	* 1 first time tracking
	* 2 Old cube was moving but in between the traker agian come 
	* 3 The old cube was playing video (i.e. All previous steps of previous tracking are done)
	*/
	private void ResetOldCube(){ // Tracker Found
		// TODO: IF the old cube is moving on then we need to stop that
		// TODO: Delete the previous instantiated cube 
		print ("Restting cube - IN");


		// Stop old cube moving
		moveCube = false;

		//
		if (instantiatedCube) Destroy (instantiatedCube);

		print ("Restting cube - OUT");

	}

	private void HandleCube () // Tracker Found
	{
		print ("Handle cube - IN");
		// TODO: MAKE A NEW CUBE
		// TODO: PROVIDE THE SAME POS AS THE REFERENCE CUBE
		// TODO: NOW DISABLE THE ORIGINAL ONE 
		// TODO: MOVE THE INSTANTIATED CUBE UPWORDS
		// TODO: AFTER 4 SECONDS (OR SO) PLAY THE VIDEO 


		// Instantie the duplicate cube which need to be replace with the original one
		instantiatedCube = Instantiate <GameObject> (dublicateCube);

		print ("Instantiate cube " + instantiatedCube);
		// Provide the postiton of the original ( tracker cube's the instantiated cube 
		instantiatedCube.transform.position = trackerCube.transform.position;

		print ("Setting Cube pos " + instantiatedCube.transform.position);
		// Get the refernce of mediaConrtoller
		meadiaCntrl = instantiatedCube.GetComponent<MediaPlayerCtrl>();

		print ("meadiaCntrl ref " + meadiaCntrl);

		// Now set disable the orignal one  ( Make it tru when the object is not on scree)
		trackerCube.SetActive (false);

		print ("Disable orinal one ");

		moveCube = true;
		print ("Start move cube");
		StartCoroutine (PlayMovie());
	}

	private IEnumerator PlayMovie ()
	{
		// Wait for the seconds and play video
		yield return new WaitForSeconds (3);

		print ("Start video");

		// Stop the movemenet of the cube
		moveCube = false;

		// Let him know the name of the video to be play
		meadiaCntrl.Load (Constants.VIDEO_NAME);
	}


	private void  ReEnableCube ()// Tracker Lost
	{
		print ("Enabling cube video");

		//As the tracker get lost.. It can appear again so you need to put it back.
		trackerCube.SetActive (true);
	}
}
