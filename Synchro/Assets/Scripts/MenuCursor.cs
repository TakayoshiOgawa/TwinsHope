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
        MoveUp((y >= +1F), -1, 30);

        MoveUp((y <= -1F), +1, 30);

        sceneNumber = Mathf.Clamp(sceneNumber, 0, menu.Length - 1);
        cursor.position = menu[sceneNumber].position;
    }

    private void MoveUp(bool move, int num, int recast) {
        if (move)
        {
            if (recastCount++ <= recast)
            {
                sceneNumber += num;
                recastCount = 0;
            }
        }
        else
        {
            recastCount = 0;
        }
    }
}
