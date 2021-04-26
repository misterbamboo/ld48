using System.Collections.Generic;
using UnityEngine;

public class RedBuoyAnimations : MonoBehaviour
{
    [SerializeField] private float maxAngle = 15;

    private float totalTime;

    private float targetAngle;

    private Quaternion previousRotation;
    private Quaternion newRotation;

    private float duration = 2;

    private void Start()
    {
        ChangeWantedAngle();
    }

    void Update()
    {
        totalTime += Time.deltaTime;
        if (totalTime > duration)
        {
            totalTime -= duration;
            ChangeWantedAngle();
        }

        LerpAngle();
    }

    private void ChangeWantedAngle()
    {
        var newAngle = Random.Range(0, maxAngle);
        targetAngle = targetAngle > 0 ? -newAngle : newAngle;

        previousRotation = transform.rotation;

        var euler = previousRotation.eulerAngles;
        euler.z = targetAngle;
        newRotation = Quaternion.Euler(euler);
    }

    private void LerpAngle()
    {
        var progression = totalTime / duration;
        progression = EaseInOut(progression);
        transform.rotation = Quaternion.Lerp(previousRotation, newRotation, progression);
    }

    // https://www.febucci.com/2018/08/easing-functions/
    public static float EaseIn(float t)
    {
        return t * t;
    }

    public static float EaseOut(float t)
    {
        return Flip(Mathf.Pow(Flip(t), 2));
    }

    public static float EaseInOut(float t)
    {
        return Mathf.Lerp(EaseIn(t), EaseOut(t), t);
    }

    static float Flip(float x)
    {
        return 1 - x;
    }
}
