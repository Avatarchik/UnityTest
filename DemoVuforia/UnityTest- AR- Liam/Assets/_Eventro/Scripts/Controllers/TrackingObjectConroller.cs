using UnityEngine;
using System.Collections;
using Eventro.Testapp.Common;

namespace Eventro.Testapp.Controllers
{
	public class TrackingObjectConroller : MonoBehaviour
	{

		public float speed = 2;

		[Tooltip ("Place the prefab of the cube from the 'Project' view" +
		"It will get replaced with the tracked cube ")] 
		public GameObject dublicateCube;

		[Tooltip ("Place the cube which is child of the tracker. " +
		"Duplciate cube will get replaced with this cube's position ")] 
		public GameObject trackerCube;

		public Material videoMaterial;


		private string videoMaterialName = "";
		private bool foundStatus = false, moveCube = false;
		private GameObject instantiatedCube;
		private MediaPlayerCtrl mediaCnrtl;


		#region Init

		// Use this for initialization
		void Start ()
		{
			videoMaterialName = videoMaterial.name;
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
			if (GameManager.Instance.isTrakingEnabled) {
				if (obj) { // Object Found
					ResetOldCube ();
					foundStatus = true;
					HandleCube ();
				} else { // Object Lost
					foundStatus = false;
					ReEnableCube ();
				}
			}
		}

		#endregion

		// Update is called once per frame
		void Update ()
		{
			if (moveCube) {
				instantiatedCube.transform.Translate (Vector3.forward * speed * Time.deltaTime * 1.5f);
//				instantiatedCube.transform.position = Vector3.MoveTowards (transform.position,
//					Vuforia.DigitalEyewearBehaviour.Instance.PrimaryCamera.transform.position,1);
			} 
		}
			
	/* THis method will handle following casees. i.e. if the user:
	* 1 first time tracking
	* 2 Old cube was moving but in between the traker agian come 
	* 3 The old cube was playing video (i.e. All previous steps of previous tracking are done)
	*/
		private void ResetOldCube ()
		{ // Tracker Found
			// TODO: IF the old cube is moving on then we need to stop that
			// TODO: Delete the previous instantiated cube 

			// Stop old cube moving
			moveCube = false;

			if (mediaCnrtl) {
				mediaCnrtl.Stop ();
				mediaCnrtl.UnLoad ();
			}

//		if (instantiatedCube)
//			Destroy (instantiatedCube);
		
		}

		private void HandleCube () // Tracker Found
		{
			// 1. MAKE A NEW CUBE
			// 2. PROVIDE THE SAME POS AS THE REFERENCE CUBE
			// 3. NOW DISABLE THE ORIGINAL ONE 
			// 4. MOVE THE INSTANTIATED CUBE UPWORDS
			// 5. AFTER 4 SECONDS (OR SO) PLAY THE VIDEO
			//ADDON
			// 6. When cube appear.. No more tracking
			// 7. A "Done" Button will appear by click on that button the traking will be appear again
			// 8. Disable that "Done" button 

			// Instantie the duplicate cube which need to be replace with the original one
			if (!instantiatedCube) {
				instantiatedCube = Instantiate <GameObject> (dublicateCube);
				// Get the refernce of mediaConrtoller
				mediaCnrtl = instantiatedCube.GetComponent<MediaPlayerCtrl> ();
				GameManager.Instance.SetObjectToPan (instantiatedCube);
			}	

			// Disable Tracker And Enable Done Button 
			GameManager.Instance.TargetTracking (false);

			// Provide the postiton of the original ( tracker cube's the instantiated cube 
			RepositionInstantiatedCube ();

			// Now set disable the orignal one  ( Make it tru when the object is not on scree)
			trackerCube.SetActive (false);

			moveCube = true;
			StartCoroutine (PlayMovie ());
		}

		private IEnumerator PlayMovie ()
		{
			// Wait for the seconds and play video
			yield return new WaitForSeconds (3);

			// Stop the movemenet of the cube
			moveCube = false;

			//Setting the video maetrial to cube
			if (instantiatedCube.GetComponent<Renderer> ().material.name.ToString () != videoMaterialName.ToString ())
				instantiatedCube.GetComponent<Renderer> ().material = videoMaterial;

			// Let him know the name of the video to be play
			mediaCnrtl.Load (Constants.VIDEO_NAME);
			StartCoroutine (PlayDelay ());
		}

		IEnumerator PlayDelay ()
		{
			yield return new WaitForSeconds (0.5f);
			mediaCnrtl.Play ();
		}

		private void RepositionInstantiatedCube ()
		{
			instantiatedCube.transform.position = trackerCube.transform.position;
			Material mat = Resources.Load<Material> ("VRFocused");
			instantiatedCube.GetComponent<Renderer> ().material = mat;
		}

		private void  ReEnableCube ()// Tracker Lost
		{
			//As the tracker get lost.. It can appear again so you need to put it back.
			trackerCube.SetActive (false);
		}
	
	}
}