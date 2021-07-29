using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New facility", menuName = "CityBuilding/StructureData/Facility")]
public class SingleFacilitySO : SingleStructureBaseSO
{
    public int maxCustomers;
    public int upKeepPerCustomer;
}
