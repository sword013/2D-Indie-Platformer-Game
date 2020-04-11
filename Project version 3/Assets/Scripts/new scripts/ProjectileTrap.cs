using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTrap : MonoBehaviour
{
    public GameObject TrapSpawn, TrapPrefab;
    public int ReloadDelay;

    void Start()
    {
        InvokeRepeating("SpawnTrap", ReloadDelay, ReloadDelay);
    }

    void SpawnTrap()
    {
        // TODO! - change all Instantiate to a reuseable poolable logic for performance optimization, Instantiate and destorying is usually bad
        Instantiate(TrapPrefab, TrapSpawn.transform.position, TrapSpawn.transform.rotation);
    }
}
