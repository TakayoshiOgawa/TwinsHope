﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public Gamepad gamepad { get; private set; }

    public static GameController instance { get; private set; }

    public int mapLevel = 0;

    private void Awake() {
        // シーン遷移で破棄されないように処理
        if(instance != null)
        {
            Destroy(gameObject);
        }
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        // ゲームパッドを取得
        gamepad = GetComponent<Gamepad>();
    }
}
