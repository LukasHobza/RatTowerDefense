using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class HpManager : MonoBehaviour
{
    public static HpManager hM;
    public Text hpText;
    public GameObject gameOver;
    public GameObject canvas;
    public int hp = 100;
    public bool over = false;

    private void Awake()
    {
        hM = this;
    }

    private void Update()
    {
        hpText.text = hp.ToString();
        if(hp<0 && !over)
        {
            over = true;
            Instantiate(gameOver);
            canvas.SetActive(false);
        }
    }
}
