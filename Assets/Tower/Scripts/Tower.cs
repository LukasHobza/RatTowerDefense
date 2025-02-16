using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    public Rigidbody2D rb;
    private int coolDownNum = 0;
    public int coolDown;
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

    [SerializeField] private AudioSource shootSound;
    private AudioManager audioManager;

    public int upgradeLevel = 0;
    private const int maxUpgradeLevel = 4;

    public string towerName; // 📌 Název věže pro menu

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    void FixedUpdate()
    {
        GameObject[] enemyList = GameObject.FindGameObjectsWithTag("Enemy");

        if (targetEnemy == null || Vector2.Distance(targetEnemy.transform.position, rb.transform.position) >= (range + dorimeRangeBoost))
        {
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

            if ((coolDownNum - dorimeCoolDownBoost) <= 0)
            {
                GameObject bulletToSpawn = bullet;
                bulletToSpawn.GetComponent<Bullet>().targetForwardDirection = targetForwardDirection;
                bulletToSpawn.GetComponent<Bullet>().target = targetEnemy.transform;
                Instantiate(bulletToSpawn, this.transform.position, Quaternion.identity);

                bulletToSpawn.GetComponent<Bullet>().damage = damage + dorimeDamageBoost;
                bulletToSpawn.GetComponent<Bullet>().armorDamage = armorDamage;
                bulletToSpawn.GetComponent<Bullet>().slowPower = slowPower;
                bulletToSpawn.GetComponent<Bullet>().slowDuration = slowDuration;
                bulletToSpawn.GetComponent<Bullet>().rangeDamage = rangeDamage;

                if (shootSound != null && audioManager != null)
                {
                    shootSound.volume = audioManager.GetSFXVolume();
                    shootSound.Play();
                }

                coolDownNum = coolDown;
            }
            else coolDownNum--;
        }
    }

    // 📌 Kliknutí na věž → otevře menu
    
}
public class TowerClickHandler : MonoBehaviour
{
    private void OnMouseDown() // Tato metoda se volá při kliknutí na věž
    {
        // Zjistíme, zda tento objekt má komponentu Tower
        Tower tower = GetComponent<Tower>(); // Získáme komponentu Tower

        if (tower != null) // Pokud komponenta existuje, pokračujeme
        {
            TowerUpgradeMenu upgradeMenu = FindObjectOfType<TowerUpgradeMenu>();
            if (upgradeMenu != null)
            {
                // Otevřeme upgrade menu a předáme komponentu věže
                upgradeMenu.OpenUpgradeMenu(tower);
            }
        }
    }
}

