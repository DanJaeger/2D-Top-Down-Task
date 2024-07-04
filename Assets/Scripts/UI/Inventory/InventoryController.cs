using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory.UI;
using Inventory.Model;
using Shop.UI;
using Shop.Model;
using TMPro;
using HUD;

namespace Inventory
{
    public class InventoryController : MonoBehaviour
    {
        [SerializeField] private UIInventoryPage _inventoryPage;
        [SerializeField] private UIShopPage _shopPage;
        [SerializeField] private InventorySO _inventoryData;
        [SerializeField] private ShopItemSO _shopInventoryData;
        [SerializeField] private CoinsCounterHandler _coinsCounterHandler;
        public List<InventoryItems> initialItems = new List<InventoryItems>();
        public List<InventoryItems> initialShopItems = new List<InventoryItems>();
        [SerializeField] private TextMeshProUGUI _itemBuyPrice;
        [SerializeField] private TextMeshProUGUI _itemSellPrice;

        int _currentIndexOption = 0;
        private void Start()
        {
            PrepareUI();
            PrepareInventoryData();
            PrepareShopInventoryData();
        }
        #region Prepare UI
        void PrepareUI()
        {
            _inventoryPage.InitializeInventoryUI(_inventoryData.Size);
            _shopPage.InitializeShopInventoryUI();

            _inventoryPage.OnDescriptionRequested += HandleDescriptionRequest;
            _inventoryPage.OnItemActionRequested += HandleItemActionRequest;

            _shopPage.OnDescriptionRequested += HandleDescriptionShopRequest;
        }
        private void PrepareInventoryData()
        {
            _inventoryData.Initialize();
            _inventoryData.OnInventoryUpdated += UpdateInventoryUI;
            foreach (InventoryItems item in initialItems)
            {
                if (item.IsEmpty)
                    continue;
                _inventoryData.AddItem(item.ItemSO, 1);
            }
            _inventoryPage.Show();
        }
        private void PrepareShopInventoryData()
        {
            _shopInventoryData.Initialize();
            _shopInventoryData.OnInventoryUpdated += UpdateShopInventoryUI;
            foreach (InventoryItems item in initialShopItems)
            {
                if (item.IsEmpty)
                    continue;
                _shopInventoryData.AddItem(item.ItemSO, 1);
            }
        }
        #endregion

        #region Update Inventory
        private void UpdateInventoryUI(Dictionary<int, InventoryItems> inventoryState)
        {
            _inventoryPage.ResetAllItems();
            foreach (var item in inventoryState)
            {
                _inventoryPage.UpdateData(item.Key, item.Value.ItemSO.ItemImage,
                    item.Value.Quantity);
            }
        }
        private void UpdateShopInventoryUI(Dictionary<int, InventoryItems> inventoryState)
        {
            _shopPage.ResetAllItems();
            foreach (var item in inventoryState)
            {
                _shopPage.UpdateData(item.Key, item.Value.ItemSO.ItemImage,
                    item.Value.Quantity);
            }
        }
        #endregion

        #region Handle Events
        private void HandleItemActionRequest(int itemIndex)
        {
            InventoryItems inventoryItem = _inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
                return;

            IDestroyableItem destroyableItem = inventoryItem.ItemSO as IDestroyableItem;
            if(destroyableItem != null)
            {
                _inventoryData.RemoveItem(itemIndex, 1);
            }

            IItemAction itemAction = inventoryItem.ItemSO as IItemAction;
            if (itemAction != null)
            {
                itemAction.PerformAction(this.gameObject);
            }
        }
        private void HandleDescriptionRequest(int itemIndex)
        {
            InventoryItems inventoryItem = _inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
            {
                _inventoryPage.ResetSelection();
                return;
            }
            Item item = inventoryItem.ItemSO;
            _inventoryPage.UpdateDescription(itemIndex, item.ItemImage, item.Name, item.Description);
            _itemSellPrice.text = "Price:" + _shopPage.GetItemPrice(itemIndex)*2;
            _currentIndexOption = itemIndex;
        }
        private void HandleDescriptionShopRequest(int itemIndex)
        {
            InventoryItems inventoryItem = _shopInventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
            {
                _shopPage.ResetSelection();
                return;
            }
            Item item = inventoryItem.ItemSO;
            _shopPage.UpdateDescription(itemIndex, item.ItemImage, item.Name, item.Description);
            _itemBuyPrice.text = "Price:" + _shopPage.GetItemPrice(itemIndex);
            _currentIndexOption = itemIndex;
        }
        #endregion
        public void BuyItem()
        {
            InventoryItems inventoryItem = _shopInventoryData.GetItemAt(_currentIndexOption);
            if (inventoryItem.IsEmpty)
            {
                _shopPage.ResetSelection();
                return;
            }
            Item item = inventoryItem.ItemSO;
            int price = _shopPage.GetItemPrice(_currentIndexOption);
            if (_coinsCounterHandler.CurrentCoins >= price)
            {
                _inventoryData.AddItem(item, 1);
                _coinsCounterHandler.SpendCoins(price);
            }
        }
        public void SellItem()
        {
            InventoryItems inventoryItem = _inventoryData.GetItemAt(_currentIndexOption);
            if (inventoryItem.IsEmpty)
            {
                _inventoryPage.ResetSelection();
                return;
            }
            IDestroyableItem destroyableItem = inventoryItem.ItemSO as IDestroyableItem;
            if (destroyableItem != null)
            {
                _inventoryData.RemoveItem(_currentIndexOption, 1);
                _coinsCounterHandler.AddCoins(_shopPage.GetItemPrice(_currentIndexOption) * 2);
                _inventoryPage.ResetSelection();

            }
        }
        public void DisplayInventory()
        {
            if (_inventoryPage.isActiveAndEnabled == false)
            {
                _inventoryPage.Show();
                foreach (var item in _inventoryData.GetCurrentInventoryState())
                {
                    _inventoryPage.UpdateData(item.Key, item.Value.ItemSO.ItemImage, item.Value.Quantity);
                }
            }
            else
            {
                _inventoryPage.Hide();
            }
        }
    }
}