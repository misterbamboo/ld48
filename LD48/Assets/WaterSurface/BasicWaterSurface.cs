using Assets;
using Assets.MapGeneration;
using Assets.WaterSurfaceManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicWaterSurface : MonoBehaviour
{
    public void Init(Mesh mesh)
    {
        GetComponent<MeshFilter>().sharedMesh = mesh;
    }
}
