using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageTask_1 : MonoBehaviour
{
    private int StageID = 1;

    private DoorScript door;
    private ButtonScript button;
    
    private StageTaskScript stageManager;
    // Start is called before the first frame update
   
    public void Initialized()
    {
        Debug.Log("ステージID：" + StageID);
        door = GameObject.Find("Door_1").GetComponent<DoorScript>();
        door.ID = StageID;
        button = GameObject.Find("Button_1").GetComponent<ButtonScript>();

    }

    public void Stage_Updata()
    {
        if(button.GetButtonOnFlag)
        {
            door.Open_Door();
        }
        if(door.GetSetPassingFlag)
        {
            stageManager.NextTask(StageID + 1);
        }
    }

    public StageTaskScript SetManager
    {
        set { stageManager = value; }
    }
}
