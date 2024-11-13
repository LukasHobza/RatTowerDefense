using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpManager : MonoBehaviour
{
    public static HpManager hM;
    public Text hpText;
    public int hp = 100;

    private void Awake()
    {
        hM = this;
    }

    private void Update()
    {
        hpText.text = hp.ToString();
    }
}
