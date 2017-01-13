using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerControl : MonoBehaviour
{
    public State state;
    public Animator ani;

    public bool attackCheck;
    public int  attackCount;
    public float attackTime;
    public bool skillCheck;

    GameObject weaponeObMax;
    GameObject weaponeObMin;
    GameObject targetMonster;
    GameObject weaponeSlash;
        
    float closestDistSqr;
    float dist;
    float dist_1;
    float distPos;

    public int hitCount;

    public BoxCollider attack_Coll;
    public BoxCollider skill_1_Coll;
    public BoxCollider skill_2_Coll;
    public BoxCollider defend_Coll;

    public GameObject A;
    public GameObject B;
    public GameObject C;
    public GameObject D;
    public GameObject hit_1;
    public GameObject hit_2;
    public GameObject hit_3;
    public GameObject ultimated;
    public GameObject ultimated_1;
    public GameObject ultimated_2;
    public GameObject ultimated_3;

    public int attackCount_Effect;

    public GameObject taggedAction;
    List<Action> action = new List<Action>();

    PlayerDataLoader        playerAttackData;
    public Motor            motor;

    void Awake()
    {
        state = State.idle;
        //StartCoroutine(FSM());
        ani = GetComponent<Animator>();

        playerAttackData = GameObject.Find("GameManager").GetComponent<PlayerDataLoader>();

        weaponeObMax = GameObject.Find("B");
        weaponeObMin = GameObject.Find("A");
        targetMonster = GameObject.Find("Monster");
        weaponeSlash = GameObject.Find("trail_bone");
        
        A.gameObject.SetActive(false);
        B.gameObject.SetActive(false);
        C.gameObject.SetActive(false);
        D.gameObject.SetActive(false);
        ultimated.gameObject.SetActive(false);
        ultimated_1.gameObject.SetActive(false);
        ultimated_2.gameObject.SetActive(false);
        ultimated_3.gameObject.SetActive(false);

        weaponeSlash.gameObject.SetActive(false);
        closestDistSqr = Mathf.Infinity;
        distPos = 0.7f;

        CheckList();
        ani.SetBool("AttackMove", true);
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
        if (targetMonster != null)
        {
            Vector3 obPos = targetMonster.transform.position;
            dist = (obPos - weaponeObMax.transform.position).sqrMagnitude;
            dist_1 = (obPos - weaponeObMin.transform.position).sqrMagnitude;
            if (dist < distPos || dist_1 < distPos)
            {
                if (dist < closestDistSqr)
                {
                    action[0].PlayerDamage();
                    if (attackCount_Effect == 1)
                        hit_1.gameObject.SetActive(true);
                    if (attackCount_Effect == 2)
                        hit_2.gameObject.SetActive(true);
                    if (attackCount_Effect == 3)
                        hit_3.gameObject.SetActive(true);
                }
            }
        }
    }
    /*  IEnumerator FSM()
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

          }

          yield return new WaitForSeconds(0.5f);
      }
      IEnumerator attack()
      {
          if (state == State.attack)
          {

          }
          yield return new WaitForSeconds(0.11f);
      }*/
    public void AttackButton()
    {
        if (attackCheck)
        {
            ani.SetBool("Attack1", true);
            ani.SetBool("Attack2", true);
            ani.SetBool("Attack3", true);
            state = State.attack;
            motor.moveSpeed = 0;
        }
    }
    public void AttackStart()
    {
        ani.SetBool("Attack1", false);
        ani.SetBool("Attack2", false);
        ani.SetBool("Attack3", false);
        ani.SetBool("AttackMove", false);
        attackCheck = false;
    }
    public void AttackEnd()
    {
        // 액션,애니메이션
        skillCheck = false;
        attackCheck = true;
        ani.SetBool("AttackMove", true);
        ani.SetBool("Attack1", false);
        ani.SetBool("Attack2", false);
        ani.SetBool("Attack3", false);
        // 버튼콜린더
        attack_Coll.enabled = true;
        defend_Coll.enabled = true;
        // 이펙트
        A.gameObject.SetActive(false);

        attackCount_Effect = 1;
        hit_1.gameObject.SetActive(false);
        if (ani.GetBool("Skill1") == true || ani.GetBool("Skill2") == true)
        {
            ani.SetBool("Skill1", false);
            ani.SetBool("Skill2", false);
            return;
        }
        if (ani.GetBool("Ultimated") == true)
        {
            ani.SetBool("Ultimated", false);
            playerAttackData.cam_3.gameObject.SetActive(false);
            ultimated_2.gameObject.SetActive(false);
            ultimated_3.gameObject.SetActive(false);
            playerAttackData.cam_1.gameObject.SetActive(true);
        }
    }
    public void AttackFalse()
    {
        ani.SetBool("Attack1", false);
        ani.SetBool("Attack2", false);
        ani.SetBool("Attack3", false);
        attackCheck = false;
        skillCheck = false;
        ani.SetBool("AttackMove", true);
    }
    public void IdleStart()
    {
        skillCheck = false;
        weaponeSlash.gameObject.SetActive(false);
    }
    public void AttackTrue()
    {
        attackCheck = true;
    }
    public void AttackHit()
    {
        TargetCheck();
    }
    public void MoveSpeedCheck()
    {
        motor.moveSpeed = 5;
    }
    public void MoveSpeedCheckOff()
    {
        motor.moveSpeed = 0;
    }
    public void DefendButtonOn()
    {
        if (ani.GetBool("Defend") == false && playerAttackData.playerNowMP > 5)
        {
            ani.SetBool("Defend", true);
            weaponeSlash.gameObject.SetActive(false);
            attackCheck = true;
            motor.moveSpeed = 5;
            attackCount = 0;

            attackCount_Effect = 1;
            hit_1.gameObject.SetActive(false);
            A.gameObject.SetActive(false);
        }
    }
    /// <summary>
    /// 방어관련
    /// </summary>
    public void DefendButtonOff()
    {
        weaponeSlash.gameObject.SetActive(false); // 이펙트
        D.gameObject.SetActive(false);

        ani.SetBool("AttackMove", true); // 애니메이션
        ani.SetBool("Attack1", false);
        ani.SetBool("Attack2", false);
        ani.SetBool("Attack3", false);
        ani.SetBool("Defend", false);

        motor.moveSpeed = 5; // 수치
        attackTime = 0;
    }
    public void DefendStart()
    {
        playerAttackData.playerNowMP -= 5;
        skillCheck = true;
        weaponeSlash.gameObject.SetActive(false);
    }
    /// <summary>
    /// 스킬 버튼
    /// </summary>
    public void SkillButton1()
    {
        if (ani.GetBool("Skill2") == false)
        {
            ani.SetBool("Skill1", true);
            skillCheck = true;
            attack_Coll.enabled = false;
            skill_1_Coll.enabled = false;
            defend_Coll.enabled = false;
            playerAttackData.skillDelayTime_1 = 14;
        }
    }
    public void SkillButton2()
    {
        if (ani.GetBool("Skill1") == false)
        {
            ani.SetBool("Skill2", true);
            attack_Coll.enabled = false;
            skill_2_Coll.enabled = false;
            defend_Coll.enabled = false;
            playerAttackData.skillDelayTime_2 = 18;
        }
    }
    /// <summary>
    /// 애니메이션 이벤트 및 이펙트 관련
    /// </summary>
    public void OnAttack_B()
    {
        weaponeSlash.gameObject.SetActive(true);
    }
    public void OffAttack_B()
    {
        weaponeSlash.gameObject.SetActive(false);
    }
    public void AttackEffect_1_Off()
    {
        A.gameObject.SetActive(false);
    }
    /// <summary>
    /// 이펙트 관련
    /// </summary>
    public void AttackEffect_1()
    {
        attackCount_Effect = 1;
        if (ani.GetBool("Attack1") == true)
        {
            A.gameObject.SetActive(true);
            B.gameObject.SetActive(false);
            hit_2.gameObject.SetActive(false);
        }
    }
    public void AttackEffect_2()
    {
        attackCount_Effect = 2;
        if (ani.GetBool("Attack2") == true)
        {
            B.gameObject.SetActive(true);
            //A.gameObject.SetActive(false);
            C.gameObject.SetActive(false);
            hit_3.gameObject.SetActive(false);
        }
    }
    public void AttackEffect_3()
    {
        attackCount_Effect = 3;
        if (ani.GetBool("Attack3") == true)
        {
            C.gameObject.SetActive(true);
            hit_1.gameObject.SetActive(false);
        }
    }
    /// <summary>
    /// 겨루기시 방향
    /// </summary>
    public void AttackLook()
    {
        motor.moveJoystick.inputVector = Vector3.zero;
        motor.moveJoystick.joystickImg.rectTransform.anchoredPosition = Vector3.zero;
        motor.moveSpeed = 0;
        motor.state = Motor.State.idle;
        ani.SetBool("Move", false);
        skillCheck = true;
        
        transform.LookAt(targetMonster.transform);
    }
    /// <summary>
    /// 피격모션 시작 이벤트
    /// </summary>
    public void PlayerHitMotion()
    {
        motor.moveJoystick.inputVector = Vector3.zero;
        motor.moveJoystick.joystickImg.rectTransform.anchoredPosition = Vector3.zero;
        motor.moveSpeed = 0;
        motor.state = Motor.State.idle;
        ani.SetBool("Move", false);
        skillCheck = true;
        transform.LookAt(targetMonster.transform);
        ani.SetBool("Hit", true);
    }
    /// <summary>
    /// 피격모션에 끝날때 관련된 이벤트
    /// </summary>
    public void EndHitMotion()
    {
        hitCount = 15;
        skillCheck = false;
        motor.moveSpeed = 5;
        ani.SetBool("Hit", false);
        AttackEnd();
    }
    /// <summary>
    /// 변신이펙트 이벤트
    /// </summary>
    public void UltimatedAction()
    {
        //메터리얼
        playerAttackData.monsterShaderChange_2.rend.sharedMaterial = playerAttackData.monsterShaderChange_2.material[2];
        playerAttackData.monsterShaderChange_3.rend.sharedMaterial = playerAttackData.monsterShaderChange_3.material[2];
        playerAttackData.monsterShaderChange_4.rend.sharedMaterial = playerAttackData.monsterShaderChange_4.material[2];

        // 이펙트
        ultimated.gameObject.SetActive(true);
        ultimated_1.gameObject.SetActive(true);
    }
}



