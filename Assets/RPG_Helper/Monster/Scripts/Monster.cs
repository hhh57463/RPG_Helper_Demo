using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    Rigidbody rigid;
    [Tooltip("[Monster Type]\n1: In Range Move Monster\n2: Esacape Range, revert point Monster\n3: In Range Follow Monster")]
    [SerializeField] int monsterType;
    [Tooltip("[Monster Patern]\n0: Comback SpawnPoint\n1: Standing\n2: Move Random Direction\n3: Follow Player")]
    [SerializeField] int patern;
    [SerializeField] float speed;
    float moveSpeed;
    Vector3 spawnLocalPos;
    Vector3 spawnWorldPos;
    Vector3 targetPos;

    float spawnRadius;
    bool thinkDelay;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        spawnLocalPos = transform.localPosition;
        spawnWorldPos = transform.position;
        speed = 11.0f;
        spawnRadius = 30f;
        thinkDelay = false;
        StartCoroutine("Thinking");
    }

    void Update()
    {
        if (Manager.I.playerTr != null)
        {
            switch (monsterType)
            {
                case 1:
                    MonsterType1();
                    break;
                case 2:
                    MonsterType2();
                    break;
                case 3:
                    MonsterType3();
                    break;
            }
            if (Input.GetKeyDown(KeyCode.R))                    // Restore to existing settings
            {
                StopCoroutine("Thinking");
                transform.localPosition = spawnLocalPos;
                speed = 11.0f;
                StartCoroutine("Thinking");
            }
        }
    }
    void MonsterType1()
    {
        if (Vector3.Distance(spawnLocalPos, transform.localPosition) >= spawnRadius)
        {
            StopCoroutine("Thinking");
            patern = 0;
        }

        PaternAction();
        rigid.velocity = (transform.position - targetPos).normalized * moveSpeed;
        if (transform.localPosition == targetPos)
            MovePos();
    }

    [Header("Chase Range")]
    [SerializeField] float chaseRadius = 30f;
    void MonsterType2()
    {
        if (Vector3.Distance(spawnLocalPos, transform.localPosition) >= spawnRadius)
        {
            StopCoroutine("Thinking");
            patern = 0;
        }
        if (Vector3.Distance(spawnWorldPos, Manager.I.playerTr.position) <= chaseRadius)
        {
            StopCoroutine("Thinking");
            patern = 3;
        }

        PaternAction();
        PatternCheck();
    }

    bool isChasing = false;
    void MonsterType3()
    {
        if (Vector3.Distance(Manager.I.playerTr.position, transform.position) <= chaseRadius)
        {
            StopCoroutine("Thinking");
            isChasing = true;
            patern = 3;
        }
        if (isChasing && Vector3.Distance(Manager.I.playerTr.position, transform.position) >= chaseRadius)
        {
            if (Vector3.Distance(spawnLocalPos, transform.localPosition) >= spawnRadius)
            {
                isChasing = false;
                patern = 0;
            }
        }
        PaternAction();
        PatternCheck();
    }

    void PatternCheck()
    {
        if (patern == 3)
            rigid.velocity = -(transform.position - targetPos).normalized * moveSpeed;
        else
            rigid.velocity = (transform.localPosition - targetPos).normalized * moveSpeed;
        if (transform.localPosition == targetPos)
            MovePos();
    }

    float thinkCycle = 3.0f;

    IEnumerator Thinking()
    {
        thinkDelay = false;
        SelectPatern();
        yield return new WaitForSeconds(thinkCycle);
        thinkDelay = true;
        StartCoroutine("Thinking");
    }

    void MovePos()
    {
        targetPos = new Vector3(Random.Range(-25, 25), 1.5f, Random.Range(-25, 25));
        transform.LookAt(targetPos);
    }

    void SelectPatern()
    {
        patern = Random.Range(1, 3);
        if (patern == 2)
            MovePos();
    }

    /// <summary>
    /// Settings by pattern
    /// </summary>
    void PaternAction()
    {
        switch (patern)
        {
            case 0:                                                     // Return to spawn point
                targetPos = spawnLocalPos;
                moveSpeed = speed;
                if (Vector3.Distance(spawnLocalPos, transform.localPosition) >= 1.0f && thinkDelay)
                    StartCoroutine("Thinking");
                break;
            case 1:                                                     // Standing
                moveSpeed = 0f;
                break;
            case 2:                                                     // Move Random Direction
                moveSpeed = speed;
                break;
            case 3:                                                     // Follow Player
                targetPos = Manager.I.playerTr.position;
                moveSpeed = speed;
                break;
        }
    }
}
