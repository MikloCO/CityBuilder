using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New facility", menuName = "CityBuilding/StructureData/Facility")]
public class SingleFacilitySO : SingleStructureBaseSO
{
    public int maxCustomers;
    public int upKeepPerCustomer;
    private HashSet<StructureBaseSO> customers = new HashSet<StructureBaseSO>();
    public FacilityType facilityType = FacilityType.None;

    public void RemoveClient(StructureBaseSO clientStructure)
    {
        if(customers.Contains(clientStructure))
        {
            if(facilityType == FacilityType.Water)
            {
                clientStructure.RemoveWaterFacility();  
            }
            if(facilityType == FacilityType.Power)
            {
                clientStructure.RemovePowerFacility();
            }
            customers.Remove(clientStructure);
        }
    }
    public override int GetIncome()
    {
        return customers.Count * income;
    }

    public int GetNumberOfCustomers()
    {
        return customers.Count;
    }

    public void AddClients(IEnumerable<StructureBaseSO> structuresAroundFacility)
    {
        foreach (var nearbyStructure in structuresAroundFacility)
        {
            if(maxCustomers > customers.Count && nearbyStructure != this)
            {
                if(facilityType == FacilityType.Water && nearbyStructure.requireWater)
                {
                    if(nearbyStructure.AddPowerFacility(this))
                    {
                        customers.Add(nearbyStructure);
                    }
                    if(facilityType == FacilityType.Water && nearbyStructure.requireWater)
                    {
                        if(nearbyStructure.AddWaterFacility(this))
                        {
                            customers.Add(nearbyStructure);
                        }
                    }
                }
            }
        }
    }

    public override IEnumerable<StructureBaseSO> PrepareForDestruction()
    {
        base.PrepareForDestruction();
            foreach (var clientStructure in customers)
            {
                RemoveClient(clientStructure);
            }
            return customers;   
    }

}

public enum FacilityType
{
    Power,
    Water,
    None
}
