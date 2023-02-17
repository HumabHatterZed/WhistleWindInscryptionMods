using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections;
using System.Linq;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.Properties;

namespace WhistleWind.AbnormalSigils
{
    public class Spores : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        private bool CheckValid(CardSlot slot)
        {
            return slot != null && slot.Card != null && slot.Card.Info.name != "wstl_theLittlePrinceMinion" && slot.Card.LacksSpecialAbility(SporeDamage.specialAbility);
        }
        private IEnumerator EmitSpores(PlayableCard card)
        {
            yield return base.PreSuccessfulTriggerSequence();
            card.AddPermanentBehaviour<SporeDamage>();
        }

        public override bool RespondsToTurnEnd(bool playerTurnEnd)
        {
            return base.Card.OpponentCard != playerTurnEnd;
        }
        public override IEnumerator OnTurnEnd(bool playerTurnEnd)
        {
            bool gaveSpore = false;
            foreach (CardSlot slot in Singleton<BoardManager>.Instance.GetAdjacentSlots(base.Card.Slot).Where(s => s.Card != null))
            {
                if (CheckValid(slot))
                {
                    gaveSpore = true;
                    yield return EmitSpores(slot.Card);
                }
            }
            if (gaveSpore)
                base.LearnAbility(0.4f);
        }
    }
    public class RulebookEntrySpore : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }

    public partial class AbnormalPlugin
    {
        private void Ability_FungalInfector()
        {
            const string rulebookName = "Fungal Infector";
            const string rulebookDescription = "At the end of the owner's turn, adjacent cards gain 1 Spore.";
            const string dialogue = "Even if this turns out to be a curse, they will love this curse like a blessing.";
            Spores.ability = AbnormalAbilityHelper.CreateAbility<Spores>(
                Artwork.sigilSpores, Artwork.sigilSpores_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 2,
                modular: false, opponent: false, canStack: false).Id;
        }
        private void Rulebook_Spore()
        {
            const string rulebookName = "Spore";
            const string rulebookDescription = "At the end of the owner's turn, [creature] will take [spore:X] damage. When this card dies, create a Spore Mold Creature in its place with [spore:X] Power and Health.";
            RulebookEntrySpore.ability = AbnormalAbilityHelper.CreateRulebookAbility<RulebookEntrySpore>(rulebookName, rulebookDescription).Id;
        }
    }
}