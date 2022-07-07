using Assets;
using Assets.Map;
using Assets.Map.Domain;
using UnityEngine;

public class SideForces : MonoBehaviour
{
    [SerializeField] private float forceResistance = 2;

    private Transform subTransform;
    private Rigidbody2D subRb;

    void Start()
    {
        subTransform = Submarine.GameObject.transform;
        subRb = subTransform.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (subTransform.position.x < 0)
        {
            var invertForce = -subTransform.position.x * forceResistance;
            subRb.AddForce(new Vector2(invertForce, 0));
        }
        else if (subTransform.position.x > MapScript.Instance.MapSizeBoundries())
        {
            var invertForce = -(subTransform.position.x - MapScript.Instance.MapSizeBoundries()) * forceResistance;
            subRb.AddForce(new Vector2(invertForce, 0));
        }
    }
}
