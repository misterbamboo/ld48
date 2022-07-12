using Assets;
using Assets.Map;
using Assets.Map.Domain;
using UnityEngine;

public class SideForces : MonoBehaviour
{
    [SerializeField] private float forceResistance = 2;

    private Transform subTransform;
    private Rigidbody2D subRb2D;
    private Rigidbody subRb;

    void Start()
    {
        subTransform = Submarine.GameObject.transform;
        subRb2D = subTransform.GetComponent<Rigidbody2D>();
        subRb = subTransform.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (subTransform.position.x < 0)
        {
            var invertForce = -subTransform.position.x * forceResistance;
            var forceVector = new Vector2(invertForce, 0);
            AddForce(forceVector);
        }
        else if (subTransform.position.x > MapScript.Instance.MapSizeBoundries())
        {
            var invertForce = -(subTransform.position.x - MapScript.Instance.MapSizeBoundries()) * forceResistance;
            var forceVector = new Vector2(invertForce, 0);
            AddForce(forceVector);
        }
    }

    private void AddForce(Vector2 forceVector)
    {
        if (subRb != null)
            subRb.AddForce(forceVector);

        if (subRb2D != null)
            subRb2D.AddForce(forceVector);
    }
}
