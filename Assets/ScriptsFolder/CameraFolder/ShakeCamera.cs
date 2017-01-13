
using UnityEngine;
using System.Collections;

public class ShakeCamera : MonoBehaviour {
    public Vector2 velocity;
    public float smoothTimey;
    public float smoothTimex;

    public GameObject player;

    public float shakeTimer;
    public float shakeAmount;

    public bool testCamPos;

    void FixedUpdate()
    {
        float posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref velocity.x, smoothTimex);
        float posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref velocity.y, smoothTimey);

        transform.position = new Vector3(posX/* - 0.07097737f*/, posY /*+ 6.333f*/, transform.position.z /*- 0.0003753711f*/);

        if (testCamPos == true)
        {
            testCamPos = false;
        }
    }

    void Update()
    {

        if (shakeTimer >= 0) // 0보다 이상일경우 카메라를 흔든다.
        {
            Vector2 ShakePos = Random.insideUnitCircle * shakeAmount;

            transform.position = new Vector3(transform.position.x + ShakePos.x, transform.position.y + ShakePos.y, transform.position.z);

            shakeTimer -= Time.deltaTime;
        }
    }
    public void ShakeCam(float shakepwr, float shakedur)
    {
        shakeAmount = shakepwr;
        shakeTimer = shakedur;
    }

}
