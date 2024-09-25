using UnityEngine;

public class LevelLayout : MonoBehaviour
{
    public GameObject outsideCornerPrefab;
    public GameObject outsideWallPrefab;
    public GameObject insideCornerPrefab;
    public GameObject insideWallPrefab;
    public GameObject pelletPrefab;
    public GameObject powerPelletPrefab;
    public GameObject tJunctionPrefab;


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

    // Set the starting position (top left corner of the grid)
    public Vector2 startPosition = new(-7, 7); // Adjust based on your scene setup
    public float tileSize = 1f; // Size of each tile in world units (match with sprite size)

    private void Start()
    {
        CreateLevel();
    }

    private void CreateLevel()
    {
        
        for (var y = 0; y < levelMap.GetLength(0); y++)
        for (var x = 0; x < levelMap.GetLength(1); x++)
        {
            int rightNeighbor = 0;
            int leftNeighbor = 0;
            int aboveNeighbor = 0;
            int belowNeighbor = 0;
            var position = startPosition + new Vector2(x * tileSize, -y * tileSize);
            var tileType = levelMap[y, x];
            
            if (x+1 < levelMap.GetLength(1))
            {rightNeighbor = levelMap[y, x+1];}
            if (x-1 >= 0)
            {leftNeighbor = levelMap[y, x-1];}
            
            if (y-1 >= 0)
            {aboveNeighbor = levelMap[y-1, x];}
            if (y+1 < levelMap.GetLength(0))
            {belowNeighbor = levelMap[y+1, x];}
            
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
                // Instantiate the prefab first, then apply rotation based on conditions
                GameObject tileInstance = Instantiate(tilePrefab, position, Quaternion.identity, transform);

                // Handle rotation for outside corners
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

                // Handle rotation for outside walls
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
                    if (rightNeighbor != 3 && rightNeighbor != 4)
                    {
                        tileInstance.transform.rotation = Quaternion.Euler(0, 0, 270); // Rotate 90 degrees
                        if (aboveNeighbor == 3 ||  aboveNeighbor == 4)
                        {
                            tileInstance.transform.rotation = Quaternion.Euler(0, 0, 180); // Rotate 180 degrees
                        }
                    }

                    if (aboveNeighbor == 3 && (belowNeighbor == 3 || belowNeighbor == 4))
                    {
                        tileInstance.transform.rotation = Quaternion.Euler(0, 0, 90);
                        if (belowNeighbor == 3)
                        {
                            tileInstance.transform.rotation = Quaternion.Euler(0, 0, 270);
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
}