using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageTask_2 : MonoBehaviour
{
    
    private EnemyManager enemyManager;
    [SerializeField]
    private Vector3 stagePos;
    private int StageID = 2;
    private StageTaskScript stageManager;

    private DoorScript door;
    public void Initialized()
    {
        Debug.Log("ステージID：" + StageID);
        enemyManager = GetComponent<EnemyManager>();
        stagePos = GameObject.Find("Stage2").transform.position;
        door = GameObject.Find("Door_2").GetComponent<DoorScript>();
        enemyManager.Initialized(StageID, stagePos);
        
    }

    public void Stage_Updata()
    {
        enemyManager.Manager_Update();

        if (enemyManager.Stage2_clearFlag)
        {
            door.Open_Door();
        }
        if (door.GetSetPassingFlag)
        {
            stageManager.NextTask(3);
        }
    }

    public StageTaskScript SetManager
    {
        set { stageManager = value; }
    }
}
