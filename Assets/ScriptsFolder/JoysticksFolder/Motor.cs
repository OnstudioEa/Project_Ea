using UnityEngine;
using System.Collections;

public class Motor : MonoBehaviour
{
    public enum State
    {
        idle, move
    }
    public State state;

    public float moveSpeed;
    public VirtualJoysticks moveJoystick;

    private Rigidbody controller;
    private Transform camTransform;
    //-----
    private Transform thisTransform;
    Vector3 rotatedDir;

    public PlayerControl playerControl;

    private void Awake()
    {
        state = State.idle;
        StartCoroutine(FSM());
        camTransform = Camera.main.transform;
        thisTransform = GetComponent<Transform>();
        controller = this.gameObject.GetComponent<Rigidbody>();
    }
    IEnumerator FSM()
    {
        while (true)
        {
            yield return StartCoroutine(state.ToString());
        }
    }
    IEnumerator idle()
    {
        if (state == State.idle)
            yield return new WaitForSeconds(0.1f);
    }
    IEnumerator move()
    {
        if (state == State.move)
        {
            if (playerControl.ani.GetBool("AttackMove"))
            {
                CamRotote();
                FaceMovementDirection();
            }
        }
        yield return new WaitForEndOfFrame();
    }
    private void CamRotote()
    {
        rotatedDir = camTransform.TransformDirection(new Vector3(moveJoystick.joystickImg.rectTransform.anchoredPosition.x, 0, moveJoystick.joystickImg.rectTransform.anchoredPosition.y));
        rotatedDir.y = 0;
        rotatedDir.Normalize();
        
        controller.AddForce(rotatedDir * moveSpeed);
        
    }
    private void FaceMovementDirection()
    {
        Vector3 horizontalVelocity = controller.velocity;
        horizontalVelocity = rotatedDir;

        if (horizontalVelocity.sqrMagnitude > 0.1f)
        {
            thisTransform.forward = horizontalVelocity.normalized;
        }
    }
}