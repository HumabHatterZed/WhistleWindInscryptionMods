using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using InscryptionAPI.Triggers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public class Cowardly : SpecialCardBehaviour, IOnBellRung
    {
        public SpecialTriggeredAbility SpecialAbility => specialAbility;

        public static SpecialTriggeredAbility specialAbility;

        public const string rName = "Cowardly";
        public const string rDesc = "If there is an non-Structure ally card on the board, Scaredy Cat transforms into a stronger forme. Otherwise transform into a weaker forme.";

        public bool RespondsToBellRung(bool playerCombatPhase) => base.PlayableCard.OpponentCard != playerCombatPhase;
        public override bool RespondsToResolveOnBoard() => true;
        public override bool RespondsToOtherCardResolve(PlayableCard otherCard) => otherCard.OpponentCard == base.PlayableCard.OpponentCard;
        public override bool RespondsToTurnEnd(bool playerTurnEnd) => playerTurnEnd != base.PlayableCard.OpponentCard;
        public override IEnumerator OnResolveOnBoard() => CheckTransform();
        public override IEnumerator OnOtherCardResolve(PlayableCard otherCard) => CheckTransform();
        public IEnumerator OnBellRung(bool playerCombatPhase) => CheckTransform();
        private IEnumerator CheckTransform()
        {
            List<CardSlot> slots = BoardManager.Instance.GetSlotsCopy(!base.PlayableCard.OpponentCard)
                .Where(x => x.Card != null && x.Card != base.Card && x.Card.LacksTrait(Trait.Structure)).ToList();

            CardInfo transformation = null;
            System.Action action = null;
            if (slots.Count > 0)
            {
                if (base.PlayableCard.Info.name == "wstl_scaredyCat")
                    transformation = HelperMethods.GetInfoWithMods(base.PlayableCard, "wstl_scaredyCatStrong");
            }
            else if (base.PlayableCard.Info.name == "wstl_scaredyCatStrong")
            {
                // reset damage taken if we're changing to the weak forme, since it has less Health
                transformation = HelperMethods.GetInfoWithMods(base.PlayableCard, "wstl_scaredyCat");
                action = () => base.PlayableCard.Status.damageTaken = 0;
            }

            if (transformation != null)
            {
                if (base.PlayableCard.InHand)
                    yield return base.PlayableCard.TransformIntoCardAboveHand(transformation, action);
                else
                    yield return base.PlayableCard.TransformIntoCard(transformation, action);

                yield return new WaitForSeconds(0.5f);
            }
        }
    }
    public class RulebookEntryCowardly : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }
    public partial class LobotomyPlugin
    {
        private void Rulebook_Cowardly()
            => RulebookEntryCowardly.ability = LobotomyAbilityHelper.CreateRulebookAbility<RulebookEntryCowardly>(Cowardly.rName, Cowardly.rDesc).Id;
        private void SpecialAbility_Cowardly()
            => Cowardly.specialAbility = AbilityHelper.CreateSpecialAbility<Cowardly>(pluginGuid, Cowardly.rName).Id;
    }
}
