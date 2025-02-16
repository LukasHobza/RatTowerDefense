using UnityEngine;
using UnityEngine.UI;

public class TowerUpgrade : MonoBehaviour
{
    public Tower towerScript; // Odkaz na skript v�e
    public GameObject upgradeMenu; // Menu pro vylep�en� v�e
    public Text upgradeLevelText; // Text pro zobrazen� �rovn� vylep�en�
    public Button upgradeButton; // Tla��tko pro vylep�en�
    public Button closeButton; // Tla��tko pro zav�en� menu

    private int maxUpgradeLevel = 4; // Maxim�ln� �rove� vylep�en�

    void Start()
    {
        // Skryt� menu p�i startu hry
        upgradeMenu.SetActive(false);

        // Nastaven� akce pro tla��tko vylep�en�
        upgradeButton.onClick.AddListener(UpgradeTower);
        // Nastaven� akce pro tla��tko zav�en�
        closeButton.onClick.AddListener(CloseUpgradeMenu);
    }

    void Update()
    {
        // Pokud hr�� klikne na v�, otev�e se menu pro vylep�en�
        if (Input.GetMouseButtonDown(0)) // Left click
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject == towerScript.gameObject)
            {
                OpenUpgradeMenu();
            }
        }
    }

    // Otev�e menu pro vylep�en�
    void OpenUpgradeMenu()
    {
        upgradeMenu.SetActive(true);
        upgradeLevelText.text = "Upgrade Level: " + towerScript.upgradeLevel;
        upgradeButton.interactable = towerScript.upgradeLevel < maxUpgradeLevel; // Zablokov�n� tla��tka, pokud je �rove� maxim�ln�
    }

    // Zav�e menu pro vylep�en�
    void CloseUpgradeMenu()
    {
        upgradeMenu.SetActive(false);
    }

    // Metoda pro vylep�en� v�e
    void UpgradeTower()
    {
        if (towerScript.upgradeLevel < maxUpgradeLevel)
        {
            towerScript.upgradeLevel++; // Zv��en� �rovn� vylep�en�
            // M��e� zde p�idat dal�� zm�ny pro vylep�enou v�, nap�. zv��en� damage, dosahu, atd.
            towerScript.damage += 10; // Nap��klad zv��en� damage o 10 p�i ka�d�m vylep�en�

            upgradeLevelText.text = "Upgrade Level: " + towerScript.upgradeLevel;

            // Zvuk nebo vizu�ln� efekt p�i vylep�en� (pokud je k dispozici)
            // Pokud m� zvuk pro vylep�en�
            // towerScript.upgradeSound.Play();
        }
    }
}
