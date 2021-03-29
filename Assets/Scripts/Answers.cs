using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MathDebbuger;
using CustomMath;

public class Answers : MonoBehaviour
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

    [Space]

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

        Vector3Debugger.AddVector(A, Color.blue, "Blue");
        Vector3Debugger.EnableEditorView("Blue");

        Vector3Debugger.AddVector(B, Color.green, "Green");
        Vector3Debugger.EnableEditorView("Green");

        Vector3Debugger.AddVector(new Vector3(value.x, value.y, value.z).normalized, Color.red, "Red");
        Vector3Debugger.EnableEditorView("Red");
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
                break;

            case EXCERSICE.EX_2:
                break;

            case EXCERSICE.EX_3:
                break;

            case EXCERSICE.EX_4:
                break;

            case EXCERSICE.EX_5:
                break;

            case EXCERSICE.EX_6:
                break;

            case EXCERSICE.EX_7:
                break;

            case EXCERSICE.EX_8:
                break;

            case EXCERSICE.EX_9:
                break;

            case EXCERSICE.EX_10:
                break;
        }
        
        Vector3Debugger.UpdatePosition("Red", value);
    }
}
