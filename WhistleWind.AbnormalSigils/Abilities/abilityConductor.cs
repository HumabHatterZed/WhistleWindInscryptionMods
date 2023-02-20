using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Triggers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.Properties;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public class Conductor : AbilityBehaviour, IPassiveAttackBuff
    {
        public static Ability ability;
        public override Ability Ability => ability;

        private int turnCount = 0;

        public override bool RespondsToResolveOnBoard() => true;
        public override IEnumerator OnResolveOnBoard() => base.LearnAbility(0.4f);

        public override bool RespondsToUpkeep(bool onPlayerUpkeep)
        {
            if (turnCount < 3)
                return base.Card.OpponentCard != onPlayerUpkeep;
            return false;
        }
        public override IEnumerator OnUpkeep(bool onPlayerUpkeep)
        {
            yield return base.PreSuccessfulTriggerSequence();
            yield return HelperMethods.ChangeCurrentView(View.Board);
            base.Card.Anim.StrongNegationEffect();
            turnCount++;
            yield return new WaitForSeconds(0.4f);
            yield return HelperMethods.ChangeCurrentView(View.Default);
        }

        public int GetPassiveAttackBuff(PlayableCard target)
        {
            // do nothing if not on the board or we've just been played
            if (!this.Card.OnBoard || turnCount == 0)
                return 0;

            if (target == this.Card)
            {
                // if turn 1, return number of valid adjacent cards
                if (turnCount == 1)
                    return Singleton<BoardManager>.Instance.GetAdjacentSlots(target.Slot).FindAll(slot => slot.Card != null).Count;

                // get number of valid ally cards
                int powerToSelf = HelperMethods.GetSlotsCopy(base.Card.OpponentCard)
                    .FindAll(x => x.Card != base.Card && x.Card != null && x.Card.LacksAbility(Neutered.ability)).Count;

                if (turnCount == 3)
                {
                    List<CardSlot> opposingSlots = HelperMethods.GetSlotsCopy(!base.Card.OpponentCard).FindAll(x => x.Card != null);
                    powerToSelf += opposingSlots.Exists(s => s.Card.HasTrait(Trait.Giant)) ? 1 : opposingSlots.Count;
                }

                return powerToSelf;
            }

            // target is not this card

            // turn 1, only give power to adjacent
            if (Singleton<BoardManager>.Instance.GetAdjacentSlots(target.Slot).Exists(slot => slot.Card == base.Card))
                return turnCount == 1 ? 1 : 0;

            if (turnCount > 1)
            {
                // turn 2+, buff all allies
                if (target.OpponentCard == base.Card.OpponentCard)
                    return 1;

                // turn 3, buff enemies
                else if (turnCount == 3)
                    return 1;
            }

            AbnormalPlugin.Log.LogError("Conductor ability is not working properly! Who's the idiot who coded it?");
            return 0;
        }
    }
    public partial class AbnormalPlugin
    {
        private void Ability_Conductor()
        {
            const string rulebookName = "Conductor";
            const string rulebookDescription = "While this card is on the board, reduce all other card's Power by the number of turns this card has been on the board, up to 3.";
            const string dialogue = "From break and ruin, the most beautiful performance begins.";

            Conductor.ability = AbnormalAbilityHelper.CreateAbility<Conductor>(
                Artwork.sigilConductor, Artwork.sigilConductor_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 3,
                modular: false, opponent: true, canStack: false).Id;
        }
    }
}
