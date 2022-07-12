using Assets.Map.Application;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPagingTests : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] int size = 5;

    private MapPaging mapPaging;
    private Mesh mesh;

    void Start()
    {
        mapPaging = new MapPaging(size);
        mapPaging.PageChanged += MapPaging_PageChanged;
        mesh = new Mesh();
    }

    void Update()
    {
        mapPaging.UpdatePlayerPosition(target.position);
    }

    private void MapPaging_PageChanged(MapPaging.MapPageInfo obj)
    {
        var from = obj.FromPoint();
        var to = obj.ToPoint();
        mesh.vertices = new Vector3[]
        {
            from, // 0, 0
            new Vector2(from.x, to.y), // 0, 1
            new Vector2(to.x, from.y), // 1, 0
            to, // 1, 1
        };
        mesh.triangles = new int[]
        {
            0, 1, 2,
            2, 1, 3,
        };
        mesh.RecalculateNormals();

        GetComponent<MeshFilter>().sharedMesh = mesh;
    }
}
