using DiskCardGame;
using System.Collections;
using System.Linq;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.Properties;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_Regenerator()
        {
            const string rulebookName = "Regenerator";
            const string rulebookDescription = "Adjacent cards gain 1 Health on upkeep.";
            const string dialogue = "Wounds heal, but the scars remain.";

            Regenerator.ability = AbnormalAbilityHelper.CreateAbility<Regenerator>(
                Artwork.sigilRegenerator, Artwork.sigilRegenerator_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 3,
                modular: true, opponent: false, canStack: false, isPassive: false).Id;
        }
    }
    public class Regenerator : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
        public override bool RespondsToUpkeep(bool playerUpkeep)
        {
            return base.Card.OpponentCard != playerUpkeep;
        }
        public override IEnumerator OnUpkeep(bool playerUpkeep)
        {
            bool faceDown = false;
            yield return AbnormalMethods.ChangeCurrentView(View.Board);
            yield return PreSuccessfulTriggerSequence();
            yield return new WaitForSeconds(0.2f);

            foreach (CardSlot slot in Singleton<BoardManager>.Instance.GetAdjacentSlots(Card.Slot).Where(slot => slot.Card != null))
            {
                if (slot.Card.Health < slot.Card.MaxHealth)
                {
                    if (slot.Card.FaceDown)
                    {
                        faceDown = true;
                        slot.Card.SetFaceDown(false);
                        slot.Card.UpdateFaceUpOnBoardEffects();
                        yield return new WaitForSeconds(0.55f);
                    }
                    slot.Card.Anim.LightNegationEffect();
                    slot.Card.HealDamage(1);
                    if (faceDown)
                    {
                        faceDown = false;
                        yield return new WaitForSeconds(0.2f);
                        slot.Card.SetFaceDown(false);
                        slot.Card.UpdateFaceUpOnBoardEffects();
                        yield return new WaitForSeconds(0.2f);
                    }
                }
            }
            yield return new WaitForSeconds(0.2f);
            yield return LearnAbility();
        }
    }
}
