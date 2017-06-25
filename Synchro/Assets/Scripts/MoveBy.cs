// ==============================
// file:MoveBy(.cs)
// brief:強制移動ギミック
// ==============================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBy : MonoBehaviour {

    [SerializeField]
    private float moveSpeed = 1F;

    /// <summary>
    /// 衝突されてる間の処理
    /// </summary>
    private void OnCollisionStay(Collision collision) {
        // キャラクターのタグであれば処理を行う
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Child")
        {
            // キャラクターをギミックの右方向へ移動させる
            collision.transform.Translate(transform.right * moveSpeed);
        }
    }
}
