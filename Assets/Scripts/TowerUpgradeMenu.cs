using UnityEngine;
using UnityEngine.UI;

public class TowerUpgradeMenu : MonoBehaviour
{
    public GameObject upgradeMenuPanel; // Panel pro upgrade
    public Text towerNameText; // Text pro název věže
    public Text upgradeLevelText; // Text pro úroveň a počet zbývajících vylepšení
    public Button damageUpgradeButton; // Tlačítko pro vylepšení síly (pro normální věže)
    public Button rangeUpgradeButton; // Tlačítko pro vylepšení dosahu
    public Button freezeUpgradeButton; // Tlačítko pro vylepšení zmražení (pro zmrazovací věže)
    public Button speedUpgradeButton; // Tlačítko pro vylepšení rychlosti útoku
    public Button sellButton; // Tlačítko pro prodej věže
    public Button closeButton; // Tlačítko pro zavření menu

    private Tower selectedTower; // Odkaz na vybranou věž
    private int totalInvestment; // Celková investice do věže (kupní cena + upgrady)
    private const int upgradeCost = 10; // Cena za jeden upgrade

    void Start()
    {
        // Zavřít panel při startu
        upgradeMenuPanel.SetActive(false);

        // Připojit funkce k tlačítkům
        damageUpgradeButton.onClick.AddListener(() => UpgradeDamage());
        rangeUpgradeButton.onClick.AddListener(() => UpgradeRange());

        speedUpgradeButton.onClick.AddListener(() => UpgradeSpeed());
        sellButton.onClick.AddListener(() => SellTower());
        closeButton.onClick.AddListener(() => CloseUpgradeMenu());
    }

    public void OpenUpgradeMenu(Tower tower)
    {
        selectedTower = tower; // Uložíme vybranou věž
        totalInvestment = selectedTower.initialCost; // Začínáme s počáteční cenou věže
        upgradeMenuPanel.SetActive(true); // Zobrazit menu

        // Aktualizace informací o věži v menu
        towerNameText.text = "Věž: " + tower.towerName; // Zobrazení názvu věže
        upgradeLevelText.text = "Úroveň: " + tower.upgradeLevel + "/4"; // Zobrazení úrovně a zbývajících vylepšení

        // Zakázat tlačítka podle typu věže a počtu upgradů
        damageUpgradeButton.interactable = CoinManager.cM.coin >= upgradeCost && (selectedTower.upgradeLevel < 4 && selectedTower.damage < 500) && (selectedTower.name == "DefTower(Clone)" || selectedTower.name == "BazookaTower(Clone)");
        rangeUpgradeButton.interactable = CoinManager.cM.coin >= upgradeCost && selectedTower.upgradeLevel < 4 && selectedTower.range < 500;
        speedUpgradeButton.interactable = CoinManager.cM.coin >= upgradeCost && (selectedTower.upgradeLevel < 4 && selectedTower.attackSpeed > 2) && (selectedTower.name == "DefTower(Clone)" || selectedTower.name == "BazookaTower(Clone)" || selectedTower.name == "FreezeTower(Clone)" || selectedTower.name == "SlowTower(Clone)");
    }

    public void CloseUpgradeMenu()
    {
        upgradeMenuPanel.SetActive(false); // Zavřít upgrade menu
    }

    private void UpgradeDamage()
    {
        if (selectedTower.upgradeLevel < 4 && selectedTower.damage < 500)
        {
            CoinManager.cM.coin -= upgradeCost;
            selectedTower.damage += 10; // Zvýšení síly normální věže
            selectedTower.upgradeLevel++; // Zvýšení úrovně
            totalInvestment += upgradeCost; // Přičítání ceny upgradu
            Debug.Log("Poškození věže zvýšeno na: " + selectedTower.damage);
            UpdateUpgradeMenu(); // Aktualizace menu
        }
    }

    private void UpgradeRange()
    {
        if (selectedTower.upgradeLevel < 4 && selectedTower.range < 500)
        {
            CoinManager.cM.coin -= upgradeCost;
            selectedTower.range += 5; // Zvýšení dosahu
            selectedTower.upgradeLevel++; // Zvýšení úrovně
            totalInvestment += upgradeCost; // Přičítání ceny upgradu
            Debug.Log("gsefgwsefwsef: " + selectedTower.upgradeLevel + ", " + selectedTower.range);
            UpdateUpgradeMenu(); // Aktualizace menu
        }
    }

    private void UpgradeSpeed()
    {
        if (selectedTower.upgradeLevel < 4 && selectedTower.attackSpeed > 2)
        {
            CoinManager.cM.coin -= upgradeCost;
            selectedTower.attackSpeed -= 1; // Zvýšení rychlosti útoku (nižší hodnota = vyšší rychlost)
            selectedTower.upgradeLevel++; // Zvýšení úrovně
            totalInvestment += upgradeCost; // Přičítání ceny upgradu
            Debug.Log("Rychlost útoku zvýšena na: " + selectedTower.attackSpeed);
            UpdateUpgradeMenu(); // Aktualizace menu
        }
    }
   
    private void SellTower()
    {
        // Prodej věže (80% z celkové investice)
        int sellPrice = Mathf.RoundToInt(totalInvestment * 0.8f);
        CoinManager.cM.coin += sellPrice; // Přidání mincí hráči
        Debug.Log("Věž prodána za: " + sellPrice + " mincí");
        Destroy(selectedTower.gameObject); // Smazání věže
        CloseUpgradeMenu(); // Zavření menu
    }

    private void UpdateUpgradeMenu()
    {
        // Aktualizace textu pro úroveň
        upgradeLevelText.text = "Úroveň: " + selectedTower.upgradeLevel + "/4";

        // Zakázat tlačítka podle typu věže a počtu upgradů
        damageUpgradeButton.interactable = CoinManager.cM.coin >= upgradeCost && (selectedTower.upgradeLevel < 4 && selectedTower.damage < 500) && (selectedTower.name == "DefTower(Clone)" || selectedTower.name == "BazookaTower(Clone)");
        rangeUpgradeButton.interactable = CoinManager.cM.coin >= upgradeCost && selectedTower.upgradeLevel < 4 && selectedTower.range < 500;
        speedUpgradeButton.interactable = CoinManager.cM.coin >= upgradeCost && (selectedTower.upgradeLevel < 4 && selectedTower.attackSpeed > 2) && (selectedTower.name == "DefTower(Clone)" || selectedTower.name == "BazookaTower(Clone)" || selectedTower.name == "FreezeTower(Clone)" || selectedTower.name == "SlowTower(Clone)");
    }
}
