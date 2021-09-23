using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManagerTestStub : MonoBehaviour, IResourceManager
{
    public float MoneyCalculationInterval { get; }

    public int StartMoneyAmount { get; }

    public int DemolishionPrice { get; }

    public void AddMoney(int amount)
    {
    }

    public void AddToPopulation(int value)
    {
    }

    public void CalculateTownIncome()
    {
       
    }

    public bool CanIBuyIt(int amount)
    {
         return true;
    }

    public int HowManyStructuresCanIPlace(int placementCost, int count)
    {
            return 0;
    }

    public void PrepareResourceManager(BuildingManager buildingManager)
    {
       
    }

    public void ReducePopulation(int value)
    {
    }

    public bool SpendMoney(int amount)
    {
        return true;
    }
}
