using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class DoorUIScript : MonoBehaviour
{
    public GameObject Panel;
    int DoorCount = 4;
    private DoorScript[] doors;
    private bool[] Flag;
    // Start is called before the first frame update
    void Start()
    {

        GameObject[] door = GameObject.FindGameObjectsWithTag("Door");
        doors = new DoorScript[DoorCount];
        Flag = new bool[DoorCount];
        for (int i = 0; i < DoorCount; i++)
        {
            doors[i] = door[i].GetComponent<DoorScript>();
            Flag[i] = true;
        }

        Panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < DoorCount; i++)
        {
            if (doors[i].OpenFlag && Flag[i])
            {
                Flag[i] = false;
                OnPanel();
                Invoke("OffPanel", 3.0f);
            }
        }
    }

    void OnPanel()
    {
        Panel.SetActive(true);
    }

    void OffPanel()
    {
        Panel.SetActive(false);
    }
}
