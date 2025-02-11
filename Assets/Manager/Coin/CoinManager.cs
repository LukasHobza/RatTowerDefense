using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public static CoinManager cM;
    public Text coinText;
    public int coin = 0;

    private void Awake()
    {
        cM = this;
    }

    private void Update()
    {
        coinText.text = coin.ToString();
    }
}
