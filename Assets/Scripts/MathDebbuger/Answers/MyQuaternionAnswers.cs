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

        Debug.Log("Real Quaternion: " + Quaternion.Angle(testQuat1, testQuat2));
        Debug.Log("My Quat: " + MyQuaternion.Angle(myTestQuaternionA, myTestQuaternionB));
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Real Quaternion: " + testQuat1.eulerAngles);// Quaternion.Angle(test1.transform.rotation, test2.transform.rotation));
            Debug.Log("Test Quaternion: " + myTestQuaternionA.eulerAngles);// Quaternion.Angle(test1.transform.rotation, test2.transform.rotation));
        }
    }
}
