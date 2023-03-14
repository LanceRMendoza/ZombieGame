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
    
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.GetComponent<EnemyBehavior>() != null)
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
