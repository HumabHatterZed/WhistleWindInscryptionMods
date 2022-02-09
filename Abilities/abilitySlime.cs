using System.Linq;
using System.Collections;
using System.Collections.Generic;
using DiskCardGame;
using UnityEngine;
using APIPlugin;
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
            return ActivateOnPlay();
        }
        public override IEnumerator OnResolveOnBoard()
        {
            yield return base.PreSuccessfulTriggerSequence();
            base.Card.Anim.StrongNegationEffect();
            yield return new WaitForSeconds(0.4f);

            CardInfo cardInfo = CardLoader.GetCardByName("wstl_meltingLoveMinion");

            bool traits;
            foreach (var slot in Singleton<BoardManager>.Instance.GetAdjacentSlots(base.Card.Slot).Where(slot => slot.Card != null))
            {
                traits = slot.Card.Info.HasTrait(Trait.Terrain) || slot.Card.Info.HasTrait(Trait.Pelt);
                if (!slot.Card.Info.name.ToLowerInvariant().Contains("meltinglove") && !traits)
                {
                    int killedHp = slot.Card.Info.baseHealth - 1 <= 0 ? 0 : slot.Card.Info.baseHealth - 1;
                    CardModificationInfo stats = new CardModificationInfo(0, killedHp);

                    cardInfo.Mods.Add(stats);

                    foreach (Ability item in slot.Card.Info.Abilities.FindAll((Ability x) => x != Ability.NUM_ABILITIES))
                    {
                        cardInfo.Mods.Add(new CardModificationInfo(item));
                    }

                    slot.Card.Anim.StrongNegationEffect();
                    yield return new WaitForSeconds(0.4f);
                    yield return slot.Card.TransformIntoCard(cardInfo);
                    yield return new WaitForSeconds(0.5f);
                    if (base.Card.Info.name.ToLowerInvariant().Equals("wstl_meltinglove"))
                    {
                        base.Card.Anim.StrongNegationEffect();
                        yield return new WaitForSeconds(0.4f);
                        base.Card.AddTemporaryMod(new(1, 2));
                    }
                    yield return base.LearnAbility(0.5f);
                }
            }
        }

        public override bool RespondsToOtherCardResolve(PlayableCard otherCard)
        {
            return ActivateOnPlay();
        }
        public override IEnumerator OnOtherCardResolve(PlayableCard otherCard)
        {
            yield return base.PreSuccessfulTriggerSequence();
            base.Card.Anim.StrongNegationEffect();
            yield return new WaitForSeconds(0.4f);

            CardInfo cardInfo = CardLoader.GetCardByName("wstl_meltingLoveMinion");

            bool traits;
            foreach (var slot in Singleton<BoardManager>.Instance.GetAdjacentSlots(base.Card.Slot).Where(slot => slot.Card != null))
            {
                traits = slot.Card.Info.HasTrait(Trait.Terrain) || slot.Card.Info.HasTrait(Trait.Pelt);
                if (!slot.Card.Info.name.ToLowerInvariant().Contains("meltinglove") && !traits)
                {
                    int killedHp = slot.Card.Info.baseHealth - 1 <= 0 ? 0 : slot.Card.Info.baseHealth - 1;
                    CardModificationInfo stats = new CardModificationInfo(0, killedHp);

                    cardInfo.Mods.Add(stats);

                    foreach (Ability item in slot.Card.Info.Abilities.FindAll((Ability x) => x != Ability.NUM_ABILITIES))
                    {
                        cardInfo.Mods.Add(new CardModificationInfo(item));
                    }

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
        public bool ActivateOnPlay()
        {
            int num = 0;
            bool traits;
            foreach (CardSlot slot in Singleton<BoardManager>.Instance.GetAdjacentSlots(base.Card.Slot))
            {
                if (slot.Card != null)
                {
                    traits = slot.Card.Info.HasTrait(Trait.Terrain) || slot.Card.Info.HasTrait(Trait.Pelt);
                    if (!slot.Card.Info.name.ToLowerInvariant().Contains("meltinglove") && !traits)
                    {
                        num++;
                    }
                }
            }
            return num > 0;
        }
    }
}
