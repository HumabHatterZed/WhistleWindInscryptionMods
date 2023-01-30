using DiskCardGame;
using EasyFeedback.APIs;
using InscryptionAPI.Triggers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public class Cowardly : SpecialCardBehaviour, IOnBellRung
    {
        public SpecialTriggeredAbility SpecialAbility => specialAbility;

        public static SpecialTriggeredAbility specialAbility;

        public static readonly string rName = "Cowardly";
        public static readonly string rDesc = "While Scaredy Cat is on the board, if an ally card has at least 1 Power, Scaredy Cat transforms into a stronger forme.";

        public bool RespondsToBellRung(bool playerCombatPhase) => base.PlayableCard.OpponentCard != playerCombatPhase;
        public override bool RespondsToResolveOnBoard() => true;
        public override bool RespondsToOtherCardResolve(PlayableCard otherCard) => otherCard.OpponentCard == base.PlayableCard.OpponentCard;
        public override bool RespondsToTurnEnd(bool playerTurnEnd) => playerTurnEnd != base.PlayableCard.OpponentCard;
        public override IEnumerator OnResolveOnBoard() => CheckTransform();
        public override IEnumerator OnOtherCardResolve(PlayableCard otherCard) => CheckTransform();
        public IEnumerator OnBellRung(bool playerCombatPhase) => CheckTransform();
        private IEnumerator CheckTransform()
        {
            List<CardSlot> slots = HelperMethods.GetSlotsCopy(base.PlayableCard.OpponentCard)
                .Where(x => x.Card != null && x.Card != base.Card).ToList();

            if (slots.Count > 0)
            {
                if (base.PlayableCard.Info.name == "wstl_scaredyCatStrong" && !slots.Exists(x => x.Card.Attack > 0))
                {
                    // if there are no other cards that can attack, become weak (reset damage)
                    CardInfo weakForme = HelperMethods.GetInfoWithMods(base.PlayableCard, "wstl_scaredyCat");
                    yield return base.PlayableCard.TransformIntoCard(weakForme, ResetDamage);
                    yield return new WaitForSeconds(0.4f);
                }
                else if (base.PlayableCard.Info.name == "wstl_scaredyCat" && slots.Exists(x => x.Card.Attack > 0))
                {
                    // if there are other cards that can attack, become strong
                    CardInfo strongForme = HelperMethods.GetInfoWithMods(base.PlayableCard, "wstl_scaredyCatStrong");
                    yield return base.PlayableCard.TransformIntoCard(strongForme);
                    yield return new WaitForSeconds(0.4f);
                }
            }
        }
        private void ResetDamage() => base.PlayableCard.Status.damageTaken = 0;
    }
    public class RulebookEntryCowardly : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }
    public partial class LobotomyPlugin
    {
        private void Rulebook_Cowardly()
        {
            RulebookEntryCowardly.ability = LobotomyAbilityHelper.CreateRulebookAbility<RulebookEntryCowardly>(Cowardly.rName, Cowardly.rDesc).Id;
        }
        private void SpecialAbility_Cowardly()
        {
            Cowardly.specialAbility = AbilityHelper.CreateSpecialAbility<Cowardly>(pluginGuid, Cowardly.rName).Id;
        }
    }
}
