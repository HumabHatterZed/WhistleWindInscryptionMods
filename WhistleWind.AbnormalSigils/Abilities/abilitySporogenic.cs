using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;

using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_Sporogenic()
        {
            const string rulebookName = "Sporogenic";
            const string rulebookDescription = "Creatures adjacent to this card gain 1 Spores at the end of its owner's turn. This sigil activates before other sigils.";
            const string dialogue = "They will love this curse like a blessing.";
            const string triggerText = "[creature] scatters spores on the adjacent cards!";
            Sporogenic.ability = AbnormalAbilityHelper.CreateAbility<Sporogenic>(
                "sigilSporogenic",
                rulebookName, rulebookDescription, dialogue, triggerText, powerLevel: 2,
                modular: false, opponent: true, canStack: false).Id;
        }
    }
    public class Sporogenic : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
        private bool CheckValid(PlayableCard card)
        {
            if (card != null)
                return card.LacksTrait(AbnormalPlugin.SporeFriend) && card.GetComponent<Spores>() == null;
            return false;
        }
        private readonly CardModificationInfo sporeStatusMod = new(StatusEffectSpores.ability)
        {
            singletonId = "bad_status_effect_spore",
            nonCopyable = true,
        };
        private readonly CardModificationInfo sporeDecalMod = new()
        {
            singletonId = "bad_status_effect_spore_decal",
            DecalIds = { "decalSpore_0" },
            nonCopyable = true,
        };
        public override bool RespondsToTurnEnd(bool playerTurnEnd) => base.Card.OpponentCard != playerTurnEnd;
        public override IEnumerator OnTurnEnd(bool playerTurnEnd)
        {
            PlayableCard leftCard = Singleton<BoardManager>.Instance.GetAdjacent(base.Card.Slot, true)?.Card;
            PlayableCard rightCard = Singleton<BoardManager>.Instance.GetAdjacent(base.Card.Slot, false)?.Card;
            bool leftValid = CheckValid(leftCard);
            bool rightValid = CheckValid(rightCard);

            if (!leftValid && !rightValid)
                yield break;

            yield return base.PreSuccessfulTriggerSequence();
            yield return HelperMethods.ChangeCurrentView(View.Board);

            if (leftValid)
                yield return AddSporesToCard(leftCard);
            if (rightValid)
                yield return AddSporesToCard(rightCard);

            base.LearnAbility(0.4f);
        }
        private IEnumerator AddSporesToCard(PlayableCard card)
        {
            card.Anim.LightNegationEffect();
            card.AddPermanentBehaviour<Spores>();
            card.GetComponent<Spores>().turnPlayed = Singleton<TurnManager>.Instance.TurnNumber;
            card.AddTemporaryMods(sporeStatusMod, sporeDecalMod);
            yield return new WaitForSeconds(0.1f);
        }
    }
}