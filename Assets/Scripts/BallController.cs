using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BallController : MonoBehaviour {
	public Text scoreText;
	public Text winText;
	public Text timeText;
	private int count;
	private const float FALLING_HEIGHT = -10.0f;
	private float elpasedTime;
	private bool endFlag;

	void Start() {
		// 初期化
		count = 0;
		scoreText.text = "";
		winText.text = "";
        elpasedTime = 0.0f;
		endFlag = false;

		SetCountText();
		DisplayTimeFormat(elpasedTime);
	}

	// Before physics calculations
	void FixedUpdate() {
		if (!endFlag) {
			// 経過時間を計測、表示
        	elpasedTime += Time.deltaTime;
			DisplayTimeFormat(elpasedTime);
		}

		DetectGameEnd();
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "PickUp") {
			other.gameObject.SetActive (false);
			count++;
			
			SetCountText();
		}
	}

	void SetCountText() {
		scoreText.text = "Score: " + count.ToString();
	}

	void DetectGameEnd() {
		// transformを取得
        Transform myTransform = this.transform;

		 // 座標を取得
        Vector3 pos = myTransform.position;

		if (pos.y < FALLING_HEIGHT) {
			endFlag = true;
			winText.text = "YOU LOSE!  quit: push [ESC]";

			// ゲーム終了
			EndGame();
		}
		else if (count >= 12) {
			endFlag = true;
			winText.text = "YOU WIN!  quit: push [ESC]";

			// ゲーム終了
			EndGame();
		}
	}

	//ゲーム終了
    private void EndGame() {
        //Escが押された時
        if (Input.GetKey(KeyCode.Escape))
        {

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
#else
    		Application.Quit();//ゲームプレイ終了
#endif
        }

    }

	//floatの値をタイム表記の文字列で返す
    private void DisplayTimeFormat(float time) {
        string timeString = string.Format("{0:D2}:{1:D2}:{2:D2}",
            (int)time / 60,
            (int)time % 60,
            (int)(time * 100) % 60);
        
		timeText.text = timeString;
    }
}