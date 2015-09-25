using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoadManager : MonoBehaviour {

	public Vector3 startPosition;
	public Vector3 nextPosition;

	/* The Prefab required for entireRoad
	 */
	public GameObject prefabRoad;

	/* The prefab for Police Car
		 */
	public GameObject prefabPoliceCar;
	/* The prefab for Taxi
		 */
	public GameObject prefabTaxi;
	/* The prefab for Trailer
		 */
	public GameObject prefabTrailer;
	
	private GameObject[] taxi;
	private GameObject[] trailer;
	private GameObject[] policeCar;


	public int numberOfObjects;

	private Queue<GameObject> queueRoad;

	private Transform bike;
	private float scale;

	// Difficulty settings

	/*Static Variables
	 */
	public static int dificulty;
	public int maxDifficulty=5;


	private float startTime=0;
	private float elapsedTime=0;
	private float triggerTime=30;


	public int maxObjectsPresent=6;
	public int minObjectsPresent=2;

	/*Static Variables
	 */
	public static int objectsPresent;



	/* Avoid mutiple object creations hence jc calls
	 */
	int[] probs={5,6,2};
	int i;
	int total;
	float randomPoint;
	GameObject obstacle;

	float minObstacleZ,maxObstacleZ;
	private Vector3 obstaclePosition;

	bool lane=false;

	bool spawning=false;

	public enum ObstacleType
	{
		Taxi,
		Trailer, 
		PoliceCar,
	}






	void Start () {

		startTime=Time.time;

		//Init Static Variable
		objectsPresent=0;
		dificulty=1;

		nextPosition=startPosition;
		queueRoad=new Queue<GameObject>(numberOfObjects);

		for(int i=0;i<numberOfObjects;i++){
			GameObject g=(GameObject)Instantiate(prefabRoad);
			g.transform.position=nextPosition;
			scale=g.transform.localScale.z;
			nextPosition.z+=scale;
			queueRoad.Enqueue(g);

		}

		CreateObjectPool();


	
	}
	
	void Update () {

		/*This Ensures that your bike collided no platform will be generated
		 */
		if (Harley.exploded){
			return;
		}

		if (queueRoad.Peek().transform.localPosition.z+ scale < Harley.distanceTraveled) {
			GameObject g = queueRoad.Dequeue();
			g.transform.localPosition = nextPosition;
			nextPosition.z += g.transform.localScale.z;
			queueRoad.Enqueue(g);
		}


		/* This Ensures that count is done and user can controll the bike*/
		if (!Harley.gameStarted){
			return;
		}

		UpdateDifficulty();

		if (!spawning){
			StartCoroutine(SpawnObstacles());

		}




	
	}

	void CreateObjectPool(){
		taxi=new GameObject[6];
		policeCar=new GameObject[6];
		trailer=new GameObject[6];
		
		for(i=0;i<6;i++){
			GameObject gTaxi=(GameObject)Instantiate(prefabTaxi);
			gTaxi.SetActive(false);
			taxi[i]=gTaxi;
			
			GameObject gTrailer=(GameObject)Instantiate(prefabTrailer);
			gTrailer.SetActive(false);
			trailer[i]=gTrailer;
			
			GameObject gPoliceCar=(GameObject)Instantiate(prefabPoliceCar);
			gPoliceCar.SetActive(false);
			policeCar[i]=gPoliceCar;
		}
		
		
	}



	GameObject GetObstacle(ObstacleType obs){

		if (obs==ObstacleType.PoliceCar){

			for(i=0;i<policeCar.Length;i++){
				if(!policeCar[i].activeSelf){
					policeCar[i].SetActive(true);
					policeCar[i].transform.localRotation=Quaternion.Euler(Vector3.zero);
					return policeCar[i];
				}
			}

		}
		if (obs==ObstacleType.Taxi){
			for(i=0;i<taxi.Length;i++){
				if(!taxi[i].activeSelf){
					taxi[i].SetActive(true);
					taxi[i].transform.localRotation=Quaternion.Euler(Vector3.zero);

					return taxi[i];
				}
			}
		}
		if (obs==ObstacleType.Trailer){
			for(i=0;i<trailer.Length;i++){
				if(!trailer[i].activeSelf){
					trailer[i].SetActive(true);
					trailer[i].transform.localRotation=Quaternion.Euler(Vector3.zero);
					return trailer[i];
				}
			}
		}

		return null;
	}


	void UpdateDifficulty(){


		if (dificulty>5){
			return;
		}
		elapsedTime=Time.time-startTime;

		if (elapsedTime>=triggerTime){
			dificulty++;
			startTime=Time.time;
			triggerTime*=3;

		}

		switch(dificulty){
		case 2:
			minObjectsPresent=3;
			break;
		case 3:
			minObjectsPresent=4;
			break;
		case 4:
			minObjectsPresent=5;
			break;
		case 5:
			minObjectsPresent=maxObjectsPresent;
			break;
		}

	}

	IEnumerator SpawnObstacles(){

		spawning=true;
		if (objectsPresent<minObjectsPresent){
			objectsPresent++;

			i=Choose();
			switch(i){

			case 0:
				obstacle=GetObstacle(ObstacleType.Taxi);
				obstacle.transform.position=getObstaclePosition();


				break;
			case 1:
				obstacle=GetObstacle(ObstacleType.PoliceCar);
				obstacle.transform.position=getObstaclePosition();

				break;
			case 2:
				obstacle=GetObstacle(ObstacleType.Trailer);
				obstacle.transform.position=getObstaclePosition();

				break;

			}


		}


		switch(dificulty){

		case 1:
			yield return new WaitForSeconds(1);

			break;
		case 2:
			yield return new WaitForSeconds(0.8f);

			break;
		case 3:
			yield return new WaitForSeconds(0.6f);

			break;
		case 4:
			yield return new WaitForSeconds(0.4f);
			break;
		case 5:
			yield return new WaitForSeconds(0.2f);
			break;
		}

		spawning=false;


	}


	Vector3 getObstaclePosition(){

		obstaclePosition=Vector3.zero;
		lane=!lane;
		if (lane){
		obstaclePosition.x=Random.Range(-11.0f,-1);
		}else{
		obstaclePosition.x=Random.Range(1,11.0f);

		}
		obstaclePosition.y=0.1f;

		obstaclePosition.z=Random.Range((nextPosition.z-3*prefabRoad.transform.localScale.z),(nextPosition.z-prefabRoad.transform.localScale.z));

		return obstaclePosition;

	}

	/* This works for sure*/
	int Choose(){

		total=0;

		for(i=0;i<probs.Length;i++){

			total+=probs[i];
		}



		randomPoint=Random.value *total;

		for (i = 0; i < probs.Length; i++) {
			if (randomPoint < probs[i])
				return i;
			else
				randomPoint -= probs[i];
		}
		
		return probs.Length - 1;


	}
	
}
