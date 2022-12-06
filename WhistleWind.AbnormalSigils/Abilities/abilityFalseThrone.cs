using WhistleWind.Core.Helpers;
using DiskCardGame;
using System.Collections;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.Properties;
using WhistleWind.Core.AbilityClasses;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_FalseThrone()
        {
            const string rulebookName = "False Throne";
            const string rulebookDescription = "Once per turn, choose a creature to gain the Neutered sigil. Create a costless copy of the selected card in your hand.";
            const string dialogue = "The will to fight has been lost.";
            FalseThrone.ability = AbnormalAbilityHelper.CreateActivatedAbility<FalseThrone>(
                Artwork.sigilFalseThrone, Artwork.sigilFalseThrone_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 5, special: true).Id;
        }
    }
    public class FalseThrone : ActivatedSelectSlotBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
        public override Ability LatchAbility => Neutered.ability;
        public override IEnumerator OnValidTargetSelected(CardSlot slot)
        {
            if (slot != null && slot.Card != null)
            {
                CardInfo cardInfo = slot.Card.Info;
                cardInfo.cost = 0;
                cardInfo.bonesCost = 0;
                cardInfo.energyCost = 0;
                cardInfo.gemsCost = new();

                CardModificationInfo cardModificationInfo = new(this.LatchAbility) { fromTotem = true, fromLatch = true };

                slot.Card.Anim.LightNegationEffect();
                slot.Card.AddTemporaryMod(cardModificationInfo);
                yield return new WaitForSeconds(0.75f);
                yield return HelperMethods.ChangeCurrentView(View.Default);
                yield return Singleton<CardSpawner>.Instance.SpawnCardToHand(cardInfo, null);
                yield return new WaitForSeconds(0.45f);
                yield return base.LearnAbility(0.1f);
            }
        }
    }
}
