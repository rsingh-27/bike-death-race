using UnityEngine;
using System.Collections;

public class Harley : MonoBehaviour {

	public GameObject explosion;
	private bool exploding=false;

	private Vector3 velocity;

	public WheelCollider wheelR;
	public WheelCollider wheelF;

	/*Static Variables
	 */
	public static bool gameStarted,/*Bike has exploded so donot genereate Platform*/exploded;
	public static float distanceTraveled;

	public GUIText txtCounter;
	public GUIText txtMinSpeed;
	public GUIText txtCurrentSpeed;

	public GUIText txtDistance;

	public AudioClip crash;
	public AudioClip burning;
	public AudioClip blast;



	private float explosionTime=3;

	private Color clrYellow;
	private Color clrRed;

	public static float distance;


	void Start () {

		// Init static variables
		distanceTraveled=transform.position.z;
		distance=0f;

		gameStarted=false;
		exploded=false;
		StartCoroutine(CountDown());

		txtMinSpeed.color=Color.red;
		clrYellow=txtCurrentSpeed.color;
		clrRed=Color.red;


	}
	
	void Update () {


		distanceTraveled=transform.position.z;


		/*Initial Velocity */
		if (!gameStarted){
			transform.GetComponent<Rigidbody>().velocity=new Vector3(0f,0f,100f);

		}

		UpdateDistance();

		GameEventManager.instance.currentSpeed=(int)transform.GetComponent<Rigidbody>().velocity.z;

		switch(RoadManager.dificulty){
		case 2:
		GameEventManager.instance.minSpeed=80;
			break;
		case 3:
			GameEventManager.instance.minSpeed=100;
			break;
		case 4:
			GameEventManager.instance.minSpeed=120;
			break;
		case 5:
			GameEventManager.instance.minSpeed=140;
			break;
		}

		txtMinSpeed.text=""+GameEventManager.instance.minSpeed;
		if (GameEventManager.instance.currentSpeed>=0){
			txtCurrentSpeed.text=""+GameEventManager.instance.currentSpeed;
		}

			CheckSpeedForExplosion();
	}



	void UpdateDistance(){

		distance+=Time.deltaTime*GameEventManager.instance.currentSpeed;

		if (distance<1000){
		txtDistance.text=(int)distance+"m";
		}else{
			txtDistance.text=Mathf.Round((distance/1000)*100f)/100f+"km";

		}

		if(!exploding){
		GetComponent<AudioSource>().pitch=GetComponent<Rigidbody>().velocity.z*0.018f;
		}


	}

	/* Get current score
	 */
	public float GetScore(){

		return Mathf.Round((distance/1000)*100f)/100f;

	}

	/* Get the best score
	 */
	public float GetBestScore(){
		
		return PlayerPrefs.GetFloat("Score");
		
	}

	/* Saves the score as well as post to leaderboard
	 */
	private void SaveScore (){
		
		float score=GetScore();
		float bestScore=PlayerPrefs.GetFloat("Score");
		
		if (score>bestScore){
			PlayerPrefs.SetFloat("Score", score);

			Social.ReportScore(Mathf.FloorToInt(distance), "YOUR_SCORE_ID", (bool success) => {
				// handle success or failure
				Debug.Log("Score Post Sucees : "+success+" for distance: "+Mathf.FloorToInt(distance));
			});	
		}

	
	}
	
	void CheckSpeedForExplosion(){
		if (exploding){
			return;
		}

		if (GameEventManager.instance.currentSpeed<=GameEventManager.instance.minSpeed){
			txtCurrentSpeed.color=clrRed;
			explosionTime-=Time.deltaTime;
			if (explosionTime<=0){
				StartCoroutine(ExplodeSpeed());

			}
		}else{
			explosionTime=3;
			txtCurrentSpeed.color=clrYellow;

		}

	}



	void OnCollisionEnter(Collision col){



		if (col.gameObject.tag=="Obstacle"){
			exploded=true;

			Camera.main.transform.localPosition=new Vector3(Camera.main.transform.localPosition.x,
			                                                Camera.main.transform.localPosition.y,
			                                                Camera.main.transform.localPosition.z);

			Camera.main.transform.parent=null;


			if (!exploding){
				AudioSource.PlayClipAtPoint(crash,transform.position);

				StartCoroutine(Explode());
			}

		}

	

	}

	IEnumerator CountDown(){

		txtCounter.gameObject.SetActive(true);
		txtCounter.text="3";

		yield return new WaitForSeconds(1);
		txtCounter.text="2";

		yield return new WaitForSeconds(1);
		txtCounter.text="1";

		yield return new WaitForSeconds(1);

		gameStarted=true;
		txtCounter.text="Go";

		yield return new WaitForSeconds(1);


		txtCounter.gameObject.SetActive(false);

	}



	IEnumerator Explode(){
		exploding=true;



		yield return new WaitForSeconds(1);

		GameObject g=(GameObject)Instantiate(explosion, transform.position,transform.rotation);
		g.transform.parent=transform;
		//AudioSource.PlayClipAtPoint(burning,transform.position);
		GetComponent<AudioSource>().clip=burning;
		GetComponent<AudioSource>().pitch=1;
		GetComponent<AudioSource>().Play();

		yield return new WaitForSeconds(2);
			SaveScore();
		GameEventManager.TriggerGameOver();
			
	}

	/*Explosion due to low speed
	 */

	IEnumerator ExplodeSpeed(){
		exploded=true;
		exploding=true;




		GameObject g=(GameObject)Instantiate(explosion, transform.position,transform.rotation);
		g.transform.parent=transform;
		AudioSource.PlayClipAtPoint(blast,transform.position);
		//AudioSource.PlayClipAtPoint(burning,transform.position);
		GetComponent<AudioSource>().clip=burning;
		GetComponent<AudioSource>().pitch=1;

		GetComponent<AudioSource>().Play();


		transform.GetComponent<Rigidbody>().AddForce(transform.up*500000.0f);
		Camera.main.transform.localPosition=new Vector3(Camera.main.transform.localPosition.x,
		                                                Camera.main.transform.localPosition.y,
		                                                Camera.main.transform.localPosition.z-4);
		
		Camera.main.transform.parent=null;

		yield return new WaitForSeconds(2);
		SaveScore();
		GameEventManager.TriggerGameOver();

	}




}
