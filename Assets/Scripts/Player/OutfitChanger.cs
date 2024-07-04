using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutfitChanger : MonoBehaviour
{
    [SerializeField] private InventorySO _inventoryData;

    //Head
    [SerializeField] private SpriteRenderer _hood;

    //UpperBody
    [SerializeField] private SpriteRenderer _torso;
    [SerializeField] private SpriteRenderer _shoulder_l;
    [SerializeField] private SpriteRenderer _shoulder_r;
    [SerializeField] private SpriteRenderer _elbow_l;
    [SerializeField] private SpriteRenderer _elbow_r;
    [SerializeField] private SpriteRenderer _wrist_l;
    [SerializeField] private SpriteRenderer _wrist_r;

    //LowerBody
    [SerializeField] private SpriteRenderer _pelvis;
    [SerializeField] private SpriteRenderer _leg_l;
    [SerializeField] private SpriteRenderer _leg_r;
    [SerializeField] private SpriteRenderer _boot_l;
    [SerializeField] private SpriteRenderer _boot_r;

    [SerializeField] private List<Item> currentItems;

    public void ChangeHood(Item newItem)
    {
        if (currentItems != null && currentItems.Count > 0) {
            _inventoryData.AddItem(currentItems[0], 1);
            currentItems[0] = newItem;

            _hood.sprite = newItem.ItemImage;
        }
    }
    public void ChangeUpperBody(Item newItem)
    {
        if (currentItems != null && currentItems.Count > 0)
        {
            _inventoryData.AddItem(currentItems[1], 1);
            currentItems[1] = newItem;

            EquipableItemSO equipableItem = newItem as EquipableItemSO;

            if (equipableItem != null) {
                _elbow_l.sprite = equipableItem.ItemImages[0];
                _elbow_r.sprite = equipableItem.ItemImages[1];
                _wrist_l.sprite = equipableItem.ItemImages[2];
                _wrist_r.sprite = equipableItem.ItemImages[3];
                _shoulder_l.sprite = equipableItem.ItemImages[4];
                _shoulder_r.sprite = equipableItem.ItemImages[5];
                _torso.sprite = equipableItem.ItemImages[6];
            }
        }
    }
    public void ChangeLowerBody(Item newItem)
    {
        if (currentItems != null && currentItems.Count > 0)
        {
            _inventoryData.AddItem(currentItems[2], 1);
            currentItems[2] = newItem;

            EquipableItemSO equipableItem = newItem as EquipableItemSO;

            if (equipableItem != null)
            {
                _pelvis.sprite = equipableItem.ItemImages[0];
                _leg_l.sprite = equipableItem.ItemImages[1];
                _leg_r.sprite = equipableItem.ItemImages[2];
                _boot_l.sprite = equipableItem.ItemImages[3];
                _boot_r.sprite = equipableItem.ItemImages[4];
            }
        }
    }
}
