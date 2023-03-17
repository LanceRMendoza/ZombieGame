using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    //void Start()
    //{
//        Destroy(gameObject, 10f);
    //}

    // Update is called once per frame

    public float life = 3;

    // public GameObject hitBlood;

    void Awake()
    {
        Destroy(gameObject, life);
    }
    
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.GetComponent<EnemyBehavior>() != null)
        {
            HitTarget();

        }

    }

    void HitTarget()
    {
        // GameObject effectIns = (GameObject)Instantiate(hitBlood, transform.position, transform.rotation);
        // Destroy (effectIns, 2);
        Destroy(gameObject);
    }
}
