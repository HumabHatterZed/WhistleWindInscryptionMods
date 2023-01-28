using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.Properties;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_Gardener()
        {
            const string rulebookName = "Gardener";
            const string rulebookDescription = "When an ally card is killed, create a Sapling in their place. [define:wstl_parasiteTreeSapling]";
            const string dialogue = "They proliferate and become whole. Can you feel it?";
            Gardener.ability = AbnormalAbilityHelper.CreateAbility<Gardener>(
                Artwork.sigilGardener, Artwork.sigilGardener_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 4,
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
                return base.Card.OnBoard && !card.Info.name.Equals("wstl_parasiteTreeSapling");

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
