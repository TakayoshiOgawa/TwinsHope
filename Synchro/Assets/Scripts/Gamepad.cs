// ==============================
// file:Gamepad(.cs)
// brief:Unity上のゲームパッド
// ==============================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamepad : MonoBehaviour {

    public struct Button
    {
        public bool down { get; private set; }
        public bool trigger { get; private set; }
        public bool relese { get; private set; }

        public void GetButton(string buttonName)
        {
            down = Input.GetButton(buttonName);
            trigger = Input.GetButtonDown(buttonName);
            relese = Input.GetButtonUp(buttonName);
        }
    }

    public struct Axis
    {
        public float value { get; private set; }
        public bool min { get; private set; }
        public bool max { get; private set; }

        public void GetAxis(string axisName)
        {
            value = Input.GetAxis(axisName);
            min = (value <= -1F) ? true : false;
            max = (value >= +1F) ? true : false;
        }
    }

    public struct Stick
    {
        public Axis horizontal { get; private set; }
        public Axis vertical { get; private set; }

        public float x { get { return horizontal.value; } }
        public float y { get { return vertical.value; } }

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
        leftStick.GetStick("Horizontal", "Vertical");
        rightStick.GetStick("Pitch", "Yaw");
        trigger.GetAxis("Roll");
        shoulder.GetAxis("Shoulder");

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
