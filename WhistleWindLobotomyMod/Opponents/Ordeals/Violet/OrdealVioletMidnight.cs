using DiskCardGame;
using InscryptionAPI.Encounters;
using System.Collections.Generic;

namespace WhistleWindLobotomyMod.Opponents {
    public class OrdealVioletMidnight : OrdealBattleSequencer {
        public override void TryAddOrdealRandomBuff(PlayableCard card) {
            if (card.HasAbility(Delusion.ability)) {
                card.Info.Mods.Add(new(0, 3 * RunState.CurrentRegionTier + 2 * (RunState.Run.DifficultyModifier - 1)));
            }
        }
        public override int ConstructOrdealBlueprint(EncounterData encounterData, int baseDifficulty) {
            EncounterData.StartCondition cond = new();
            List<CardInfo> midnightOrdeals = new() {
                CardLoader.GetCardByName(Cards.godDelusionR),
                CardLoader.GetCardByName(Cards.godDelusionW),
                CardLoader.GetCardByName(Cards.godDelusionB),
                CardLoader.GetCardByName(Cards.godDelusionP)
            };
            midnightOrdeals = new(midnightOrdeals.Randomize());
            if (RunState.CurrentRegionTier + RunState.Run.DifficultyModifier > 1) {
                cond.cardsInOpponentSlots = midnightOrdeals.ToArray();
            }
            else {
                cond.cardsInOpponentQueue = midnightOrdeals.ToArray();
            }
            encounterData.startConditions.Add(cond);

            HighestPositiveScaleBalance = 0;
            ValidCards.Add(Cards.godDelusionR);
            ValidCards.Add(Cards.godDelusionW);
            ValidCards.Add(Cards.godDelusionB);
            ValidCards.Add(Cards.godDelusionP);
            return 4;
        }
    }
}