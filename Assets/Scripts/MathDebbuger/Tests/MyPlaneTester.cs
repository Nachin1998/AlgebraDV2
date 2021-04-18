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

                for (int i = 0; i < planes.Count; i++)
                {
                    CheckPlanes(planes[i], i, cube.transform.position, cube.transform.localScale.x / 2);
                }
                break;

            case EXCERSICE.FRUSTRUM:

                frustrumPlanes = GeometryUtility.CalculateFrustumPlanes(cam);

                for (int i = 0; i < frustrumPlanes.Length; i++)
                {
                    CheckPlanes(frustrumPlanes[i], i, cube.transform.position, cube.transform.localScale.x / 2);
                }
                break;

            case EXCERSICE.VERTEX:

                for (int i = 0; i < VerticesObj.Count; i++)
                {
                    VerticesObj[i].transform.position = vertices[i] + cube.transform.position;
                }

                for (int i = 0; i < planes.Count; i++)
                {
                    for (int j = 0; j < vertices.Length; j++)
                    {
                         CheckPlanes(planes[i], i, vertices[j] + cube.transform.position);
                    }
                }
                break;
        }
    }

    void CheckPlanes(Plane plane, int matIndex, Vector3 point)
    {
        if (plane.GetSide(point))
        {
            mrs[matIndex].material.color = Color.green;
            return;
        }
        mrs[matIndex].material.color = Color.red;
    }

    void CheckPlanes(Plane plane, int matIndex, Vector3 point, float distanceCheck)
    {
        if (plane.GetSide(cube.transform.position))
        {
            if (plane.GetDistanceToPoint(point) >= distanceCheck)
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
