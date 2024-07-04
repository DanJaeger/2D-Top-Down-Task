using Inventory.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Inventory.Model
{
    [CreateAssetMenu]
    public class InventorySO : ScriptableObject
    {
        [SerializeField] private List<InventoryItems> _inventoryItems;
        [field: SerializeField] public int Size { get; private set; } = 10;

        public event Action<Dictionary<int, InventoryItems>> OnInventoryUpdated;

        public void Initialize()
        {
            _inventoryItems = new List<InventoryItems>();
            for (int i = 0; i < Size; i++)
            {
                _inventoryItems.Add(InventoryItems.GetEmptyItem());
            }
        }
        public int AddItem(Item item, int quantity)
        {
            if (item.IsStackable == false)
            {
                for (int i = 0; i < _inventoryItems.Count; i++)
                {
                    while (quantity > 0 && IsInventoryFull() == false)
                    {
                        quantity -= AddItemToFirstFreeSlot(item, 1);
                    }
                    InformAboutChange();
                    return quantity;
                }
            }
            quantity = AddStackableItem(item, quantity);
            InformAboutChange();
            return quantity;
        }
        public void AddItem(InventoryItems item)
        {
            AddItem(item.ItemSO, item.Quantity);
        }
        private int AddStackableItem(Item item, int quantity)
        {
            for (int i = 0; i < _inventoryItems.Count; i++)
            {
                if (_inventoryItems[i].IsEmpty)
                    continue;
                if (_inventoryItems[i].ItemSO.ID == item.ID)
                {
                    int amountPossibleToTake =
                        _inventoryItems[i].ItemSO.MaxStackSize - _inventoryItems[i].Quantity;

                    if (quantity > amountPossibleToTake)
                    {
                        _inventoryItems[i] = _inventoryItems[i]
                            .ChangeQuantity(_inventoryItems[i].ItemSO.MaxStackSize);
                        quantity -= amountPossibleToTake;
                    }
                    else
                    {
                        _inventoryItems[i] = _inventoryItems[i]
                            .ChangeQuantity(_inventoryItems[i].Quantity + quantity);
                        InformAboutChange();
                        return 0;
                    }
                }
            }
            while (quantity > 0 && IsInventoryFull() == false)
            {
                int newQuantity = Mathf.Clamp(quantity, 0, item.MaxStackSize);
                quantity -= newQuantity;
                AddItemToFirstFreeSlot(item, newQuantity);
            }
            return quantity;
        }
        private void InformAboutChange()
        {
            OnInventoryUpdated?.Invoke(GetCurrentInventoryState());
        }
        public Dictionary<int, InventoryItems> GetCurrentInventoryState()
        {
            Dictionary<int, InventoryItems> returnValue = new Dictionary<int, InventoryItems>();

            for (int i = 0; i < _inventoryItems.Count; i++)
            {
                if (_inventoryItems[i].IsEmpty)
                    continue;
                returnValue[i] = _inventoryItems[i];
            }

            return returnValue;
        }
        private int AddItemToFirstFreeSlot(Item item, int quantity)
        {
            InventoryItems newItem = new InventoryItems
            {
                ItemSO = item,
                Quantity = quantity
            };

            for (int i = 0; i < _inventoryItems.Count; i++)
            {
                if (_inventoryItems[i].IsEmpty)
                {
                    _inventoryItems[i] = newItem;
                    return quantity;
                }
            }
            return 0;
        }
        private bool IsInventoryFull()
           => _inventoryItems.Where(item => item.IsEmpty).Any() == false;
        public InventoryItems GetItemAt(int itemIndex)
        {
            return _inventoryItems[itemIndex];
        }
        public void RemoveItem(int itemIndex, int amount)
        {
            if (_inventoryItems.Count > itemIndex)
            {
                if (_inventoryItems[itemIndex].IsEmpty)
                    return;
                int reminder = _inventoryItems[itemIndex].Quantity - amount;
                if (reminder <= 0)
                    _inventoryItems[itemIndex] = InventoryItems.GetEmptyItem();
                else
                    _inventoryItems[itemIndex] = _inventoryItems[itemIndex]
                        .ChangeQuantity(reminder);

                InformAboutChange();
            }
        }
    }
    [Serializable]
    public struct InventoryItems
    {
        public int Quantity;
        public Item ItemSO;
        public bool IsEmpty => ItemSO == null;

        public InventoryItems ChangeQuantity(int newQuantity)
        {
            return new InventoryItems
            {
                ItemSO = this.ItemSO,
                Quantity = newQuantity
            };
        }
        public static InventoryItems GetEmptyItem() => new InventoryItems
        {
            ItemSO = null,
            Quantity = 0,
        };
    }
}

