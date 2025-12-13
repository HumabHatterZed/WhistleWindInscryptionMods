using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections;
using WhistleWind.Core.Helpers;

namespace WhistleWindLobotomyMod {
    public class MimicryCombat : SpecialCardBehaviour {
        public static SpecialTriggeredAbility specialAbility;
        public SpecialTriggeredAbility SpecialAbility => specialAbility;

        private bool foundNewDisguise = false;
        public override bool RespondsToOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer) {
            if (!foundNewDisguise && fromCombat && killer == base.PlayableCard && !base.PlayableCard.OpponentCard && !base.PlayableCard.OriginatedFromQueue) {
                return card.LacksAllTraits(Trait.Giant, Trait.DeathcardCreationNonOption, Trait.Uncuttable);
            }
            return false;
        }
        public override IEnumerator OnOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer) {
            CardModificationInfo mod = Mimicry.GetNothingThereMod();
            if (mod != null) {
                mod.SetSingletonId("NothingThere:" + (card.Info.name == "!DEATHCARD_BASE" ? Cards.nothingThere : card.Info.name));
                RunState.Run.playerDeck.UpdateModDictionary();
            }
            yield break;
        }
    }

    public partial class Abilities {
        private static void AddSpecial_MimicryCombat()
            => MimicryCombat.specialAbility = AbilityHelper.CreateSpecialAbility<MimicryCombat>(LobotomyPlugin.pluginGuid, "MimicryCombat").Id;
    }
}
