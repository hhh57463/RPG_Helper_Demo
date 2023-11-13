using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thief : PlayerMng
{
    [SerializeField] GameObject[] teleportPos = new GameObject[2];
    [SerializeField] TrailRenderer trailRenderer;
    [SerializeField] GameObject[] effectList = new GameObject[2];
    protected override void Start()
    {
        base.Start();
        originSpeed = speed = 15.0f;
        jumpPower = 7.5f;
        attackDelay = 0.5f;
        skillCoolTime = 5.0f;
    }
    public override void basic_attack()
    {
        Debug.Log("Thief basic Attack");
        speed = 0;
        animator.SetBool("Attack", !attackAccess);
        StartCoroutine(AttackDelay());
    }
    public override void skill_1()
    {
        Debug.Log("Thief skill 1");
        if(skillAccess)
        {
            StartCoroutine(SkillCool());
            StartCoroutine(Skill());
        }
    }
    public override void skill_2()
    {
        Debug.Log("Thief skill 2");
    }
    public override void skill_3()
    {
        Debug.Log("Thief skill 3");
    }
    public override void skill_4()
    {
        Debug.Log("Thief skill 4");
    }
    public override void skill_5()
    {
        Debug.Log("Thief skill 5");
    }
    
    IEnumerator SkillCool()
    {
        skillAccess = false;
        yield return YieldInstructionCache.WaitForSeconds(skillCoolTime);
        skillAccess = true;
    }

    IEnumerator Skill()
    {
        moveAccess = false;
        trailRenderer.enabled = true;
        trailRenderer.Clear();
        transform.position =  teleportPos[0].transform.position;
        SpawnEffect();
        yield return YieldInstructionCache.WaitForSeconds(0.5f);
        transform.position = teleportPos[1].transform.position;
        SpawnEffect();
        yield return YieldInstructionCache.WaitForSeconds(0.5f);
        transform.position = teleportPos[0].transform.position;
        SpawnEffect();
        yield return YieldInstructionCache.WaitForSeconds(0.5f);
        transform.position = teleportPos[1].transform.position;
        trailRenderer.enabled = false;
        moveAccess = true;
    }
    
    void SpawnEffect()
    {
        Destroy(Instantiate(effectList[0], transform.position, Quaternion.identity), 1.0f);
        Destroy(Instantiate(effectList[1], transform.position, Quaternion.identity), 1.0f);
    }
}