using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Look_At : MonoBehaviour
{
    [SerializeField] Transform lookAtPoint;
    [SerializeField] CameraControl cameraTarget;
    // Start is called before the first frame update

    private float lerpPercent = 0.115f;

    // Update is called once per frame
    void Update(){
        
        // Lerp the camera to the camera target orbiting the player OR
        // the point where there is an obstacle in the way between the player and the offset.
        transform.position = Vector3.Lerp(transform.position, cameraTarget.chasePoint, lerpPercent * Time.deltaTime * 60);
        transform.LookAt(lookAtPoint);
    }
}
