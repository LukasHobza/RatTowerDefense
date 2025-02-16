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

    [SerializeField] private AudioSource shootSound; // Zvuk st�elby

    // Odkaz na AudioManager
    private AudioManager audioManager;


    void Start()
    {
        // Z�sk�n� reference na AudioManager
        audioManager = FindObjectOfType<AudioManager>();
    }

    void FixedUpdate()
    {
        GameObject[] enemyList = GameObject.FindGameObjectsWithTag("Enemy"); // list v�ech enemy

        // Pokud v� nem� target, najde si nov�ho
        if (targetEnemy == null || Vector2.Distance(targetEnemy.transform.position, rb.transform.position) >= (range + dorimeRangeBoost))
        {
            foreach (GameObject enemyInList in enemyList) // Projde v�echny nep��tele
            {
                if (enemyInList != null)
                {
                    if (Vector2.Distance(enemyInList.transform.position, rb.transform.position) <= (range + dorimeRangeBoost)) // Pokud je nep��tel bl�zko v�e
                    {
                        if (!enemyInList.gameObject.GetComponent<Enemy>().isInvisible)
                        {
                            targetEnemy = enemyInList; // Nastav� nov�ho targeta
                            break;
                        }
                    }
                }
            }
        }
        else
        {
            // Rotace v�e k nep��teli
            Vector2 direction = (targetEnemy.GetComponent<EnemyMovement>().rb.transform.position - transform.position);
            Vector3 targetForwardDirection = direction;
            Quaternion targetRotation = Quaternion.LookRotation(targetForwardDirection);
            if (targetRotation.y > 0.5f) targetRotation = Quaternion.Euler(0f, 0f, -180f) * targetRotation;
            rb.MoveRotation(targetRotation);

            // Kontrola cooldownu st�elby
            if ((coolDownNum - dorimeCoolDownBoost) <= 0)
            {
                GameObject bulletToSpawn = bullet;
                bulletToSpawn.GetComponent<Bullet>().targetForwardDirection = targetForwardDirection;
                bulletToSpawn.GetComponent<Bullet>().target = targetEnemy.transform;
                Instantiate(bulletToSpawn, this.transform.position, Quaternion.identity); // Spawne st�elu

                // Nastaven� hodnot st�ely
                bulletToSpawn.GetComponent<Bullet>().damage = damage + dorimeDamageBoost;
                bulletToSpawn.GetComponent<Bullet>().armorDamage = armorDamage;
                bulletToSpawn.GetComponent<Bullet>().slowPower = slowPower;
                bulletToSpawn.GetComponent<Bullet>().slowDuration = slowDuration;
                bulletToSpawn.GetComponent<Bullet>().rangeDamage = rangeDamage;

                // P�ehraje zvukov� efekt st�elby s hlasitost� ovlivn�nou AudioManagerem
                if (shootSound != null && audioManager != null)
                {
                    shootSound.volume = audioManager.GetSFXVolume(); // Nastav� hlasitost podle AudioManageru
                    shootSound.Play();
                }

                // Resetuje cooldown
                coolDownNum = coolDown;
            }
            else coolDownNum--;
        }
    }
}
