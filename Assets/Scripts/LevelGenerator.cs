using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject outsideCornerPrefab;
    public GameObject outsideWallPrefab;
    public GameObject insideCornerPrefab;
    public GameObject insideWallPrefab;
    public GameObject pelletPrefab;
    public GameObject powerPelletPrefab;
    public GameObject tJunctionPrefab;
    public GameObject quadrantParentPrefab;
    
    public float tileSize = 1f;  // Tile size
    [SerializeField] Vector2 startPosition; // Starting position of the quadrant

    // The level layout map (2D array)
    private readonly int[,] levelMap =
    {
        { 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 7 },
        { 2, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 4 },
        { 2, 5, 3, 4, 4, 3, 5, 3, 4, 4, 4, 3, 5, 4 },
        { 2, 6, 4, 0, 0, 4, 5, 4, 0, 0, 0, 4, 5, 4 },
        { 2, 5, 3, 4, 4, 3, 5, 3, 4, 4, 4, 3, 5, 3 },
        { 2, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
        { 2, 5, 3, 4, 4, 3, 5, 3, 3, 5, 3, 4, 4, 4 },
        { 2, 5, 3, 4, 4, 3, 5, 4, 4, 5, 3, 4, 4, 3 },
        { 2, 5, 5, 5, 5, 5, 5, 4, 4, 5, 5, 5, 5, 4 },
        { 1, 2, 2, 2, 2, 1, 5, 4, 3, 4, 4, 3, 0, 4 },
        { 0, 0, 0, 0, 0, 2, 5, 4, 3, 4, 4, 3, 0, 3 },
        { 0, 0, 0, 0, 0, 2, 5, 4, 4, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 2, 5, 4, 4, 0, 3, 4, 4, 0 },
        { 2, 2, 2, 2, 2, 1, 5, 3, 3, 0, 4, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 5, 0, 0, 0, 4, 0, 0, 0 }
    };

    private List<GameObject> originalTiles = new(); // Store original tiles

    private void Awake()
    {
        // Initialize startPosition
        startPosition = new Vector2(-13.5f * tileSize, 14f * tileSize);
        Transform fullMap = transform.Find("Full Map"); 
        if (fullMap is not null)
            fullMap.gameObject.SetActive(false);
    }

    void Start()
    {
        // Create the first quadrant
        GameObject firstQuadrantParent = CreateQuadrant(Vector2.zero, "FirstQuadrant");

        // Duplicate and mirror for the other quadrants
        CreateMirroredQuadrants(firstQuadrantParent);
    }

    // Store the first quadrant under a parent GameObject
    GameObject CreateQuadrant(Vector2 offset, string quadrantName)
    {
        // Create a new empty GameObject to act as the parent for this quadrant
        GameObject quadrantParent = Instantiate(quadrantParentPrefab, Vector3.zero, Quaternion.identity, transform);
        quadrantParent.name = quadrantName;

        // Loop through the level map and instantiate the tiles
        for (int y = 0; y < levelMap.GetLength(0); y++)
        {
            for (int x = 0; x < levelMap.GetLength(1); x++)
            {
                Vector2 position = startPosition + new Vector2(x * tileSize, -y * tileSize) + offset;
                int tileType = levelMap[y, x];

                GameObject tilePrefab = GetPrefabForTile(tileType);
                if (tilePrefab == null) continue;

                // Instantiate the tile and parent it to the quadrant
                GameObject tileInstance = Instantiate(tilePrefab, position, Quaternion.identity, quadrantParent.transform);

                // Apply rotations based on tile type
                ApplyRotation(tileInstance, tileType, x, y);
            }
        }
        return quadrantParent;
    }

    // Mirror the first quadrant to create the other quadrants
    void CreateMirroredQuadrants(GameObject firstQuadrantParent)
    {
        // Duplicate and Scale X axis -1
        GameObject secondQuadrant = Instantiate(firstQuadrantParent, Vector3.zero, Quaternion.identity, transform);
        secondQuadrant.transform.localScale = new Vector3(-1, 1, 1); // Mirror on X axis
        secondQuadrant.name = "SecondQuadrant";

        // Duplicate and Scale  Y axis -1
        GameObject thirdQuadrant = Instantiate(firstQuadrantParent, Vector3.zero, Quaternion.identity, transform);
        thirdQuadrant.transform.localScale = new Vector3(1, -1, 1); // Mirror on Y axis
        thirdQuadrant.name = "ThirdQuadrant";

        // Duplicate and Scale X, Y axis -1
        GameObject fourthQuadrant = Instantiate(firstQuadrantParent, Vector3.zero, Quaternion.identity, transform);
        fourthQuadrant.transform.localScale = new Vector3(-1, -1, 1); // Mirror on both X and Y axes
        fourthQuadrant.name = "FourthQuadrant";
    }

    // Retrieve the correct prefab for the given tile type
    GameObject GetPrefabForTile(int tileType)
    {
        return tileType switch
        {
            1 => outsideCornerPrefab,
            2 => outsideWallPrefab,
            3 => insideCornerPrefab,
            4 => insideWallPrefab,
            5 => pelletPrefab,
            6 => powerPelletPrefab,
            7 => tJunctionPrefab,
            _ => null
        };
    }
    private void ApplyRotation(GameObject tileInstance, int tileType, int x, int y)
    {
        int rightNeighbor = -1;
        int leftNeighbor = -1;
        int aboveNeighbor = -1;
        int belowNeighbor = -1;
        var position = startPosition + new Vector2(x * tileSize, -y * tileSize);

        if (x + 1 < levelMap.GetLength(1))
        {
            rightNeighbor = levelMap[y, x + 1];
        }

        if (y + 1 < levelMap.GetLength(0))
        {
            belowNeighbor = levelMap[y + 1, x];
        }

        if (x - 1 >= 0)
        {
            leftNeighbor = levelMap[y, x - 1];
        }

        if (y - 1 >= 0)
        {
            aboveNeighbor = levelMap[y - 1, x];
        }

        GameObject tilePrefab = null;

        switch (tileType)
        {
            case 1: // Outside Corner
                tilePrefab = outsideCornerPrefab;
                break;

            case 2: // Outside Wall
                tilePrefab = outsideWallPrefab;
                break;

            case 3: // Inside Corner
                tilePrefab = insideCornerPrefab;
                break;

            case 4: // Inside Wall
                tilePrefab = insideWallPrefab;
                break;

            case 5: // Pellet
                tilePrefab = pelletPrefab;
                break;

            case 6: // Power Pellet
                tilePrefab = powerPelletPrefab;
                break;

            case 7: // T-Junction
                tilePrefab = tJunctionPrefab;
                break;
        }

        if (tilePrefab != null)
        {
            if (tileType == 1) // Outside Corner
            {
                // Check right neighbor
                if (rightNeighbor != 1 && rightNeighbor != 2 && rightNeighbor != 7)
                {
                    tileInstance.transform.rotation = Quaternion.Euler(0, 0, 270); // Rotate 90 degrees
                    if (aboveNeighbor == 1 || aboveNeighbor == 2 || aboveNeighbor == 7)
                    {
                        tileInstance.transform.rotation = Quaternion.Euler(0, 0, 180); // Rotate 180 degrees
                    }
                }
                // Check upper neighbor
                else if (aboveNeighbor == 1 || aboveNeighbor == 2 || aboveNeighbor == 7)
                {
                    tileInstance.transform.rotation = Quaternion.Euler(0, 0, 90); // Rotate 180 degrees
                }
            }
            
            if (tileType == 2) // Outside Wall
            {
                if (aboveNeighbor == 1 || aboveNeighbor == 2)
                {
                    tileInstance.transform.rotation = Quaternion.Euler(0, 0, 90); // Rotate 90 degrees
                }

                if ((rightNeighbor == 1 && leftNeighbor == 2) ||
                    (rightNeighbor == 2 && leftNeighbor == 1))
                {
                    tileInstance.transform.rotation = Quaternion.identity;
                }
            }

            // Handle rotation for inside corners
            if (tileType == 3) // Inside Corner
            {
                if (rightNeighbor == 3 || rightNeighbor == 4)
                {
                    if (aboveNeighbor == 3 || aboveNeighbor == 4 || aboveNeighbor == 7)
                    {
                        tileInstance.transform.rotation *= Quaternion.Euler(0, 0, 90); // Rotate 180 degrees
                    }
                }

                if (leftNeighbor == 3 || leftNeighbor == 4)
                {
                    tileInstance.transform.rotation = Quaternion.Euler(0, 0, 270);
                    if (aboveNeighbor == 3 || aboveNeighbor == 4 || aboveNeighbor == 7)
                    {
                        tileInstance.transform.rotation = Quaternion.Euler(0, 0, 180);
                    }
                }

                if (aboveNeighbor == 3 || aboveNeighbor == 4 || aboveNeighbor == 7)
                {
                    if (rightNeighbor != 3 && rightNeighbor != 4)
                    {
                        tileInstance.transform.rotation = Quaternion.Euler(0, 0, 180);
                    }

                    if (belowNeighbor == 4)
                    {
                        tileInstance.transform.rotation = Quaternion.Euler(0, 0, 90);

                        if (aboveNeighbor == 4 && leftNeighbor == 4)
                        {
                            tileInstance.transform.rotation = Quaternion.Euler(0, 0, 270);
                        }

                        if (aboveNeighbor == 3 && leftNeighbor == 4)
                        {
                            tileInstance.transform.rotation = Quaternion.Euler(0, 0, 0);
                        }
                    }
                    else if (belowNeighbor == 3 && leftNeighbor == 4)
                    {
                        tileInstance.transform.rotation = Quaternion.Euler(0, 0, 90);
                    }
                    else if (rightNeighbor == -1)
                    {
                        tileInstance.transform.rotation = Quaternion.Euler(0, 0, 90);
                    }
                }
            }

            // Handle rotation for inside walls
            if (tileType == 4) // Inside Wall
            {
                if (aboveNeighbor == 3 || aboveNeighbor == 4 || aboveNeighbor == 7) // Not an interior wall
                {
                    tileInstance.transform.rotation = Quaternion.Euler(0, 0, 90); // Rotate 90 degrees
                }

                if ((rightNeighbor == 3 && leftNeighbor == 4) ||
                    (rightNeighbor == 4 && leftNeighbor == 3))
                {
                    tileInstance.transform.rotation = Quaternion.identity;
                }
            }
        }
    }
}