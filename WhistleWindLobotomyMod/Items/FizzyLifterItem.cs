using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.AbilityClasses;

namespace WhistleWindLobotomyMod
{
    public class FizzyLifterItem : SodaItem
    {
        public override string ID => FizzyLifter.id;
        public override Ability AbilityToAdd => Ability.Flying;
        public override SpecialTriggeredAbility StatusEffect => FizzyLifterEffect.specialAbility;
    }
}
