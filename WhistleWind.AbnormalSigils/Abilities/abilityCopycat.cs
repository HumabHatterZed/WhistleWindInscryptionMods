using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.Properties;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_Copycat()
        {
            const string rulebookName = "Copycat";
            const string rulebookDescription = "When [creature] is played, become an imperfect copy of the opposing card if it exists.";
            const string dialogue = "A near perfect impersonation.";
            Copycat.ability = AbnormalAbilityHelper.CreateAbility<Copycat>(
                Artwork.sigilCopycat, Artwork.sigilCopycat_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 2,
                modular: true, opponent: true, canStack: false).Id;
        }
    }
    public class Copycat : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToResolveOnBoard() => true;
        public override IEnumerator OnResolveOnBoard()
        {
            yield return base.PreSuccessfulTriggerSequence();

            // if the opposing card is null
            if (!(base.Card.OpposingCard() != null))
            {
                base.Card.Anim.StrongNegationEffect();
                yield return new WaitForSeconds(0.4f);
                yield return AbnormalDialogueManager.PlayDialogueEvent("CopycatFail");
                yield break;
            }
            yield return base.Card.TransformIntoCard(CopyOpposingCard(base.Card.OpposingCard().Info));
            yield return base.LearnAbility(0.5f);
        }
        private CardInfo CopyOpposingCard(CardInfo cardToCopy)
        {
            CardInfo baseInfo = base.Card.Info;
            CardInfo transformation = CardLoader.GetCardByName(cardToCopy.name);

            transformation.displayedName = baseInfo.displayedName;
            transformation.SetPortrait(baseInfo.portraitTex);
            transformation.SetPixelPortrait(baseInfo.pixelPortrait);

            foreach (CardModificationInfo mod in cardToCopy.Mods.FindAll((CardModificationInfo x) => !x.nonCopyable))
                transformation.Mods.Add((CardModificationInfo)mod.Clone());

            foreach (CardModificationInfo mod2 in baseInfo.Mods.FindAll((CardModificationInfo x) => !x.nonCopyable))
                transformation.Mods.Add((CardModificationInfo)mod2.Clone());

            CardModificationInfo imperfectMod = new();
            
            int currentRandomSeed = SaveManager.SaveFile.GetCurrentRandomSeed();
            float num = SeededRandom.Value(currentRandomSeed++);

            // add random sigil
            if (num < 0.33f && transformation.Mods.Exists((CardModificationInfo x) => x.abilities.Count > 0))
            {
                List<CardModificationInfo> list = new()
                {
                    new(Ability.Sharp)
                    {
                        fromCardMerge = true
                    }
                };

                if (transformation.Mods.Exists((CardModificationInfo x) => x.abilities.Count > 0))
                    list = transformation.Mods.FindAll((CardModificationInfo x) => x.abilities.Count > 0);
                
                list[SeededRandom.Range(0, list.Count, currentRandomSeed++)].abilities[0] = AbilitiesUtil.GetRandomLearnedAbility(currentRandomSeed++);
            }
            else if (num < 0.66f && transformation.Attack > 0) // add -1/2 stats
                imperfectMod.attackAdjustment = SeededRandom.Range(-1, 2, currentRandomSeed++);

            else if (transformation.Health > 1) // add 1/-2 stats
            {
                int num2 = Mathf.Min(2, transformation.Health - 1);
                imperfectMod.healthAdjustment = SeededRandom.Range(-num2, 3, currentRandomSeed++);
            }

            transformation.Mods.Add(imperfectMod);

            return transformation;
        }
    }
}
