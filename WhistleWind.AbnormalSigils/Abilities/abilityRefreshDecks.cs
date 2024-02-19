using DiskCardGame;
using Infiniscryption.Spells.Patchers;
using System.Collections;
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
                yield return HelperMethods.ChangeCurrentView(View.CardPiles, 0.2f, 0.4f);

            // remove any remaining cards then create new piles
            Singleton<CardDrawPiles>.Instance.CleanUp();
            yield return new WaitForSeconds(0.4f);
            yield return Singleton<CardDrawPiles>.Instance.Initialize();
        }
        public override IEnumerator OnDie(bool wasSacrifice, PlayableCard killer) => OnResolveOnBoard();
    }

    public partial class AbnormalPlugin
    {
        private void Ability_RefreshDecks()
        {
            const string rulebookName = "Refresh Decks";
            RefreshDecks.ability = AbnormalAbilityHelper.CreateAbility<RefreshDecks>(
                "sigilRefreshDecks", rulebookName, "Refreshes and reshuffles both draw piles.",
                null, powerLevel: 0, canStack: false).Id;
        }
    }
}
