// ==============================
// file:Player(.cs)
// brief:操作キャラクター
// ==============================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character {

    public bool isControll { get; private set; }
    public bool connected { get; private set; }

    private LineRenderer connectLine;

	// Use this for initialization
	protected override void Start () {
        // 基底クラスの初期化
        base.Start();
        // 接続線としてラインレンダラーを取得
        connectLine = GetComponent<LineRenderer>();

        // マテリアルから描画色を設定
        GetComponent<SpriteRenderer>().color = colorMat.color;
	}
	
	// Update is called once per frame
	protected override void Update () {
        // 基底クラスの更新
        base.Update();

        // 操作しない場合ここで終了
        if (!connected) return;
	}
}
