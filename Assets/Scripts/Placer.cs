using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class TilemapPlacer : MonoBehaviour
{
    public Tilemap tilemap; // Reference to your Tilemap
    public GameObject[] prefabs; // Array of prefabs to instantiate
    private int currentPrefabIndex = 0; // Index to track the currently selected prefab

    void Start()
    {
        // Set up button listeners
        for (int i = 0; i < prefabs.Length; i++)
        {
            int index = i; // Capture the current index
            Button button = GameObject.Find("Button" + (i + 1)).GetComponent<Button>();
            button.onClick.AddListener(() => SelectPrefab(index));
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Check for left mouse button click
        {
            PlacePrefab();
        }
    }

    void SelectPrefab(int index)
    {
        currentPrefabIndex = index; // Set the selected prefab index
        Debug.Log("Selected prefab: " + prefabs[currentPrefabIndex].name);
    }

    void PlacePrefab()
    {
        // Get the mouse position in world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Convert the world position to the Tilemap's cell position
        Vector3Int cellPosition = tilemap.WorldToCell(mousePosition);

        if (IsCellOccupied(cellPosition))
        {
            Debug.Log("Cell is already occupied!");
            return; // Exit if the cell is occupied
        }

        // Check if the tilemap cell is empty
        if (tilemap.GetTile(cellPosition) == null)
        {
            // Instantiate the selected prefab at the cell position
            Instantiate(prefabs[currentPrefabIndex], tilemap.GetCellCenterWorld(cellPosition), Quaternion.identity);
        }
    }

    bool IsCellOccupied(Vector3Int cellPosition)
    {
        // Check for any GameObjects in the vicinity of the cell position
        Collider2D[] colliders = Physics2D.OverlapCircleAll(tilemap.GetCellCenterWorld(cellPosition), 0.1f);
        return colliders.Length > 0; // Return true if there's at least one collider (indicating occupancy)
    }
}
