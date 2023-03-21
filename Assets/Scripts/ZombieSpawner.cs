using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject ZombiePrefab;
    public double zombieSpawnInterval = 2;
    private double zombieSpawnCountdown = 2;
    public GameObject playerFollow;

    // Update is called once per frame
    void Update()
    {
        if (zombieSpawnCountdown <= 0) {
            zombieSpawnCountdown = zombieSpawnInterval;
            GameObject zombie = Instantiate(ZombiePrefab, transform.position, Quaternion.identity);
            ZombieScript zombieScript = zombie.GetComponent<ZombieScript>();
            zombieScript.Target = playerFollow;
        }
        else {
            zombieSpawnCountdown -= Time.deltaTime;
        }
    }
}
