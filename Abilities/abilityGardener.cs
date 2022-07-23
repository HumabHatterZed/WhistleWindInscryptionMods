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
            const string rulebookDescription = "When an ally card dies, create a Sapling in their place. A slot is defined as: 1 Power, 2 Health.";
            const string dialogue = "They proliferate and become whole. Can you feel it?";
            Gardener.ability = AbilityHelper.CreateAbility<Gardener>(
                Resources.sigilGardener,// Resources.sigilGardener_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 4).Id;
        }
    }
    public class Gardener : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
        public override bool RespondsToOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            return fromCombat && base.Card.Slot.IsPlayerSlot && deathSlot.IsPlayerSlot;
        }
        public override IEnumerator OnOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            if (card != null && !card.Info.HasTrait(Trait.Terrain) && !card.Info.HasTrait(Trait.Pelt))
            {
                if (!card.Info.name.ToLowerInvariant().Contains("parasitetreesapling") && card != base.Card && base.Card != null)
                {
                    yield return PreSuccessfulTriggerSequence();

                    base.Card.Anim.StrongNegationEffect();
                    yield return new WaitForSeconds(0.4f);

                    yield return SpawnCardOnSlot(deathSlot);

                    yield return new WaitForSeconds(0.4f);
                    yield return LearnAbility(0.4f);
                }
            }
        }

        private IEnumerator SpawnCardOnSlot(CardSlot slot)
        {
            yield return Singleton<BoardManager>.Instance.CreateCardInSlot(CardLoader.GetCardByName("wstl_parasiteTreeSapling"), slot, 0.15f);
        }
    }
}
