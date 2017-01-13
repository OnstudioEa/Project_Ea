using UnityEngine;
using System.Collections;

public class LookCamera : MonoBehaviour
{
    public GameObject enemyTarget;
    //-----
    private GameObject playerTarget;
    private Transform playerTransform;
    //-----
    private Transform _transform;

    public float dist;
    public float height;
    public float dampRotate;

    // Use this for initialization
    void Awake()
    {
        playerTarget = GameObject.Find("Player");
        playerTransform = playerTarget.transform;
        _transform = GetComponent<Transform>();

        dist = 2f;
        height = 0.5f;
    }
    void LateUpdate()
    {
        float curryAngle = Mathf.LerpAngle(_transform.eulerAngles.y, playerTransform.eulerAngles.y, dampRotate * Time.deltaTime);

        Quaternion rot = Quaternion.Euler(5, curryAngle, 0);

        _transform.position = playerTransform.position - (rot * Vector3.forward * dist) + (Vector3.up * height);
        transform.LookAt(enemyTarget.transform);
    }
}

