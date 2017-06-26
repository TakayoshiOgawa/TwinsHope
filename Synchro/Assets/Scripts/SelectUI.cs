// ==============================
// file:SelectUI(.cs)
// brief:Selectの回転するUI
// ==============================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectUI : MonoBehaviour {

    [SerializeField]
    private float speed = 60F;

    /// <summary>
    /// 更新
    /// </summary>
    void Update () {
        // 回転方向を定義して全フレーム回転
        for (var i = 0; i < transform.childCount - 1; i++)
        {
            // フレームを１つずつ取得
            var frame = transform.GetChild(i).transform;

            // ２つに１つは回転方向を変える
            if (i % 2 == 0)
                frame.Rotate(Vector3.forward * speed * Time.deltaTime);
            else
                frame.Rotate(Vector3.back * speed * Time.deltaTime);
        }
    }
}
