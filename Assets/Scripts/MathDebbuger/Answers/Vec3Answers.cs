using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MathDebbuger;
using CustomMath;

public class Vec3Answers : MonoBehaviour
{
    public enum EXCERSICE
    {
        EX_1,
        EX_2,
        EX_3,
        EX_4,
        EX_5,
        EX_6,
        EX_7,
        EX_8,
        EX_9,
        EX_10
    }
    public EXCERSICE excersice = EXCERSICE.EX_1;
    public Color vectorColor;

    [Space(10)]

    public Vector3 A;
    public Vector3 B;

    private Vec3 a;
    private Vec3 b;
    private Vec3 value;

    private float timer;

    void Start()
    {
        a = new Vec3(A);
        b = new Vec3(B);
        value = Vec3.Zero;

        Vector3Debugger.AddVector(a, Color.blue, "Blue");
        Vector3Debugger.EnableEditorView("Blue");

        Vector3Debugger.AddVector(a, Color.green, "Green");
        Vector3Debugger.EnableEditorView("Green");

        Vector3Debugger.AddVector(value, vectorColor, "value");
        Vector3Debugger.EnableEditorView("value");
    }

    // Update is called once per frame
    void Update()
    {
        a = new Vec3(A);
        b = new Vec3(B);

        Vector3Debugger.UpdatePosition("Blue", a);
        Vector3Debugger.UpdatePosition("Green", b);

        switch (excersice)
        {
            case EXCERSICE.EX_1:
                value = a + b; //sum
                break;

            case EXCERSICE.EX_2:
                value = b - a; //difference 
                break;

            case EXCERSICE.EX_3:
                Vec3 aux3 = a;
                aux3.Scale(b);
                value = aux3;
                break;

            case EXCERSICE.EX_4:
                value = Vec3.Cross(b, a);
                break;

            case EXCERSICE.EX_5:
                timer += Time.deltaTime;
                if (timer >= 1)
                {
                    timer = 0;
                }

                value = Vec3.Lerp(b, a, timer);
                break;

            case EXCERSICE.EX_6:
                value = Vec3.Max(a, b);
                break;

            case EXCERSICE.EX_7:
                value = Vec3.Project(a, b);
                break;

            case EXCERSICE.EX_8:
                break;

            case EXCERSICE.EX_9:
                value = Vec3.Reflect(b, a).normalized;
                break;

            case EXCERSICE.EX_10:
                timer += Time.deltaTime;
                if (timer >= 10)
                {
                    timer = 0;
                }

                value = Vec3.LerpUnclamped(b, a, timer);
                break;
        }

        Vector3Debugger.UpdateColor("value", vectorColor);
        Vector3Debugger.UpdatePosition("value", value);
    }
}
