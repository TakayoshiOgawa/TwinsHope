// ==============================
// file:Player(.cs)
// brief:操作キャラクター
// ==============================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character {

    [HideInInspector]
    public bool isControll;
    [HideInInspector]
    public bool connected;

    private Gamepad gamepad;

    private LineRenderer connectLine;

    /// <summary>
    /// 初期化
    /// </summary>
	// Use this for initialization
	protected override void Start () {
        // 基底クラスの初期化
        base.Start();

        // フラグの初期化
        isControll = true;
        connected = false;

        // 静的クラスからゲームパッドを取得
        gamepad = GameController.instance.gamepad;

        // 接続線としてラインレンダラーを取得
        connectLine = GetComponent<LineRenderer>();

        // マテリアルから描画色を設定
        GetComponent<SpriteRenderer>().color = colorMat.color;
	}
	
    /// <summary>
    /// 更新
    /// </summary>
	// Update is called once per frame
	protected override void Update () {
        // 基底クラスの更新
        base.Update();

        transform.GetChild(0).GetComponent<SpriteRenderer>().color = (isControll) ? colorMat.color : Color.white;

        // 操作しない場合ここで終了
        if (!isControll) return;

        // 移動処理
        var move_x = Input.GetAxis("Horizontal");
        Move(new Vector3(move_x, 0F, 0F), moveSpeed);

        // ジャンプ処理
        if(gamepad.A.down)
        {
            Jump(jumpPower);
        }

        // 重力反転処理
        if(gamepad.RB.trigger)
        {
            ChangeGravity();
        }

        // 足場反転処理
        if(gamepad.LB.trigger)
        {
            BlinkFloor();
        }
	}

    /// <summary>
    /// 衝突された瞬間の処理
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "DeadZone")
        {
            Restart();
        }
    }
}
