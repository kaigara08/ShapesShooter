using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Task : MonoBehaviour
{
    EnemyController controller;
    private float deletTime;
    private float shotSpan = 2f;
    private float moveSpeed = 0.05f;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<EnemyController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Action()
    {
        Shot();
        Rotate();
        Move();
    }

    void Shot()
    {
        if (deletTime > shotSpan)
        {
            controller.audioSource.PlayOneShot(controller.shotClip);
            GeneratarShot(controller.ShotSpawnPos.forward);
            deletTime = 0;
        }
        deletTime += Time.deltaTime;
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
        //プレイヤーと自分の距離を測る
        //Vector3 moveVectro = playerTrans.position - transform.position;
        Vector3 moveVectro = controller.playerTrans.position - transform.position;

        moveVectro.y = 0;
        //Debug.Log("変化前" + moveVectro.magnitude + ":" + moveVectro);
        if (moveVectro.magnitude < 6.0f)
        {
            moveVectro *= -1;
        }
        moveVectro = moveVectro.normalized;
        //Debug.Log("変化後" + moveVectro);
        transform.position += (moveVectro * moveSpeed);

    }
}
