using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour, IResourceManager
{
    [SerializeField]
    private int startMoneyAmount = 5000;
    [SerializeField]
    private int demolishPrice = 20;
    [SerializeField]
    private float moneyCalculationInterval = 2;
    MoneyHelper moneyHelper;
    private BuildingManager buildingManager;
    public UIController uiController;

    public int StartMoneyAmount { get => startMoneyAmount;  }
    public float MoneyCalculationInterval { get => moneyCalculationInterval; }

    int IResourceManager.DemolishionPrice{ get => demolishPrice; }


    //Start is called before the first frame update
    void Start()
    {
        moneyHelper = new MoneyHelper(startMoneyAmount);
        UpdateMoneyValueUI();
    }

    public void PrepareResourceManager(BuildingManager buildingManager)
    {
        this.buildingManager = buildingManager;
        InvokeRepeating("CalculateTownIncome", 0, MoneyCalculationInterval);
    }

    public bool SpendMoney(int amount)
    {
        if (CanIBuyIt(amount))
        {
            try
            {
                moneyHelper.ReduceMoney(amount);
                UpdateMoneyValueUI();
                return true;
            }
            catch (MoneyException)
            {
                ReloadGame(); // Money is below 0. 
            }
        }
        return false;
    }

    private void ReloadGame()
    {
        Debug.Log("You have no more money left!");
    }

    public bool CanIBuyIt(int amount)
    {
        if (moneyHelper.Money >= amount)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void CalculateTownIncome()
    {
        try
        {
            moneyHelper.CalculateMoney(buildingManager.GetAllStructures());
            UpdateMoneyValueUI();
        }
        catch (MoneyException)
        {
            ReloadGame();
        }
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    public void AddMoney(int amount)
    {
        moneyHelper.AddMoney(amount);
        UpdateMoneyValueUI();
    }

    private void UpdateMoneyValueUI()
    {
        uiController.SetMoneyValue(moneyHelper.Money);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public int HowManyStructuresCanIPlace(int placementCost, int numberOfStructures)
    {
        int amount = (int)(moneyHelper.Money / placementCost);
        return amount > numberOfStructures ? numberOfStructures : amount;
    }
}
