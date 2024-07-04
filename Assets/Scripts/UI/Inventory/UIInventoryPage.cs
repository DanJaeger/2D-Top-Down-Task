using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace Inventory.UI
{
    public class UIInventoryPage : MonoBehaviour
    {
        [SerializeField] private UIInventoryItem _itemPrefab;
        [SerializeField] private InventoryDescription _itemDescription;
        [SerializeField] private RectTransform _contentPanel;

        List<UIInventoryItem> _listOfItems = new List<UIInventoryItem>();

        public event Action<int> OnDescriptionRequested, OnItemActionRequested;

        [SerializeField] TextMeshProUGUI _priceText;
        private void Awake()
        {
            Hide();
            _itemDescription.ResetDescription();
        }
        public void InitializeInventoryUI(int inventorySize)
        {
            for (int i = 0; i < inventorySize; i++)
            {
                UIInventoryItem uiItem = Instantiate(_itemPrefab, Vector3.zero, Quaternion.identity);
                uiItem.transform.SetParent(_contentPanel);
                _listOfItems.Add(uiItem);

                uiItem.OnItemClicked += HandleItemSelection;
                uiItem.OnRightMouseBtnClicked += HandleShowItemActions;
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
        private void HandleShowItemActions(UIInventoryItem item)
        {
            int index = _listOfItems.IndexOf(item);
            if (index == -1)
                return;
            OnItemActionRequested?.Invoke(index);
        }
        private void HandleItemSelection(UIInventoryItem item)
        {
            int index = _listOfItems.IndexOf(item);
            if (index == -1)
                return;
            OnDescriptionRequested?.Invoke(index);
        }
        public void Show()
        {
            this.gameObject.SetActive(true);
            ResetSelection();
        }
        public void ResetSelection()
        {
            _itemDescription.ResetDescription();
            DeselectAllItems();
            _priceText.text = "";
        }
        private void DeselectAllItems()
        {
            foreach (UIInventoryItem item in _listOfItems)
            {
                item.Deselect();
            }
        }
        public void Hide()
        {
            this.gameObject.SetActive(false);
        }
        internal void ResetAllItems()
        {
            foreach (var item in _listOfItems)
            {
                item.ResetData();
                item.Deselect();
            }
        }
    }
}