using WhistleWind.Core.Helpers;
using DiskCardGame;
using System.Collections;
using UnityEngine;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public class Syrinx : SpecialCardBehaviour
    {
        public static SpecialTriggeredAbility specialAbility;
        public SpecialTriggeredAbility SpecialAbility => specialAbility;

        public static readonly string rName = "Syrinx";
        public static readonly string rDesc = "Nameless Fetus transforms when sacrificed six times.";

        private int sacrificeCount;

        public override bool RespondsToSacrifice() => true;

        public override IEnumerator OnSacrifice()
        {
            this.sacrificeCount++;

            if (this.sacrificeCount >= 6)
            {
                yield return new WaitForSeconds(0.25f);
                CardInfo cardByName = CardLoader.GetCardByName("wstl_namelessFetusAwake");
                yield return DialogueEventsManager.PlayDialogueEvent("NamelessFetusAwake", 0f);
                yield return base.PlayableCard.TransformIntoCard(cardByName);
                yield return new WaitForSeconds(0.5f);
            }
        }
    }
    public class RulebookEntrySyrinx : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }
    public partial class LobotomyPlugin
    {
        private void Rulebook_Syrinx()
        {
            RulebookEntrySyrinx.ability = LobotomyAbilityHelper.CreateRulebookAbility<RulebookEntrySyrinx>(Syrinx.rName, Syrinx.rDesc).Id;
        }
        private void SpecialAbility_Syrinx()
        {
            Syrinx.specialAbility = AbilityHelper.CreateSpecialAbility<Syrinx>(pluginGuid, Syrinx.rName).Id;
        }
    }
}
