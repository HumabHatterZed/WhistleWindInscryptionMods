using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using Pixelplacement;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_ExplosiveOpening()
        {
            const string rulebookName = "Explosive Opening";
            const string rulebookDescription = "When this card is played, adjacent and opposing cards are dealt 10 damage.";
            const string dialogue = "A bitter grudge laid bare.";

            AbilityManager.FullAbility ab = AbnormalAbilityHelper.CreateAbility<ExplosiveOpening>(
                "sigilExplosiveOpening",
                rulebookName, rulebookDescription, dialogue, powerLevel: 1,
                modular: false, opponent: true, canStack: false)
                .SetPart3Rulebook()
                .SetGrimoraRulebook()
                .SetMagnificusRulebook();
            ab.SetCustomFlippedTexture(TextureLoader.LoadTextureFromFile("sigilExplosiveOpening_flipped.png"));
            
            ExplosiveOpening.ability = ab.Id;
        }
    }
    public class ExplosiveOpening : ExplodeOnDeath
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToResolveOnBoard() => true;
        public override bool RespondsToPreDeathAnimation(bool wasSacrifice) => false;
        public override IEnumerator OnResolveOnBoard()
        {
            base.Card.Anim.LightNegationEffect();
            yield return base.PreSuccessfulTriggerSequence();
            
            yield return ExplodeFromSlots(base.Card.Slot);
            yield return base.LearnAbility(0.25f);
        }
        private IEnumerator ExplodeFromSlots(CardSlot slot)
        {
            List<CardSlot> slots = BoardManager.Instance.GetSlotsCopy(slot.IsPlayerSlot).Where(x => x.Card == slot.Card).ToList();
            List<PlayableCard> opposingCards = slots.Select(x => x.opposingSlot.Card).ToList();
            List<PlayableCard> adjacentCards = slots.SelectMany(BoardManager.Instance.GetAdjacentSlots, (slot, adjacent) => adjacent.Card).Distinct().ToList();
            adjacentCards.RemoveAll(x => x == null || x == slot.Card);
            opposingCards.RemoveAll(x => x == null);

            if (adjacentCards.Count == 0 && opposingCards.Count == 0)
                yield break;

            List<PlayableCard> targets = adjacentCards.Concat(opposingCards).ToList();

            // perform visual effects first
            List<GameObject> bombs = new();
            if (!SaveManager.SaveFile.IsPart1 && this.bombPrefab != null)
            {
                foreach (PlayableCard card in targets)
                {
                    GameObject bomb = Object.Instantiate(this.bombPrefab);
                    bomb.transform.position = slot.Card.transform.position + Vector3.up * 0.1f;
                    Tween.Position(bomb.transform, card.transform.position + Vector3.up * 0.1f, 0.5f, 0f, Tween.EaseLinear);
                    bombs.Add(bomb);
                }
                yield return new WaitForSeconds(0.5f);

            }

            targets.ForEach(x => x.Anim.PlayHitAnimation());
            for (int i = 0; i < targets.Count; i++)
            {
                targets[i].Anim.PlayHitAnimation();
                
                if (i < bombs.Count)
                    Destroy(bombs[i]);

                yield return targets[i].TakeDamage(10, slot.Card);
            }
        }
    }
}
