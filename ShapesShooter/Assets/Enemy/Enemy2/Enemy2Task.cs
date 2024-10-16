using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Task : MonoBehaviour
{
    EnemyController controller;
    [SerializeField]
    private int ActionMode;
    private float deletTime;
    private float shotSpan = 2f;
    private float moveSpeed = 0.05f;

    private Rigidbody rigid;
    private Vector3 JumpVector;
    private Vector3 JumpPlayerPos;
    private Vector3 PlayerUpVect;
    private bool PlayerUp = false;

    private float moveDeletTime;

    private bool TimeFlag;
    private Vector3 TimeJumpVector;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<EnemyController>();
        rigid = GetComponent<Rigidbody>();
        ActionMode = 0;
        TimeFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(controller.GetSetTimeFlag && !TimeFlag)
        {
            TimeJumpVector = rigid.velocity;
            rigid.isKinematic = true;
            TimeFlag = true;
        }
        if(!controller.GetSetTimeFlag && TimeFlag)
        {
            rigid.isKinematic = false;
            rigid.velocity = TimeJumpVector;
            TimeFlag = false;
        }
    }

    public void Action()
    {
        switch (ActionMode)
        {
            case 0:
                
                Rotate();
                Move();
                Shot1();
                break;
            case 1:
                Jump();
                break;
            case 2:
                Shot2();
                break;
            default:
                ActionMode = 0;
                break;
        }
    }

    void Shot1()
    {
        if (deletTime > shotSpan)
        {
            controller.audioSource.PlayOneShot(controller.shotClip);
            GeneratarShot(controller.ShotSpawnPos.forward);
            deletTime = 0;
        }
        deletTime += Time.deltaTime;
    }

    //放射状に球を発射
    void Shot2()
    {
        controller.audioSource.PlayOneShot(controller.shotClip);
        Vector3 beceVector = transform.forward;
        for (int i = 0; i < 12; ++i)
        {
            //Quaternion.Euler( 0, 0, 90 ) * vec;
            //Debug.Log("角度：" + i * 30);
            var ShotVector = Quaternion.Euler(0, i * 30, 0) * beceVector;
            GeneratarShot(ShotVector);
            
        }
        ActionMode = 0;
    }

    void GeneratarShot(Vector3 vct)
    {
        GameObject shots = Instantiate(controller.EnemyShotPrefab);
        shots.transform.position = controller.ShotSpawnPos.position;
        shots.GetComponent<EnemyShotController>().Fire(vct);
    }

    void Rotate()
    {
        var direction = controller.playerTrans.position - transform.position;
        direction.y = 0;

        var lookRotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, 0.5f);
    }

    void Move()
    {
        Vector3 moveVectro = (controller.playerTrans.position - transform.position);
        moveVectro.y = 0;
       
        if(moveVectro.magnitude > 16.0f&&moveDeletTime > 2.0f)
        {
            JumpVector = moveVectro * 400;
            JumpVector.y = 20000;
            JumpPlayerPos = controller.playerTrans.position;
            PlayerUpVect = controller.playerTrans.up;
            rigid.AddForce(JumpVector);
            controller.isGround = false;
            moveDeletTime = 0;
            ActionMode = 1;
        }
        else
        {
            moveVectro *= -1;
        }

        moveVectro = moveVectro.normalized;
        transform.position += (moveVectro * moveSpeed);
        moveDeletTime += Time.deltaTime;
    }

    void Jump()
    {
        Vector3 vectorA = PlayerUpVect;
        Vector3 vectorB = transform.position - JumpPlayerPos;
        var angle = Vector3.Angle(vectorA, vectorB);
        
        if ((angle <= 1f || controller.isClearWall) && !PlayerUp)
        {
            rigid.velocity = Vector3.zero;
            rigid.AddForce(-transform.up * 2500);
            PlayerUp = true;
            Debug.Log("真上です");
        }
        if (controller.isGround)
        {
            Debug.Log("地面です");
            PlayerUp = false;
            ActionMode = 2;
            controller.isClearWall = false;
        }
    }

}
