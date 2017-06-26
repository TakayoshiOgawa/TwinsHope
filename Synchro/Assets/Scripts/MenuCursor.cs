// ==============================
// file:MenuCursor(.cs)
// brief:メニュー時の操作カーソル
// ==============================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuCursor : MonoBehaviour {

    [SerializeField]
    private RectTransform cursor;

    [SerializeField]
    private RectTransform[] menu;
    [SerializeField]
    private string[] sceneName;

    private Gamepad gamepad;
    private int sceneNumber;
    private int recastCount;

    /// <summary>
    /// 初期化
    /// </summary>
	void Start () {
        // 静的クラスからゲームパッドを取得
        gamepad = GameController.instance.gamepad;
        // 初期の設定
        sceneNumber = 0;
        recastCount = 0;
	}

    /// <summary>
    /// 更新
    /// </summary>
    void Update () {
        // Aボタンで選択されたシーンへ遷移
        if(gamepad.A.trigger)
        {
            if (sceneName[sceneNumber] != "")
            {
                SceneManager.LoadScene(sceneName[sceneNumber]);
            }
            else
            {
                Application.Quit();
            }
        }

        var y = Input.GetAxis("Vertical");
        // シーン番号を上へ
        MoveUp((y >= +1F), -1, 30);
        // シーン番号を下へ
        MoveUp((y <= -1F), +1, 30);

        // カーソルの位置を修正
        sceneNumber = Mathf.Clamp(sceneNumber, 0, menu.Length - 1);
        cursor.position = menu[sceneNumber].position;
    }

    /// <summary>
    /// 入力されたら値を変更（リキャスト付き）
    /// </summary>
    /// <param name="move"></param>
    /// <param name="num"></param>
    /// <param name="recast"></param>
    private void MoveUp(bool move, int num, int recast) {
        // 入力が検知されたらカウントアップ
        if (move)
        {
            // 待機時間を超えたら値を変動させる
            if (recastCount++ <= recast)
            {
                sceneNumber += num;
                recastCount = 0;
            }
        }
        else
        {
            // 入力されていない状態へ戻す
            recastCount = 0;
        }
    }
}
