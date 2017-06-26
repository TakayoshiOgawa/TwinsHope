using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Floor : MonoBehaviour {

    public TextAsset[] mapdata;

    [SerializeField]
    private Player character;

    [HideInInspector]
    public float scaling = 1F;
    [HideInInspector]
    public GameObject[] mapchip;

    public float width { get; private set; }
    public float height { get; private set; }

    /// <summary>
    /// テキストからブロックを生成しフロアを作成
    /// </summary>
    /// <param name="stageLevel"></param>
    public void Make(int stageLevel) {
        var row = 0;
        var sub = Vector3.zero;

        // テキストからマップデータを読み込む
        var reader = new StringReader(mapdata[stageLevel].text);
        while(reader.Peek() > -1)
        {
            // 行ごとに読み込みカンマで区切る
            var line = reader.ReadLine();
            var values = line.Split(',');

            if(row++ == 0)
            {
                // １行目のみキャラクターデータを編集
            }
            //else
            {
                // 以降はマップチップの作成
                foreach(var value in values)
                {
                    var integer = int.Parse(value);
                    if(0 <= integer && integer < mapchip.Length)
                    {
                        // スクリプトの子要素にマップチップを設定
                        var obj = Instantiate(mapchip[integer], transform);
                        // 生成に順じて座標を設定、全体の拡大率を合わせる
                        obj.transform.position = transform.position + sub;
                        obj.transform.localScale *= scaling;

                        // テキストの番号に応じて種類を分別する
                        switch(integer)
                        {
                            case 0:
                                var characterColor = character.GetComponent<SpriteRenderer>().color;
                                obj.GetComponent<SpriteRenderer>().color = characterColor;
                                break;
                            case 1:
                                character.spawnPosition = obj.transform.position;
                                character.Restart();
                                break;
                            default:
                                break;
                        }
                    }
                    // 横の座標差分を設定
                    sub.x += transform.localScale.x * scaling * 2.5F;
                }
                // 縦の座標差分を設定
                sub.x = 0F;
                sub.y -= transform.localScale.y * scaling * 2.5F;
            }
        }
    }

    /// <summary>
    /// 全てのブロックを破棄
    /// </summary>
    public void Remove() {
        for(var i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}
