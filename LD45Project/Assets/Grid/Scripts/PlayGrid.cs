using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayGrid : MonoBehaviour
{
    [SerializeField] private float tileSize;
    [SerializeField] private Vector2 gridSize;
    private Vector2Int gridPoints;
    //tiles states
    public enum TileStatus { Empty, Free, Blocked }
    private TileStatus[,] tiles;
    //objects in grid
    public List<GameObject> inGridObjects;

    // Start is called before the first frame update
    void Start()
    {
        //setup grid
        gridPoints.x = (int)((gridSize.x - transform.position.x) / tileSize);
        gridPoints.y = (int)((gridSize.y - transform.position.y) / tileSize);
        tiles = new TileStatus[gridPoints.x, gridPoints.y];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        //draw grid boundaries
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position + new Vector3(gridSize.x / 2, gridSize.y / 2), new Vector2(gridSize.x, gridSize.y));
    }

    #region GET_SET

    public float getTileSize() { return tileSize; }
    public Vector2 getGridSize() { return gridSize; }
    public Vector2Int getGridPoints() { return gridPoints; }

    #endregion

    #region GRID_HANDLING

    //setup
    public void SetupGridTiles()
    {
        //set every tile to empty
        for(int x = 0; x < tiles.GetLength(0); x++)
        {
            for (int y = 0; y < tiles.GetLength(0); y++)
            {
                tiles[x, y] = TileStatus.Empty;
            }
        }

        //check for object in grid
        foreach(GameObject go in inGridObjects)
        {
            //check if is in grid
            Vector2Int goPoint = getPointFromPosition(go.transform.position);
            if (goPoint.x > 0 &&  goPoint.x <= gridPoints.x && goPoint.y > 0 && goPoint.y <= gridPoints.y)
            {
                //not blocking way

                //blocking way
            }
        }
    }

    //get actual state of tiles in play grid
    public void UpdateGridTiles()
    {
        foreach (TileStatus tile in tiles)
        {
            
        }
    }

    #endregion

    #region GRID_ORIENTATION

    //return vector of tile point in grid
    public Vector2Int getPointFromPosition(Vector2 position)
    {
        int x = (int)((position.x - transform.position.x) / tileSize);
        int y = (int)((position.y - transform.position.y) / tileSize);

        return new Vector2Int(x, y);
    }

    //return position from given coordinates of point
    public Vector3 getGridPositionFromPoint(Vector2Int point)
    {
        float x = transform.position.x + (point.x * tileSize);
        float y = transform.position.y + (point.y * tileSize);

        return new Vector2(x, y);
    }

    //return if position is free to move
    public TileStatus getTileStatusFromPoint(Vector2Int point)
    {
        TileStatus tile = tiles[point.x, point.y];
        return tile;
    }

    #endregion
}
