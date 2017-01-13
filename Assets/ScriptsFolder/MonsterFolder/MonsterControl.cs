using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MonsterControl : MonoBehaviour
{
    public State        state;

    Transform           trans;
    //----------------타겟 체크
    public GameObject   targetPlayer;
    Transform           target;
    float                closestDistSqr;
    float                dist;
    public float         distPos;
    //----------------스피드 관련
    public float         speed;
    private Vector3     lookDir;
    //----------------애니메이션 및 카메라
    Animator            ani;
    public ShakeCamera  shakeCam;
    //----------------공격모션 관련
    public CapsuleCollider monsterColl;
    public int attackDist;
    public int attackDist_1;

    int attackNumber = 0;
    bool attackbool;
    bool jumpbool;
    //----------------데미지 전달
    public GameObject   taggedAction;
    List<Action> action = new List<Action>();

    public PlayerDataLoader playerdataLoader;
    PlayerControl playerControl;
    Animator ani_Player;

    void Awake () {
        state = State.idle;
        StartCoroutine(FSM());

        ani = GetComponent<Animator>();
        ani.SetBool("Idle", true);
        targetPlayer = GameObject.Find("Player");
        playerControl = GameObject.Find("Player").GetComponent<PlayerControl>();
        ani_Player = GameObject.Find("Player").GetComponent<Animator>();

        closestDistSqr = Mathf.Infinity;
        trans = gameObject.transform;        

        CheckList();
        TargetCheck();
    }
    void CheckList()
    {
        if (taggedAction.name == "GameManager")
        {
            action.Add(taggedAction.GetComponent<PlayerDataLoader>());
        }
    }
    void TargetCheck()
    {
        if (targetPlayer != null)
        {
            Vector3 obPos = targetPlayer.transform.position;
            dist = (obPos - trans.position).sqrMagnitude;
            if (dist < distPos)
            {
                if (dist < closestDistSqr)
                {
                    target = targetPlayer.transform;
                    state = State.move;
                }
            }
        }
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
        {
            TargetCheck();
            Idle();
        }
        yield return new WaitForSeconds(0.5f);
    }
    IEnumerator attack()
    {
        if (state == State.attack)
        {
            Attack();
        }
        yield return new WaitForSeconds(3.5f);
    }
    IEnumerator move()
    {
        if (state == State.move)
        {
            Move();
        }
        yield return new WaitForEndOfFrame();
    }
    
    void Idle()
    {
        if (target != null)
        {
            float dist = Vector3.Distance(target.position, trans.position);
            if (dist < 1)
            {
                Vector3 vec = target.position - this.trans.position;
                float angle = Vector3.Angle(transform.forward, vec);
                if (angle > 0 && angle <= 50)
                {
                //    Debug.Log("전방공격");
                    state = State.attack;
                }
                else
                {
                    attackbool = true;
                    state = State.attack;
                //   Debug.Log("후방공격");
                }
            }
        }
    }
    void Move()
    {
        if (ani.GetBool("Groggy") == false && ani.GetBool("Died") == false)
        {
            float dist = Vector3.Distance(target.position, trans.position);
            if (dist < 0.8f)
            {
                ani.SetBool("Move", false);
                state = State.idle;
                return;
            }
            else
            {
                if (dist < 2 && dist >= 1.9f)
                {
                    jumpbool = true;
                    state = State.attack;
                    return;
                }
                else
                {
                    ani.SetBool("Move", true);
                    lookDir = (target.position - this.trans.position);
                    lookDir.Normalize();

                    Quaternion from = this.trans.rotation;
                    Quaternion to = Quaternion.LookRotation(lookDir);

                    trans.rotation = Quaternion.Lerp(from, to, Time.fixedDeltaTime * 1.2f);
                    trans.Translate(Vector3.forward * Time.deltaTime * speed);
                }
            }
        }
    }
    void Attack()
    {
        ani.SetBool("Idle", false);
        ani.SetBool("Move", false);

        if (attackbool == true)
        {
            ani.SetBool("BackAttack", true);
            attackDist = 1;
            attackDist_1 = 50;
            return;
        }
        else
        {
            if (jumpbool == true)
            {
                //Debug.Log("점프공격 실시");
                attackDist = 1;
                attackDist_1 = 50;
                ani.SetBool("Attack2", true);
                return;
            }
            else
            {
                attackNumber = Random.Range(1, 3);
                if (attackNumber == 1)
                {
                    attackDist = 1;
                    attackDist_1 = 50;
                    ani.SetBool("Attack1", true);
                    return;
                }
                if (attackNumber == 2)
                {
                    attackDist = 40;
                    attackDist_1 = 30;
                    ani.SetBool("Attack3", true);
                    return;
                }
            }
        }
    }
    public void ShakeCam()
    {
        if (ani.GetBool("Move") == true)
        {
            shakeCam.ShakeCam(0.01f, 0.2f);
            return;
        }else
        {
            if (ani.GetBool("Attack3") == true)
            {
                shakeCam.ShakeCam(0.03f, 3.4f);
                return;
            }
            else {
                if (ani.GetBool("GroggyAttack") == true)
                {

                    shakeCam.ShakeCam(0.01f, 0.1f);
                }
                else
                    shakeCam.ShakeCam(0.02f, 0.2f);
        }
      }  
    }
    public void AttackDamageTo()
    {
        float dist = Vector3.Distance(target.position, trans.position);
        if (dist < attackDist)
        {
            Vector3 vec = target.position - this.trans.position;
            vec.Normalize();
            float angle = Vector3.Angle(transform.forward, vec);
            
            if (angle > 0 && angle <= attackDist_1)
            {
                action[0].MonsterDamage();
              //  Debug.Log("Forword");
                return;
            }
           /* else if (130 <= angle && angle <= 180)
            {
              //  Debug.Log("Back");
                return;
            }            
            else
            {
                // 두벡의 외적 벡터를 구한다 
                Vector3 lrVec = Vector3.Cross(vec, Vector3.up);
                lrVec.Normalize();

                float rightAngle = Vector3.Angle(transform.forward, lrVec);
                if(rightAngle <= 90)
                {
                 //   Debug.Log("Right");
                }
                else
                {
                 //   Debug.Log("Left");
                }
            }*/
        }
    }
    public void AttackStart()
    {
        monsterColl.enabled = false;
    }
    public void AttackEnd()
    {
        if (ani.GetBool("PartsD") == true)
        {
            ani.SetBool("PartsD", false);
        }
        monsterColl.enabled = true;
        ani.SetBool("Attack1", false);
        ani.SetBool("Attack2", false);
        ani.SetBool("Attack3", false);
        ani.SetBool("BackAttack", false);
        ani.SetBool("Idle", true);
        
        attackbool = false;
        jumpbool = false;
        state = State.idle;
    }
    /// <summary>
    /// 그로기 종료
    /// </summary>
    public void GroggyAnimationCheck()
    {
        if (playerdataLoader.groggyAttack_bool == false)
            ani.SetBool("Groggy", false);
    }
    /// <summary>
    /// 겨루기시 방향
    /// </summary>
    public void AttackLook()
    {
        transform.LookAt(target);
    }
    /// <summary>
    /// 겨루기공격시 애니메이션 이벤트
    /// </summary>
    public void MonsterGroggyAttackEvent()
    {
        playerdataLoader.buttonActionPanel.gameObject.SetActive(true);
        playerdataLoader.buttonPanelActive_1.gameObject.SetActive(false);
        playerdataLoader.buttonPanelActive_2.gameObject.SetActive(false);
        playerdataLoader.buttonActionGayge = 100;
        ani_Player.SetBool("Defend_1", true);
    }
}