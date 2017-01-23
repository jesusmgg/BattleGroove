using UnityEngine;
using System.Collections;


public class Checkpoint : MonoBehaviour
{
    public Timer captureTimer;
    public float captureTime;

    [SerializeField]
    int blueUnits = 0;
    [SerializeField]
    int redUnits = 0;

    int capturingPlayer = -1;

    void Start()
    {
        captureTimer = gameObject.AddComponent<Timer>();
        captureTimer.Stop();
    }

    void Update()
    {
        if (GetComponent<Stats>().player == -1)
        {
            if (redUnits > blueUnits)
            {
                capturingPlayer = 1;
                captureTimer.Start();
            }
            else if (redUnits < blueUnits)
            {
                capturingPlayer = 0;
                captureTimer.Start();
            }
            else
            {
                capturingPlayer = -1;
                captureTimer.Stop();
            }
        }

        else if (GetComponent<Stats>().player == 0)
        {
            if (redUnits > blueUnits)
            {
                capturingPlayer = 1;
                captureTimer.Start();
            }
            else
            {
                capturingPlayer = -1;
                captureTimer.Stop();
            }
        }

        else if (GetComponent<Stats>().player == 1)
        {
            if (redUnits < blueUnits)
            {
                capturingPlayer = 0;
                captureTimer.Start();
            }
            else
            {
                capturingPlayer = -1;
                captureTimer.Stop();
            }
        }

        if (captureTimer.GetTime() >= captureTime)
        {
            GetComponent<Stats>().player = capturingPlayer;
            capturingPlayer = -1;
            captureTimer.Stop();
        }

        if (GetComponent<Stats>().player == 0)
        {
            GetComponent<Renderer>().material.SetColor("_Color", new Color32(0x06, 0x08, 0xB8, 0xFF));
            GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color32(0x06, 0x08, 0xB8, 0xFF));
        }
        else if (GetComponent<Stats>().player == 1)
        {
            GetComponent<Renderer>().material.SetColor("_Color", new Color32(0xC1, 0x06, 0x08, 0xFF));
            GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color32(0xC1, 0x06, 0x08, 0xFF));
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Unit")
        {
            if (collider.gameObject.GetComponent<Stats>().player == 0)
            {
                blueUnits++;
            }
            else if (collider.gameObject.GetComponent<Stats>().player == 1)
            {
                redUnits++;
            }
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Unit")
        {
            if (collider.gameObject.GetComponent<Stats>().player == 0)
            {
                blueUnits--;
            }
            else if (collider.gameObject.GetComponent<Stats>().player == 1)
            {
                redUnits--;
            }
        }
    }
}
