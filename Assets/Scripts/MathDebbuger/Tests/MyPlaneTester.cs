using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPlaneTester : MonoBehaviour
{
    private enum EXCERSICE
    {
        HOUSE,
        FRUSTRUM,
        VERTEX
    }
    [SerializeField] private EXCERSICE excersice;

    [SerializeField] private GameObject cube;
    private MeshRenderer mr;
    private MeshFilter mf;
    private Vector3[] vertices;
    private List<GameObject> VerticesObj = new List<GameObject>();

    [SerializeField] private List<GameObject> walls = new List<GameObject>();
    private List<MeshRenderer> mrs = new List<MeshRenderer>();
    private List<Plane> planes = new List<Plane>();

    private Camera cam;
    private Plane[] frustrumPlanes;

    void Start()
    {
        mr = cube.GetComponent<MeshRenderer>();
        mf = cube.GetComponent<MeshFilter>();
        vertices = mf.mesh.vertices;

        for (int i = 0; i < vertices.Length; i++)
        {
            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            go.transform.position = vertices[i] + cube.transform.position;
            go.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            go.transform.SetParent(cube.transform);
            VerticesObj.Add(go);
        }

        for (int i = 0; i < walls.Count; i++)
        {
            planes.Add(new Plane(walls[i].transform.up, walls[i].transform.position));
            mrs.Add(walls[i].GetComponent<MeshRenderer>());
        }

        cam = Camera.main;
        frustrumPlanes = GeometryUtility.CalculateFrustumPlanes(cam);
    }


    void Update()
    {
        switch (excersice)
        {
            case EXCERSICE.HOUSE:

                CheckPlanes(planes, cube.transform.position, cube.transform.localScale.x / 2);

                break;

            case EXCERSICE.FRUSTRUM:

                frustrumPlanes = GeometryUtility.CalculateFrustumPlanes(cam);

                CheckFustrum(frustrumPlanes, cube.transform.position, cube.transform.localScale.x / 2);

                break;

            case EXCERSICE.VERTEX:

                for (int i = 0; i < VerticesObj.Count; i++)
                {
                    VerticesObj[i].transform.position = vertices[i] + cube.transform.position;
                }
                for (int i = 0; i < vertices.Length; i++)
                {
                    CheckPlanes(planes, vertices[i] + cube.transform.position);
                }

                break;
        }
    }

    void CheckPlanes(List<Plane> planesList, Vector3 point)
    {
        for (int i = 0; i < planesList.Count; i++)
        {
            if (planesList[i].GetSide(point))
            {
                mrs[i].material.color = Color.green;
            }
            else
            {
                mrs[i].material.color = Color.red;
            }
        }
    }

    void CheckPlanes(List<Plane> planesList, Vector3 point, float distanceCheck)
    {
        for (int i = 0; i < planesList.Count; i++)
        {
            if (planesList[i].GetSide(cube.transform.position))
            {
                if (planesList[i].GetDistanceToPoint(point) >= distanceCheck)
                {
                    mrs[i].material.color = Color.green;
                }
                else
                {
                    mrs[i].material.color = Color.red;
                }
            }
        }
    }

    void CheckFustrum(Plane[] planesArray, Vector3 point, float distanceCheck)
    {
        for (int i = 0; i < planesArray.Length; i++)
        {
            if (planesArray[i].GetSide(cube.transform.position))
            {
                if (planesArray[i].GetDistanceToPoint(point) >= distanceCheck)
                {
                    mr.enabled = true;
                }
                else
                {
                    mr.enabled = false;
                }
            }
        }
    }
}
