using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : PlayerMng
{
    [SerializeField] GameObject skillObj;
    float skillDuration;
    protected override void Start()
    {
        base.Start();        
        originSpeed = speed = 10.0f;
        jumpPower = 5.0f;
        attackDelay = 1.5f;
        skillCoolTime = 10.0f;
        skillDuration = 5.0f;
    }
    public override void basic_attack()
    {
        Debug.Log("Warrior basic Attack");
        speed = 0;  
        animator.SetBool("Attack", !attackAccess);
        StartCoroutine(AttackDelay());
    }
    public override void skill_1()
    {
        Debug.Log("Warrior skill 1");
        if(skillAccess)
        {
            StartCoroutine(Skill(skillObj));
            StartCoroutine(SkillCool());
        }
    }
    public override void skill_2()
    {
        Debug.Log("Warrior skill 2");
    }
    public override void skill_3()
    {
        Debug.Log("Warrior skill 3");
    }
    public override void skill_4()
    {
        Debug.Log("Warrior skill 4");
    }
    public override void skill_5()
    {
        Debug.Log("Warrior skill 5");
    }

    IEnumerator Skill(GameObject effect)
    {
        effect.SetActive(true);
        yield return YieldInstructionCache.WaitForSeconds(skillDuration);
        effect.SetActive(false);
    }

    IEnumerator SkillCool()
    {
        skillAccess = false;
        yield return YieldInstructionCache.WaitForSeconds(skillCoolTime);
        skillAccess = true;
    }
}
