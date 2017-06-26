// ==============================
// file:Gamepad(.cs)
// brief:Unity上のゲームパッド
// ==============================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamepad : MonoBehaviour {

    /// <summary>
    /// ゲームパッドのボタン
    /// </summary>
    public struct Button {
        public bool down { get; private set; }
        public bool trigger { get; private set; }
        public bool relese { get; private set; }

        /// <summary>
        /// 押されている状態を取得
        /// </summary>
        /// <param name="buttonName"></param>
        public void GetButton(string buttonName)
        {
            down = Input.GetButton(buttonName);
            trigger = Input.GetButtonDown(buttonName);
            relese = Input.GetButtonUp(buttonName);
        }
    }

    /// <summary>
    /// ゲームパッドの軸
    /// </summary>
    public struct Axis {
        public float value { get; private set; }
        public bool min { get { return (value <= -1F); } }
        public bool max { get { return (value >= +1F); } }

        /// <summary>
        /// 軸の値を取得
        /// </summary>
        /// <param name="axisName"></param>
        public void GetAxis(string axisName)
        {
            value = Input.GetAxis(axisName);
        }
    }

    /// <summary>
    /// ゲームパッドのスティック
    /// </summary>
    public struct Stick {
        public Axis horizontal { get; private set; }
        public Axis vertical { get; private set; }

        public float x { get { return horizontal.value; } }
        public float y { get { return vertical.value; } }

        /// <summary>
        /// 二つの軸の値を取得
        /// </summary>
        /// <param name="horizontalName"></param>
        /// <param name="verticalName"></param>
        public void GetStick(string horizontalName, string verticalName)
        {
            horizontal.GetAxis(horizontalName);
            vertical.GetAxis(verticalName);
        }
    }

    [HideInInspector] public Stick leftStick;
    [HideInInspector] public Stick rightStick;
    [HideInInspector] public Axis trigger;
    [HideInInspector] public Axis shoulder;

    [HideInInspector] public Button A;
    [HideInInspector] public Button B;
    [HideInInspector] public Button X;
    [HideInInspector] public Button Y;
    [HideInInspector] public Button LB;
    [HideInInspector] public Button RB;
    [HideInInspector] public Button BACK;
    [HideInInspector] public Button START;
    [HideInInspector] public Button LSP;
    [HideInInspector] public Button RSP;

    private void Update() {
        // スティックの値を更新
        leftStick.GetStick("Horizontal", "Vertical");
        rightStick.GetStick("Pitch", "Yaw");
        // 軸の値を更新
        trigger.GetAxis("Roll");
        shoulder.GetAxis("Shoulder");

        // ボタンの状態を更新
        A.GetButton("Jump");
        B.GetButton("Fire1");
        X.GetButton("Fire2");
        Y.GetButton("Fire3");
        LB.GetButton("LeftAct");
        RB.GetButton("RightAct");
        BACK.GetButton("Cancel");
        START.GetButton("Submit");
        LSP.GetButton("LPress");
        RSP.GetButton("RPress");
    }
}
