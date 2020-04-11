using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public bool Activated = false;
    public Sprite greenFlag, redFlag;
    Vector3 SpawnOffset;
    SpriteRenderer spriteRenderer;

    private void Start() {
        SpawnOffset = transform.position;
        SpawnOffset.y += 0.5f;

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag.Equals("Player") && other.GetComponent<Player>().isAlive)
        {
            SetActivated();
        }
    }

    void SetActivated()
    {
        if (!Activated)
        {
            Activated = true;
            LevelManager.instance.CurrentSpawnPoint = SpawnOffset;
            spriteRenderer.sprite = greenFlag;
        }
    }
}
