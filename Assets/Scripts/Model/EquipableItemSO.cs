using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Model
{
    [CreateAssetMenu]
    public class EquipableItemSO : Item, IDestroyableItem, IItemAction
    {
        [SerializeField] private List<ModifierData> modifiersData = new List<ModifierData>();
        public string ActionName => "Equip";
        [field: SerializeField]
        public List<Sprite> ItemImages { get; set; } //Made for equipables with more than one sprite

        [field: SerializeField]
        public AudioClip actionSFX { get; private set; }

        [field: SerializeField]
        public ClothType ClothPart { get; set; }

        public bool PerformAction(GameObject character)
        {
            foreach (ModifierData data in modifiersData)
            {
                data.statModifier.AffectCharacter(character, data.value, ClothPart, this);
            }

            return true;
        }
    }
    [Serializable]
    public enum ClothType
    {
        None,
        Hood,
        UpperBody,
        LowerBody
    }
}