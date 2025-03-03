using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.AbilityClasses;

namespace WhistleWindLobotomyMod
{
    public class SurefireDrinkItem : SodaItem
    {
        public override string ID => SurefireDrink.id;
        public override Ability AbilityToAdd => Ability.Sniper;
        public override SpecialTriggeredAbility StatusEffect => SurefireDrinkEffect.specialAbility;
    }
}
