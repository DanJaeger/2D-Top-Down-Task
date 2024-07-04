using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    public int MyProperty { get; set; }

    [field:SerializeField] public EdibleItemSO InventoryItem { get; private set; }
    [field: SerializeField] public int Quantity { get; set; } = 1;
    [SerializeField] List<EdibleItemSO> _itemsToGrow;

    Animator _animator;
    public bool CanPickUp;
    public bool IsRotten;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        CanPickUp = false;
        IsRotten = false;

        _animator.Play("Grow");
        InventoryItem = _itemsToGrow[0];
    }
    public void SetCanPickUp()
    {
        CanPickUp = true;
        InventoryItem = _itemsToGrow[1];
    }
    public void SetIsRotten()
    {
        IsRotten = true;
        InventoryItem = _itemsToGrow[2];
    }
    public void DestroyFruit()
    {
        this.gameObject.SetActive(false);
    }
}
