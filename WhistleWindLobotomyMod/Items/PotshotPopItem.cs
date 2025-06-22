using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.AbilityClasses;

namespace WhistleWindLobotomyMod {
    public class PotshotPopItem : SodaItem {
        public override string ID => PotshotPop.id;
        public override Ability AbilityToAdd => Ability.Sentry;
        public override SpecialTriggeredAbility StatusEffect => PotshotPopEffect.specialAbility;
        public override int TurnsApplied => 2;
    }
}
