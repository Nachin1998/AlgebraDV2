using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomMath;

public class MyQuaternionAnswers : MonoBehaviour
{
    public GameObject test1;
    public GameObject test2;

    public Quaternion testQuaternionA;
    public Quaternion testQuaternionB;
    public Vector3 testVector;
    public float testAngle;

    Quaternion testQuat1;
    Quaternion testQuat2;

    MyQuaternion myTestQuaternionA;
    MyQuaternion myTestQuaternionB;

    void Start()
    {
        testQuat1 = test1.transform.rotation;
        testQuat2 = test2.transform.rotation;

        myTestQuaternionA = new MyQuaternion(testQuat1);
        myTestQuaternionB = new MyQuaternion(testQuat2);
    }


    void Update()
    {
            myTestQuaternionA = new MyQuaternion(testQuat1);
            myTestQuaternionB = new MyQuaternion(testQuat2);

        if (Input.GetKeyDown(KeyCode.Space))
        {


        }
    }
}
