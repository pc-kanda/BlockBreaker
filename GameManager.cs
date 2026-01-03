using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour 
{
	public int score;				// プレイヤーの現在のスコア
	public int lives;				// プレイヤーの残りライフ数
	public int ballSpeedIncrement;	// ボールがブロックに当たるたびに増加するスピード量
	public bool gameOver;			// ゲームオーバー時に true になる
	public bool wonGame;			// ゲームクリア時に true になる

	public GameObject paddle;		// パドルのゲームオブジェクト
	public GameObject ball;			// ボールのゲームオブジェクト

	public GameUI gameUI;			// GameUI クラス

	// プレハブ
	public GameObject brickPrefab;	// 生成されるブロックのプレハブ

	public List<GameObject> bricks = new List<GameObject>();	// 現在画面上に存在するすべてのブロックのリスト
	public int brickCountX;										// 横方向に生成されるブロックの数（奇数推奨）
	public int brickCountY;										// 縦方向に生成されるブロックの数

	public Color[] colors;			// ブロックの色配列（色の並びパターンを変更できる）

	void Start ()
	{
		StartGame(); // 値の初期化とブロック生成を行い、ゲームを開始する
	}

	// ゲーム開始時に呼ばれる
	public void StartGame ()
	{
		score = 0;
		lives = 3;
		gameOver = false;
		wonGame = false;
		paddle.active = true;
		ball.active = true;
		paddle.GetComponent<Paddle>().ResetPaddle();
		CreateBrickArray();
	}

	// ブロックを生成し、色を設定する
	public void CreateBrickArray ()
	{
		int colorId = 0;	// 現在使用している色を示すID（ブロックの行ごとに +1 される）

		for(int y = 0; y < brickCountY; y++){															
			for(int x = -(brickCountX / 2); x < (brickCountX / 2); x++){
				Vector3 pos = new Vector3(0.8f + (x * 1.6f), 1 + (y * 0.4f), 0);						// ブロックが生成される位置
				GameObject brick = Instantiate(brickPrefab, pos, Quaternion.identity) as GameObject;	// pos の位置に新しいブロックを生成
				brick.GetComponent<Brick>().manager = this;												// Brick コンポーネントを取得し、GameManager を設定
				brick.GetComponent<SpriteRenderer>().color = colors[colorId];							// ブロックの色を設定
				bricks.Add(brick);																		// ブロックをリストに追加
			}

			colorId++;						// 次の行に進むので色IDを 1 増やす

			if(colorId == colors.Length)	// 色配列の最後まで使い切った場合
				colorId = 0;				// 最初の色に戻す
		}

		ball.GetComponent<Ball>().ResetBall();	// ボールを画面中央にリセットする
	}

	// ブロックがすべてなくなり、ゲームクリアしたときに呼ばれる
	public void WinGame ()
	{
		wonGame = true;
		paddle.active = false;			// パドルを無効化（見えなくする）
		ball.active = false;			// ボールを無効化（見えなくする）
		gameUI.SetWin();				// ゲームクリア用UIを表示
	}

	// ボールがパドルの下に落ちて「ミス」したときに呼ばれる
	public void LiveLost ()
	{
		lives--;										// ライフを 1 減らす

		if(lives < 0){									// ライフが 0 未満（残機なし）の場合
			gameOver = true;
			paddle.active = false;						// パドルを無効化
			ball.active = false;						// ボールを無効化
			gameUI.SetGameOver();						// ゲームオーバーUIを表示

			for(int x = 0; x < bricks.Count; x++){		// bricks リストを順に処理
				Destroy(bricks[x]);						// ブロックを破壊
			}

			bricks = new List<GameObject>();			// bricks リストを初期化
		}
	}
}
