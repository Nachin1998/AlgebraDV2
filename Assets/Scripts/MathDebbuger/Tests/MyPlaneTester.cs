using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPlaneTester : MonoBehaviour
{
    private enum EXCERSICE
    {
        INSIDE_HOUSE,
        OUTSIDE_HOUSE,
        FRUSTRUM_CULLING_CUBE,
        FRUSTRUM_CULLING_ALL
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

    private List<Collider> objectsToOccludeCol = new List<Collider>();
    private List<MeshRenderer> objectsToOccludeMeshRenderer = new List<MeshRenderer>();

    private Camera cam;
    private Plane[] frustrumPlanes;


    void Start()
    {
        InitVertices();
        InitPlanes();
        InitOcludees();
        

        cam = Camera.main;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            UpdatePlanes();
        }

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

            case EXCERSICE.FRUSTRUM_CULLING_CUBE:

                frustrumPlanes = GeometryUtility.CalculateFrustumPlanes(cam);

                for (int i = 0; i < frustrumPlanes.Length; i++)
                {
                    if (IsBoxOutsideFrustrum())
                    {
                        mr.enabled = false;
                    }
                    else
                    {
                        mr.enabled = true;
                    }
                }

                break;

            case EXCERSICE.FRUSTRUM_CULLING_ALL:

                frustrumPlanes = GeometryUtility.CalculateFrustumPlanes(cam);

                for (int i = 0; i < objectsToOccludeCol.Count; i++)
                {
                    if (GeometryUtility.TestPlanesAABB(frustrumPlanes, objectsToOccludeCol[i].bounds))
                    {
                        objectsToOccludeMeshRenderer[i].enabled = true;
                    }
                    else
                    {
                        objectsToOccludeMeshRenderer[i].enabled = false;
                    }
                }

                break;
        }
    }

    void InitVertices()
    {
        mr = cube.GetComponent<MeshRenderer>();
        mf = cube.GetComponent<MeshFilter>();
        vertices = mf.mesh.vertices;

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

    void InitOcludees()
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Occludee");
        for (int i = 0; i < gos.Length; i++)
        {
            objectsToOccludeCol.Add(gos[i].GetComponent<Collider>());
            objectsToOccludeMeshRenderer.Add(gos[i].GetComponent<MeshRenderer>());
        }
    }

    void InitPlanes()
    {
        frustrumPlanes = GeometryUtility.CalculateFrustumPlanes(cam);

        for (int i = 0; i < walls.Count; i++)
        {
            planes.Add(new Plane(walls[i].transform.up, walls[i].transform.position));
            mrs.Add(walls[i].GetComponent<MeshRenderer>());
        }
    }

    void UpdatePlanes()
    {
        for (int i = 0; i < planes.Count; i++)
        {
            planes[i].SetNormalAndPosition(walls[i].transform.up, walls[i].transform.position);
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

    bool IsBoxOutsideFrustrum()
    {
        for (int i = 0; i < frustrumPlanes.Length; i++)
        {
            if (IsBoxOutside(frustrumPlanes[i]))
            {
                return true;
            }
        }
        return false;
    }

    bool IsObjectOutside(Plane plane, GameObject go)
    {
        for (int i = 0; i < vertices.Length; i++)
        {
            if (plane.GetSide(vertices[i] + go.transform.position))
            {
                return false;
            }
        }
        return true;
    }

    bool IsObjectOutsideFrustrum()
    {
        for (int i = 0; i < frustrumPlanes.Length; i++)
        {
            if (IsBoxOutside(frustrumPlanes[i]))
            {
                return true;
            }
        }
        return false;
    }
}
