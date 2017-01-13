using UnityEngine;
using System.Collections;

public class IntroMonster : MonoBehaviour
{
    public State state;
    public GameObject intro_Ground;
    public float ground_MoveSpeed;

    public GameObject boss_Effect_1;

    public GameManager gameManager;
    public ShakeCamera shakeCam;
    void Awake()
    {
        boss_Effect_1.gameObject.SetActive(false);

        state = State.idle;
        StartCoroutine(FSM());
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
        yield return new WaitForSeconds(0.3f);
    }
    IEnumerator move()
    {
        intro_Ground.transform.Translate(Vector3.forward * ground_MoveSpeed * Time.deltaTime);
        yield return new WaitForEndOfFrame();
    }
    public void MoveStart()
    {
        if (state == State.idle)
            state = State.move;
        else
            state = State.idle;
    }
    public void MotionEnd()
    {
        gameManager.IngameStart();
    }
    public void ShakeStart()
    {
        shakeCam.ShakeCam(0.05f, 0.3f);
    }
    public void ShakeStart1()
    {
        shakeCam.ShakeCam(0.08f, 3f);
    }
    public void Boss_Effect()
    {
        boss_Effect_1.gameObject.SetActive(true);
    }
}
