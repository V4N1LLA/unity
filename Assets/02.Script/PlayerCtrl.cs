using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{

    [SerializeField]
    private Rigidbody rigid;

    // 컴포넌트를 캐시 처리할 변수
    private Transform tr;
    //Animation 컴포넌트를 저장할 변수
    private Animation anim;
    // 이동 속력 변수 (public으로 선언되어 인스펙터 뷰에 노출됨)
    public float moveSpeed = 3.0f;
    // 회전 속도 변수
    public float turnSpeed = 160.0f;
    // 점프 높이
    public float JumpForce = 1.0f;



    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        tr = GetComponent<Transform>();
        anim = GetComponent<Animation>();

        anim.Play("Idle");
    }


    void Update()
    {
        Jump();

        float h = Input.GetAxis("Horizontal"); //-1.0f ~ 0.0f ~ +1.0f
        float v = Input.GetAxis("Vertical");   //-1.0f ~ 0.0f ~ +1.0f
        float r = Input.GetAxis("Mouse X");

        //Debug.Log("h=" + h);
        //Debug.Log("v=" + v);

        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);

        tr.Translate(moveDir.normalized * moveSpeed * Time.deltaTime);

        tr.Rotate(Vector3.up * turnSpeed * Time.deltaTime * r);

        PlayerAnim(h, v);
        //tr.Translate(Vector3.forward * Time.deltaTime * v * moveSpeed);

        //transform.position += new Vector3(0, 0, 1);

        //transform.position += Vector3.forward * 1;
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            rigid.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
        }
    }

    void PlayerAnim(float h, float v)
    {
        if (v >= 0.1f)
        {
            anim.CrossFade("RunF", 0.25f);
        }
        else if (v <= -0.1f)
        {
            anim.CrossFade("RunB", 0.25f);
        }
        else if (h >= 0.1f)
        {
            anim.CrossFade("RunR", 0.25f);
        }
        else if (h <= -0.1f)
        {
            anim.CrossFade("RunL", 0.25f);
        }
        else
        {
            anim.CrossFade("Idle", 0.25f);
        }

    }
}