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

        InitVertices();

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

                mr.enabled = true;
                CheckPlanes(planes, cube.transform.position, cube.transform.localScale.x / 2);

                break;

            case EXCERSICE.FRUSTRUM:

                frustrumPlanes = GeometryUtility.CalculateFrustumPlanes(cam);

                if (CheckFustrum(planes, vertices))
                {
                    mr.enabled = true;
                }
                else
                {
                    mr.enabled = false;
                }

                break;


        }
    }

    void InitVertices()
    {
        for (int i = 0; i < vertices.Length / 3; i++)
        {
            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            go.name = "Vertex " + i;
            go.transform.position = vertices[i] + cube.transform.position;
            go.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            go.transform.SetParent(cube.transform);
            VerticesObj.Add(go);
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

    bool CheckFustrum(List<Plane> planes, Vector3[] vertices)
    {
        mrs[0].material.color = Color.green;
        mrs[1].material.color = Color.red;
        mrs[2].material.color = Color.blue;
        mrs[3].material.color = Color.black;
        mrs[4].material.color = Color.yellow;
        mrs[5].material.color = Color.magenta;

        for (int j = 0; j < vertices.Length; j++)
        {
            if (planes[0].GetSide(vertices[j] + cube.transform.position))
            {
                return false;
            }
        }

        return true;
    }

    bool CheckNewPlanes(List<Plane> planesList, Vector3 point)
    {
        for (int i = 0; i < planesList.Count; i++)
        {
            if (!planesList[i].GetSide(point))
            {
                return false;
            }
        }
        return true;
    }

    bool CheckVertices(Vector3[] vertices, List<Plane> frustrumPlanes)
    {
        for (int i = 0; i < frustrumPlanes.Count; i++)
        {
            for (int j = 0; j < vertices.Length; j++)
            {
                if (frustrumPlanes[i].GetSide(vertices[j]))
                {
                    return false;
                }
            }
        }
        return true;
    }
}
