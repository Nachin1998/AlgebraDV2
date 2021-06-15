using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomMath;

public class MyPlaneAnswers : MonoBehaviour
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
    private List<GameObject> verticesObj = new List<GameObject>();

    [SerializeField] private List<GameObject> walls = new List<GameObject>();
    private List<MeshRenderer> mrs = new List<MeshRenderer>();
    private List<MyPlane> planes = new List<MyPlane>();
    private Plane[] frustrumPlanes;
    //private MyPlane[] frustrumPlanes = new MyPlane[6];
    private Plane[] auxFrustrumPlanes;

    private List<Collider> occludeeColliderList = new List<Collider>();
    private List<MeshRenderer> occludeeMeshRendererList = new List<MeshRenderer>();

    private Camera cam;

    void Start()
    {
        cam = Camera.main;

        InitVertices();
        InitPlanes();
        InitOcludees();
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
                
                for (int i = 0; i < occludeeColliderList.Count; i++)
                {
                    if (GeometryUtility.TestPlanesAABB(frustrumPlanes, occludeeColliderList[i].bounds))
                    {
                        occludeeMeshRendererList[i].enabled = true;
                    }
                    else
                    {
                        occludeeMeshRendererList[i].enabled = false;
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
            verticesObj.Add(go);
        }
    }

    void InitOcludees()
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Occludee");
        for (int i = 0; i < gos.Length; i++)
        {
            occludeeColliderList.Add(gos[i].GetComponent<Collider>());
            occludeeMeshRendererList.Add(gos[i].GetComponent<MeshRenderer>());
        }
    }

    void InitPlanes()
    {
        frustrumPlanes = GeometryUtility.CalculateFrustumPlanes(cam);

        for (int i = 0; i < walls.Count; i++)
        {
            //planes.Add(new Plane(walls[i].transform.up, walls[i].transform.position));
            planes.Add(new MyPlane(new Vec3(walls[i].transform.up), new Vec3(walls[i].transform.position)));
            mrs.Add(walls[i].GetComponent<MeshRenderer>());
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

    bool IsBoxInside(MyPlane plane)
    {
        for (int i = 0; i < vertices.Length; i++)
        {
            if (!plane.GetSide(new Vec3(vertices[i] + cube.transform.position)))
            {
                return false;
            }
        }
        return true;
    }

    bool IsBoxOutside(MyPlane plane)
    {
        for (int i = 0; i < vertices.Length; i++)
        {
            if (plane.GetSide(new Vec3(vertices[i] + cube.transform.position)))
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
