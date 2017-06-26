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

    /// <summary>
    /// 初期化
    /// </summary>
    void Start () {
		for(var i = 0; i < transform.childCount;i++)
        {
            // マップ内のフロアにオブジェクトと拡大率を設定
            var floor = transform.GetChild(i).GetComponent<Floor>();
            floor.mapchip = mapchip;
            floor.scaling = stageScale;
        }
	}
	
    /// <summary>
    /// 更新
    /// </summary>
	void Update () {
        // 前と同じレベルなら再生成を行わない
        if (oldLevel == stageLevel) return;

        for (var i = 0; i < transform.childCount; i++)
        {
            // 古いデータを破棄して生成を行う
            var floor = transform.GetChild(i).GetComponent<Floor>();
            floor.Remove();
            floor.Make(stageLevel);
        }

        // 再生成が行われないように設定
        oldLevel = stageLevel;
    }
}
