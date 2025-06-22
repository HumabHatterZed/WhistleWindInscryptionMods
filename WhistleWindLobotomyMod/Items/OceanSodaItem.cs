using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.AbilityClasses;

namespace WhistleWindLobotomyMod {
    public class OceanSodaItem : SodaItem {
        public override string ID => OceanSoda.id;
        public override Ability AbilityToAdd => Ability.Submerge;
        public override SpecialTriggeredAbility StatusEffect => OceanSodaEffect.specialAbility;
        public override int TurnsApplied => 2;
    }
}
