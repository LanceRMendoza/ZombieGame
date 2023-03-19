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

    public float life = 1;
    public AudioSource bulletSound;
    public AudioSource splatterSound;


    // public GameObject hitBlood;

    void Awake()
    {
        Destroy(gameObject, life);
    }
    
    void Update()
    {
        playBulletSound();
    }

    //void HitTarget()
    //{
        // GameObject effectIns = (GameObject)Instantiate(hitBlood, transform.position, transform.rotation);
        // Destroy (effectIns, 2);
        //Destroy(gameObject);
    //}

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Zombie")
            splatterSound.Play();
            Destroy(gameObject);
            Debug.Log("working");
    }
    public void playBulletSound(){
        if (Input.GetKeyDown(KeyCode.Mouse0)){
            bulletSound.Play();
            Debug.Log("bullet");
        }
    }
}
