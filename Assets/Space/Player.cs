using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Space
{
    [Serializable]
    public struct PlayerItem
    {
        public string ItemName;
        public ItemCategory Category;
    }

    public enum ItemCategory
    {
        CannedFood,
        Engine,
        Generator,
        Windshield,
        HeatShield,
        SmallCabin,
        ReinforcedDoor,
        Pipe,
        Fuel
    }

    public class Player : MonoBehaviour
    {
        public List<PlayerItem> items = new List<PlayerItem>
        {
            new PlayerItem
            {
                ItemName = "Tomato soup",
                Category = ItemCategory.CannedFood
            }
        };

        public int funds;

        public void AddItem(PlayerItem playerItem)
        {
            items.Add(playerItem);
        }

        public void RemoveItem(PlayerItem playerItem)
        {
            items.Remove(playerItem);
        }

        public void RemoveFunds(int itemPrice)
        {
            funds -= itemPrice;
        }

        public void AddFunds(int itemPrice)
        {
            funds += itemPrice;
        }

        public PlayerItem[] GetItems()
        {
            return items.ToArray();
        }
    }
}