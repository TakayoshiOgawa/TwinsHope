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

    [HideInInspector] public Button a;
    [HideInInspector] public Button b;
    [HideInInspector] public Button x;
    [HideInInspector] public Button y;
    [HideInInspector] public Button lb;
    [HideInInspector] public Button rb;
    [HideInInspector] public Button back;
    [HideInInspector] public Button start;
    [HideInInspector] public Button ls;
    [HideInInspector] public Button rs;

    private void Update() {
        leftStick.GetStick("", "");
        rightStick.GetStick("", "");
        trigger.GetAxis("");
        shoulder.GetAxis("");

        a.GetButton("");
        b.GetButton("");
        x.GetButton("");
        y.GetButton("");
        lb.GetButton("");
        rb.GetButton("");
        back.GetButton("");
        start.GetButton("");
        ls.GetButton("");
        rs.GetButton("");
    }
}
