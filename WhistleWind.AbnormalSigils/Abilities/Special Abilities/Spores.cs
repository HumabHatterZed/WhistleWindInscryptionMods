using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.Properties;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public class Spores : SpecialCardBehaviour
    {
        public static SpecialTriggeredAbility specialAbility;
        public SpecialTriggeredAbility SpecialAbility => specialAbility;

        public static readonly string rName = "Spores";
        public static readonly string rDesc = "At the end of its owner's turn, this card takes damage equal to its Spores. Create a Spore Mold Creature with stats equal to its Spores upon perishing.";

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
                yield return base.PlayableCard.TakeDamageTriggerless(spore, null);
                yield return new WaitForSeconds(0.4f);
            }
        }

        public override bool RespondsToDie(bool wasSacrifice, PlayableCard killer) => !wasSacrifice && spore > 0;
        public override IEnumerator OnDie(bool wasSacrifice, PlayableCard killer)
        {
            if (base.PlayableCard.Slot == null)
                yield break;

            CardInfo minion = CardLoader.GetCardByName("wstl_theLittlePrinceMinion");

            minion.SetCost(base.PlayableCard.Info.BloodCost, base.PlayableCard.Info.BonesCost, base.PlayableCard.Info.EnergyCost, base.PlayableCard.Info.GemsCost);

            minion.Mods.Add(new(spore, spore));

            foreach (CardModificationInfo item in base.PlayableCard.Info.Mods.FindAll((CardModificationInfo x) => !x.nonCopyable))
            {
                if (item.abilities.Count == 0) // Add merged sigils
                    continue;

                CardModificationInfo cardModificationInfo = new()
                {
                    abilities = item.abilities,
                    fromCardMerge = item.fromCardMerge,
                    fromDuplicateMerge = item.fromDuplicateMerge
                };

                minion.Mods.Add(cardModificationInfo);
            }

            foreach (Ability item in base.PlayableCard.Info.Abilities.FindAll((Ability x) => x != Ability.NUM_ABILITIES))
                minion.Mods.Add(new CardModificationInfo(item)); // Add base sigils

            CardModificationInfo cardModificationInfo2 = new()
            {
                singletonId = "spore_status",
                DecalIds = { "wstl_spore_" + (Mathf.Min(3, spore) - 1).ToString() },
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
