using UnityEngine;
using System.Collections;

public class Tree : MonoBehaviour
{
    public Spawner redSpawner;
    public Spawner blueSpawner;

    public MusicController musicController;
    public int band;

    public float redRate;
    public float blueRate;
    public float greenRate;

    public int minRed;
    public int minBlue;
    public int minGreen;

    public int maxRed;
    public int maxBlue;
    public int maxGreen;

    int r,g,b;      //Values
    int rs, gs, bs; //Signs

    void Start()
    {
        r = 0;
        g = 0;
        b = 0;

        rs = 1;
        gs = 1;
        bs = 1;
    }
	
    void Update()
    {
        float music = musicController.GetBandData(band);
        int extraRed = (redSpawner.income - 1) * 20;
        int extraBlue = (blueSpawner.income - 1) * 20;

        if (r >= maxRed || r <= minRed) {rs *= -1;}
        if (g >= maxGreen || g <= minGreen) {gs *= -1;}
        if (b >= maxBlue || b <= minBlue) {bs *= -1;}

        r += (int)(rs * redRate * music) + extraRed;
        b += (int)(bs * blueRate * music) + extraBlue;
        g += (int)(gs * greenRate * music);

        if (r > maxRed) {r = maxRed;}
        if (r < minRed) {r = minRed;}
        if (g > maxGreen) {g = maxGreen;}
        if (g < minGreen) {g = minGreen;}
        if (b > maxBlue) {b = maxBlue;}
        if (b < minBlue) {b = minBlue;}

        GetComponent<Renderer>().material.SetColor("_Color", new Color32((byte)r, (byte)g, (byte)b, 0xFF));
    }
}
