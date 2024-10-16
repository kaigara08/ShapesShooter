using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageTask_4 : MonoBehaviour
{
    
    private EnemyManager enemyManager;
    [SerializeField]
    private Vector3 stagePos;
    private int StageID = 4;
    private StageTaskScript stageManager;

    private DoorScript door;
    public void Initialized()
    {
        Debug.Log("ステージID：" + StageID);
        enemyManager = GetComponent<EnemyManager>();
        stagePos = GameObject.Find("Stage4").transform.position;
        door = GameObject.Find("Door_4").GetComponent<DoorScript>();
        enemyManager.Initialized(StageID, stagePos);
        
    }

    public void Stage_Updata()
    {
        enemyManager.Manager_Update();

        if (enemyManager.Stage4_clearFlag)
        {
            door.Open_Door();
        }
        if (door.GetSetPassingFlag)
        {
            stageManager.NextTask(10);
        }
    }

    public StageTaskScript SetManager
    {
        set { stageManager = value; }
    }
}
