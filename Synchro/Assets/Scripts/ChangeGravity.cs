// ==============================
// file:ChangeGravity(.cs)
// brief:重力反転するギミック
// ==============================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeGravity : MonoBehaviour {

    private void OnCollisionEnter(Collision collision) {
        // キャラクターのタグであれば内部の関数を呼び出す
        if(collision.gameObject.tag == "Player" || collision.gameObject.tag == "Child")
        {
            collision.gameObject.SendMessage("ChangeGravity");
        }
    }
}
