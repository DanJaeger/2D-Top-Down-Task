using Inventory.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Shop.UI
{
    public class UIInventoryShop : MonoBehaviour
    {
        [SerializeField] private TMP_Text _quantityText;

        [SerializeField] private Image _borderImage;

        public event Action<UIInventoryShop> OnItemClicked;
        private void Awake()
        {
            Deselect();
        }

        public void Deselect()
        {
            _borderImage.enabled = false;
        }
        public void Select()
        {
            _borderImage.enabled = true;
        }
        public void SetData(Sprite sprite, int quantity)
        {
            _quantityText.text = quantity + "";
        }
        public void OnPointerClick(BaseEventData data)
        {
            PointerEventData pointerData = (PointerEventData)data;
            if (pointerData.button != PointerEventData.InputButton.Right)
            {
                OnItemClicked?.Invoke(this);
            }
        }
    }
}