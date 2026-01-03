using UnityEngine;
using System.Collections;

public class Paddle : MonoBehaviour 
{
	public float speed;				// パドルが1秒間に移動する距離
	public float minX;				// パドルが移動できる最小のX座標
	public float maxX;				// パドルが移動できる最大のX座標
	public bool canMove;			// パドルが移動可能かどうか
	public Rigidbody2D rig;			// パドルの Rigidbody2D コンポーネント

	void Update ()
	{
		if(canMove){															// パドルは移動可能か？
			if(Input.GetKey(KeyCode.LeftArrow)){								// 左矢印キーが押されているか？
				rig.linearVelocity = new Vector2(-1 * speed * Time.deltaTime, 0);	// 左方向へ移動させる
			}
			else if(Input.GetKey(KeyCode.RightArrow)){							// 右矢印キーが押されているか？
				rig.linearVelocity = new Vector2(1 * speed * Time.deltaTime, 0);	// 右方向へ移動させる
			}
			else{
				rig.linearVelocity = Vector2.zero;								// キーが押されていない場合は停止
			}

			// X座標が minX～maxX の範囲を超えないように制限する
			transform.position = new Vector3(
				Mathf.Clamp(transform.position.x, minX, maxX),
				transform.position.y,
				0
			);
		}
	}

	// 他のオブジェクトがこのオブジェクトの BoxCollider2D（Trigger）に入ったときに呼ばれる
	// 引数 col は衝突した Collider2D オブジェクト
	void OnTriggerEnter2D (Collider2D col)
	{
		if(col.gameObject.tag == "Ball"){											// 衝突したオブジェクトのタグが「Ball」か？
			col.gameObject.GetComponent<Ball>().SetDirection(transform.position);	// Ball コンポーネントを取得し、パドルの位置を使ってボールを跳ね返す
		}
	}

	// パドルを画面中央にリセットするときに呼ばれる
	public void ResetPaddle ()
	{
		transform.position = new Vector3(0, transform.position.y, 0);	// パドルの X 座標を 0 に設定する
	}
}
