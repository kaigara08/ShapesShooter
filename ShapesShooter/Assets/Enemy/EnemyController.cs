using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private EnemyStatus enemyStatus;
    
    public Transform ShotSpawnPos;
    public GameObject EnemyShotPrefab;
    public Transform playerTrans;
    private EnemyManager manager;
    
    public GameObject mesh;
    private bool meshFlag = false;
    private float meshAnimationTime = 0.0f;

    public GameObject energyItem;

    private Action actionTask;

    public bool isGround;

    public bool isClearWall = false;

    private bool TimeStop;

    public AudioSource audioSource;
    public AudioClip damageClip;
    public AudioClip shotClip;

    public GameObject effectPrefab;

    public void Initialized(EnemyStatus enemyStatus,EnemyManager manager)
    {
        this.enemyStatus = enemyStatus;
        transform.position = enemyStatus.spawnPos;
        this.manager = manager;
        playerTrans = GameObject.FindWithTag("Player").GetComponent<Transform>();
        audioSource = GetComponent<AudioSource>();
        this.enemyStatus.state = State.Nomal;
        TimeStop = false;
        switch (enemyStatus.enemytype) 
        {
            case 0:
                Enemy1Task task = GetComponent<Enemy1Task>();
                if (task != null)
                {
                    actionTask = task.Action;
                }
                break;
            case 1:
                Enemy2Task task2 = GetComponent<Enemy2Task>();
                actionTask = task2.Action;
                break;
            case 2:
                Enemy3Task task3 = GetComponent<Enemy3Task>();
                actionTask = task3.Action;
                break;

        }
    }
    // Start is called before the first frame update
    
    private void OnDestroy()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!TimeStop && enemyStatus.state == State.Nomal)
        {
            if (playerTrans != null && actionTask != null)
            {
                actionTask();
            }
        }

        if(meshFlag==true)
        {
            meshAnimationTime += Time.deltaTime;
            if (meshAnimationTime >= 0.1f)
            {
                meshFlag = false;
                meshAnimationTime = 0.0f;
                mesh.SetActive(true);
            }
        }

    }

    void EnemyDestroy()
    {
        enemyStatus.state = State.Non;
        if (enemyStatus.dropItem != null)
        {
            for (int i = 0; i < enemyStatus.dropItem_count; ++i)
            {
                GenerateItem(enemyStatus.dropItem);
            }
        }
        for (int i = 0; i < enemyStatus.energyItem_count; ++i)
        {
            GenerateItem(energyItem);
        }
        manager.CallEnemyDestroy(enemyStatus.enemyId);
        mesh.SetActive(false);
        GameObject value = effectPrefab;
        value.transform.position = transform.position;
        GameObject effect = Instantiate(value);
        Destroy(gameObject);
    }

   

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="PlayerShot")
        {
            audioSource.PlayOneShot(damageClip);
            meshFlag = true;
            mesh.SetActive(false);
            enemyStatus.hp--;
            if(enemyStatus.hp == 0)
            {
                EnemyDestroy();
            }
        }

        if (collision.gameObject.tag == "Ground")
        {
            isGround = true;
        }

        if(collision.gameObject.tag == "ClearWall")
        {
            isClearWall = true;
        }
    }

    private void GenerateItem(GameObject dropItem)
    {
        GameObject SpawnItem = dropItem;
        SpawnItem.transform.position = new Vector3(transform.position.x, 1, transform.position.z);
        GameObject item = Instantiate(SpawnItem);
    }

    public int GetSetEnemyNmb
    {
        get { return enemyStatus.enemyId; }
        set { enemyStatus.enemyId = value; }
    }

    public State GetSetState
    {
        get { return enemyStatus.state; }
        set { enemyStatus.state = value; }
    }

    public bool GetSetTimeFlag
    {
        get { return TimeStop; }
        set { TimeStop = value; }
    }


}
