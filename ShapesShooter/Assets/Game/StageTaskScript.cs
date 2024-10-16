using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StageTaskScript : MonoBehaviour
{
    private Action NowTask;


    private StageTask_1 stageTask1;
    private StageTask_2 stageTask2;
    private StageTask_3 stageTask3;
    private StageTask_4 stageTask4;

    GameDirector gameDirector;
    // Start is called before the first frame update
    void Start()
    {
        gameDirector = gameObject.GetComponent<GameDirector>();
        stageTask1 = gameObject.GetComponent<StageTask_1>();
        stageTask1.SetManager = gameObject.GetComponent<StageTaskScript>();
        stageTask2 = gameObject.GetComponent<StageTask_2>();
        stageTask2.SetManager = gameObject.GetComponent<StageTaskScript>();
        stageTask3 = gameObject.GetComponent<StageTask_3>();
        stageTask3.SetManager = gameObject.GetComponent<StageTaskScript>();
        stageTask4 = gameObject.GetComponent<StageTask_4>();
        stageTask4.SetManager = gameObject.GetComponent<StageTaskScript>();

        NextTask(1);
    }

    // Update is called once per frame
    void Update()
    {
        NowTask.Invoke();
    }

    public void NextTask(int StageNmb)
    {
        
        switch (StageNmb)
        {
            case 1:
                stageTask1.Initialized();
                NowTask = stageTask1.Stage_Updata;
                break;
            case 2:
                stageTask2.Initialized();
                NowTask = stageTask2.Stage_Updata;
                break;
            case 3:
                stageTask3.Initialized();
                NowTask = stageTask3.Stage_Updata;
                break;
            case 4:
                stageTask4.Initialized();
                NowTask = stageTask4.Stage_Updata;
                break;
            case 10:
                gameDirector.GameClear();
                break;
            default:
                break;
        }
    }

    
}
