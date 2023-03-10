using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinDestroy : MonoBehaviour


{
    public AudioSource audioPlayer3;
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
            audioPlayer3.Play();
            Destroy(gameObject);
    }
}
