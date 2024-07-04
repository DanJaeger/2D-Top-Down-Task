using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSystem : MonoBehaviour
{
    [SerializeField]
    private InventorySO inventoryData;
    [SerializeField] private AudioClip _sound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Fruit fruit = collision.GetComponent<Fruit>();
        if (fruit != null)
        {
            if (fruit.CanPickUp)
            {
                int reminder = inventoryData.AddItem(fruit.InventoryItem, fruit.Quantity);
                if (reminder == 0)
                {
                    fruit.DestroyFruit();
                    SoundFXManager.instance.PlaySound(_sound);
                }
                else
                {
                    fruit.Quantity = reminder;
                }
            }
        }
    }
}
