using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace HUD
{
    public class CoinsCounterHandler : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _quantity;
        private int _currentCoins;
        private void Awake()
        {
            if (_quantity == null)
            {
                _quantity = GetComponentInChildren<TextMeshProUGUI>();
                if(_quantity == null)
                {
                    Debug.LogError("Could not found TextMeshProUGUI Component on " + this.name);
                }
            }
        }
        void Start()
        {
            _currentCoins = 20;
            _quantity.text = "x:" + _currentCoins;
        }
        public int CurrentCoins { get => _currentCoins; private set => _currentCoins = value; }
        public void AddCoins(int value)
        {
            _currentCoins += value;
            _quantity.text = "x:" + _currentCoins;
        }
        public void SpendCoins(int value)
        {
            _currentCoins -= value;
            _quantity.text = "x:" + _currentCoins;
        }
    }
}