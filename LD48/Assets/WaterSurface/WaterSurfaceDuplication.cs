using UnityEngine;

public class WaterSurfaceDuplication : MonoBehaviour
{
    public void Init(Mesh mesh)
    {
        GetComponent<MeshFilter>().sharedMesh = mesh;
    }
}
