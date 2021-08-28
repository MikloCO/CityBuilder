using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    public Transform ground;

    public void CreateBuilding(Vector3 gridPosition, GridStructure grid, GameObject buildingPrefab)
    {
        GameObject newStructure = Instantiate(buildingPrefab, ground.position + gridPosition, Quaternion.identity);
        grid.PlaceStructureOnTheGrid(newStructure, gridPosition);
    }

    //A bug is smelled
    public void RemoveBuilding(Vector3 gridPosition, GridStructure grid)
    {
       var structure = grid.GetStructureFromGrid(gridPosition);
        if (structure != null)
        {
            Debug.Log("Destroy structure");
            Destroy(structure);
            if(structure == null) 
            {
                Debug.Log("Destroyed!");
            }
            grid.RemoveStructureFromTheGrid(gridPosition);
        }
    }
}

