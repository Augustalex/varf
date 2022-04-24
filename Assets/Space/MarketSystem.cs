using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Space
{
    public class MarketSystem
    {
        public MarketSystem(Player player)
        {
            _player = player;
        }

        public readonly Dictionary<ItemCategory, int> CategoryIndex = new Dictionary<ItemCategory, int>
        {
            {ItemCategory.CannedFood, 1},
            {ItemCategory.Engine, 20},
            {ItemCategory.Generator, 10},
            {ItemCategory.Pipe, 1},
            {ItemCategory.Windshield, 2},
            {ItemCategory.HeatShield, 10},
            {ItemCategory.ReinforcedDoor, 2},
            {ItemCategory.SmallCabin, 2},
            {ItemCategory.Fuel, 4},
        };

        private float _rate = 1.5f;

        private readonly List<PlayerItem> _listedForPurchase = new List<PlayerItem>();
        private Player _player;

        public struct DisplayItem
        {
            public string Name;
            public int Price;
        }

        public void ListItemForPurchase(PlayerItem item)
        {
            _listedForPurchase.Add(item);
        }

        public DisplayItem[] ListedForPurchase()
        {
            return _listedForPurchase.Select(item => new DisplayItem
            {
                Name = item.ItemName,
                Price = GetItemPrice(item.Category)
            }).ToArray();
        }

        private int GetItemPrice(ItemCategory itemCategory)
        {
            return Mathf.CeilToInt(CategoryIndex[itemCategory] * _rate);
        }

        public void Buy(DisplayItem item)
        {
            _player.RemoveFunds(item.Price);

            var listedItem = _listedForPurchase.First(i => i.ItemName == item.Name);
            _listedForPurchase.Remove(listedItem);

            _player.AddItem(new PlayerItem
            {
                ItemName = item.Name,
                Category = listedItem.Category
            });
        }

        public void Sell(DisplayItem item)
        {
            _player.AddFunds(item.Price);

            var listedItem = _player.GetItems().First(i => i.ItemName == item.Name);
            _player.RemoveItem(listedItem);
        }

        public DisplayItem[] GetItemsToSell()
        {
            return _player.GetItems().Select(item => new DisplayItem
            {
                Name = item.ItemName,
                Price = Mathf.FloorToInt(GetItemPrice(item.Category) * .8f),
            }).ToArray();
        }
    }
}