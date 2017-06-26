// ==============================
// file:SelectController(.cs)
// brief:Selectシーンの操作
// ==============================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectController : MonoBehaviour {

    [SerializeField]
    private SelectUI[] stages;
    private int stageNumber;

    private Gamepad gamepad;

    [SerializeField]
    private float waitTime = 1F;
    private float current = 0F;

    /// <summary>
    /// 初期化
    /// </summary>
	void Start () {
        // シーンから"SelectUI"のオブジェクトを取得して昇順にソート
        stages = FindObjectsOfType<SelectUI>();
        for (int i = 0; i < stages.Length - 1; i++)
        {
            for (int j = stages.Length - 1; j > i; j--)
            {
                var stage0 = int.Parse(stages[j].GetComponent<UnityEngine.UI.Text>().text);
                var stage1 = int.Parse(stages[j - 1].GetComponent<UnityEngine.UI.Text>().text);

                // バブルソート
                if (stage0 < stage1)
                {
                    var s = stages[j];
                    stages[j] = stages[j - 1];
                    stages[j - 1] = s;
                }
            }
        }

        // 静的クラスからゲームパッドを取得
        gamepad = GameController.instance.gamepad;
        stageNumber = 0;
	}
	
    /// <summary>
    /// 更新
    /// </summary>
	void Update () {

        if (gamepad.A.trigger)
        {
            // 静的なクラスへステージの番号を渡す
            GameController.instance.mapLevel = stageNumber;
            // プレイシーンへ遷移
            SceneManager.LoadScene("Play");
        }
        if (gamepad.B.trigger)
        {
            // タイトルシーンへ遷移
            SceneManager.LoadScene("Title");
        }

        // 選択されているUIの回転を行う
        stages[stageNumber].enabled = true;

        // 入力を受け取り一定時間で処理を行う
        var x = Input.GetAxis("Horizontal");
        if(x <= -1F)
        {
            current += Time.deltaTime;
        }
        else
        if(x >= +1F)
        {
            current += Time.deltaTime;
        }
        else
        {
            current = 0F;
        }

        // 入力された時間が待機時間以下なら処理を省略
        if (current < waitTime) return;

        // 移動量を取得
        var count = 0;
        if(x <= -1F)
        {
            count = -1;
        }
        else
        if(x >= +1F)
        {
            count = +1;
        }

        // 選択するUIを変更
        if (count != 0)
        {
            stages[stageNumber].enabled = false;
            stageNumber += count;
            stageNumber = Mathf.Clamp(stageNumber, 0, stages.Length - 1);
            current = 0;
        }
    }
}
