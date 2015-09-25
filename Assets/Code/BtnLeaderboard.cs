using UnityEngine;
using System.Collections;

using GooglePlayGames;
using GooglePlayGames.BasicApi.Multiplayer;
using UnityEngine.SocialPlatforms;

public class BtnLeaderboard : MonoBehaviour {

	private string HAS_CLICKED="has_clicked";



	void Start(){

		/* Just Activate where user is logged in or not*/
		PlayGamesPlatform.DebugLogEnabled=true;
		PlayGamesPlatform.Activate();

		if (PlayerPrefs.GetInt(HAS_CLICKED)==1){
			SignIn();
		}





	}


	void Update () {

		
		if (Input.GetMouseButtonDown(0)&&GetComponent<GUITexture>().HitTest(Input.mousePosition)){

			Debug.Log("BtnLeaderboard");

			if (!PlayGamesPlatform.Instance.IsAuthenticated()){
				SignIn();
				return;
			}

			ShowLeaderBoard();

		}

	}

	public void SignIn(){
		
		Social.localUser.Authenticate((bool success) => {
			Debug.Log("Success "+success);
			
			if (success){
				PlayerPrefs.SetInt(HAS_CLICKED, 1);
			}
			
		});
	}


	private void ShowLeaderBoard(){

		PlayGamesPlatform.Instance.ShowLeaderboardUI("YOUR_LEADERBOARD_ID");
	}
}
