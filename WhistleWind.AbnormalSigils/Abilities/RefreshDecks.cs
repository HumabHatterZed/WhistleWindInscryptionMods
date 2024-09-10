using DiskCardGame;
using EasyFeedback.APIs;
using Infiniscryption.Spells.Patchers;
using InscryptionAPI.Card;
using InscryptionAPI.Regions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public class RefreshDecks : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToResolveOnBoard() => AbnormalPlugin.SpellAPI.Enabled && base.Card.Info.IsGlobalSpell();
        public override bool RespondsToDie(bool wasSacrifice, PlayableCard killer) => !wasSacrifice && !AbnormalPlugin.SpellAPI.Enabled;

        public override IEnumerator OnResolveOnBoard()
        {
            if (!SaveManager.SaveFile.IsPart2)
                yield return HelperMethods.ChangeCurrentView(View.Hand, 0.2f, 0.4f);

            yield return Singleton<PlayerHand>.Instance.CleanUp();
            yield return new WaitForSeconds(0.2f);

            if (!SaveManager.SaveFile.IsPart2)
                yield return HelperMethods.ChangeCurrentView(View.CardPiles, 0.2f, 0.4f);

            Singleton<PlayerHand>.Instance.Initialize();
            Singleton<CardDrawPiles>.Instance.CleanUp();
            yield return new WaitForSeconds(0.4f);
            yield return Singleton<CardDrawPiles>.Instance.Initialize();
            CardDrawPiles.Instance.Deck.randomSeed *= 2;
            yield return new WaitForSeconds(0.4f);

            ViewManager.Instance.SwitchToView(View.Hand);
            yield return new WaitForSeconds(0.1f);
            yield return Singleton<CardDrawPiles>.Instance.DrawOpeningHand(TurnManager.Instance.GetFixedHand());
        }
        public override IEnumerator OnDie(bool wasSacrifice, PlayableCard killer) => OnResolveOnBoard();
    }

    public partial class AbnormalPlugin
    {
        private void Ability_RefreshDecks()
        {
            const string rulebookName = "Grand Reopening";
            RefreshDecks.ability = AbnormalAbilityHelper.CreateAbility<RefreshDecks>(
                "sigilRefreshDecks", rulebookName, "When this card is played, discard your current hand and reshuffle both draw piles, then draw a new opening hand.",
                null, powerLevel: 0, canStack: false).Id;
        }
    }
}