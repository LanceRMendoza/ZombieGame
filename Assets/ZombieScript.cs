using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieScript : MonoBehaviour
{
    public AudioSource audioPlayer3;
    // Start is called before the first frame update
    public GameObject Target;
    public float speed = 1.5f;

    public GameObject hitBlood;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Target.gameObject.transform);
        transform.position += transform.forward * Time.deltaTime * speed;
    }
    
    // Bullet destroys zombie
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")

            OnHit();
            //audioPlayer3.Play();
            Destroy(gameObject);
    }

    void OnHit()
    {
        GameObject effectIns = (GameObject)Instantiate(hitBlood, transform.position, transform.rotation);
        Destroy (effectIns, 2);

    }

}
