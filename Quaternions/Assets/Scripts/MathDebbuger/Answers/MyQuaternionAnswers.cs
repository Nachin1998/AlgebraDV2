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
    [SerializeField] private List<Vector3> secuencePointList = null;

    [Space]

    [SerializeField] private GameObject point1 = null;
    [SerializeField] private GameObject point2 = null;

    [SerializeField] private float speed = 0f;
    [SerializeField] private float angle = 0f;

    private int pointsInExcersice = 0;

    private void Start()
    {
        SetExcercise(0);

        Vector3Debugger.AddVectorsSecuence(secuencePointList, true, Color.red, "Secuence");
        Vector3Debugger.EnableEditorView();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DoNextExcercise();
        }

        switch (excersice)
        {
            case Excersice.FIRST:
                point1.transform.rotation *= MyQuaternion.Euler(0, angle * Time.deltaTime * speed, 0);

                secuencePointList[1] = point1.transform.forward * 10f;
                break;

            case Excersice.SECOND:
                point1.transform.rotation *= MyQuaternion.Euler(0, angle * Time.deltaTime * speed, 0);

                secuencePointList[1] = point1.transform.forward * 10f;
                secuencePointList[2] = secuencePointList[1] + (Vector3.up * 10f);
                secuencePointList[3] = secuencePointList[2] + (point1.transform.forward * 10f);
                break;

            case Excersice.THIRD:
                point1.transform.rotation *= Quaternion.Euler(0, angle * Time.deltaTime * speed, angle * Time.deltaTime * speed);
                point2.transform.rotation *= Quaternion.Euler(0, -angle * Time.deltaTime * speed, -angle * Time.deltaTime * speed);

                secuencePointList[1] = point1.transform.forward * 10f;
                secuencePointList[2] = secuencePointList[1] + (point1.transform.up * 10f);
                secuencePointList[3] = secuencePointList[2] + (point2.transform.forward * 10f);
                secuencePointList[4] = secuencePointList[3] + (point2.transform.up * 10f);
                break;
        }

        Vector3Debugger.UpdatePositionsSecuence("Secuence", secuencePointList);
    }

    private void SetExcercise(int index)
    {
        int maxExcercises = (int)Excersice.THIRD;

        if(index > maxExcercises)
        {
            index = 0;
        }

        excersice = (Excersice)index;

        secuencePointList.Clear();
        switch (excersice)
        {
            case Excersice.FIRST:
                pointsInExcersice = 2;                
                break;

            case Excersice.SECOND:
                pointsInExcersice = 4;
                break;

            case Excersice.THIRD:
                pointsInExcersice = 5;
                break;
        }

        for (int i = 0; i < pointsInExcersice; i++)
        {
            secuencePointList.Add(Vector3.zero);
        }

        point1.transform.rotation = MyQuaternion.Euler(0f, 90f, 0f);
        point2.transform.rotation = MyQuaternion.Euler(0f, 90f, 0f);
    }

    private void DoNextExcercise()
    {
        int lastExIndex = (int)Excersice.THIRD;
        int currentIndex = (int)excersice;
        currentIndex++;

        if(currentIndex > lastExIndex)
        {
            currentIndex = 0;
        }

        SetExcercise(currentIndex);
    }
}
