using APIPlugin;
using DiskCardGame;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private NewAbility Ability_Slime()
        {
            const string rulebookName = "Made of Slime";
            const string rulebookDescription = "A card bearing this sigil takes 1 less damage from attacks. Additionally, cards adjacent to this card are turned into Slimes at the start of the owner's turn.";
            const string dialogue = "Its army grows everyday.";

            return WstlUtils.CreateAbility<Slime>(
                Resources.sigilSlime,
                rulebookName, rulebookDescription, dialogue, 5);
        }
    }
    public class Slime : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToResolveOnBoard()
        {
            int num = 0;
            bool hasAbility;
            bool traits;
            foreach (CardSlot slot in Singleton<BoardManager>.Instance.GetAdjacentSlots(base.Card.Slot))
            {
                if (slot.Card != null)
                {
                    hasAbility = slot.Card.Info.HasAbility(Slime.ability);
                    traits = slot.Card.Info.HasTrait(Trait.Terrain) || slot.Card.Info.HasTrait(Trait.Pelt);
                    if (!traits && !hasAbility)
                    {
                        num++;
                    }
                }
            }
            return num > 0;
        }
        public override IEnumerator OnResolveOnBoard()
        {
            yield return base.PreSuccessfulTriggerSequence();
            base.Card.Anim.StrongNegationEffect();
            yield return new WaitForSeconds(0.4f);

            bool hasAbility;
            bool traits;
            foreach (var slot in Singleton<BoardManager>.Instance.GetAdjacentSlots(base.Card.Slot).Where(slot => slot.Card != null))
            {
                CardInfo cardInfo = CardLoader.GetCardByName("wstl_meltingLoveMinion");

                // does not affect terrain or pelts
                hasAbility = slot.Card.Info.HasAbility(Slime.ability);
                traits = slot.Card.Info.HasTrait(Trait.Terrain) || slot.Card.Info.HasTrait(Trait.Pelt);

                if (!traits && !hasAbility)
                {
                    // gains the killed card's Power, Health/2, sigils
                    int killedHp = Mathf.CeilToInt(slot.Card.Health / 2) <= 0 ? 1 : Mathf.CeilToInt(slot.Card.Health / 2);
                    int killedAtk = slot.Card.Info.baseAttack;
                    CardModificationInfo stats = new(killedAtk, killedHp);

                    cardInfo.Mods.Add(stats);

                    foreach (CardModificationInfo item in slot.Card.Info.Mods.FindAll((CardModificationInfo x) => !x.nonCopyable))
                    {
                        // Adds merged sigils
                        CardModificationInfo cardModificationInfo = (CardModificationInfo)item.Clone();
                        cardInfo.Mods.Add(cardModificationInfo);
                    }

                    cardInfo.displayedName = slot.Card.Info.displayedName;

                    slot.Card.Anim.StrongNegationEffect();
                    yield return new WaitForSeconds(0.4f);
                    yield return slot.Card.TransformIntoCard(cardInfo);
                    yield return new WaitForSeconds(0.5f);
                    yield return base.LearnAbility(0.5f);

                    if (base.Card.Info.name.ToLowerInvariant().Equals("wstl_meltinglove"))
                    {
                        base.Card.Anim.StrongNegationEffect();
                        yield return new WaitForSeconds(0.4f);
                        base.Card.AddTemporaryMod(new(1, 2));
                    }
                }
            }
        }

        public override bool RespondsToOtherCardResolve(PlayableCard otherCard)
        {
            bool hasAbility;
            bool traits;
            foreach (CardSlot slot in Singleton<BoardManager>.Instance.GetAdjacentSlots(base.Card.Slot))
            {
                if (slot.Card != null)
                {
                    hasAbility = slot.Card.Info.HasAbility(Slime.ability);
                    traits = slot.Card.Info.HasTrait(Trait.Terrain) || slot.Card.Info.HasTrait(Trait.Pelt);
                    if (!traits && !hasAbility)
                    {
                        return slot.Card == otherCard;
                    }
                }
            }
            return false;
        }
        public override IEnumerator OnOtherCardResolve(PlayableCard otherCard)
        {
            yield return base.PreSuccessfulTriggerSequence();
            base.Card.Anim.StrongNegationEffect();
            yield return new WaitForSeconds(0.4f);

            CardInfo cardInfo = CardLoader.GetCardByName("wstl_meltingLoveMinion");

            // does not affect terrain or pelts
            // gains the killed card's Power, Health/2, sigils
            int killedHp = Mathf.CeilToInt(otherCard.Health / 2) <= 0 ? 1 : Mathf.CeilToInt(otherCard.Health / 2);
            int killedAtk = otherCard.Info.baseAttack;
            CardModificationInfo stats = new(killedAtk, killedHp);

            cardInfo.Mods.Add(stats);

            foreach (CardModificationInfo item in otherCard.Info.Mods.FindAll((CardModificationInfo x) => !x.nonCopyable))
            {
                // Adds merged sigils
                CardModificationInfo cardModificationInfo = (CardModificationInfo)item.Clone();
                cardInfo.Mods.Add(cardModificationInfo);
            }
            foreach (Ability item in otherCard.Info.Abilities.FindAll((Ability x) => x != Ability.NUM_ABILITIES))
            {
                // Adds base sigils
                cardInfo.Mods.Add(new CardModificationInfo(item));
            }

            cardInfo.displayedName = otherCard.Info.displayedName;

            otherCard.Anim.StrongNegationEffect();
            yield return new WaitForSeconds(0.4f);
            yield return otherCard.TransformIntoCard(cardInfo);
            yield return new WaitForSeconds(0.5f);
            yield return base.LearnAbility(0.5f);

            if (base.Card.Info.name.ToLowerInvariant().Equals("wstl_meltinglove"))
            {
                base.Card.Anim.StrongNegationEffect();
                yield return new WaitForSeconds(0.4f);
                base.Card.AddTemporaryMod(new(1, 2));
            }

            if (Singleton<ViewManager>.Instance.CurrentView != View.Default)
            {
                yield return new WaitForSeconds(0.2f);
                Singleton<ViewManager>.Instance.SwitchToView(View.Default, false, false);
                yield return new WaitForSeconds(0.2f);
            }
        }

        public override bool RespondsToTakeDamage(PlayableCard source)
        {
            if (source != null)
            {
                return source.Health > 0;
            }
            return false;
        }
        public override IEnumerator OnTakeDamage(PlayableCard source)
        {
            yield return base.PreSuccessfulTriggerSequence();
            base.Card.Status.damageTaken -= 1;
        }
    }
}
