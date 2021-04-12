using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPlaneTester : MonoBehaviour
{
    private enum EXCERSICE
    {
        HOUSE,
        FRUSTRUM
    }
    [SerializeField] private EXCERSICE excersice;
    [SerializeField] private GameObject cube;

    [SerializeField] private List<GameObject> walls = new List<GameObject>();
    [SerializeField] private List<Plane> planes = new List<Plane>();

    private List<MeshRenderer> mrs = new List<MeshRenderer>();
    Camera cam;
    [SerializeField] private Plane[] extraPlanes;

    void Start()
    {
        for (int i = 0; i < walls.Count; i++)
        {
            planes.Add(new Plane(walls[i].transform.up, walls[i].transform.position));
            mrs.Add(walls[i].GetComponent<MeshRenderer>());
        }

        cam = Camera.main;
        extraPlanes = GeometryUtility.CalculateFrustumPlanes(cam);
    }


    void Update()
    {
        switch (excersice)
        {
            case EXCERSICE.HOUSE:

                for (int i = 0; i < planes.Count; i++)
                {
                    CheckPlanes(planes[i], i);
                }
                break;

            case EXCERSICE.FRUSTRUM:

                extraPlanes = GeometryUtility.CalculateFrustumPlanes(cam);

                for (int i = 0; i < extraPlanes.Length; i++)
                {
                    CheckPlanes(extraPlanes[i], i);
                }
                break;

            default:
                break;
        }
    }

    void CheckPlanes(Plane plane, int matIndex)
    {
        if (plane.GetSide(cube.transform.position))
        {
            if (plane.GetDistanceToPoint(cube.transform.position) >= cube.transform.localScale.x / 2)
            {
                mrs[matIndex].material.color = Color.green;
            }
            else
            {
                mrs[matIndex].material.color = Color.red;
            }
        }
    }
}
