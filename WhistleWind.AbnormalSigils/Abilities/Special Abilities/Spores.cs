using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers;
using InscryptionCommunityPatch.Card;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
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

        public static readonly string rName = "Spore";
        public static readonly string rDesc = "This card takes damage at the end of its owner's turn equal to its Spore. When this perishes, create a Spore Mold Creature with stats equal to its Spore.";

        private int spore;

        public override bool RespondsToTurnEnd(bool playerTurnEnd)
        {
            if (base.PlayableCard != null)
                return base.PlayableCard.OpponentCard != playerTurnEnd;

            return false;
        }
        public override IEnumerator OnTurnEnd(bool playerTurnEnd)
        {
            int newToAdd = Singleton<BoardManager>.Instance.GetAdjacentSlots(base.PlayableCard.Slot)
                .FindAll(s => s.Card != null && s.Card.HasAbility(Sporogenic.ability)).Count;

            spore += newToAdd;

            if (spore <= 0)
                yield break;

            if (newToAdd > 0)
            {
                CardInfo copyInfo = CardLoader.GetCardByName(base.PlayableCard.Info.name);
                List<CardModificationInfo> tempMods = new();

                foreach (CardModificationInfo info in base.PlayableCard.Info.Mods)
                {
                    if (info.singletonId != "wstl:SporeStatus")
                        tempMods.Add(info);
                }

                foreach (CardModificationInfo info in base.PlayableCard.Info.Mods)
                    copyInfo.Mods.Add(info);

                base.PlayableCard.SetInfo(copyInfo);

                CardModificationInfo statusMod = new()
                {
                    singletonId = "wstl:SporeStatus"
                };
                base.PlayableCard.Anim.LightNegationEffect();
                base.PlayableCard.Info.TempDecals.Add(TextureLoader.LoadTextureFromBytes(Artwork.costSpores_10));
                //base.PlayableCard.RenderCard();
                yield return new WaitForSeconds(0.2f);
            }
            
            AbnormalPlugin.Log.LogDebug($"Card {base.PlayableCard.Info.name} has {spore} Spore.");
            
            if (spore > 0)
            {
                yield return new WaitForSeconds(0.2f);
                yield return base.PlayableCard.TakeDamage(spore, null);
            }
        }

        public override bool RespondsToDie(bool wasSacrifice, PlayableCard killer) => !wasSacrifice;
        public override IEnumerator OnDie(bool wasSacrifice, PlayableCard killer)
        {
            if (spore <= 0 || base.PlayableCard.Slot == null)
                yield break;

            AbnormalPlugin.Log.LogDebug($"Die");

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
            {
                minion.Mods.Add(new CardModificationInfo(item)); // Add base sigils
            }

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
        private void RenderCost_Spores()
        {
            Part1CardCostRender.UpdateCardCost += delegate (CardInfo card, List<Texture2D> costs)
            {
                int spore = card.GetExtendedPropertyAsInt("wstl:SporeStatusEffect") ?? 0;
                if (spore > 0)
                {
                    byte[] resource = spore switch
                    {
                        1 => Artwork.costSpores,
                        2 => Artwork.costSpores_2,
                        3 => Artwork.costSpores_3,
                        4 => Artwork.costSpores_4,
                        5 => Artwork.costSpores_5,
                        6 => Artwork.costSpores_6,
                        7 => Artwork.costSpores_7,
                        8 => Artwork.costSpores_8,
                        9 => Artwork.costSpores_9,
                        _ => Artwork.costSpores_10
                    };
                    costs.Add(TextureLoader.LoadTextureFromBytes(resource));
                }
            };

            Part2CardCostRender.UpdateCardCost += delegate (CardInfo card, List<Texture2D> costs)
            {
                int spore = card.GetExtendedPropertyAsInt("wstl:SporeStatusEffect") ?? 0;
                if (spore > 0)
                {
                    byte[] resource = spore switch
                    {
                        _ => Artwork.costSpores_pixel
                    };
                    costs.Add(TextureLoader.LoadTextureFromBytes(resource));
                }
            };
        }
        private void Rulebook_Spores()
        {
            RulebookSpores.ability = AbnormalAbilityHelper.CreateRulebookAbility<RulebookSpores>(Spores.rName, Spores.rDesc).Id;
        }
        private void SpecialAbility_Spores()
        {
            Spores.specialAbility = AbilityHelper.CreateSpecialAbility<Spores>(pluginGuid, Spores.rName).Id;
        }
    }
}
