using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class FollowHookBehavior : MonoBehaviour, IHookable
{
    [SerializeField] private string key;
    public string Key => key;
    private bool isHooked;
    private Hook hook;

    public void Collect()
    {
        Destroy(gameObject);
    }

    public void Hook(Hook hook)
    {
        isHooked = true;
        this.hook = hook;

        transform.parent = hook.transform;
        transform.localPosition = Vector3.zero;
    }
}
