// ==============================
// file:JumpTo(.cs)
// brief:強制跳躍ギミック
// ==============================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpTo : MonoBehaviour {

    [SerializeField]
    private float jumpPower = 1F;

    /// <summary>
    /// 衝突された瞬間の処理
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision) {
        // キャラクターのタグであれば処理を行う
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Child")
        {
            // キャラクターに対してギミックの右上方向へ力を加える
            var rb = collision.gameObject.GetComponent<Rigidbody>();
            rb.AddForce((transform.up + transform.right) * jumpPower * 100F);
        }
    }
}
