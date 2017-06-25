// ==============================
// file:Flashing(.cs)
// brief:表示が点滅するギミック
// ==============================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashing : MonoBehaviour {

    [SerializeField]
    private float interval = 1F;

    private Vector3 position;

    /// <summary>
    /// 初期化
    /// </summary>
	void Start () {
        // 配置された座標を保存して点滅開始
        position = transform.position;
        StartCoroutine("Blink");
	}

    /// <summary>
    /// コルーチンによる点滅
    /// </summary>
    /// <returns></returns>
    private IEnumerator Blink() {
        // スタートから毎回呼び出す
        while (true)
        {
            // 元の位置に座標を設定
            transform.position = position;
            yield return new WaitForSeconds(interval);
            // 消えたように見せる座標に設定
            transform.position = Vector3.up * 10000F;
            yield return new WaitForSeconds(interval);
        }
    }
}
