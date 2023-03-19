using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZombieScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Target;
    public float speed = 1.5f;

    public GameObject hitBlood;

    [SerializeField] MeshRenderer renderer;
    [SerializeField] Collider collider;
    [SerializeField] Rigidbody rb;
    [SerializeField] Image timerFill;
    [SerializeField] Canvas uiCanvas;
    
    [SerializeField] ParticleSystem[] explosion;

    Transform playerTransform;
    float activationDistance = 5;

    const int MAX_TIMER = 250; // in frames
    bool toExplode = false;
    private bool isDead = false;

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
        transform.LookAt(Target.gameObject.transform);
        transform.position += transform.forward * Time.deltaTime * speed;

        if (isDead) return;
    }
    
    // Bullet destroys zombie
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet"){
            OnHit();
            Explode();
            toExplode = true;
            uiCanvas.enabled = true;
        }
    }

    void OnHit()
    {
        GameObject effectIns = (GameObject)Instantiate(hitBlood, transform.position, transform.rotation);
        Destroy (effectIns, 2);

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
    }

}
