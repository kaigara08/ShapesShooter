using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIScript : MonoBehaviour
{
    private PlayerController playerController;

    public GameObject HPCorePrefab;
    private List<GameObject> HPList = new List<GameObject>();
    private int hpCount = 0;

    public GameObject energyScrollbar;

    public Text TimeText;
    public static float TimeCount;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        TimeCount = 0;
        for(int i = 0; i < 5; ++i)
        {
            GenerateHPImage();
            hpCount++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        energyScrollbar.GetComponent<Slider>().value = (float)playerController.energy / (float)playerController.MaxEnergy;

        if(!playerController.TimeFlag)
        {
            TimeCount += Time.deltaTime;
            int minute;
            int second;
            float conma;

            second = (int)TimeCount;
            minute = second / 60;
            conma = (TimeCount - second) * 100;
            second = second % 60;

            TimeText.text = minute.ToString("00") + ":" + second.ToString("00.") + "." + conma.ToString("00");
        }
        
    }


    public void GenerateHPImage()
    {
        GameObject HPImage = Instantiate(HPCorePrefab);
        RectTransform rectT = HPImage.GetComponent<RectTransform>();
        rectT.localPosition = new Vector3(100, -100, 0) + new Vector3(140, 0, 0) * hpCount;
        HPImage.transform.SetParent(transform, false);
        HPList.Add(HPImage);
    }

    public void SetHPImage(int nub,int count)
    {
        for (int i = 0; i < count; ++i)
        {
            if (nub > 0)
            {
                GenerateHPImage();
                hpCount++;
            }
            if (nub < 0)
            {
                Destroy(HPList[hpCount - 1]);
                HPList.RemoveAt(hpCount - 1);
                hpCount--;
            }
        }
    }
}
