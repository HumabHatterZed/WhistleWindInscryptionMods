using DiskCardGame;
using System.Collections;
using UnityEngine;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod {
    public class Syrinx : SpecialCardBehaviour {
        public static SpecialTriggeredAbility specialAbility;
        public SpecialTriggeredAbility SpecialAbility => specialAbility;

        public const string rName = "Syrinx";
        public const string rDesc = "Whenever Nameless Fetus is sacrificed, it has a chance to awaken. Nameless Fetus is more likely to awaken the more it is sacrificed, and will always wake up when sacrificed 6 times.";

        private int sacrificeCount;

        public override bool RespondsToSacrifice() => true;

        public override IEnumerator OnSacrifice() {
            this.sacrificeCount++;

            if (this.sacrificeCount > 5 || SeededRandom.Range(0, 7 - this.sacrificeCount, base.GetRandomSeed()) == 0) {
                yield return new WaitForSeconds(0.25f);
                CardInfo cardByName = CardLoader.GetCardByName(Cards.namelessFetusAwake);
                yield return DialogueHelper.PlayDialogueEvent("NamelessFetusAwake", 0f);
                yield return base.PlayableCard.TransformIntoCard(cardByName);
                yield return new WaitForSeconds(0.5f);
            }
        }
    }
    public class RulebookEntrySyrinx : AbilityBehaviour {
        public static Ability ID { get; internal set; }
        public override Ability Ability => ID;
    }
    public partial class Abilities {
        private static void Rulebook_Syrinx()
            => RulebookEntrySyrinx.ID = LobotomyAbilityHelper.CreateRulebookAbility<RulebookEntrySyrinx>(Syrinx.rName, Syrinx.rDesc).Id;
        private static void AddSpecial_Syrinx()
            => Syrinx.specialAbility = AbilityHelper.CreateSpecialAbility<Syrinx>(LobotomyPlugin.pluginGuid, Syrinx.rName).Id;
    }
}
