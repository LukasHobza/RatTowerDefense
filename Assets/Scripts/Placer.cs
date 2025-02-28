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

    //LH
    bool selected = false;

    void Start()
    {
        // tlaèítka :)
        for (int i = 0; i < prefabs.Length; i++)
        {
            int index = i;
            Button button = GameObject.Find("Button" + (i + 1)).GetComponent<Button>();
            button.onClick.AddListener(() => SelectPrefab(index));
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // èeká na levé kliknutí
        {
            PlacePrefab();
        }
    }

    void SelectPrefab(int index)
    {
        currentPrefabIndex = index; // nastaví prefab index
        indexx = index;
        //Debug.Log("Selected prefab: " + prefabs[currentPrefabIndex].name);

        //LH
        selected = true;
    }

    void PlacePrefab()
    {
        // zjistí kam se má položit
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // zjistí na jaký cell do tilemapy se má položit
        Vector3Int cellPosition = tilemap.WorldToCell(mousePosition);

        if (IsCellOccupied(cellPosition))
        {
            Debug.Log("nn nemùžeš už tam nìco je :(");
            return; // exit jestli v cell už nìco je
        }

        // hlídá jestli je cell prázdný a jestli má hráè dost penìz LH: a není game over a jestli bylo neco vybrano
        if (tilemap.GetTile(cellPosition) == null && CoinManager.cM.coin >= prices[indexx] && !HpManager.hM.over && selected)
        {
            // položí prefab do cell
            GameObject placedTower = Instantiate(prefabs[currentPrefabIndex], tilemap.GetCellCenterWorld(cellPosition), Quaternion.identity);
            placedTower.tag = "tower"; // Pøiøadí tag "TOWER" nové vìži
            CoinManager.cM.coin -= prices[indexx];

            // Pøidáme detekci kliknutí na novou vìž
            placedTower.AddComponent<TowerClickHandler>(); // Pøidá komponentu pro detekci kliknutí

            //LH:
            selected = false;
        }
    }

    bool IsCellOccupied(Vector3Int cellPosition)
    {
        // zjistí jestli v cell nìco je
        Collider2D[] colliders = Physics2D.OverlapCircleAll(tilemap.GetCellCenterWorld(cellPosition), 0.1f);
        return colliders.Length > 0; // vrátí true, jestli v cell nìco je
    }
}

