using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Inventory.UI
{
    public class UIInventoryItem : MonoBehaviour
    {
        [SerializeField] private Image _itemImage;
        [SerializeField] private TMP_Text _quantityText;

        [SerializeField] private Image _borderImage;

        public event Action<UIInventoryItem> OnItemClicked, OnRightMouseBtnClicked;

        private void Awake()
        {
            ResetData();
            Deselect();
        }
        public void ResetData()
        {
            _itemImage.gameObject.SetActive(false);
        }
        public void Deselect()
        {
            _borderImage.enabled = false;
        }
        public void SetData(Sprite sprite, int quantity)
        {
            _itemImage.gameObject.SetActive(true);
            _itemImage.sprite = sprite;
            _quantityText.text = quantity + "";
        }
        public void Select()
        {
            _borderImage.enabled = true;
        }
        public void OnPointerClick(BaseEventData data)
        {
            PointerEventData pointerData = (PointerEventData)data;
            if (pointerData.button == PointerEventData.InputButton.Right)
            {
                OnRightMouseBtnClicked?.Invoke(this);
            }
            else
            {
                OnItemClicked?.Invoke(this);
            }
        }
    }
}