using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : PlayerMng
{
    [SerializeField] GameObject meteor;
    protected override void Start()
    {
        base.Start();
        originSpeed = speed = 10.0f;
        jumpPower = 5.0f;
        attackDelay = 1.5f;
        skillCoolTime = 10.0f;
    }
    public override void basic_attack()
    {
        Debug.Log("Wizard basic Attack");
        speed = 0;
        animator.SetBool("Attack", !attackAccess);
        StartCoroutine(AttackDelay());
    }
    public override void skill_1()
    {
        Debug.Log("Wizard skill 1");
        if(skillAccess)
        {
            StartCoroutine(SkillCool());
            Instantiate(meteor);
        }
    }
    public override void skill_2()
    {
        Debug.Log("Wizard skill 2");
    }
    public override void skill_3()
    {
        Debug.Log("Wizard skill 3");
    }
    public override void skill_4()
    {
        Debug.Log("Wizard skill 4");
    }
    public override void skill_5()
    {
        Debug.Log("Wizard skill 5");
    }

    IEnumerator SkillCool()
    {
        skillAccess = false;
        yield return YieldInstructionCache.WaitForSeconds(skillCoolTime);
        skillAccess = true;
    }
}
