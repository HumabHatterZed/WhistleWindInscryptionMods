using DiskCardGame;
using Infiniscryption.Core.Helpers;
using Infiniscryption.Spells.Patchers;
using InscryptionAPI.Card;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Infiniscryption.Spells.Sigils
{
    public class DrawTwoCards : AbilityBehaviour
    {
        public override Ability Ability => AbilityID;
        public static Ability AbilityID { get; private set; }

        public static void Register()
        {
            AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();
            info.rulebookName = "Draw Twice";
            info.rulebookDescription = "When [creature] dies, draw two more cards and add them to your deck.";
            info.canStack = true;
            info.passive = false;
            info.metaCategories = new List<AbilityMetaCategory>() { AbilityMetaCategory.Part1Rulebook };
            info.SetPixelAbilityIcon(AssetHelper.LoadTexture("drawtwocardsondeath_pixel"));

            DrawTwoCards.AbilityID = AbilityManager.Add(
                InfiniscryptionSpellsPlugin.OriginalPluginGuid,
                info,
                typeof(DrawTwoCards),
                AssetHelper.LoadTexture("ability_drawtwocardsondeath")
            ).Id;
        }

        public override bool RespondsToResolveOnBoard() => !base.Card.Info.IsSpell();
        public override bool RespondsToDie(bool wasSacrifice, PlayableCard killer) => base.Card.Info.IsSpell();

        public override IEnumerator OnResolveOnBoard() => Effect();
        public override IEnumerator OnDie(bool wasSacrifice, PlayableCard killer) => Effect();

        private IEnumerator Effect()
        {
            yield return base.PreSuccessfulTriggerSequence();
            ViewManager.Instance.SwitchToView(View.Default);

            // Now we draw the top card from each deck
            if (CardDrawPiles.Instance is CardDrawPiles3D)
            {
                CardDrawPiles3D cardPiles = CardDrawPiles.Instance as CardDrawPiles3D;
                yield return cardPiles.DrawCardFromDeck();
                yield return cardPiles.DrawFromSidePile();
            }
            else
            {
                yield return CardDrawPiles.Instance.DrawCardFromDeck();
                yield return CardDrawPiles.Instance.DrawCardFromDeck();
            }

            yield return base.LearnAbility(0.5f);
        }
    }
}
