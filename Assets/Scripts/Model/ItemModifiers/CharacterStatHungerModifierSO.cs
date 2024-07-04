using HUD;
using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

[CreateAssetMenu]
public class CharacterStatHungerModifierSO : CharacterStatModifierSO
{
    public override void AffectCharacter(GameObject character, float val, ClothType clothType, Item newItem)
    {
        PlayerController player = character.GetComponent<PlayerController>();
        HungerCounterHandler hunger = player.Hunger;
        if(hunger != null)
        {
            hunger.FeedCharacter(val);
        }
    }
}
