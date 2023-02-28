using DiskCardGame;
using EasyFeedback.APIs;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.Properties;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_Regenerator()
        {
            const string rulebookName = "Regenerator";
            const string rulebookDescription = "Adjacent cards gain 1 Health at the start of the owner's turn.";
            const string dialogue = "Wounds heal, but the scars remain.";

            Regenerator.ability = AbnormalAbilityHelper.CreateAbility<Regenerator>(
                Artwork.sigilRegenerator, Artwork.sigilRegenerator_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 3,
                modular: true, opponent: false, canStack: false).Id;
        }
    }
    public class Regenerator : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
        public override bool RespondsToUpkeep(bool playerUpkeep) => base.Card.OpponentCard != playerUpkeep;
        public override IEnumerator OnUpkeep(bool playerUpkeep)
        {
            yield return HelperMethods.ChangeCurrentView(View.Board);
            yield return PreSuccessfulTriggerSequence();
            yield return new WaitForSeconds(0.2f);

            List<CardSlot> adjacentSlots = Singleton<BoardManager>.Instance.GetAdjacentSlots(base.Card.Slot).FindAll(s => s.Card != null);
            foreach (CardSlot slot in adjacentSlots)
            {
                if (slot.Card.Health < slot.Card.MaxHealth)
                {
                    bool faceDown = slot.Card.FaceDown;
                    yield return slot.Card.FlipFaceUp(faceDown, 0.4f);
                    slot.Card.Anim.LightNegationEffect();
                    slot.Card.HealDamage(1);
                    yield return new WaitForSeconds(0.2f);
                    yield return slot.Card.FlipFaceDown(faceDown);
                }
            }
            yield return new WaitForSeconds(0.2f);
            yield return LearnAbility();
        }
    }
}
