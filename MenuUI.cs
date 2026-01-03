using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour 
{
	// 「PLAY」ボタンが押されたときに呼ばれる
	public void PlayButton ()
	{
		Application.LoadLevel(1);	// 「Game」シーンを読み込んでゲームを開始する
	}

	// 「QUIT」ボタンが押されたときに呼ばれる
	public void QuitButton ()
	{
		Application.Quit();			// ゲームを終了する
	}
}
