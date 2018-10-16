using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {



    public Vector2 gridworldSize;
    public LayerMask unWalkable;
    public float nodeRadius;
    Node[,] grid; //creates a 2d array of nodes
    public Transform player;
   
    float nodeDiameter;
    int gridSizeX, gridSizeY;

    void Awake()
    {
        nodeDiameter = nodeRadius * 2;  //assigning the total size of node
        gridSizeX = Mathf.RoundToInt(gridworldSize.x / nodeDiameter); // size of the grid in x co
        gridSizeY = Mathf.RoundToInt(gridworldSize.y / nodeDiameter);
        CreateGrid();
    }

    void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];  //size of the node in the grid.. square
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridworldSize.x/2 - Vector3.forward * gridworldSize.y/2; //bottom left point of the wolrdgrid from where the node will add
        for(int i=0; i< gridSizeX; i++) //loop through x coordinate
        {
            for(int j=0; j< gridSizeY; j++)// loop through y coordinate
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (i * nodeDiameter + nodeRadius) + Vector3.forward * (j * nodeDiameter + nodeRadius); //node starts to add from here
                bool walkable= !(Physics.CheckSphere(worldPoint,nodeRadius,unWalkable));
                grid[i,j] = new Node(walkable, worldPoint,i,j);
            }
        }
    }

    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighboursNode = new List<Node>();

        for(int x=-1; x<=1; x++) 
        {
            for(int y= -1; y<=1; y++ )
            {
                if (x == 0 && y == 0)
                    continue;

               int checkX = node.gridX + x;
               int checkY = node.gridY + y;

                if(checkX > 0 && checkX < gridSizeX && checkY > 0 && checkY < gridSizeY )
                {
                    neighboursNode.Add(grid[checkX, checkY]);
                }

            }
        }

        return neighboursNode;
    }





    public Node NodePos(Vector3 worldPos) //this method converts world position into nodes.
    {
         float percentX = ((worldPos.x / gridworldSize.x) + 0.5f);
         float percentY = ((worldPos.z / gridworldSize.y) + 0.5f);
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);
        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);

        return grid[x,y];
     }

    public List<Node> path;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green; //assgin the color of the gizmos to green
        Gizmos.DrawWireCube(transform.position, new Vector3(gridworldSize.x, 1 , gridworldSize.y));
        if (grid != null)
        {
            Node playernode = NodePos(player.position);
            foreach (Node n in grid)
            {
                
               if(n.walkable)
                {
                    Gizmos.color = Color.white;
                }
                else
                {
                    Gizmos.color = Color.red;
                }

                if (playernode == n)
                {
                    Gizmos.color = Color.blue;
                }
                //Gizmos.color = (n.walkable)?Color.white:Color.red;
                if (path!=null)
                    if(path.Contains(n))
                    {
                        Gizmos.color = Color.black;
                    }


                
                Gizmos.DrawWireCube(n.worldPos, Vector3.one * (nodeDiameter));
            }
        }
    }
}
