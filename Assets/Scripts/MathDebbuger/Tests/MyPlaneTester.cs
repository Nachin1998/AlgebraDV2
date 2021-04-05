using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPlaneTester : MonoBehaviour
{
    [SerializeField] private GameObject objectToFind;

    [SerializeField] private List<GameObject> walls = new List<GameObject>();
    [SerializeField] private List<Plane> planes = new List<Plane>();

    private List<MeshRenderer> mrs = new List<MeshRenderer>();

    void Start()
    {
        for (int i = 0; i < walls.Count; i++)
        {
            planes.Add(new Plane(walls[i].transform.up, walls[i].transform.position));
            mrs.Add(walls[i].GetComponent<MeshRenderer>());
        }
    }


    void Update()
    {
        for (int i = 0; i < planes.Count; i++)
        {            
            if (planes[i].GetSide(objectToFind.transform.position))
            {
                if (planes[i].GetDistanceToPoint(objectToFind.transform.position) >= objectToFind.transform.localScale.x / 2)
                {
                    mrs[i].material.color = Color.green;
                }
                else
                {
                    mrs[i].material.color = Color.red;
                }
            }
            else
            {
                mrs[i].material.color = Color.red;
            }
        }
    }
}
