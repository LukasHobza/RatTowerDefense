using UnityEngine;
using UnityEngine.UI;

public class TowerUpgrade : MonoBehaviour
{
    public Tower towerScript; // Odkaz na skript vìže
    public GameObject upgradeMenu; // Menu pro vylepšení vìže
    public Text upgradeLevelText; // Text pro zobrazení úrovnì vylepšení
    public Button upgradeButton; // Tlaèítko pro vylepšení
    public Button closeButton; // Tlaèítko pro zavøení menu

    private int maxUpgradeLevel = 4; // Maximální úroveò vylepšení

    void Start()
    {
        // Skrytí menu pøi startu hry
        upgradeMenu.SetActive(false);

        // Nastavení akce pro tlaèítko vylepšení
        upgradeButton.onClick.AddListener(UpgradeTower);
        // Nastavení akce pro tlaèítko zavøení
        closeButton.onClick.AddListener(CloseUpgradeMenu);
    }

    void Update()
    {
        // Pokud hráè klikne na vìž, otevøe se menu pro vylepšení
        if (Input.GetMouseButtonDown(0)) // Left click
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject == towerScript.gameObject)
            {
                OpenUpgradeMenu();
            }
        }
    }

    // Otevøe menu pro vylepšení
    void OpenUpgradeMenu()
    {
        upgradeMenu.SetActive(true);
        upgradeLevelText.text = "Upgrade Level: " + towerScript.upgradeLevel;
        upgradeButton.interactable = towerScript.upgradeLevel < maxUpgradeLevel; // Zablokování tlaèítka, pokud je úroveò maximální
    }

    // Zavøe menu pro vylepšení
    void CloseUpgradeMenu()
    {
        upgradeMenu.SetActive(false);
    }

    // Metoda pro vylepšení vìže
    void UpgradeTower()
    {
        if (towerScript.upgradeLevel < maxUpgradeLevel)
        {
            towerScript.upgradeLevel++; // Zvýšení úrovnì vylepšení
            // Mùžeš zde pøidat další zmìny pro vylepšenou vìž, napø. zvýšení damage, dosahu, atd.
            towerScript.damage += 10; // Napøíklad zvýšení damage o 10 pøi každém vylepšení

            upgradeLevelText.text = "Upgrade Level: " + towerScript.upgradeLevel;

            // Zvuk nebo vizuální efekt pøi vylepšení (pokud je k dispozici)
            // Pokud máš zvuk pro vylepšení
            // towerScript.upgradeSound.Play();
        }
    }
}
