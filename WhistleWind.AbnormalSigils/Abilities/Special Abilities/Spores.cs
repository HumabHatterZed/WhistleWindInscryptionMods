using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers;
using InscryptionCommunityPatch.Card;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using UnityEngine;
using UnityEngine.Assertions;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.Properties;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public class Spores : SpecialCardBehaviour
    {
        public static SpecialTriggeredAbility specialAbility;
        public SpecialTriggeredAbility SpecialAbility => specialAbility;

        public static readonly string rName = "Spore";
        public static readonly string rDesc = "This card takes damage at the end of its owner's turn equal to its Spore. When this perishes, create a Spore Mold Creature with stats equal to its Spore.";

        public int spore;
        public override bool RespondsToTurnEnd(bool playerTurnEnd)
        {
            if (base.PlayableCard != null)
                return base.PlayableCard.OpponentCard != playerTurnEnd;

            return false;
        }
        public override IEnumerator OnTurnEnd(bool playerTurnEnd)
        {
            int num = Singleton<BoardManager>.Instance.GetAdjacentSlots(base.PlayableCard.Slot)
                .FindAll(s => s.Card != null && s.Card.HasAbility(Sporogenic.ability)).Count;

            spore += num;

            if (spore <= 0)
                yield break;

            if (num > 0)
            {
                yield return HelperMethods.ChangeCurrentView(View.Board);
                base.PlayableCard.Anim.LightNegationEffect();
                if (spore <= 3)
                {
                    CardModificationInfo cardModificationInfo = new()
                    {
                        singletonId = "spore_status",
                        DecalIds = { "wstl_spore_" + (spore - 1).ToString() },
                        nonCopyable = true,
                    };
                    base.PlayableCard.Info.Mods.RemoveAll(x => x.singletonId == "spore_status");
                    base.PlayableCard.Info.Mods.Add(cardModificationInfo);
                }
                yield return new WaitForSeconds(0.2f);
            }

            if (spore > 0)
            {
                yield return new WaitForSeconds(0.2f);
                yield return base.PlayableCard.TakeDamage(spore, null);
                yield return new WaitForSeconds(0.4f);
            }
        }

        public override bool RespondsToDie(bool wasSacrifice, PlayableCard killer) => !wasSacrifice;
        public override IEnumerator OnDie(bool wasSacrifice, PlayableCard killer)
        {
            if (spore <= 0 || base.PlayableCard.Slot == null)
                yield break;

            CardInfo minion = CardLoader.GetCardByName("wstl_theLittlePrinceMinion");

            minion.Mods.Add(new(spore, spore));
            minion.cost = base.PlayableCard.Info.BloodCost;
            minion.bonesCost = base.PlayableCard.Info.BonesCost;
            minion.energyCost = base.PlayableCard.Info.EnergyCost;
            minion.gemsCost = base.PlayableCard.Info.GemsCost;

            foreach (CardModificationInfo item in base.PlayableCard.Info.Mods.FindAll((CardModificationInfo x) => !x.nonCopyable))
            {
                if (item.abilities.Count == 0) // Add merged sigils
                    continue;

                CardModificationInfo cardModificationInfo = new()
                {
                    abilities = item.abilities,
                    fromCardMerge = item.fromCardMerge,
                    fromDuplicateMerge = item.fromDuplicateMerge,
                    fromLatch = item.fromLatch
                };

                minion.Mods.Add(cardModificationInfo);
            }
            foreach (Ability item in base.PlayableCard.Info.Abilities.FindAll((Ability x) => x != Ability.NUM_ABILITIES))
                minion.Mods.Add(new CardModificationInfo(item)); // Add base sigils

            CardModificationInfo cardModificationInfo2 = new()
            {
                singletonId = "spore_status",
                DecalIds = { "wstl_spore_" + ((Mathf.Min(3, spore)) - 1).ToString() },
                nonCopyable = true,
            };
            minion.Mods.Add(cardModificationInfo2);

            yield return Singleton<BoardManager>.Instance.CreateCardInSlot(minion, base.PlayableCard.Slot, 0.15f);
        }
    }
    public class RulebookSpores : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }
    public partial class AbnormalPlugin
    {
        private void Rulebook_Spores()
        {
            RulebookSpores.ability = AbnormalAbilityHelper.CreateAbility<RulebookSpores>(Artwork.sigilSpores, Artwork.sigilSpores_pixel, Spores.rName, Spores.rDesc, "", unobtainable: true).Id;
        }
        private void SpecialAbility_Spores()
        {
            Spores.specialAbility = AbilityHelper.CreateSpecialAbility<Spores>(pluginGuid, Spores.rName).Id;
        }
    }
}
