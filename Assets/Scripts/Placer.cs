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
        // tla��tka :)
        for (int i = 0; i < prefabs.Length; i++)
        {
            int index = i;
            Button button = GameObject.Find("Button" + (i + 1)).GetComponent<Button>();
            button.onClick.AddListener(() => SelectPrefab(index));
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // �ek� na lev� kliknut�
        {
            PlacePrefab();
        }
    }

    void SelectPrefab(int index)
    {
        currentPrefabIndex = index; // nastav� prefab index
        indexx = index;
        //Debug.Log("Selected prefab: " + prefabs[currentPrefabIndex].name);

        //LH
        selected = true;
    }

    void PlacePrefab()
    {
        // zjist� kam se m� polo�it
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // zjist� na jak� cell do tilemapy se m� polo�it
        Vector3Int cellPosition = tilemap.WorldToCell(mousePosition);

        if (IsCellOccupied(cellPosition))
        {
            Debug.Log("nn nem��e� u� tam n�co je :(");
            return; // exit jestli v cell u� n�co je
        }

        // hl�d� jestli je cell pr�zdn� a jestli m� hr�� dost pen�z LH: a nen� game over a jestli bylo neco vybrano
        if (tilemap.GetTile(cellPosition) == null && CoinManager.cM.coin >= prices[indexx] && !HpManager.hM.over && selected)
        {
            // polo�� prefab do cell
            GameObject placedTower = Instantiate(prefabs[currentPrefabIndex], tilemap.GetCellCenterWorld(cellPosition), Quaternion.identity);
            placedTower.tag = "tower"; // P�i�ad� tag "TOWER" nov� v�i
            CoinManager.cM.coin -= prices[indexx];

            // P�id�me detekci kliknut� na novou v�
            placedTower.AddComponent<TowerClickHandler>(); // P�id� komponentu pro detekci kliknut�

            //LH:
            selected = false;
        }
    }

    bool IsCellOccupied(Vector3Int cellPosition)
    {
        // zjist� jestli v cell n�co je
        Collider2D[] colliders = Physics2D.OverlapCircleAll(tilemap.GetCellCenterWorld(cellPosition), 0.1f);
        return colliders.Length > 0; // vr�t� true, jestli v cell n�co je
    }
}

