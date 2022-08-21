using InscryptionAPI;
using DiskCardGame;
using System.Collections;
using System.Linq;
using UnityEngine;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Ability_Gardener()
        {
            const string rulebookName = "Gardener";
            const string rulebookDescription = "When an ally card dies, create a Sapling in their place. A Sapling is defined as: 0 Power, 2 Health.";
            const string dialogue = "They proliferate and become whole. Can you feel it?";
            Gardener.ability = AbilityHelper.CreateAbility<Gardener>(
                Resources.sigilGardener, Resources.sigilGardener_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 4,
                addModular: false, opponent: false, canStack: false, isPassive: false).Id;
        }
    }
    public class Gardener : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
        public override bool RespondsToOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            if (base.Card.OnBoard && card != base.Card)
            {
                return fromCombat && card.OpponentCard == base.Card.OpponentCard && !(deathSlot != null) && !card.Info.name.Equals("wstl_parasiteTreeSapling");
            }
            return false;
        }
        public override IEnumerator OnOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            if (card != null && !card.Info.HasTrait(Trait.Terrain) && !card.Info.HasTrait(Trait.Pelt))
            {
                yield return PreSuccessfulTriggerSequence();
                base.Card.Anim.StrongNegationEffect();
                yield return new WaitForSeconds(0.4f);
                yield return SpawnCardOnSlot(card, deathSlot);
                yield return LearnAbility(0.4f);
            }
        }

        private IEnumerator SpawnCardOnSlot(PlayableCard card, CardSlot slot)
        {
            CardInfo minion = CardLoader.GetCardByName("wstl_parasiteTreeSapling");
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
            yield return Singleton<BoardManager>.Instance.CreateCardInSlot(minion, slot, 0.15f);
        }
    }
}
