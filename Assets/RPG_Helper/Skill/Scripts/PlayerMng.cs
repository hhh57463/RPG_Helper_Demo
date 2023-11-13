using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMng : MonoBehaviour
{
    CharacterController controller;

    Vector3 moveDir;

    [Header("Player State")]
    protected float originSpeed;
    public float speed;
    public float jumpPower;
    [SerializeField] float rotSpeed;
    public float attackDelay;
    public bool attackAccess;
    public float skillCoolTime;
    public bool skillAccess;
    float gravity;
    float viewDirX;
    public GameObject weaponEffect;
    public bool moveAccess;

    public static bool isDialog;

    [Header("Player Level")]
    public int level;
    public float exp;
    public float maxExp;

    protected Animator animator;

    protected virtual void Start()
    {
        controller = GetComponent<CharacterController>();
        gravity = 10.0f;
        rotSpeed = 2.0f;
        level = 1;
        exp = 0.0f;
        maxExp = 10.0f;
        attackAccess = true;
        skillAccess = true;
        moveAccess = true;
        if (Manager.I != null)
        {
            Manager.I.playerName = gameObject.name;
            Manager.I.GetPlayerTransform(Manager.I.playerName);
            Manager.I.CameraSetting();
        }

        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isDialog && moveAccess)
        {
            Move();
            Skill();
            if (Input.GetMouseButtonDown(0) && attackAccess && controller.isGrounded){
                attackAccess = false;
                basic_attack();
            }
        }
    }

    float h => Input.GetAxis("Horizontal");
    float v => Input.GetAxis("Vertical");

    void Move()
    {
        viewDirX += Input.GetAxis("Mouse X") * rotSpeed;
        if (controller.isGrounded)
        {
            moveDir = new Vector3(h, 0f, v);
            moveDir = transform.TransformDirection(moveDir);
            moveDir *= speed;

            // if (Input.GetButton("Jump"))
            // {
            //     moveDir.y = jumpPower;
            // }
        }
        transform.rotation = Quaternion.Euler(0f, viewDirX, 0f);
        moveDir.y -= gravity * Time.deltaTime;
        controller.Move(moveDir.normalized * Time.deltaTime);
        animator.SetFloat("Speed", controller.velocity.magnitude);
    }

    void Skill()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && skillAccess)
            skill_1();
        if (Input.GetKeyDown(KeyCode.Alpha2))
            skill_2();
        if (Input.GetKeyDown(KeyCode.Alpha3))
            skill_3();
        if (Input.GetKeyDown(KeyCode.Alpha4))
            skill_4();
        if (Input.GetKeyDown(KeyCode.Alpha5))
            skill_5();
    }

    public virtual void basic_attack() { }
    public virtual void skill_1() { }
    public virtual void skill_2() { }
    public virtual void skill_3() { }
    public virtual void skill_4() { }
    public virtual void skill_5() { }

    protected IEnumerator AttackDelay(){
        weaponEffect.SetActive(true);
        yield return YieldInstructionCache.WaitForSeconds(attackDelay);
        attackAccess = true;
        speed = originSpeed;
        weaponEffect.SetActive(false);
        animator.SetBool("Attack", !attackAccess);
    }
}
