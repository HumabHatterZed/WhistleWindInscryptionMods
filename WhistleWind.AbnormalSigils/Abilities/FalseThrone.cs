using DiskCardGame;
using EasyFeedback.APIs;
using InscryptionAPI.Card;
using InscryptionAPI.RuleBook;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;

using WhistleWind.Core.AbilityClasses;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_FalseThrone()
        {
            const string rulebookName = "False Throne";
            const string rulebookDescription = "Once per turn, pay 1 Health to give Neutered to a chosen creature and create a costless, unaltered copy of it in your hand.";
            const string dialogue = "A simple little magic trick.";
            const string triggerText = "[creature] gives a false present to the chosen creature.";
            FalseThrone.ability = AbnormalAbilityHelper.CreateActivatedAbility<FalseThrone>(
                "sigilFalseThrone",
                rulebookName, rulebookDescription, dialogue, triggerText, powerLevel: 4, special: true)
                .SetAbilityRedirect("Neutered", Neutered.ability, GameColors.Instance.gray)
                .SetPart3Rulebook()
                .SetGrimoraRulebook()
                .SetMagnificusRulebook().Id;
        }
    }
    public class FalseThrone : ActivatedSelectSlotBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
        public override Ability LatchAbility => Neutered.ability;
        public override int StartingHealthCost => 1;
        public override int TurnDelay => 1;

        public override bool IsValidTarget(CardSlot slot)
        {
            if (!base.IsValidTarget(slot))
                return false;

            return slot.Card.LacksAllTraits(Trait.Giant, Trait.Uncuttable);
        }
        public override IEnumerator OnValidTargetSelected(CardSlot slot)
        {
            if (slot != null && slot.Card != null)
            {
                CardInfo cardInfo = slot.Card.Info.Clone() as CardInfo;
                cardInfo.Mods.Clear();
                base.Card.Info.Mods.ForEach(x => cardInfo.Mods.Add(x.FullClone()));
                foreach (CardModificationInfo mod in cardInfo.Mods)
                {
                    mod.bloodCostAdjustment = 0;
                    mod.bonesCostAdjustment = 0;
                    mod.energyCostAdjustment = 0;
                    mod.addGemCost = new();
                }

                slot.Card.Anim.LightNegationEffect();
                slot.Card.AddTemporaryMod(new(LatchAbility) { singletonId = "wstl:EmeraldNeuter" });
                yield return new WaitForSeconds(0.75f);

                yield return HelperMethods.ChangeCurrentView(View.Default);
                yield return Singleton<CardSpawner>.Instance.SpawnCardToHand(cardInfo, null);
                yield return new WaitForSeconds(0.45f);
                yield return base.LearnAbility(0.1f);
            }
        }
    }
}
