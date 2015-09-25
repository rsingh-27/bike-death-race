using UnityEngine;
using System.Collections;

public class TouchPad : MonoBehaviour {

	public GameObject bike;
	public GameObject bikeInternal;

	public Transform cOM;
	private float h,v;

	public WheelCollider wheelR;
	public WheelCollider wheelF;

	public int motorTorque=40;
	public float steerAngle=2.0f;

	public float currentSpeed=0f;


	//Accelerate Script for phone


	void Start () {
	
		bike.GetComponent<Rigidbody>().centerOfMass=new Vector3(cOM.localPosition.x,cOM.localPosition.y,cOM.localPosition.z);
	}
	
	void Update () {
		currentSpeed = bike.GetComponent<Rigidbody>().velocity.magnitude;
		if (wheelR ==null &&wheelF==null){
			return;
		}

		if (!Harley.gameStarted){
			return;
		}

		if (Application.platform==RuntimePlatform.Android){

			if (Input.touchCount > 0 /*&& Input.GetTouch(0).phase==TouchPhase.Began*/){
				Debug.Log("Mouse button");

				wheelR.motorTorque=motorTorque;
			}else{
				wheelR.motorTorque=0;
			}


			if (currentSpeed>10.0f){
				h=Input.acceleration.x;
				wheelF.steerAngle=steerAngle*h;
			}

		}else{



			h = Input.GetAxisRaw("Horizontal"); //get horizontal input
			v = Input.GetAxisRaw("Vertical"); //get vertical input

			wheelR.motorTorque=motorTorque*v;
			wheelF.steerAngle=steerAngle*h;

			//Change steer angle according to speed here

		}
		UpdateDragAndSteer();
	
	}

	void UpdateDragAndSteer(){

		if (currentSpeed<100){
			bike.GetComponent<Rigidbody>().drag=0.1f;
			steerAngle=3.0f;

		}else if (currentSpeed<150){
			bike.GetComponent<Rigidbody>().drag=0.2f;
			steerAngle=2.0f;

		}else if (currentSpeed<200){
			bike.GetComponent<Rigidbody>().drag=0.3f;
			steerAngle=1.0f;
			
		}else if (currentSpeed<250){
			bike.GetComponent<Rigidbody>().drag=0.4f;
			steerAngle=0.5f;

			
		}




	}



	void FixedUpdate(){

		// Not DOing any thing in here

		if ( Vector3.Angle( Vector3.up, bike.transform.up ) > 45 ) 
		{
			//bike.rigidbody.AddRelativeTorque (Vector3.forward * 5000 * Input.GetAxis("Horizontal"));
		}

		if (Application.platform==RuntimePlatform.Android){
			
		}else{


		}


	}

}
