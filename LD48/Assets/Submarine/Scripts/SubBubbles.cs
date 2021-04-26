using UnityEngine;

public class SubBubbles : MonoBehaviour
{
    private ParticleSystem leftbubbles;
    private ParticleSystem rightbubbles;
    private bool shouldEmit = false;
    private Sides side = Sides.Left;

    private void Awake()
    {
        leftbubbles = transform.Find("Left Particle System").GetComponent<ParticleSystem>();
        rightbubbles = transform.Find("Right Particle System").GetComponent<ParticleSystem>();
    }

    void Update()
    {
        side = GetSide(Input.GetAxisRaw("Horizontal"));
        EmitBubblesWhenPressingKeys();
    }

    private Sides GetSide(float input)
    {
        if (input == 1) return Sides.Left;
        if (input == -1) return Sides.Right;

        return side;
    }

    private void EmitBubblesWhenPressingKeys()
    {
        if (!SubIsMoving())
        {
            TurnOffBubbles();
        }
        else 
        {
            ToggleBubbles();
        }

    }

    private void ToggleBubbles()
    {
        if (side == Sides.Right)
        {
            HandleRightSideBubbles();
        }
        else
        {
            HandleLeftSideBubbles();
        }
    }

    private void TurnOffBubbles()
    {
        TurnOffBubbles(rightbubbles);
        TurnOffBubbles(leftbubbles);
    }

    private void HandleLeftSideBubbles()
    {
        TurnOffBubbles(rightbubbles);
        TurnOnBubbles(leftbubbles);
    }

    private static bool SubIsMoving()
    {
        return Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0;
    }

    private void HandleRightSideBubbles()
    {
        TurnOffBubbles(leftbubbles);
        TurnOnBubbles(rightbubbles);    
    }

    private void TurnOnBubbles(ParticleSystem bubbles)
    {
        if (!bubbles.isPlaying)
        {
            bubbles.Play();
        }
    }

    private void TurnOffBubbles(ParticleSystem bubbles)
    {
        if (bubbles.isPlaying)
        {
            bubbles.Stop();
        }
    }
}

public enum Sides
{
    Right,
    Left
}
