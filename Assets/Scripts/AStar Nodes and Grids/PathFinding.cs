using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PathFinding : MonoBehaviour {


    PathRequestmanager requestManager;
    Grid grid;
    
    

    void  Awake()
        {
        requestManager = GetComponent<PathRequestmanager>();
        grid = GetComponent<Grid>();
        
        }

    
    public void StartFindPath(Vector3 startPos, Vector3 targetPos)
    {
        StartCoroutine(FindPath(startPos,targetPos));

    }

    IEnumerator FindPath(Vector3 startPos, Vector3 targetPos)
    {
        Vector3[] waypoints = new Vector3[0];
        bool pathSuccess = false;

        Node startNode = grid.NodePos(startPos); //assigns AI node in world position
        Node targetNode = grid.NodePos(targetPos); //assigns Player position in the world

        List<Node> openSet = new List<Node>(); //create a list for openset values
        HashSet<Node> closeSet = new HashSet<Node>(); // create a hashset of array for closedset values

        if (startNode.walkable && targetNode.walkable)
        {

            openSet.Add(startNode);
            while (openSet.Count > 0) //loop through if the openset is not empty
            {
                Node currentNode = openSet[0]; // initial position or node is the first value in openset
                for (int i = 1; i < openSet.Count; i++)
                {
                    if (openSet[i].fcost < currentNode.fcost || openSet[i].fcost == currentNode.fcost && openSet[i].hCost < currentNode.hCost)   // calculate fcost and compare hcost for next movement
                    {
                        currentNode = openSet[i];
                    }
                }
                openSet.Remove(currentNode); //remove  currentnode from open set
                closeSet.Add(currentNode); // add current node to closed set

                if (currentNode == targetNode) //path has been found
                {

                    pathSuccess = true;
                    
                    break;
                }

                foreach (Node neighboursNode in grid.GetNeighbours(currentNode)) //loop through the  neighbours from the current position 
                {
                    if (!neighboursNode.walkable || closeSet.Contains(neighboursNode)) //is the neighbour is walkable(no obstacle) or is neighbour is in the closed set
                    {
                        continue; //skip
                    }

                    int moveCost = currentNode.gCost + GetManhattenDistance(currentNode, neighboursNode); //move cost to the next neighbouring node
                    if (moveCost < neighboursNode.gCost || !openSet.Contains(neighboursNode))  //set the fcost of the neighbour
                    {
                        neighboursNode.gCost = moveCost;
                        neighboursNode.hCost = GetManhattenDistance(neighboursNode, targetNode);
                        neighboursNode.parent = currentNode;

                        if (!openSet.Contains(neighboursNode))
                           openSet.Add(neighboursNode);
                       
                            


                    }
                }
            }
        }
            yield return null;
            if (pathSuccess)
            {
                waypoints = RetracePath(startNode, targetNode);
                
            }
         requestManager.FinishProcessingPath(waypoints, pathSuccess);
    }
    

    


   Vector3[] RetracePath(Node InitialNode, Node endNode)
   {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;
        //here endnode will be the target node
        while(currentNode!= InitialNode) // loop until initial node doesn't meet the target node
        {

            path.Add(currentNode);  //keep adding node


            currentNode = currentNode.parent;
        }

        grid.path = path; //displays the path in black
        Vector3[] waypoints = SimplifyPath(path);
        Array.Reverse(waypoints);
        
        return waypoints;
        
   }

    Vector3[] SimplifyPath(List<Node> path)
    {
        List<Vector3> waypoints = new List<Vector3>();
            Vector2 directionOld = Vector2.zero;
        for(int i=1; i<path.Count; i++)
        {
            Vector2 directionNew = new Vector2(path[i - 1].gridX - path[i].gridX, path[i - 1].gridY - path[i].gridY);
            if(directionNew!= directionOld)
            {
                waypoints.Add(path[i].worldPos);
            }
            directionOld = directionNew;
        }
        return waypoints.ToArray();
    }



        int GetManhattenDistance(Node nodeA, Node nodeB)
        {
            int distX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
            int distY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

            if (distX > distY)

                return 14 * distY + 10 * (distX - distY); //diagnol movement is 14 and vertical or horizontal movement is 10
                return 14 * distX + 10 * (distY - distX);

        }


}
