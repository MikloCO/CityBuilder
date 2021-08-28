using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    public Transform ground;

    public Material transparentMaterial;
    private Dictionary<GameObject, Material[]> originalMaterials = new Dictionary<GameObject, Material[]>();

    //public void CreateBuilding(Vector3 gridPosition, GridStructure grid, GameObject buildingPrefab)
    //{
    //    GameObject newStructure = Instantiate(buildingPrefab, ground.position + gridPosition, Quaternion.identity);
    //    grid.PlaceStructureOnTheGrid(newStructure, gridPosition);
    //}

    public GameObject CreateGhostStructure(Vector3 gridPosition, GameObject buildingPrefab)
    {
          GameObject newStructure = Instantiate(buildingPrefab, ground.position + gridPosition, Quaternion.identity);
        foreach (Transform child in newStructure.transform)
        {
            var renderer = child.GetComponent<MeshRenderer>();
            if(originalMaterials.ContainsKey(child.gameObject) == false)
            {
                originalMaterials.Add(child.gameObject, renderer.materials);
            }
            Material[] materialsToSet = new Material[renderer.materials.Length];
            for(int i = 0; i < materialsToSet.Length; i++)
            {
                materialsToSet[i] = transparentMaterial;
                materialsToSet[i].color = Color.green;
            }
            renderer.materials = materialsToSet;
        }
        return newStructure;
    }

    public void ConfirmPlacement(IEnumerable<GameObject> structureCollection)
    {
        foreach (var structure in structureCollection)
        {
            foreach (Transform child in structure.transform)
            {
                var renderer = child.GetComponent<MeshRenderer>();
                if(originalMaterials.ContainsKey(child.gameObject))
                {
                    renderer.materials = originalMaterials[child.gameObject];
                }
            }
        }
    }

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

