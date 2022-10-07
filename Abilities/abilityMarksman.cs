using InscryptionAPI;
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
        private void Ability_Marksman()
        {
            const string rulebookName = "Marksman";
            const string rulebookDescription = "You may choose which opposing space a card bearing this sigil strikes.";
            const string dialogue = "Your beast strikes with precision.";

            Marksman.ability = AbilityHelper.CreateAbility<Marksman>(
                Artwork.sigilMarksman, Artwork.sigilMarksman_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 3,
                addModular: false, opponent: false, canStack: false, isPassive: false).Id;
        }
    }
    public class Marksman : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
        private bool IsDevil => base.Card.Info.name.ToLowerInvariant().Contains("derfreischutz");

        private readonly string freischutzDialogue = "The Devil proposed a childist contract.";
        private readonly string freischutzDialogue2 = "The seventh bullet would pierce the heart of his most beloved.";
        private readonly string freischutzDialogue3 = "On hearing this, the hunter sought and shot everyone he loved.";
        private int freischutzShots = 0;
        public override bool RespondsToDealDamage(int amount, PlayableCard target)
        {
            return amount > 0;
        }
        public override IEnumerator OnDealDamage(int amount, PlayableCard target)
        {
            yield return base.PreSuccessfulTriggerSequence();
            if (IsDevil)
            {
                freischutzShots++;
                if (freischutzShots >= 6)
                {
                    yield return SeventhBullet();
                }
            }
            yield return base.LearnAbility(0.4f);
        }

        public override bool RespondsToDealDamageDirectly(int amount)
        {
            return amount > 0;
        }
        public override IEnumerator OnDealDamageDirectly(int amount)
        {
            yield return base.PreSuccessfulTriggerSequence();
            if (IsDevil)
            {
                freischutzShots++;
                if (freischutzShots >= 6)
                {
                    yield return SeventhBullet();
                }
            }
            yield return base.LearnAbility(0.4f);
        }
        private IEnumerator SeventhBullet()
        {
            freischutzShots = 0;
            yield return new WaitForSeconds(0.5f);
            base.Card.Anim.StrongNegationEffect();
            yield return new WaitForSeconds(0.5f);

            if (!WstlSaveManager.HasSeenDerFreischutzSeventh)
            {
                WstlSaveManager.HasSeenDerFreischutzSeventh = true;
                yield return CustomMethods.PlayAlternateDialogue(Emotion.Neutral, DialogueEvent.Speaker.Leshy, 0.2f, freischutzDialogue, freischutzDialogue2, freischutzDialogue3);
                yield return new WaitForSeconds(0.3f);
            }

            foreach (var slot in Singleton<BoardManager>.Instance.GetSlots(!base.Card.OpponentCard).Where(slot => slot.Card != base.Card))
            {
                if (slot.Card != null)
                {
                    yield return slot.Card.TakeDamage(base.Card.Attack, base.Card);
                }
            }
        }
    }
}
