using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraControl : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Transform phantomCamera;
    [SerializeField] Transform anchor;
    [SerializeField] LayerMask collisions;

    private float defaultCameraDistance;
    private float defaultCameraHeight;
    public Vector3 chasePoint {private set; get;}
    private float speed = 2.25f;

    // Start is called before the first frame update
    void Start()
    {
        defaultCameraDistance = Mathf.Abs(transform.position.z);
        defaultCameraHeight = transform.localPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        chasePoint = transform.position;

        // Collision Checking
        RaycastHit hit;
        if (Physics.Raycast(anchor.position, transform.position - anchor.position, out hit, defaultCameraDistance, collisions, QueryTriggerInteraction.Ignore)){
            Debug.DrawLine(anchor.position, hit.point, Color.red);
            chasePoint = hit.point;
        }

        transform.position = anchor.position + ForceOnUnitCircle(defaultCameraDistance);
    }

    Vector3 ForceOnUnitCircle(float radius){
        // Compress all the relevant vectors into Vector2s
        Vector2 distanceVector = new Vector2(transform.localPosition.x, transform.localPosition.z);
        Vector2 phantomVector = new Vector2(phantomCamera.position.x, phantomCamera.position.z);
        Vector2 anchorVector = new Vector2(anchor.position.x, anchor.position.z);
        Vector2 targetVector = phantomVector - anchorVector;

        // Construct the resulting vector that is both closer to the phantom camera and the correct distance from the player.
        distanceVector.Normalize();
        distanceVector = Vector2.MoveTowards(distanceVector, targetVector, speed * Time.deltaTime);
        distanceVector.Normalize();
        distanceVector *= radius;


        Vector3 result = new Vector3(distanceVector.x, defaultCameraHeight, distanceVector.y);
        return result;

    }
}
