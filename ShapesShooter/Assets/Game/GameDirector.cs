using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameDirector : MonoBehaviour
{
    private MarkerManager markerManager;

    private GameObject player;

    private PlayerController playerController;

    public GameObject GameOverPanel;

    private bool GameOver;

    public PostProcessProfile postProfile;

    private EnemyManager enemyManager;

    private AudioSource BGMAudio;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
        markerManager = GameObject.Find("MarkerManager").GetComponent<MarkerManager>();
        enemyManager = GetComponent<EnemyManager>();
        BGMAudio = GetComponent<AudioSource>();
        GameOverPanel.SetActive(false);
        GameOver = false;
        Time.timeScale = 1.0f;
    }

    
    // Update is called once per frame
    void Update()
    {
        if(playerController.state == State.Non)
        {
            GameOverPanelActive();
        }   

    }

    private void OnDisable()
    {
        ColorGrading colorGrading = postProfile.GetSetting<ColorGrading>();
        colorGrading.saturation.Override(0f);
    }

    public void TimeStopMesod(bool flag)
    {
        if(flag == true)
        {
            enemyManager.TimeStop = true;

            GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
            GameObject[] enemyshots = GameObject.FindGameObjectsWithTag("EnemyShot");
            for(int i=0;i< enemys.Length; ++i)
            {
                    EnemyController enemyController = enemys[i].GetComponent<EnemyController>();
                    enemyController.GetSetTimeFlag = true;
            }
            for(int i = 0; i < enemyshots.Length; ++i)
            {
                EnemyShotController enemyShotController = enemyshots[i].GetComponent<EnemyShotController>();
                enemyShotController.SetTimeFlag = true;
            }

            BGMAudio.volume = 0.01f;
            ColorGrading colorGrading = postProfile.GetSetting<ColorGrading>();
            colorGrading.saturation.Override(-98.75f);
        }
        if(flag == false)
        {
            enemyManager.TimeStop = false;

            GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
            GameObject[] enemyshots = GameObject.FindGameObjectsWithTag("EnemyShot");
            for (int i = 0; i < enemys.Length; ++i)
            {
                EnemyController enemyController = enemys[i].GetComponent<EnemyController>();
                enemyController.GetSetTimeFlag = false;
            }
            for (int i = 0; i < enemyshots.Length; ++i)
            {
                EnemyShotController enemyShotController = enemyshots[i].GetComponent<EnemyShotController>();
                enemyShotController.SetTimeRestartFlag = true;
            }

            BGMAudio.volume = 0.8f;
            ColorGrading colorGrading = postProfile.GetSetting<ColorGrading>();
            colorGrading.saturation.Override(0f);
        }
    }

    

    public void GameClear()
    {
        SceneManager.LoadScene("ClearScene");
    }

    public void CallGenerateMarker(Transform target,int targetnmb)
    {
        markerManager.GenerateMarker(target,targetnmb);
    }

    public void CallRemoveMarkers(int nmb)
    {
        markerManager.RemoveMarkers(nmb);
    }

    public void CallCangeAim(int nmb)
    {
        markerManager.SetimageColor(nmb);
    }

    #region ゲームオーバー
    void GameOverPanelActive()
    {
        if (GameOver == false)
        {
            GameOverPanel.SetActive(true);
            Button firstButton = GameOverPanel.transform.Find("FirstButton").GetComponent<Button>();
            firstButton.Select();
            Time.timeScale = 0;
            GameOver = true;
        }
    }
    public void OnRestart()
    {
        SceneManager.LoadScene("GameScene",LoadSceneMode.Single);
        GameOver = false;
        Time.timeScale = 1.0f;
    }
    public void OnBackTitle()
    {
        SceneManager.LoadScene("TitleScene");
        GameOver = false;
        Time.timeScale = 1.0f;
    }
    public void OnContinue()
    {
        Time.timeScale = 1.0f;
        playerController.ResurrectionPlayer();
        
        GameOver = false;
        GameOverPanel.SetActive(false);
    }
    #endregion

}
