using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public bool Vertical, Horizontal;
    public float MoveSpeed = 1f;
    public int MoveDistance;

    Vector3 originalPos;

    void Start()
    {
        originalPos = transform.position;
    }

    private void FixedUpdate() {
        if (Vertical)
        {
            Horizontal = false;
            float newY = Mathf.Sin(Time.time * MoveSpeed) * MoveDistance + originalPos.y;
            transform.position = new Vector3(originalPos.x, newY, originalPos.z);
        }

        if (Horizontal)
        {
            Vertical = false;
            float newX = Mathf.Sin(Time.time * MoveSpeed) * MoveDistance + originalPos.x;
            transform.position = new Vector3(newX, originalPos.y, originalPos.z);
        }
    }

}
