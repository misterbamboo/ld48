using Assets;
using Assets.Map;
using UnityEngine;

public class SideForcesParticles : MonoBehaviour
{
    [SerializeField] private float mapOffset = 30;
    [SerializeField] private float height = 26;

    [SerializeField] private ParticleSystem particleLeft;
    [SerializeField] private ParticleSystem particleRight;

    private void FixedUpdate()
    {
        var width = MapScript.Instance.MapSizeBoundries();
        UpdateOffset(particleLeft.transform, -mapOffset);
        UpdateOffset(particleRight.transform, width + mapOffset);

        UpdateFollow(particleLeft.transform);
        UpdateFollow(particleRight.transform);
    }

    private void UpdateOffset(Transform psTransform, float offset)
    {
        var pos = psTransform.position;
        pos.x = offset;
        psTransform.position = pos;
    }

    private void UpdateFollow(Transform transform)
    {
        if (Submarine.Instance.Deepness > height / 2)
        {
            transform.position = new Vector3(transform.position.x, -Submarine.Instance.SensitiveDeepness, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, -height / 2, transform.position.z);
        }
    }
}
