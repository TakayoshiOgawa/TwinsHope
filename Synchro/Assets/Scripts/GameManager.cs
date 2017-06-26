// ==============================
// file:GameManager(.cs)
// brief:ゲームシーンのみの管理者
// ==============================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [SerializeField]
    private Player player1;
    [SerializeField]
    private Player player2;
    [SerializeField]
    private Player child;
    [SerializeField]
    private RectTransform selectUI;
    [SerializeField]
    private RectTransform clearLayer;
    [SerializeField]
    private RectTransform pauseLayer;

    private Gamepad gamepad;
    private int controllNum;
    private bool isClear;
    private bool isPause;

    /// <summary>
    /// 初期化
    /// </summary>
    void Start () {
        // 中央のキャラは操作不可能に設定
        child.isControll = false;

        // 静的なクラスからゲームパッドを取得
        gamepad = GameController.instance.gamepad;
        // 同時操作の番号に設定
        controllNum = 0;
        isClear = false;
        isPause = false;
	}

    /// <summary>
    /// 更新
    /// </summary>
    void Update () {
        if (isClear)
        {
            Pause(isClear);
            clearLayer.gameObject.SetActive(isClear);
        }
        else
        {
            if (gamepad.START.trigger)
            {
                isPause = !isPause;
                Pause(isPause);
                pauseLayer.gameObject.SetActive(isPause);
            }
        }

        // 操作キャラの切り替え
        SwitchControll();

        // 選択されたキャラになるようにUIを回転
        var toRota = Quaternion.Euler(0F, 0F, controllNum % 3 * 120F);
        selectUI.rotation = Quaternion.RotateTowards(selectUI.rotation, toRota, 180F * Time.deltaTime);

        // PlayerとChildの半径が接触していたら接続中であるとフラグに変更を加える
        player1.connected = HitCircle2D(player1.transform, child.transform);
        player2.connected = HitCircle2D(player2.transform, child.transform);
        // Player1と2が同時に接続中であればChildは操作可能となる
        child.isControll = player1.connected && player2.connected;
        // PlayerとChildの座標を繋ぐ
        player1.Connect(child.transform);
        player2.Connect(child.transform);
    }

    /// <summary>
    /// 入力により操作キャラクターを切り替える
    /// </summary>
    private void SwitchControll() {
        if (gamepad.X.trigger)
        {
            // 円滑な右回りのための三項演算子
            controllNum = (controllNum >= 3) ? 1 : controllNum + 1;
        }
        if (gamepad.Y.trigger)
        {
            // 中央へ設定
            controllNum = 3;
        }
        if (gamepad.B.trigger)
        {
            // 円滑な左回りのための三項演算子
            controllNum = (controllNum <= 0) ? 2 : controllNum - 1;
        }

        switch (controllNum % 3)
        {
            case 0:  // 同時操作
                player1.isControll = true;
                player2.isControll = true;
                break;
            case 1:  // "青"操作
                player1.isControll = false;
                player2.isControll = true;
                break;
            case 2:  // "赤"操作
                player1.isControll = true;
                player2.isControll = false;
                break;
        }
    }

    /// <summary>
    /// PlayerとChildの円衝突(x,y)を簡略化
    /// </summary>
    /// <param name="player"></param>
    /// <param name="child"></param>
    /// <param name="circleScale"></param>
    /// <returns></returns>
    private bool HitCircle2D(Transform player, Transform child, float circleScale = 1F) {
        var x = (player.position.x - child.position.x);
        var y = (player.position.y - child.position.y);
        var r = (player.localScale.x * circleScale + child.localScale.x * circleScale);
        return (x * x + y * y <= r * r);
    }

    /// <summary>
    /// 処理等の停止
    /// </summary>
    /// <param name="stop"></param>
    private void Pause(bool stop) {
        // 移動処理の停止
        player1.enabled = !stop;
        player2.enabled = !stop;
        child.enabled = !stop;
        // 物理演算の処理を停止
        player1.GetComponent<Rigidbody>().isKinematic = stop;
        player2.GetComponent<Rigidbody>().isKinematic = stop;
        child.GetComponent<Rigidbody>().isKinematic = stop;
    }

    /// <summary>
    /// 外部からクリアを取得する
    /// </summary>
    private void Clear() {
        isClear = true;
    }
}
