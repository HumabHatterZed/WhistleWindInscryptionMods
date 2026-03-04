using Core.Helpers;
using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections;
using UnityEngine;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod {
    public class CensoredSpecial : SpecialCardBehaviour {
        public static SpecialTriggeredAbility specialAbility;
        public SpecialTriggeredAbility SpecialAbility => specialAbility;

        public const string rName = "CENSORED";
        public const string rDesc = "Whenver CENSORED kills a card, create a CENSORED in your hand with the killed card's Power.";

        public override bool RespondsToOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer) {
            if (fromCombat)
                return killer == base.PlayableCard && card.LacksAllTraits(Trait.Giant, Trait.Terrain, Trait.Pelt);

            return false;
        }

        public override IEnumerator OnOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer) {
            // Creates a minion that has the abilities, tribes, power of the killed card
            CardInfo minion = CardLoader.GetCardByName(Cards.censoredMinion);

            int newAttack = card.Info.baseAttack > 0 ? card.Info.baseAttack - 1 : 0;

            minion.Mods.Add(new(newAttack, 0) {
                nameReplacement = card.Info.displayedName
            });

            base.PlayableCard.Anim.StrongNegationEffect();
            yield return new WaitForSeconds(0.4f);

            // create minion in hand if not an opponent, otherwise add to queue
            yield return CombatHelpers.QueueOrCreateDrawnCard(minion, base.PlayableCard.OpponentCard);
            yield return DialogueHelper.PlayDialogueEvent("CENSOREDKilledCard");
            yield return new WaitForSeconds(0.25f);
        }
    }
    public class RulebookEntryCensoredSpecial : AbilityBehaviour {
        public static Ability ID { get; internal set; }
        public override Ability Ability => ID;
    }
    public partial class Abilities {
        private static void Rulebook_CensoredSpecial()
            => RulebookEntryCensoredSpecial.ID = LobotomyAbilityHelper.CreateRulebookAbility<RulebookEntryCensoredSpecial>(CensoredSpecial.rName, CensoredSpecial.rDesc).Id;
        private static void AddSpecial_CensoredSpecial()
            => CensoredSpecial.specialAbility = AbilityHelper.CreateSpecialAbility<CensoredSpecial>(LobotomyPlugin.pluginGuid, CensoredSpecial.rName).Id;
    }
}
