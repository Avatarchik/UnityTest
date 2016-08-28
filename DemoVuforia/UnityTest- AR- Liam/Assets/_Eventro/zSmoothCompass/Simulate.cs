using UnityEngine;
using System.Collections;

public class Simulate : MonoBehaviour
{

	/// <summary>
	/// The type of the simulations.
	/// 1. Contineous - It will let you provide the contineous values in linear Upward/downward
	/// 2. ContinuousWithFluctuation - It will let you provide the values with some minor up/down with subtraction/addition respectively 
	/// </summary>
	private Simulations simulationType = Simulations.ContinuousWithFluctuation;

	float currentValue = 0;
	float countLower = 0, countUpper = 360;

	//Increase/ increment speed
	public float calucateSpeed = 0.5f;

	private bool isIncrementing = true;
	private bool canSimulate = false;

	// IT is the range of the variation when value will get Simulate
	//------------
	public float fromVariation = 20;
	public float toVariation = 80;

	private Vector2 VariationRange {
		get { 
			return new Vector2 (fromVariation, toVariation);
		}
	}
	//--------------------

	//It will let you very small amount of values to in opposite direction
	// as it was going
	public bool needFluctuation = true;
	public float fluctuationValueLower = -0.5f;
	public float fluctuationValueUpper = 0.01f;


	private Vector2 FluctuateValue {
		get {
			if (needFluctuation)
				return new Vector2 (fluctuationValueLower, fluctuationValueUpper);
			else
				return Vector2.zero;
		}
	}
	//--------------------



	#region Init

	// Use this for initialization
	void Start ()
	{
		CheckValidations ();	
	}

	void CheckValidations ()
	{
		if (toVariation < fromVariation) {
			Debug.LogError ("Your Minimum variation range is greater than the Maximum!!");
			return;
		}
		if (countUpper < countLower) {
			Debug.LogError ("Your Minimum count range is greater than the Maximum!!");
			return;
		}

		if (toVariation > countUpper) {
			Debug.LogError ("Your variation limit exceeded than the maxCount");
			return;
		}

		if (countLower > fromVariation) {
			Debug.LogError ("Your variation limit is below than the lowest value");
			return;
		}

		if (calucateSpeed == 0) {
			Debug.LogError ("Your increament speed is zero the code can't be execute");
			return;
		}

		if (simulationType == Simulations.ContinuousWithFluctuation)
			needFluctuation = true;

		canSimulate = true;

	}

	#endregion

	// Update is called once per frame
	void Update ()
	{
		if (canSimulate)
			switch (simulationType) {
			case Simulations.Continuous:
				ContineousSimulation ();
				break;
			case Simulations.ContinuousWithFluctuation:
				ContineousSimulation (true);
				break;
			}
	}


	private void ContineousSimulation (bool canFluctuate = false)
	{
		// 1. start the value from the lowe count and increase that
		// 2. when the vlaue will reach the 'fromVariation' then it will get loop between 'fromVariation' & 'toVariation'
		// 3. If fluctuation is 'True' then the fluctuateValue will add and decrease randomly 
		if (currentValue >= VariationRange.y) { // Now decrease the value
			isIncrementing = false;
		} else if (currentValue <= VariationRange.x) {// Now increase to remain in the range 
			isIncrementing = true;
		}
		CalculateVal (isIncrementing, canFluctuate);
//		print (currentValue);
	}

	private void CalculateVal (bool isInc, bool fluctuation = false)
	{
		if (isInc) {
			currentValue = currentValue + calucateSpeed;
			if (fluctuation)
				currentValue = currentValue + Random.Range (FluctuateValue.x, FluctuateValue.y);
		} else {
			currentValue = currentValue - calucateSpeed;
			if (fluctuation)
				currentValue = currentValue - Random.Range (FluctuateValue.y, FluctuateValue.x);
		}

	}

	public float GetSimulatedValue ()
	{
		return currentValue;
	}
}

public enum Simulations
{
	Continuous,
	ContinuousWithFluctuation,
	//	ContinuousWithLittleDelay,
}
