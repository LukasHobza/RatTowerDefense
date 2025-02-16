using UnityEngine;

public class TowerSelection : MonoBehaviour
{
    public TowerUpgradeMenu upgradeMenu; // Odkaz na UI menu

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Levé kliknutí
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject.CompareTag("tower")) // Kliknutí na vìž (opravený tag)
            {
                Tower tower = hit.collider.gameObject.GetComponent<Tower>();
                upgradeMenu.OpenUpgradeMenu(tower); // Otevøe menu pro upgrade vìže
            }
        }
    }
}
