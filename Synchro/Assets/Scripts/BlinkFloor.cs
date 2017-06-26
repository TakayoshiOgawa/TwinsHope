// ==============================
// file:BlinkFloor(.cs)
// brief:足場を反転するギミック
// ==============================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkFloor : MonoBehaviour {

    private void OnCollisionEnter(Collision collision) {
        // キャラクターのタグであれば内部の関数を呼び出す
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Child")
        {
            collision.gameObject.SendMessage("BlinkFloor");
        }
    }
}
