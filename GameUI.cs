using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameUI : MonoBehaviour 
{
	public GameManager manager;		// GameManager クラス

	public Text scoreText;			// スコアを表示する Text コンポーネント
	public Text livesText;			// ライフ数を表示する Text コンポーネント

	public GameObject gameOverScreen;	// ゲームオーバー画面のゲームオブジェクト
	public Text gameOverScoreText;	// ゲームオーバー時のスコアを表示する Text コンポーネント

	public GameObject winScreen;	// 勝利画面のゲームオブジェクト

	void Update ()
	{
		if(!manager.gameOver && !manager.wonGame){					// ゲームオーバーでもクリアでもない場合
			scoreText.text = "<b>SCORE</b>\n" + manager.score;		// 「SCORE」を太字で表示し、改行して GameManager のスコアを表示
			livesText.text = "<b>LIVES</b>: " + manager.lives;		// 「LIVES」を太字で表示し、GameManager のライフ数を表示
		}else{														// ゲームオーバー、またはクリア時
			scoreText.text = "";
			livesText.text = "";
		}
	}

	// ゲームオーバー時に呼ばれる
	public void SetGameOver ()
	{
		gameOverScreen.active = true;
		gameOverScoreText.text = "<b>YOU ACHIEVED A SCORE OF</b>\n" + manager.score;
		// 「YOU ACHIEVED A SCORE OF」を太字で表示し、改行して GameManager のスコアを表示
	}

	// ゲームクリア時に呼ばれる
	public void SetWin ()
	{
		winScreen.active = true;
	}

	// 「TRY AGAIN」ボタンが押されたときに呼ばれる
	public void TryAgainButton ()
	{
		gameOverScreen.active = false;
		winScreen.active = false;
		manager.StartGame();			// ゲームを最初からやり直す
	}

	// 「MENU」ボタンが押されたときに呼ばれる
	public void MenuButton ()
	{
		Application.LoadLevel(0);	// 「Menu」シーンを読み込む
	}
}
