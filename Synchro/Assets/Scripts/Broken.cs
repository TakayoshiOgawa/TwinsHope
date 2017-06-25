// ==============================
// file:Broken(.cs)
// brief:足場が消えるギミック
// ==============================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Broken : MonoBehaviour {

    [SerializeField]
    private float waitTime = 1F;
    [SerializeField]
    private float restoreTime = 5F;

    private Vector3 position;

    /// <summary>
    /// 初期化
    /// </summary>
	void Start () {
        position = transform.position;
	}

    /// <summary>
    /// 足場の消える処理
    /// </summary>
    /// <returns></returns>
    private IEnumerator Break() {
        yield return new WaitForSeconds(waitTime);
        transform.position = Vector3.up * 10000F;
        yield return new WaitForSeconds(restoreTime);
        transform.position = position;
    }

    /// <summary>
    /// 衝突された瞬間の処理
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Child")
        {
            StartCoroutine("Break");
        }
    }
}
