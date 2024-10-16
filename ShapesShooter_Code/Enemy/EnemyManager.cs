using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject[] EnemyType;

    public ProjectData projectData;

    [SerializeField]
    private EnemyList[] enemyLists;

    private int Stagenmb = 5;

    private float deltaTime;

    int detheCount;

    private GameDirector director;

    private int NowTaskStageID;

    private Vector3 nowTaskStagePosition;

    public bool Stage2_clearFlag;
    public bool Stage3_clearFlag;
    public bool Stage4_clearFlag;

    public GameObject[] dropItems;

    public bool TimeStop;

    private void Start()
    {
        LoadEnemyData();
    }

    public void Initialized(int stageID,Vector3 stagePosition)
    {
        NowTaskStageID = stageID;
        nowTaskStagePosition = stagePosition;
        deltaTime = 0;

        for(int i = 0; i < enemyLists[NowTaskStageID].enemyCount; i++)
        {
            enemyLists[NowTaskStageID].enemyStatus[i].spawnPos += nowTaskStagePosition;
        }
    }

    public void Manager_Update()
    {
        if (!TimeStop)
        {
            for (int i = 0; i < enemyLists[NowTaskStageID].enemyCount; ++i)
            {
                if (deltaTime >= enemyLists[NowTaskStageID].enemyStatus[i].appearanceTime && enemyLists[NowTaskStageID].enemyStatus[i].appearanceFlag == false)
                {
                    EnemyGenerate(enemyLists[NowTaskStageID].enemyStatus[i]);
                    enemyLists[NowTaskStageID].enemyStatus[i].appearanceFlag = true;
                }
            }
            deltaTime += Time.deltaTime;
        }
    }

    public void EnemyGenerate(EnemyStatus enemyStatus)
    {
        GameObject enemys = Instantiate(EnemyType[enemyStatus.enemytype]);
        EnemyController controller = enemys.GetComponent<EnemyController>();
        controller.Initialized(enemyStatus, gameObject.GetComponent<EnemyManager>());        
        
    }

    public void LoadEnemyData()
    {
        //配列用意
        enemyLists = new EnemyList[Stagenmb];
        for (int i = 2; i <= 4; ++i)
        {
            for (int l= 0;l<projectData.EnemyData.Count; ++l)
            {
                if (projectData.EnemyData[l].stageID == i)
                {
                    enemyLists[i].enemyCount++;
                }
            }
            enemyLists[i].enemyStatus = new EnemyStatus[enemyLists[i].enemyCount];
        }

        //配列にデータ入力
        for (int i = 0; i < enemyLists.Length; ++i)
        {
            int nmb = 0;
            for (int l = 0; l < projectData.EnemyData.Count; ++l)
            {
                if(projectData.EnemyData[l].stageID == i)
                {
                    enemyLists[i].enemyStatus[nmb].enemyId = projectData.EnemyData[l].enemyID;
                    enemyLists[i].enemyStatus[nmb].enemytype = projectData.EnemyData[l].enemytype;
                    enemyLists[i].enemyStatus[nmb].hp = projectData.EnemyData[l].hp;
                    enemyLists[i].enemyStatus[nmb].spawnPos = new Vector3(projectData.EnemyData[l].spawnPos_x,
                                                                        projectData.EnemyData[l].spawnPos_y,
                                                                        projectData.EnemyData[l].spawnPos_z
                                                                        );
                    enemyLists[i].enemyStatus[nmb].appearanceTime = projectData.EnemyData[l].appearanceTime;
                    if (projectData.EnemyData[l].dropItem < 0)
                    {
                        enemyLists[i].enemyStatus[nmb].dropItem = null;
                    }
                    else
                    {
                        enemyLists[i].enemyStatus[nmb].dropItem = dropItems[projectData.EnemyData[l].dropItem];
                    }
                    enemyLists[i].enemyStatus[nmb].dropItem_count = projectData.EnemyData[l].dropItem_count;
                    enemyLists[i].enemyStatus[nmb].energyItem_count = projectData.EnemyData[l].energy_count;
                    enemyLists[i].enemyStatus[nmb].appearanceFlag = false;
                    nmb++;
                }
            }
        }
        
    }

    public void CallEnemyDestroy(int enemyID)
    {
        for (int i = 0; i < enemyLists[NowTaskStageID].enemyCount; ++i)
        {
            if (enemyID == enemyLists[NowTaskStageID].enemyStatus[i].enemyId)
            {
                enemyLists[NowTaskStageID].enemyStatus[i].state = State.Non;
                enemyLists[NowTaskStageID].deathEnemyCount++;

                if (enemyLists[NowTaskStageID].enemyCount == enemyLists[NowTaskStageID].deathEnemyCount)
                {
                    switch(NowTaskStageID)
                    {
                        case 2:
                            Stage2_clearFlag = true;
                            break;
                        case 3:
                            Stage3_clearFlag = true;
                            break;
                        case 4:
                            Stage4_clearFlag = true;
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        
    }

}

