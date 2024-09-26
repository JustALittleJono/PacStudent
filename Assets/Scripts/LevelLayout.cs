using System.Collections.Generic;
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
    public float tileSize = 1f;
    private Vector2 startPosition;
     

    private List<GameObject> originalTiles = new(); // Store original tiles

       private void Start()
    {
        startPosition = new(-13.5f*tileSize, 14.5f*tileSize);
        // First, create the top-left quadrant (original layout with proper rotations)
        CreateQuadrant(Vector2.zero);

        // Now, mirror the quadrant to create the other quadrants
        MirrorQuadrants();
    }

    private void CreateQuadrant(Vector2 offset)
    {
        for (var y = 0; y < levelMap.GetLength(0); y++)
        {
            for (var x = 0; x < levelMap.GetLength(1); x++)
            {
                Vector2 position = startPosition + new Vector2(x * tileSize, -y * tileSize) + offset;
                int tileType = levelMap[y, x];

                GameObject tilePrefab = GetPrefabForTile(tileType);
                if (tilePrefab == null) continue;

                GameObject tileInstance = Instantiate(tilePrefab, position, Quaternion.identity, transform);

                // Apply rotation based on neighbors for the original quadrant
                ApplyRotation(tileInstance, tileType, x, y);

                // Store the original tile for later mirroring
                originalTiles.Add(tileInstance);
            }
        }
    }

    private void MirrorQuadrants()
    {
        // Get the width and height of the original quadrant
        float width = (levelMap.GetLength(1) - 1) * tileSize; // Subtract one tile size to remove extra spacing
        float height = (levelMap.GetLength(0) - 1) * tileSize; // Subtract one tile size to remove extra spacing

        // Top-right (X-axis Mirrored)
        MirrorQuadrant(new Vector2(width, 0), true, false);

        // Bottom-left (Y-axis Mirrored)
        MirrorQuadrant(new Vector2(0, -height), false, true);

        // Bottom-right (X and Y-axis Mirrored)
        MirrorQuadrant(new Vector2(width, -height), true, true);
    }

    private void MirrorQuadrant(Vector2 offset, bool mirrorX, bool mirrorY)
    {
        foreach (GameObject tile in originalTiles)
        {
            // Get the position of the original tile
            Vector2 originalPosition = tile.transform.position;

            // Calculate the mirrored position based on the X and Y axes
            float mirroredX = mirrorX ? -originalPosition.x + 2 * startPosition.x + (levelMap.GetLength(1)) * tileSize : originalPosition.x;
            float mirroredY = mirrorY ? -originalPosition.y + 2 * startPosition.y - (levelMap.GetLength(0)) * tileSize : originalPosition.y;
            Vector2 mirroredPosition = new Vector2(mirroredX, mirroredY) + offset;

            // Apply the correct mirrored rotation based on the axes
            Quaternion mirroredRotation = tile.transform.rotation;

            if (mirrorX && mirrorY)
            {
                mirroredRotation *= Quaternion.Euler(0, 0, 180);  // Flip both X and Y (rotate 180 degrees)
            }
            else if (mirrorX)
            {
                mirroredRotation *= Quaternion.Euler(0, 180, 0);  // Flip along X (horizontal flip)
            }
            else if (mirrorY)
            {
                mirroredRotation *= Quaternion.Euler(180, 0, 0);  // Flip along Y (vertical flip)
            }

            // Instantiate the mirrored version of the tile with the mirrored position and rotation
            Instantiate(tile.gameObject, mirroredPosition, mirroredRotation, transform);
        }
    }

    private GameObject GetPrefabForTile(int tileType)
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