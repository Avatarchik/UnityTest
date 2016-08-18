using UnityEngine;
using System.Collections;

public class TrackingObjectConroller : MonoBehaviour {

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
			Vuforia.DefaultTrackableEventHandler.TrackerState += DefaultTrackableEventHandler_TrackerState;
		} else {
			Vuforia.DefaultTrackableEventHandler.TrackerState -= DefaultTrackableEventHandler_TrackerState;
		}
	}
	#endregion

	#region Object Font And Lost 
	void DefaultTrackableEventHandler_TrackerState (bool obj)
	{
		if (obj) { // Object Found
			Debug.LogError ("Find");
		} else { // Object Lost
			Debug.LogError ("Lost");
		}
	}
	#endregion

	// Update is called once per frame
	void Update () {

	}
}
