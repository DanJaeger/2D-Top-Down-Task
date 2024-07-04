using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemPrice : MonoBehaviour
{
    TextMeshProUGUI _priceText;

    private void Awake()
    {
        _priceText = GetComponent<TextMeshProUGUI>();
    }
    private void Start()
    {
        _priceText.text = "";
    }
    public void UpdatePriceText(int newPrice)
    {
        _priceText.text = "Price:" + newPrice;
    }
}
