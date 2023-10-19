using DiskCardGame;
using Infiniscryption.Spells.Patchers;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using InscryptionAPI.Triggers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Opponents.Apocalypse;
using static UnityEngine.GraphicsBuffer;

namespace WhistleWindLobotomyMod
{
    public class ApocalypseAbility : AbilityBehaviour, IGetOpposingSlots
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public bool RemoveDefaultAttackSlot() => true;
        public bool RespondsToGetOpposingSlots() => base.Card.HasTrait(Trait.Giant);
        public List<CardSlot> GetOpposingSlots(List<CardSlot> originalSlots, List<CardSlot> otherAddedSlots)
        {
            return new((TurnManager.Instance.SpecialSequencer as ApocalypseBattleSequencer).specialTargetSlots);
        }
    }

    public partial class LobotomyPlugin
    {
        private void Ability_Apocalypse()
        {
            const string rulebookName = "Monster in the Black Forest";
            ApocalypseAbility.ability = LobotomyAbilityHelper.CreateAbility<ApocalypseAbility>(
                "sigilApocalypse", rulebookName,
                "'Once upon a time, three birds lived happily in the lush forest...'",
                "The three birds, now one, wandered vainly looking for the monster.", powerLevel: 0, canStack: false).Id;
        }
    }
}
