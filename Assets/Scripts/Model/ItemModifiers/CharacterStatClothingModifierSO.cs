using HUD;
using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterStatClothingModifier : CharacterStatModifierSO
{
    public override void AffectCharacter(GameObject character, float val, ClothType clothType, Item newItem)
    {
        PlayerController player = character.GetComponent<PlayerController>();
        OutfitChanger changer = player.ClothingChanger;
        if (changer != null)
        {
            switch (clothType)
            {
                case ClothType.Hood:
                    changer.ChangeHood(newItem);
                    break;
                case ClothType.UpperBody:
                    changer.ChangeUpperBody(newItem);
                    break;
                case ClothType.LowerBody:
                    changer.ChangeLowerBody(newItem);
                    break;
                default: return;
            }
        }
    }
}

