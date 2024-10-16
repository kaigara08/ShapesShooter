using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageTask_3 : MonoBehaviour
{
    
    private EnemyManager enemyManager;
    [SerializeField]
    private Vector3 stagePos;
    private int StageID = 3;
    private StageTaskScript stageManager;

    private DoorScript door;
    public void Initialized()
    {
        Debug.Log("ステージID：" + StageID);
        enemyManager = GetComponent<EnemyManager>();
        stagePos = GameObject.Find("Stage3").transform.position;
        door = GameObject.Find("Door_3").GetComponent<DoorScript>();
        enemyManager.Initialized(StageID, stagePos);
        
    }

    public void Stage_Updata()
    {
        enemyManager.Manager_Update();

        if (enemyManager.Stage3_clearFlag)
        {
            door.Open_Door();
        }
        if (door.GetSetPassingFlag)
        {
            stageManager.NextTask(4);
        }
    }

    public StageTaskScript SetManager
    {
        set { stageManager = value; }
    }
}
