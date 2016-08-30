using UnityEngine;
using System.Collections;
using System;

public class PlayerController : MonoBehaviour
{
	public bool smoothMovement = false;
	public Smooth smoothType = Smooth.WithDelyInSeconds;
	public  float threshold = 0.012f;//  0.007f;
	private Simulate simulate;

	private float delayInSeconds = 0.1f;
	private float simulatedValue = 0;

	// Use this for initialization
	void Start ()
	{
		simulate = GameObject.Find ("Scripts").GetComponent<Simulate> ();

		if (smoothType == Smooth.WithDelyInSeconds)
			InvokeRepeating ("GetValuesWithDelayInterval", 0, delayInSeconds);
		else if (smoothType == Smooth.ReadCompassValue) {
			Input.location.Start ();
			Input.compass.enabled = true;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		switch (smoothType) {
		case Smooth.Natural:
			SetEulerAngle (simulate.GetSimulatedValue ());
			break;
		case Smooth.WithDelyInSeconds:
			SetEulerAngle (delayReturn);
			break;
		case Smooth.ReadCompassValue:
			SetEulerAngle (Input.compass.trueHeading);
			break;
		}
	}

	private void SetEulerAngle (float value)
	{
//		print ("without  diff "+ value );
//		print ("with diff "+ GetDiff (value) );
		if (smoothMovement) {
			Quaternion target = Quaternion.Euler (
				                     gameObject.transform.eulerAngles.x,
				                     value,
				                     gameObject.transform.eulerAngles.z);
			if (GetDiff (value) > threshold)
				transform.rotation = Quaternion.Slerp (transform.rotation,
					target, 1f * Time.deltaTime);
		} else {
			gameObject.transform.eulerAngles = new Vector3 (
				gameObject.transform.eulerAngles.x,
				value,
				gameObject.transform.eulerAngles.z
			);
		}
	}

	private float delayReturn = 0;

	private float GetValuesWithDelayInterval ()
	{
		delayReturn = simulate.GetSimulatedValue ();
		return delayReturn;
	}

	public void SmoothToggleUI (UnityEngine.UI.Toggle toggle)
	{
		smoothMovement = toggle.isOn;
	}

	private float GetDiff (float value)
	{
		float diff = Mathf.Abs (value) - Mathf.Abs (transform.rotation.y);
		diff = Mathf.Abs (diff);
		return (float)Math.Round (diff, 3);
	}
}

public enum Smooth
{
	// It will send value to the player as as-it-is
	Natural,

	// It will send value to the player as with some delays
	WithDelyInSeconds,
	ReadCompassValue,

	
}
