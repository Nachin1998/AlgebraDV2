using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MathDebbuger;
using CustomMath;
public class Tester : MonoBehaviour
{
    public bool showMainTest = false;

    [Space]

    public Vector3 A;
    public Vector3 B;
    private Vector3 value;

    private Vec3 a;
    private Vec3 b;
    private Vec3 value2;

    float timer = 0;
    void Start()
    {
        if(showMainTest) InitMainTest();

        a = new Vec3(A);
        b = new Vec3(B);
        value = Vector3.Reflect(A, B);
        value2 = Vec3.Reflect(a, b);

        Vector3Debugger.AddVector(Vector3.zero, A, Color.blue, "Blue");
        Vector3Debugger.EnableEditorView("Blue");

        Vector3Debugger.AddVector(Vector3.zero, B, Color.green, "Green");
        Vector3Debugger.EnableEditorView("Green");

        Vector3Debugger.AddVector(Vector3.zero, new Vector3(value2.x, value2.y, value2.z).normalized, Color.red, "Red");
        Vector3Debugger.EnableEditorView("Red");

        Debug.Log("1: " + value);
    }

    void Update()
    {
        //timer += Time.deltaTime;       
        a = new Vec3(A);
        b = new Vec3(B);

        value2 = Vec3.Reflect(a, b);

        Vector3Debugger.UpdatePosition("Blue", A);
        Vector3Debugger.UpdatePosition("Green", B);
        Vector3Debugger.UpdatePosition("Red", new Vector3(value2.x, value2.y, value2.z).normalized);

        Inputs();
    }

    void InitMainTest()
    {
        List<Vector3> vectors = new List<Vector3>();
        vectors.Add(new Vec3(10.0f, 0.0f, 0.0f));
        vectors.Add(new Vec3(10.0f, 10.0f, 0.0f));
        vectors.Add(new Vec3(20.0f, 10.0f, 0.0f));
        vectors.Add(new Vec3(20.0f, 20.0f, 0.0f));
        Vector3Debugger.AddVectorsSecuence(vectors, false, Color.red, "secuencia");
        Vector3Debugger.EnableEditorView("secuencia");

        Vector3Debugger.AddVector(new Vector3(10, 10, 0), Color.blue, "Vector3 Blue");
        Vector3Debugger.EnableEditorView("Vector3 Blue");

        Vector3Debugger.AddVector(Vector3.down * 7, Color.green, "Vector3 Green");
        Vector3Debugger.EnableEditorView("Vector3 Green");
    }

    void Inputs()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Vector3Debugger.SetVectorState("Blue", !Vector3Debugger.IsVectorActive("Blue"));
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Vector3Debugger.SetVectorState("Green", !Vector3Debugger.IsVectorActive("Green"));
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Vector3Debugger.SetVectorState("Red", !Vector3Debugger.IsVectorActive("Red"));
        }
    }
}
