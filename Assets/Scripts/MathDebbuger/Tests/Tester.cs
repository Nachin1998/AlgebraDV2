using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MathDebbuger;
using CustomMath;
public class Tester : MonoBehaviour
{
    public bool showMainTest = false;
    [Space]
    public Vector3 TEST_VEC;
    [Space]
    public Vector3 A;
    public Vector3 B;

    private Vec3 testVec;
    private Vec3 a;
    private Vec3 b;

    float timer = 0;
    void Start()
    {
        if(showMainTest) InitMainTest();

        testVec = new Vec3(TEST_VEC);
        a = new Vec3(A);
        b = new Vec3(B);

        Vector3 value = Vector3.Reflect(A, B);
        Debug.Log("1 " + value);

        Vec3 value2 = Vec3.Reflect(a, b);
        Debug.Log("2 " + value2);
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.L))
        {
            StartCoroutine(UpdateBlueVector());
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            Vector3Debugger.TurnOffVector("Blue");
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            Vector3Debugger.TurnOnVector("Blue");
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
        
        Vector3Debugger.AddVector(new Vector3(10, 10, 0), Color.blue, "Blue");
        Vector3Debugger.EnableEditorView("Blue");

        Vector3Debugger.AddVector(Vector3.down * 7, Color.green, "Green");
        Vector3Debugger.EnableEditorView("Green");
    }

    IEnumerator UpdateBlueVector()
    {
        for (int i = 0; i < 100; i++)
        {
            Vector3Debugger.UpdatePosition("Blue", new Vector3(2.4f, 6.3f, 0.5f) * (i * 0.05f));
            yield return new WaitForSeconds(0.2f);
        }
    }

}
