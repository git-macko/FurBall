using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyCamScript : MonoBehaviour
{
    //Camera Shake
    public float CameraShakeDuration = 0.1f;
    public AnimationCurve CurveShake;

    //Follow Bunny
    public Transform target;
    public Vector3 offset;
    public float damping;
    public bool CanShake;

    private Vector3 velocity = Vector3.zero;

    private void Start() {
        CanShake = false;
  
        QualitySettings.vSyncCount = 0;  // VSync must be disabled
        Application.targetFrameRate = 61;
    }

    void FixedUpdate() 
    {
        Vector3 movePosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, movePosition, ref velocity, damping);
        // Only shake screen if rifle is on and has ammo
        if(Input.GetMouseButton(0) && CanShake)
        {
            StartCoroutine(Shaking());
        }
    }


    IEnumerator Shaking()
    {
        Vector3 startPos = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < CameraShakeDuration)
        {
            elapsedTime += Time.deltaTime;
            float shakeStrength = CurveShake.Evaluate(elapsedTime / CameraShakeDuration);
            transform.position = startPos + Random.insideUnitSphere * shakeStrength;
            yield return null;
        }
        transform.position = startPos;
    }
}
