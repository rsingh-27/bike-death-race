using UnityEngine;
using System.Collections;

public class Horizon : MonoBehaviour {

	private Vector3 bikeposition;

	void Start () {
		bikeposition=Vector3.zero;

	}


	
	void Update () {
		/*This Ensures that your bike collided no Horizon will be not move
		 */
		if (Harley.exploded){
			return;
		}

		bikeposition.z=Harley.distanceTraveled;
		transform.localPosition=bikeposition;

		}
}