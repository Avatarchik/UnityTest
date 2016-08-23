using UnityEngine;
using System.Collections;

public class GyroRotate : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Input.gyro.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.Rotate (0, Input.gyro.rotationRateUnbiased.y, 0);
	}
}
