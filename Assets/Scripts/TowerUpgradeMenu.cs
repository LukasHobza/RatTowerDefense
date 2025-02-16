using UnityEngine;
using UnityEngine.UI;

public class TowerUpgradeMenu : MonoBehaviour
{
    public GameObject upgradeMenuPanel; // Panel pro upgrade
    public Text towerNameText; // Text pro název věže
    public Text upgradeLevelText; // Text pro úroveň a počet zbývajících vylepšení
    public Button damageUpgradeButton; // Tlačítko pro vylepšení poškození
    public Button rangeUpgradeButton; // Tlačítko pro vylepšení dosahu
    public Button slowUpgradeButton; // Tlačítko pro vylepšení zpomalení
    public Button sellButton; // Tlačítko pro prodej věže
    public Button closeButton; // Tlačítko pro zavření menu

    private Tower selectedTower; // Odkaz na vybranou věž

    void Start()
    {
        // Zavřít panel při startu
        upgradeMenuPanel.SetActive(false);

        // Připojit funkce k tlačítkům
        damageUpgradeButton.onClick.AddListener(() => UpgradeDamage());
        rangeUpgradeButton.onClick.AddListener(() => UpgradeRange());
        slowUpgradeButton.onClick.AddListener(() => UpgradeSlow());
        sellButton.onClick.AddListener(() => SellTower());
        closeButton.onClick.AddListener(() => CloseUpgradeMenu());
    }

    public void OpenUpgradeMenu(Tower tower)
    {
        selectedTower = tower; // Uložit vybranou věž
        upgradeMenuPanel.SetActive(true); // Zobrazit menu

        // Aktualizace informací o věži v menu
        towerNameText.text = "Věž: " + tower.towerName; // Zobrazení názvu věže
        upgradeLevelText.text = "Úroveň: " + tower.upgradeLevel + "/4"; // Zobrazení úrovně a zbývajících vylepšení

        // Zakázat tlačítka, pokud je věž na maximální úrovni
        damageUpgradeButton.interactable = tower.upgradeLevel < 4;
        rangeUpgradeButton.interactable = tower.upgradeLevel < 4;
        slowUpgradeButton.interactable = tower.upgradeLevel < 4;
    }

    public void CloseUpgradeMenu()
    {
        upgradeMenuPanel.SetActive(false); // Zavřít upgrade menu
    }

    private void UpgradeDamage()
    {
        if (selectedTower.upgradeLevel < 4)
        {
            selectedTower.damage += 10; // Příklad zvyšování poškození
            selectedTower.upgradeLevel++; // Zvýšení úrovně
            Debug.Log("Poškození věže zvýšeno na: " + selectedTower.damage);
            UpdateUpgradeMenu(); // Aktualizace menu
        }
    }

    private void UpgradeRange()
    {
        if (selectedTower.upgradeLevel < 4)
        {
            selectedTower.range += 5; // Příklad zvyšování dosahu
            selectedTower.upgradeLevel++; // Zvýšení úrovně
            Debug.Log("Dosah věže zvýšen na: " + selectedTower.range);
            UpdateUpgradeMenu(); // Aktualizace menu
        }
    }

    private void UpgradeSlow()
    {
        if (selectedTower.upgradeLevel < 4)
        {
            selectedTower.slowPower += 1; // Příklad zvyšování zpomalení
            selectedTower.upgradeLevel++; // Zvýšení úrovně
            Debug.Log("Zpomalení věže zvýšeno na: " + selectedTower.slowPower);
            UpdateUpgradeMenu(); // Aktualizace menu
        }
    }

    private void SellTower()
    {
        // Prodej věže (například přidání peněz zpět)
        CoinManager.cM.coin += selectedTower.upgradeLevel * 50; // Příklad prodeje věže
        Debug.Log("Věž prodána za: " + (selectedTower.upgradeLevel * 50) + " mincí");
        Destroy(selectedTower.gameObject); // Smazání věže
        CloseUpgradeMenu(); // Zavření menu
    }

    private void UpdateUpgradeMenu()
    {
        // Aktualizace textu pro úroveň
        upgradeLevelText.text = "Úroveň: " + selectedTower.upgradeLevel + "/4";
        damageUpgradeButton.interactable = selectedTower.upgradeLevel < 4;
        rangeUpgradeButton.interactable = selectedTower.upgradeLevel < 4;
        slowUpgradeButton.interactable = selectedTower.upgradeLevel < 4;
    }
}
