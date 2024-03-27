using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections;
using System.Linq;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;


namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_Gardener()
        {
            const string rulebookName = "Gardener";
            const string rulebookDescription = "When an allied card is killed, create a Sapling in their place. [define:wstl_parasiteTreeSapling]";
            const string dialogue = "They proliferate and become whole. Can you feel it?";
            const string triggerText = "A sapling grows out of the dead card's corpse.";
            Gardener.ability = AbnormalAbilityHelper.CreateAbility<Gardener>(
                "sigilGardener",
                rulebookName, rulebookDescription, dialogue, triggerText, powerLevel: 4,
                modular: false, opponent: false, canStack: false).Id;
        }
    }
    public class Gardener : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
        public override bool RespondsToOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            if (fromCombat && card.OpponentCard == base.Card.OpponentCard && (deathSlot.Card == null || deathSlot.Card.Dead))
                return card != base.Card && base.Card.OnBoard && !card.Info.name.Equals("wstl_parasiteTreeSapling");

            return false;
        }
        public override IEnumerator OnOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            if (card.LacksAllTraits(Trait.Giant, Trait.Pelt, Trait.Terrain, Trait.Uncuttable))
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
            foreach (CardModificationInfo item in card.Info.Mods.Where((CardModificationInfo x) => !x.nonCopyable))
            {
                // Adds merged sigils
                CardModificationInfo cardModificationInfo = (CardModificationInfo)item.Clone();
                minion.Mods.Add(cardModificationInfo);
            }
            // Adds base sigils
            foreach (Ability item in card.Info.DefaultAbilities.Where((Ability x) => x != Ability.NUM_ABILITIES))
            {
                minion.Mods.Add(new CardModificationInfo(item) { nonCopyable = true });
            }
            yield return Singleton<BoardManager>.Instance.CreateCardInSlot(minion, slot, 0.15f);
        }
    }
}
