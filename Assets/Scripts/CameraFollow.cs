using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class CameraFollow : MonoBehaviour
{

    public Transform target;
    public MainPlayer player;
    public float smoothedSpeed;
    private Vector3 rotation;
    public Vector3 offset;

    void LateUpdate()
    {
        if (player.cameraFinish)
        {
            if (target.rotation.y < 0.05f && target.rotation.y > -0.05f)
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, target.localEulerAngles.y,
                    transform.localEulerAngles.z);
            //transform.localEulerAngles = new Vector3(transform.localEulerAngles.x,target.localEulerAngles.y , transform.localEulerAngles.z);
            else
                this.transform.localEulerAngles = Vector3.Lerp(transform.localEulerAngles,
                    new Vector3(transform.localEulerAngles.x, target.localEulerAngles.y, target.localEulerAngles.z),
                    0.6f);
            //transform.localEulerAngles = Vector3.Lerp(transform.localEulerAngles, target.localEulerAngles, 0.5f);
            //transform.LookAt(target.position + target.forward * 5f);
            transform.position = target.position - target.forward * 3f + Vector3.up * 4f;
        }
    }

}