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
        Color colorToSet = Color.green;
        ModifyStructurePrefabLook(newStructure, colorToSet);
        return newStructure;
    }
    private void ModifyStructurePrefabLook(GameObject newStructure, Color colorToSet)
    {
        foreach (Transform child in newStructure.transform)//newStructure.transform)
        {
            var renderer = newStructure.transform.GetComponent<MeshRenderer>();
            if (originalMaterials.ContainsKey(newStructure.transform.gameObject) == false)
            {
                originalMaterials.Add(newStructure.transform.gameObject, renderer.materials);
            }
            Material[] materialsToSet = new Material[renderer.materials.Length];
            for (int i = 0; i < materialsToSet.Length; i++)
            {
                materialsToSet[i] = transparentMaterial;
                materialsToSet[i].color = colorToSet;
            }
            renderer.materials = materialsToSet;
        }
    }
    public void PlaceStructureOnTheMap(IEnumerable<GameObject> structureCollection)
    {
        foreach (var structure in structureCollection)
        {
            ResetBuildingMaterial(structure);
        }
        originalMaterials.Clear();
    }

    public void ResetBuildingMaterial(GameObject structure)
    {
        foreach (Transform child in structure.transform)
        {
            var renderer = child.GetComponent<MeshRenderer>();
            if (originalMaterials.ContainsKey(child.gameObject))
            {
                renderer.materials = originalMaterials[child.gameObject];
            }
        }
    }

    public void DestroyStructures(IEnumerable<GameObject> structureCollection)
    {
        foreach(var structure in structureCollection)
        {
            DestroySingleStructure(structure);
        }
        originalMaterials.Clear();
    }

    public void DestroySingleStructure(GameObject structure)
    {
        Destroy(structure);
    }

    //public void RemoveBuilding(Vector3 gridPosition, GridStructure grid)
    //{
    //   var structure = grid.GetStructureFromGrid(gridPosition);
    //    if (structure != null)
    //    {
    //        Debug.Log("Destroy structure");
    //        Destroy(structure);
    //        if(structure == null) 
    //        {
    //            Debug.Log("Destroyed!");
    //        }
    //        grid.RemoveStructureFromTheGrid(gridPosition);
    //    }
    //}

    public void SetBuildingForDemolishion(GameObject structureToDemolish)
    {
        Color colorToSet = Color.red;
        ModifyStructurePrefabLook(structureToDemolish, colorToSet);
    }
}

