using System;
using System.Collections.Generic;
using EliteMMO.API;

public class InventoryHelper
{
    public class ItemDetail
    {
        public ushort ItemID { get; set; }
        public uint ItemCount { get; set; }
        public int ContainerID { get; set; }

        public override string ToString()
        {
            return $"Container: {ContainerID}, ItemID: {ItemID}, Count: {ItemCount}";
        }
    }

    public static List<ItemDetail> GetAllContainerItems(EliteAPI api)
    {
        var itemList = new List<ItemDetail>();

        for (int containerId = 0; containerId <= 16; containerId++)
        {
            // Get the number of items in the current container
            int containerItemCount = api.Inventory.GetContainerCount(containerId);

            // Loop through each item in the container
            for (int itemIndex = 0; itemIndex < containerItemCount; itemIndex++)
            {
                EliteAPI.InventoryItem inventoryItem = api.Inventory.GetContainerItem(containerId, itemIndex);

                if (inventoryItem != null && inventoryItem.Count > 0)
                {
                    // Add item details to the list
                    itemList.Add(new ItemDetail
                    {
                        ItemID = inventoryItem.Id,
                        ItemCount = inventoryItem.Count,
                        ContainerID = containerId
                    });
                }
            }
        }

        return itemList;
    }
}