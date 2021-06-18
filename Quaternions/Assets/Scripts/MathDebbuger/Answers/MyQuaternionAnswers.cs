using System.Collections.Generic;
using UnityEngine;
using CustomMath;
using MathDebbuger;

public class MyQuaternionAnswers : MonoBehaviour
{
    private enum Excersice
    {
        FIRST,
        SECOND,
        THIRD
    }
    [SerializeField] private Excersice excersice = Excersice.FIRST;
    [Space]
    [SerializeField] private List<Vector3> secuencePointList = null;

    [SerializeField] private GameObject test1 = null;
    [SerializeField] private GameObject test2 = null;

    [SerializeField] private float speed = 0f;
    [SerializeField] private float angle = 0f;

    void Start()
    {
        secuencePointList[1] = new Vector3(0f, 90f, 0f);
        secuencePointList[2] = new Vector3(25f, 20f, 20f);

        Vector3Debugger.AddVectorsSecuence(secuencePointList, true, Color.red, "Secuence");
        Vector3Debugger.EnableEditorView();
    }

    private void Update()
    {
        switch (excersice)
        {
            case Excersice.FIRST:

                test1.transform.rotation = test1.transform.rotation * MyQuaternion.Euler(new Vector3(0, angle * Time.deltaTime * speed, 0));

                secuencePointList[1] = test1.transform.forward * 10.0f;
                secuencePointList[2] = Vector3.zero;
                secuencePointList[3] = Vector3.zero;
                secuencePointList[4] = Vector3.zero;
                break;

            case Excersice.SECOND:
                break;

            case Excersice.THIRD:
                break;
        }

        Vector3Debugger.UpdatePositionsSecuence("Secuence", secuencePointList);
    }
}
