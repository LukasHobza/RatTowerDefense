using UnityEngine;
using System.Collections;

public class Tower : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    public Rigidbody2D rb;
    private int coolDownNum = 0;
    public int attackSpeed = 4;
    public int range;
    private GameObject targetEnemy;
    public int type;

    public int damage;
    public int armorDamage;
    public int slowPower;
    public int slowDuration;
    public int rangeDamage;

    public int dorimeCoolDownBoost = 0;
    public int dorimeRangeBoost = 0;
    public int dorimeDamageBoost = 0;

    [SerializeField] private AudioSource shootSound;  // Zvuk střely
    [HideInInspector] private AudioManager audioManager;  // Odkaz na AudioManager

    public int upgradeLevel = 0;
    private const int maxUpgradeLevel = 4;
    [HideInInspector] public int initialCost = 5;
    public string towerName; // Název věže pro menu

    private bool isShooting = false;  // Příznak pro kontrolu, zda věž čeká mezi střelami

    void Start()
    {
        // Získání reference na AudioManager
        audioManager = FindObjectOfType<AudioManager>();
        if (shootSound == null)
        {
            Debug.LogError("AudioSource pro shootSound není přiřazený!");
        }

        // Spuštění korutiny pro pravidelný výstřel
        StartCoroutine(ShootWithDelay());
    }

    void FixedUpdate()
    {
        armorDamage = damage / 2;
        GameObject[] enemyList = GameObject.FindGameObjectsWithTag("Enemy");

        if (targetEnemy == null || Vector2.Distance(targetEnemy.transform.position, rb.transform.position) >= (range + dorimeRangeBoost))
        {
            targetEnemy = null;

            foreach (GameObject enemyInList in enemyList)
            {
                if (enemyInList != null && Vector2.Distance(enemyInList.transform.position, rb.transform.position) <= (range + dorimeRangeBoost))
                {
                    if (!enemyInList.gameObject.GetComponent<Enemy>().isInvisible)
                    {
                        targetEnemy = enemyInList;
                        break;
                    }
                }
            }
        }
        else
        {
            Vector2 direction = (targetEnemy.GetComponent<EnemyMovement>().rb.transform.position - transform.position);
            Vector3 targetForwardDirection = direction;
            Quaternion targetRotation = Quaternion.LookRotation(targetForwardDirection);
            if (targetRotation.y > 0.5f) targetRotation = Quaternion.Euler(0f, 0f, -180f) * targetRotation;
            rb.MoveRotation(targetRotation);
        }
    }

    // Funkce pro opětovné umožnění vystřelení (po prodeji věže nebo jiných událostech)
    public void ResetShot()
    {
        StopCoroutine(ShootWithDelay());  // Zastaví aktuální korutinu
        StartCoroutine(ShootWithDelay()); // Restartuje korutinu pro opětovné vystřelení
    }

    private IEnumerator ShootWithDelay()
    {
        while (true)  // Tato smyčka bude běžet neustále
        {

            if (targetEnemy != null)
            {
                // Vystřelit střelu
                Vector2 direction = (targetEnemy.GetComponent<EnemyMovement>().rb.transform.position - transform.position);
                Vector3 targetForwardDirection = direction;

                // Vytvoření střely
                GameObject bulletToSpawn = bullet;
                bulletToSpawn.GetComponent<Bullet>().targetForwardDirection = targetForwardDirection;
                bulletToSpawn.GetComponent<Bullet>().target = targetEnemy.transform;
                Instantiate(bulletToSpawn, this.transform.position, Quaternion.identity);

                bulletToSpawn.GetComponent<Bullet>().damage = damage + dorimeDamageBoost;
                bulletToSpawn.GetComponent<Bullet>().armorDamage = armorDamage;
                bulletToSpawn.GetComponent<Bullet>().slowPower = slowPower;
                bulletToSpawn.GetComponent<Bullet>().slowDuration = slowDuration;
                bulletToSpawn.GetComponent<Bullet>().rangeDamage = rangeDamage;

                // Zvuk výstřelu
                if (shootSound != null && audioManager != null)
                {
                    Debug.Log("Přehrávám zvuk výstřelu s hlasitostí: " + audioManager.GetSFXVolume());
                    shootSound.volume = audioManager.GetSFXVolume();
                    shootSound.PlayOneShot(shootSound.clip);  // Použití PlayOneShot pro přehrání zvuku
                }
                else
                {
                    Debug.LogError("shootSound nebo audioManager je NULL!");
                }

                // Čekáme 3 sekundy, než vystřelíme znovu
                yield return new WaitForSeconds((float)attackSpeed);
            }
            else
            {
                // Pokud není žádný cíl, čekáme, než nějaký najdeme
                yield return null;
            }
        }
    }

    // Kliknutí na věž → otevře menu
    public void OnMouseDown()
    {
        // Zjištění, zda objekt má komponentu Tower
        Tower tower = GetComponent<Tower>(); // Získáme komponentu Tower

        if (tower != null) // Pokud komponenta existuje, pokračujeme
        {
            // Otevření menu pro upgrade věže
            TowerUpgradeMenu upgradeMenu = FindObjectOfType<TowerUpgradeMenu>();
            if (upgradeMenu != null)
            {
                // Předáme komponentu věže do menu pro upgrade
                upgradeMenu.OpenUpgradeMenu(tower);
            }
        }
    }
}
