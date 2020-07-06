using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{


    public Transform target;

    public float smoothSpeed = 0.125f;
    public Vector3 cameraOffset;

    private void Start()
    {
        gameObject.GetComponent<AudioSource>().volume = (PlayerPrefs.GetFloat("VolumeLevel"));

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + cameraOffset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }

    public void ReloadCamera()
    {
        transform.position = new Vector3(0f, 8f, -8f);
    }
}
