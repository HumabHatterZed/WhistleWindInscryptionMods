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
        private void Ability_Bloodfiend()
        {
            const string rulebookName = "Bloodfiend";
            const string rulebookDescription = "When a card bearing this sigil deals damage to an opposing card, it gains 1 Health.";
            const string dialogue = "Accursed fiend.";

            Bloodfiend.ability = AbilityHelper.CreateAbility<Bloodfiend>(
                Resources.sigilBloodfiend,
                rulebookName, rulebookDescription, dialogue, 3,
                addModular: true).Id;
        }
    }
    public class Bloodfiend : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        private readonly string censoredDialogue = "What have you done to my beast?";

        public override bool RespondsToDealDamage(int amount, PlayableCard target)
        {
            return amount > 0 && base.Card.Health > 0;
        }
        public override IEnumerator OnDealDamage(int amount, PlayableCard target)
        {
            yield return base.PreSuccessfulTriggerSequence();
            base.Card.HealDamage(1);
            base.Card.OnStatsChanged();
            base.Card.Anim.StrongNegationEffect();
            yield return new WaitForSeconds(0.4f);
            yield return base.LearnAbility(0.4f);
        }

        public override bool RespondsToOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            if (killer != null)
            {
                return fromCombat && killer == base.Card && base.Card.Info.name.ToLowerInvariant().Equals("wstl_censored") &&
                    !card.Info.HasTrait(Trait.Terrain) && !card.Info.HasTrait(Trait.Pelt);
            }
            return false;
        }

        public override IEnumerator OnOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            #region CENSORED
            base.Card.Anim.StrongNegationEffect();
            yield return new WaitForSeconds(0.4f);
            if (Singleton<ViewManager>.Instance.CurrentView != View.Hand)
            {
                yield return new WaitForSeconds(0.2f);
                Singleton<ViewManager>.Instance.SwitchToView(View.Hand, false, false);
                yield return new WaitForSeconds(0.2f);
            }
            // Creates a minion that has the abilities, tribes, power of the killed card
            CardInfo minion = CardLoader.GetCardByName("wstl_censoredMinion");
            List<CardModificationInfo> killedInfo = new();

            int killedAtk = card.Info.baseAttack - 1 <= 0 ? 0 : card.Info.baseAttack - 1;
            CardModificationInfo stats = new(killedAtk, 0);

            killedInfo.Add(stats);

            foreach (CardModificationInfo item in card.Info.Mods.FindAll((CardModificationInfo x) => !x.nonCopyable))
            {
                // Adds merged sigils
                CardModificationInfo cardModificationInfo = (CardModificationInfo)item.Clone();
                cardModificationInfo.fromCardMerge = true;
                minion.Mods.Add(cardModificationInfo);
            }
            foreach (Ability item in card.Info.Abilities.FindAll((Ability x) => x != Ability.NUM_ABILITIES))
            {
                // Adds base sigils
                minion.Mods.Add(new CardModificationInfo(item));
            }
            foreach (Tribe item in card.Info.tribes.FindAll((Tribe x) => x != Tribe.NUM_TRIBES))
            {
                // Adds tribes
                minion.tribes.Add(item);
            }

            minion.displayedName = card.Info.displayedName;
            minion.appearanceBehaviour = card.Info.appearanceBehaviour;

            yield return Singleton<CardSpawner>.Instance.SpawnCardToHand(minion, killedInfo, 0.25f, null);

            yield return new WaitForSeconds(0.45f);
            if (!PersistentValues.HasSeenCensoredKill)
            {
                PersistentValues.HasSeenCensoredKill = true;
                yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(censoredDialogue, -0.65f, 0.4f, Emotion.Surprise);
            }
            yield return new WaitForSeconds(0.25f);
            Singleton<ViewManager>.Instance.SwitchToView(View.Board, false, false);
            #endregion
        }
    }
}
