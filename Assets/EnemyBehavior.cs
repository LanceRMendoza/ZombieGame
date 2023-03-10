using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine. UI;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] MeshRenderer renderer;
    [SerializeField] SphereCollider collider;
    [SerializeField] Rigidbody rb;
    [SerializeField] Image timerFill;
    [SerializeField] Canvas uiCanvas;
    
    [SerializeField] ParticleSystem[] explosion;

    Transform playerTransform;
    float activationDistance = 5;

    int countDownTimer = 0;
    const int MAX_TIMER = 250; // in frames
    bool toExplode = false;
    private bool isDead = false;


    // Start is called before the first frame update
    void Start()
    {
        if (playerTransform == null){
            try {
                playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            }
            catch {
                Debug.LogError($"Error: Cannot find any gameObject with the tag Player in the scene.");
                gameObject.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead) return;

        if (!toExplode){
            // Check for the player getting to close
            if (Vector3.Distance(playerTransform.position, transform.position) < activationDistance){
                toExplode = true;
                uiCanvas.enabled = true;
            }
        }
        else {
            if (countDownTimer < MAX_TIMER){
                countDownTimer++;
                // TODO: update UI
                timerFill.fillAmount = countDownTimer / (float)MAX_TIMER;
            }
            else {
                Explode();
            }
        }
    }

    void Explode() {
        if (isDead)
            return;
        
        isDead = true;
        renderer.enabled = false;
        collider.enabled = false;
        uiCanvas.enabled = false;
        rb.useGravity = false;
        for (int i = 0; i < explosion.Length; i++){
            explosion[i].Play();
        }
        // Destroy gameObject after 4 seconds.
        print("Boom!");
        Destroy(gameObject, 4);
    }
}
