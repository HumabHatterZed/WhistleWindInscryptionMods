﻿using InscryptionAPI;
using InscryptionAPI.Triggers;
using DiskCardGame;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Ability_Cowardly()
        {
            const string rulebookName = "Cowardly";
            const string rulebookDescription = "A card bearing this sigil gains 1 Power if an ally has the Cat Lover sigil. Otherwise lose 1 Power.";
            const string dialogue = "Through love, your beast finds its inner courage.";

            Cowardly.ability = AbilityHelper.CreateAbility<Cowardly>(
                Artwork.sigilCowardly, Artwork.sigilCowardly_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 1,
                addModular: true, opponent: false, canStack: false, isPassive: false).Id;
        }
    }
    public class Cowardly : AbilityBehaviour, IPassiveAttackBuff
    {
        public static Ability ability;
        public override Ability Ability => ability;
        private bool IsStrongCat => base.Card.Info.name == "wstl_scaredyCatStrong";

        private readonly string selfLoveDialogue = "There is strength in self-love.";
        private readonly string weakenedDialogue = "Your beast's moxie withers away.";

        public int GetPassiveAttackBuff(PlayableCard target)
        {
            if (this.Card.OnBoard && target == base.Card)
            {
                if (CatLoverCount() > 0)
                    return IsStrongCat ? 2 : 1;
                return -1;
            }
            return 0;
        }

        public override bool RespondsToResolveOnBoard()
        {
            return !IsStrongCat;
        }
        public override IEnumerator OnResolveOnBoard()
        {
            // Break if there are no cards with Cat Lover
            if (CatLoverCount() < 1)
                yield break;

            // transform if Scaredy Cat
            if (base.Card.Info.name == "wstl_scaredyCat")
            {
                CardInfo evolution = GetTransformCardInfo();

                // transform into evolution
                yield return base.PreSuccessfulTriggerSequence();
                yield return base.Card.TransformIntoCard(evolution);
                yield return new WaitForSeconds(0.5f);
            }

            // if this card also has Cat Lover, acknowledge its self-sufficiency
            if (base.Card.HasAbility(CatLover.ability) && !WstlSaveManager.HasSeenCowardlySelfLove)
            {
                WstlSaveManager.HasSeenCowardlySelfLove = true;
                yield return CustomMethods.PlayAlternateDialogue(dialogue: selfLoveDialogue);
            }
            else
                yield return base.LearnAbility();
        }

        public override bool RespondsToOtherCardResolve(PlayableCard otherCard)
        {
            if (otherCard.OpponentCard == base.Card.OpponentCard)
                return !IsStrongCat && otherCard.HasAbility(CatLover.ability);
            return false;
        }
        public override IEnumerator OnOtherCardResolve(PlayableCard otherCard)
        {
            // transform if Scaredy Cat
            if (base.Card.Info.name == "wstl_scaredyCat")
            {
                CardInfo evolution = GetTransformCardInfo();
                foreach (CardModificationInfo item in base.Card.Info.Mods.FindAll((CardModificationInfo x) => !x.nonCopyable))
                {
                    CardModificationInfo cardModificationInfo = (CardModificationInfo)item.Clone();
                    if (cardModificationInfo.HasAbility(Ability.Evolve))
                    {
                        cardModificationInfo.abilities.Remove(Ability.Evolve);
                    }
                    evolution.Mods.Add(cardModificationInfo);
                }

                // transform into evolution
                yield return base.PreSuccessfulTriggerSequence();
                yield return base.Card.TransformIntoCard(evolution);
                yield return new WaitForSeconds(0.5f);
            }

            // if this card also has Cat Lover, acknowledge its self-sufficiency
            if (base.Card.HasAbility(CatLover.ability) && !WstlSaveManager.HasSeenCowardlySelfLove)
            {
                WstlSaveManager.HasSeenCowardlySelfLove = true;
                yield return CustomMethods.PlayAlternateDialogue(dialogue: selfLoveDialogue);
            }
            else
                yield return base.LearnAbility();
        }

        public override bool RespondsToOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            return card.OpponentCard == base.Card.OpponentCard && card.HasAbility(CatLover.ability);
        }
        public override IEnumerator OnOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            // break if another Cat Lover exists
            if (CatLoverCount() > 1)
                yield break;

            // revert to previous forme if Scaredy Cat Strong
            if (base.Card.Info.name == "wstl_scaredyCatStrong")
            {
                CardInfo evolution = GetTransformCardInfo();
                foreach (CardModificationInfo item in base.Card.Info.Mods.FindAll((CardModificationInfo x) => !x.nonCopyable))
                {
                    CardModificationInfo cardModificationInfo = (CardModificationInfo)item.Clone();
                    if (cardModificationInfo.HasAbility(Ability.Evolve))
                    {
                        cardModificationInfo.abilities.Remove(Ability.Evolve);
                    }
                    evolution.Mods.Add(cardModificationInfo);
                }
                evolution.Mods.Add(HealthMod(evolution, base.Card.Info));

                // transform into evolution
                yield return base.PreSuccessfulTriggerSequence();
                yield return base.Card.TransformIntoCard(evolution);
                yield return new WaitForSeconds(0.5f);
            }

            if (!WstlSaveManager.HasSeenCowardlyWeaken)
            {
                WstlSaveManager.HasSeenCowardlyWeaken = true;
                yield return CustomMethods.PlayAlternateDialogue(dialogue: weakenedDialogue);
            }
        }

        private int CatLoverCount()
        {
            return Singleton<BoardManager>.Instance.GetSlots(!base.Card.OpponentCard)
                .Where((CardSlot s) => s.Card != null && s.Card.HasAbility(CatLover.ability))
                .Count();
        }
        private CardInfo GetTransformCardInfo()
        {
            if (base.Card.Info.evolveParams == null)
            {
                return EvolveParams.GetDefaultEvolution(base.Card.Info);
            }
            return base.Card.Info.evolveParams.evolution.Clone() as CardInfo;
        }
        private static CardModificationInfo HealthMod(CardInfo beastModeCard, CardInfo botModeCard)
        {
            return new CardModificationInfo
            {
                healthAdjustment = botModeCard.Health - beastModeCard.Health,
                nonCopyable = true
            };
        }
    }
}
