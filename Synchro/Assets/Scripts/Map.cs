using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {

    public int stageLevel;

    [SerializeField]
    private float stageScale = 1F;

    [SerializeField]
    private GameObject[] mapchip;

    private int oldLevel = -1;

	// Use this for initialization
	void Start () {
		for(var i = 0; i < transform.childCount;i++)
        {
            // マップ内のフロアにオブジェクトと拡大率を設定
            var floor = transform.GetChild(i).GetComponent<Floor>();
            floor.mapchip = mapchip;
            floor.scaling = stageScale;
        }
	}
	
	// Update is called once per frame
	void Update () {
        Remake();
	}

    /// <summary>
    /// マップの再生成
    /// </summary>
    private void Remake() {
        // 前と同じレベルなら再生成を行わない
        if (oldLevel == stageLevel) return;

        for(var i = 0; i < transform.childCount; i++)
        {
            // 古いデータを破棄して生成を行う
            var floor = transform.GetChild(i).GetComponent<Floor>();
            floor.Remove();
            floor.Make(stageLevel);
            // 全体の大きさから中央に座標を変更する
            transform.position = (Vector3.left * floor.width / 2F) + (Vector3.down * floor.height / 2F);
        }

        // 再生成が行われないように設定
        oldLevel = stageLevel;
    }
}
