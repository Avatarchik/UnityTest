using UnityEngine;
using System.Collections;

public class TrackingObjectConroller : MonoBehaviour {

	public Vector3 initialPos;
	public float speed = 5;
	private bool foundStatus = false;

	#region Init
	// Use this for initialization
	void Start () {
	
	}

	private void OnEnable(){
		Delegate (true);
	}

	private void OnDisable(){
		Delegate (false);
	}

	private void Delegate( bool state){
		if (state) {
//			Vuforia.DefaultTrackableEventHandler.TrackerState += DefaultTrackableEventHandler_TrackerState;
		} else {
//			Vuforia.DefaultTrackableEventHandler.TrackerState -= DefaultTrackableEventHandler_TrackerState;
		}
	}
	#endregion

	#region Object Font And Lost 
	void DefaultTrackableEventHandler_TrackerState (bool obj)
	{
		if (obj) { // Object Found
			foundStatus = true;
		} else { // Object Lost
			foundStatus = false;
		}
	}
	#endregion

	// Update is called once per frame
	void Update () {
		if (foundStatus) {
			gameObject.transform.Translate (Vector3.up * speed * Time.deltaTime);
		}else{
			gameObject.transform.localPosition = initialPos;
		}
	}
}
