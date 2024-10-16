using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialScript : MonoBehaviour
{
    public EnemyManager enemyManager;
    EnemyStatus[] enemyStatus = new EnemyStatus[3];
    private float[] posX = { 0, -7.5f, 7.5f };

    Vector3 HPPop = new Vector3(9, 0, -2.5f);
    Vector3 EnergyPop = new Vector3(-9, 0.5f, -2.5f);

    bool HPPopFlag = true;
    bool EnergyPopFlag = true;

    public GameObject HPItem;
    public GameObject EnergyItem;

    public ButtonScript button;
    // Start is called before the first frame update
    void Start()
    {
        for(int i=0;i<3;i++)
        {
            enemyStatus[i] = new EnemyStatus();
            enemyStatus[i].enemyId = i;
            enemyStatus[i].enemytype = 0;
            enemyStatus[i].hp = 9999;
            enemyStatus[i].spawnPos = new Vector3(posX[i], 1, 9);
            enemyStatus[i].appearanceTime = 0;
            enemyStatus[i].dropItem = null;
            enemyStatus[i].dropItem_count = 0;
            enemyStatus[i].energyItem_count = 0;
            enemyStatus[i].state = State.Nomal;
            enemyStatus[i].appearanceFlag = true;
            enemyManager.EnemyGenerate(enemyStatus[i]);
        }

    }

    

    // Update is called once per frame
    void Update()
    {
        Debug.Log("HPItem:" + GameObject.FindWithTag("HPItem"));
        Debug.Log("EnergyItem:" + GameObject.FindWithTag("EnergyItem"));
        if (GameObject.FindWithTag("HPItem") == null && HPPopFlag)
        {
            HPPopFlag = false;
            Invoke("GenerateHPItem", 0.5f);

        }

        if (GameObject.FindWithTag("EnergyItem") == null && EnergyPopFlag)
        {
            EnergyPopFlag = false;
            Invoke("GenerateEnergyItem", 0.5f);
        }

        if(button.GetButtonOnFlag)
        {
            SceneManager.LoadScene("TitleScene");
        }
    }

    void GenerateHPItem()
    {
        GameObject hpitem = Instantiate(HPItem);
        hpitem.transform.position = HPPop;
        HPPopFlag = true;
    }

    void GenerateEnergyItem()
    {
        GameObject energyitem = Instantiate(EnergyItem);
        energyitem.transform.position = EnergyPop;
        EnergyPopFlag = true;
    }
}
