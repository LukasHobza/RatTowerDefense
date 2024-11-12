using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class TilemapPlacer : MonoBehaviour
{
    public Tilemap tilemap; // ref na tilemapu
    public GameObject[] prefabs;
    private int currentPrefabIndex = 0;
    public int[] prices;
    private int indexx;
    void Start()
    {
        // tlacitka :)
        for (int i = 0; i < prefabs.Length; i++)
        {
            int index = i;
            Button button = GameObject.Find("Button" + (i + 1)).GetComponent<Button>();
            button.onClick.AddListener(() => SelectPrefab(index));
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // ceka na levy kliknuti
        {
            PlacePrefab();
        }
    }

    void SelectPrefab(int index)
    {
        currentPrefabIndex = index; // nastavi prefab indexem
        indexx = index;
        Debug.Log("Selected prefab: " + prefabs[currentPrefabIndex].name);
    }

    void PlacePrefab()
    {
        // zjisti kam se ma polozit
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // zjisti na jakej cell do tilemapy se ma polozit
        Vector3Int cellPosition = tilemap.WorldToCell(mousePosition);

        if (IsCellOccupied(cellPosition))
        {
            Debug.Log("nn nemuzes uz tam neco je :(");
            return; // exit jestli v cell uz neco je
        }

        // hlida jestli je cell prazdny a jestli ma hrac dost penez
        if (tilemap.GetTile(cellPosition) == null && CoinManager.cM.coin >= prices[indexx])
        {
            // da prefab do cell
            Instantiate(prefabs[currentPrefabIndex], tilemap.GetCellCenterWorld(cellPosition), Quaternion.identity);
            CoinManager.cM.coin -= prices[indexx];
        }
    }

    bool IsCellOccupied(Vector3Int cellPosition)
    {
        // zjistuje jestli v cell neco je
        Collider2D[] colliders = Physics2D.OverlapCircleAll(tilemap.GetCellCenterWorld(cellPosition), 0.1f);
        return colliders.Length > 0; // vrati true jestli v cell neco je
    }
}
