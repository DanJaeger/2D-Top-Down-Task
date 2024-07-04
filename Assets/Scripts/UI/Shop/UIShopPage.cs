using Inventory.Model;
using Inventory.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;

namespace Shop.UI
{
    public class UIShopPage : MonoBehaviour
    {
        [SerializeField] private InventoryDescription _itemDescription;
        [SerializeField] private RectTransform _contentPanel;

        [SerializeField] List<UIInventoryShop> _listOfItems = new List<UIInventoryShop>();
        [SerializeField] List<int> _listOfPrices = new List<int>();
        [SerializeField] TextMeshProUGUI _priceText;

        public event Action<int> OnDescriptionRequested;
        private void Awake()
        {
            _itemDescription.ResetDescription();
        }
        public void InitializeShopInventoryUI()
        {
            foreach (UIInventoryShop item in _listOfItems)
            {
                item.OnItemClicked += HandleItemSelection;
            }
        }
        internal void UpdateDescription(int itemIndex, Sprite itemImage, string name, string description)
        {
            _itemDescription.SetDescription(itemImage, name, description);
            DeselectAllItems();
            _listOfItems[itemIndex].Select();
        }
        public void UpdateData(int itemIndex, Sprite itemImage, int itemQuantity)
        {
            if (_listOfItems.Count > itemIndex)
            {
                _listOfItems[itemIndex].SetData(itemImage, itemQuantity);
            }
        }
        private void HandleItemSelection(UIInventoryShop item)
        {
            int index = _listOfItems.IndexOf(item);
            if (index == -1)
                return;
            OnDescriptionRequested?.Invoke(index);
        }
        public void ResetSelection()
        {
            _itemDescription.ResetDescription();
            DeselectAllItems();
            _priceText.text = "";
        }
        private void DeselectAllItems()
        {
            foreach (UIInventoryShop item in _listOfItems)
            {
                item.Deselect();
            }
        }
        internal void ResetAllItems()
        {
            foreach (var item in _listOfItems)
            {
                item.Deselect();
            }
        }
        public int GetItemPrice(int itemIndex)
        {
            return _listOfPrices[itemIndex];
        }
    }
}