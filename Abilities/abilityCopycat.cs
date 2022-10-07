using InscryptionAPI;
using DiskCardGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Ability_Copycat()
        {
            const string rulebookName = "Copycat";
            const string rulebookDescription = "When a card bearing this sigil is played, become a copy of the opposing card if it exists. There is a chance that the copy will be imperfect.";
            const string dialogue = "A near perfect impersonation.";
            Copycat.ability = AbilityHelper.CreateAbility<Copycat>(
                Resources.sigilCopycat, Resources.sigilCopycat_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 2,
                addModular: true, opponent: true, canStack: false, isPassive: false).Id;
        }
    }
    public class Copycat : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToResolveOnBoard()
        {
            return true;
        }
        public override IEnumerator OnResolveOnBoard()
        {
            yield return base.PreSuccessfulTriggerSequence();

            // if the opposing card is null
            if (!(base.Card.OpposingCard() != null))
            {
                base.Card.Anim.StrongNegationEffect();
                yield return new WaitForSeconds(0.4f);
                if (base.Card.Health < 1)
                {
                    // prevent potential softlock situations
                    if (base.Card.HasAbility(Ability.CorpseEater) && base.Card.HasAnyOfAbilities(Ability.DrawCopy, Ability.DrawCopyOnDeath))
                    {
                        yield return base.Card.DieTriggerless();
                    }
                    else
                        yield return base.Card.Die(false);
                    yield return new WaitForSeconds(0.4f);
                }
                if (!WstlSaveManager.CopycatFailed)
                {
                    WstlSaveManager.CopycatFailed = true;
                    yield return CustomMethods.PlayAlternateDialogue(dialogue: "The lie falls apart, revealing your pitiful true self.");
                }
                yield break;
            }
            yield return base.Card.TransformIntoCard(CopyOpposingCard(base.Card.OpposingCard().Info));
            yield return base.LearnAbility(0.5f);
        }
        private CardInfo CopyOpposingCard(CardInfo cardToCopy)
        {
            CardInfo cardByName = CardLoader.GetCardByName(cardToCopy.name);

            cardByName.portraitTex = base.Card.Info.portraitTex;
            cardByName.alternatePortrait = base.Card.Info.alternatePortrait;
            cardByName.pixelPortrait = base.Card.Info.pixelPortrait;
            cardByName.animatedPortrait = base.Card.Info.animatedPortrait;
            cardByName.displayedName = base.Card.Info.displayedName;

            foreach (CardModificationInfo item2 in cardToCopy.Mods.FindAll((CardModificationInfo x) => !x.nonCopyable))
            {
                CardModificationInfo item = (CardModificationInfo)item2.Clone();
                cardByName.Mods.Add(item);
            }
            CardModificationInfo cardModificationInfo = new();
            cardByName.Mods.Add(cardModificationInfo);
            CardModificationInfo cardModificationInfo2 = new();
            int currentRandomSeed = SaveManager.SaveFile.GetCurrentRandomSeed();
            float num = SeededRandom.Value(currentRandomSeed++);
            if (num < 0.33f && cardByName.Mods.Exists((CardModificationInfo x) => x.abilities.Count > 0))
            {
                List<CardModificationInfo> list = new() { new(Ability.Sharp) { fromCardMerge = true } };
                if (cardByName.Mods.Exists((CardModificationInfo x) => x.abilities.Count > 0))
                    list = cardByName.Mods.FindAll((CardModificationInfo x) => x.abilities.Count > 0);
                list[SeededRandom.Range(0, list.Count, currentRandomSeed++)].abilities[0] = AbilitiesUtil.GetRandomLearnedAbility(currentRandomSeed++);
            }
            else if (num < 0.66f && cardByName.Attack > 0)
            {
                cardModificationInfo2.attackAdjustment = SeededRandom.Range(-1, 2, currentRandomSeed++);
            }
            else if (cardByName.Health > 1)
            {
                int num2 = Mathf.Min(2, cardByName.Health - 1);
                cardModificationInfo2.healthAdjustment = SeededRandom.Range(-num2, 3, currentRandomSeed++);
            }
            cardByName.Mods.Add(cardModificationInfo2);
            return cardByName;
        }
    }
}
