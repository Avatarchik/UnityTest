using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using Eventro.Testapp.Enums;
using Eventro.Testapp.Controllers;

namespace Eventro.Testapp.UI
{
	public class Buttons : MonoBehaviour
	{
		[Tooltip ("Select the type of button you are going to create")]
		public ButtonsType buttonType;

		// For Hotspot Buttons Only
		[Tooltip ("Place the which will be fill when under focused (optional)")]
		public GameObject hotspotFillImage;

		// For Custom 3D  Buttons Only
		[Tooltip ("Place the which will be fill when under focused (optional)")]
		public Material arFoucusMaterial, arNonFoucusMaterial;

		[Tooltip ("Check only if you need to increase the alpha when focused on the button")]
		public bool increaseAplha = false;

		[Tooltip ("Enter the time (in seconds) by which user need to remain focus on button")]
		public float focusCountdown = 2;

		[Tooltip ("Place the textBox in which you want to show the message when focus is on the button,")]
		public Text custom3DwithTextbox;

		[HideInInspector]
		public bool isFocused = false;


		private float isFocusedTime = 0;
		private float fillAmount = 1;
		private bool eventFired = false;

		// Event
		/// <summary>
		/// Occurs when button foucse complete.
		/// </summary>
		public static event Action<string> buttonFoucsedComplete ;


		// Use this for initialization
		void Start ()
		{
			
		}
		
		// Update is called once per frame
		void Update ()
		{
			if (isFocused) {
				switch (buttonType) {

				case ButtonsType.Custom:
					CustomButtonFocused ();
					break;

				case ButtonsType.VideoButton:
					VideoButtonFocused ();
					break;

				case ButtonsType.Hotspot:
					// Increasing focus time and filling the bar
					isFocusedTime = +Time.deltaTime;
					if (hotspotFillImage != null && fillAmount > 0.1) {
						fillAmount -= 1.0f / focusCountdown * Time.deltaTime;
						hotspotFillImage.GetComponent<Renderer> ().material.SetFloat ("_Cutoff", fillAmount);
					}
					// Now if the bar is full open the Respective popUp
					else if (fillAmount >= 0.001 && !eventFired) {
						buttonFoucsedComplete (gameObject.name);
						eventFired = true;
					}
					break;

				}
			} else {  
				// To reset all the setting done when not/diconnect focussed  
				isFocusedTime = 0;
				eventFired = false;
				switch (buttonType) {

				case ButtonsType.Hotspot:
					if (hotspotFillImage != null) {
						fillAmount = 1;
						hotspotFillImage.GetComponent<Renderer> ().material.SetFloat ("_Cutoff", fillAmount); 
					}
					break;
				
				case ButtonsType.Custom:
					CustomButtonNonFocused ();
					break;

				case ButtonsType.VideoButton:
					VideoButtonNonFocused ();
					break;
				}


			}
		}

		#region Custom 3D

		private void CustomButtonFocused ()
		{
			// Increasing the foucs time and change the material 
			if (isFocusedTime < focusCountdown) { 
				isFocusedTime = isFocusedTime + Time.deltaTime;
				gameObject.GetComponent<Renderer> ().material = arFoucusMaterial;
			}
			// Now Focus time is achieved fire the event 
			if ((isFocusedTime >= focusCountdown) && !eventFired) {
				//						Debug.LogError ("Focus Completed of button " + gameObject.name);
				buttonFoucsedComplete (gameObject.name);
				eventFired = true;
			}
		}

		private void CustomButtonNonFocused ()
		{
			gameObject.GetComponent<Renderer> ().material = arNonFoucusMaterial;
		
		}

		#endregion


		#region Video Button
		private void VideoButtonFocused(){
			if (isFocusedTime < focusCountdown) { 
				isFocusedTime = isFocusedTime + Time.deltaTime;
			}
			if ((isFocusedTime >= focusCountdown) && !eventFired) {
				buttonFoucsedComplete (gameObject.name);
				eventFired = true;
			}
		}

		private void VideoButtonNonFocused(){
		 // Stop Video
			GameManager.Instance.PauseCurrentVideo();
		}
		#endregion
	}
}
