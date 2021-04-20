using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPlaneTester : MonoBehaviour
{
    private enum EXCERSICE
    {
        INSIDE_HOUSE,
        OUTSIDE_HOUSE,
        FRUSTRUM_CULLING
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

    [SerializeField] private LayerMask occludees;
    private List<GameObject> objectsToOcclude = new List<GameObject>();

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
            case EXCERSICE.INSIDE_HOUSE:

                mr.enabled = true;
                for (int i = 0; i < planes.Count; i++)
                {
                    if (IsBoxInside(planes[i]))
                    {
                        mrs[i].material.color = Color.green;
                    }
                    else
                    {
                        mrs[i].material.color = Color.red;
                    }
                }

                break;

            case EXCERSICE.OUTSIDE_HOUSE:

                mr.enabled = true;
                for (int i = 0; i < planes.Count; i++)
                {
                    if (IsBoxOutside(planes[i]))
                    {
                        mrs[i].material.color = Color.green;
                    }
                    else
                    {
                        mrs[i].material.color = Color.red;
                    }
                }

                break;

            case EXCERSICE.FRUSTRUM_CULLING:

                frustrumPlanes = GeometryUtility.CalculateFrustumPlanes(cam);

                if (IsBoxOutside(frustrumPlanes[0]) ||
                    IsBoxOutside(frustrumPlanes[1]) ||
                    IsBoxOutside(frustrumPlanes[2]) ||
                    IsBoxOutside(frustrumPlanes[3]) ||
                    IsBoxOutside(frustrumPlanes[4]) ||
                    IsBoxOutside(frustrumPlanes[5]))
                {
                    mr.enabled = false;
                }
                else
                {
                    mr.enabled = true;
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

    bool IsBoxInside(Plane plane)
    {
        for (int i = 0; i < vertices.Length; i++)
        {
            if (!plane.GetSide(vertices[i] + cube.transform.position))
            {
                return false;
            }
        }
        return true;
    }

    bool IsBoxOutside(Plane plane)
    {
        for (int i = 0; i < vertices.Length; i++)
        {
            if (plane.GetSide(vertices[i] + cube.transform.position))
            {
                return false;
            }
        }
        return true;
    }
}
