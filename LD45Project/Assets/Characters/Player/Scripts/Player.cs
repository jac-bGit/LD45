using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //movement properties
    [SerializeField] private int moveSpeed;
    [SerializeField] private bool isMoveingFree;
    public enum PlayerMovement { Stand, Up, Down, Right, Left }
    private PlayerMovement actualMovement;
    public Vector2Int pointStand, pointDestination;

    //references
    [SerializeField] private PlayGrid playGrid;

    // Start is called before the first frame update
    void Start()
    {
        //get components
        //get references

        //setup
        pointStand = new Vector2Int(0, 0);
        pointDestination = new Vector2Int(1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        MoveAlongGrid();
    }

    //make player movement bounded to grid
    void MoveAlongGrid()
    {
        //get input
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        //handle movement
        switch (actualMovement)
        {
            //player is not moving
            case PlayerMovement.Stand:
                //horizontal movemnt
                Vector2Int mp = getMovingPoint(h, v);
                if (pointStand.x + mp.x > 0 && pointStand.x + mp.x < playGrid.getGridPoints().x && pointStand.y + mp.y > 0 && pointStand.y + mp.y < playGrid.getGridPoints().y)
                pointDestination = pointStand + mp;
                //another move
                if (mp.x == 1)
                    actualMovement = PlayerMovement.Right;
                else if (mp.x == -1)
                    actualMovement = PlayerMovement.Left;
                else if (mp.y == 1)
                    actualMovement = PlayerMovement.Up;
                else if (mp.y == -1)
                    actualMovement = PlayerMovement.Down;
                break;
            //player is heading up
            case PlayerMovement.Up:
                //go back down
                if (v < 0)
                {
                    pointDestination.y = pointStand.y;
                    pointStand.y++;
                    actualMovement = PlayerMovement.Down;
                }
                break;
            //player is heading down
            case PlayerMovement.Down:
                //go back up
                if (v > 0)
                {
                    pointDestination.y = pointStand.y;
                    pointStand.y--;
                    actualMovement = PlayerMovement.Up;
                }
                break;
            //player is heading right
            case PlayerMovement.Right:
                //go back left
                if (h < 0)
                {
                    pointDestination.x = pointStand.x;
                    pointStand.x++;
                    actualMovement = PlayerMovement.Left;
                }
                break;
            //player is heading left
            case PlayerMovement.Left:
                //go back left
                if (h > 0)
                {
                    pointDestination.x = pointStand.x;
                    pointStand.x--;
                    actualMovement = PlayerMovement.Right;
                }
                break;
        }

        //movement requare to have cubes in its way
        if (!isMoveingFree)
        {
            if (playGrid.getTileStatusFromPoint(new Vector2Int(pointDestination.x, pointDestination.y)) != PlayGrid.TileStatus.Blocked)
            {

            }
        }
        //free movemnt on grid - for testing and some special situations only
        else
        {
            if (playGrid.getTileStatusFromPoint(new Vector2Int(pointDestination.x, pointDestination.y)) == PlayGrid.TileStatus.Free)
            {

            }
        }


        //do the move if is not on position
        if(transform.position != playGrid.getGridPositionFromPoint(pointDestination))
        {
            Vector3 vec = playGrid.getGridPositionFromPoint(pointDestination);
            Vector3 headingPos = new Vector3(vec.x, vec.y, vec.z);
            transform.position = Vector3.MoveTowards(transform.position, headingPos, moveSpeed * Time.deltaTime);
        }
        else
        {
            //update stand point on arive
            pointStand = pointDestination;
            actualMovement = PlayerMovement.Stand;
        }
    }

    Vector2Int getMovingPoint(float x, float y)
    {
        Vector2Int finalMove = new Vector2Int(0, 0);

        //horizontal
        if (y == 0f)
        {
            if (x > 0)
            {
                finalMove = new Vector2Int(1, 0);
            }
            else if (x < 0)
            {
                finalMove = new Vector2Int(-1, 0);
            }
        }
        else
        {
            //vertical
            if (y > 0)
            {
                finalMove = new Vector2Int(0, 1);
            }
            else if (y < 0)
            {
                finalMove = new Vector2Int(0, -1);
            }
        }

        return finalMove;
    }
}
