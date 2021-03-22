using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MathDebbuger;
using CustomMath;
public class Tester : MonoBehaviour
{
    public Vector3 TEST_VEC;
    [Space]
    public Vector3 A;
    public Vector3 B;
    Vec3 testVec;

    Vec3 a;
    Vec3 b;
    void Start()
    {
        //InitMainTest();

        testVec = new Vec3(TEST_VEC);
        a = new Vec3(A);
        b = new Vec3(B);
        
        Vector3 value = Vector3.Min(A, B);
        Debug.Log("1 " + value);

        Vec3 value2 = Vec3.Min(a, b);
        Debug.Log("2 " + value2);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            StartCoroutine(UpdateBlueVector());
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            Vector3Debugger.TurnOffVector("elAzul");
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            Vector3Debugger.TurnOnVector("elAzul");
        }
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
        
        Vector3Debugger.AddVector(new Vector3(10, 10, 0), Color.blue, "elAzul");
        Vector3Debugger.EnableEditorView("elAzul");

        Vector3Debugger.AddVector(Vector3.down * 7, Color.green, "elVerde");
        Vector3Debugger.EnableEditorView("elVerde");
    }

    IEnumerator UpdateBlueVector()
    {
        for (int i = 0; i < 100; i++)
        {
            Vector3Debugger.UpdatePosition("elAzul", new Vector3(2.4f, 6.3f, 0.5f) * (i * 0.05f));
            yield return new WaitForSeconds(0.2f);
        }
    }

}
