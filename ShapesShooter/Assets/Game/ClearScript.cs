using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClearScript : MonoBehaviour
{
    public Text text;
    public Text TimeText;
    // Start is called before the first frame update
    void Start()
    {
        Button firstButton = GameObject.Find("FirstButton").GetComponent<Button>();
        firstButton.Select();

        float PlayTime = PlayerUIScript.TimeCount;

        int minute;
        int second;
        float conma;

        second = (int)PlayTime;
        minute = second / 60;
        conma = (PlayTime - second) * 100;
        second = second % 60;

        TimeText.text = minute.ToString("00") + ":" + second.ToString("00.") + "." + conma.ToString("00");
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    public void OnReStartButton()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OnTitleButton()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
