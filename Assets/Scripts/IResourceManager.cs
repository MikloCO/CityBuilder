public interface IResourceManager
{
    float MoneyCalculationInterval { get; }
    int StartMoneyAmount { get; }
    int DemolishionPrice { get; }

    void AddMoney(int amount);
    void CalculateTownIncome();
    bool CanIBuyIt(int amount);
    bool SpendMoney(int amount);
    int HowManyStructuresCanIPlace(int placementCost, int count);

    void PrepareResourceManager(BuildingManager buildingManager);
}