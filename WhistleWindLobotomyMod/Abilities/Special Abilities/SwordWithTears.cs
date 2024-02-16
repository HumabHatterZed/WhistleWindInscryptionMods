using DiskCardGame;
using System.Collections;
using UnityEngine;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public class SwordWithTears : SpecialCardBehaviour
    {
        public static SpecialTriggeredAbility specialAbility;
        public SpecialTriggeredAbility SpecialAbility => specialAbility;

        public const string rName = "The Sword Sharpened with Tears";
        public const string rDesc = "Knight of Despair and Servant of Wrath will transform when an adjacent card dies.";

        public override bool RespondsToOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            // return true if the target was in an adjacent slot
            if (base.PlayableCard.OnBoard && fromCombat)
                return Singleton<BoardManager>.Instance.GetAdjacentSlots(base.PlayableCard.Slot).Contains(deathSlot);

            return false;
        }
        public override IEnumerator OnOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            yield return new WaitForSeconds(0.15f);
            base.Card.Anim.StrongNegationEffect();
            yield return new WaitForSeconds(0.4f);

            CardInfo cardByName;
            switch (base.PlayableCard.Info.name)
            {
                case "wstl_magicalGirlSpade":
                    cardByName = HelperMethods.GetInfoWithMods(base.PlayableCard, "wstl_knightOfDespair");
                    yield return base.PlayableCard.TransformIntoCard(cardByName);
                    yield return new WaitForSeconds(0.5f);
                    yield return DialogueHelper.PlayDialogueEvent("KnightOfDespairTransform");
                    break;
                case "wstl_magicalGirlClover":
                    cardByName = HelperMethods.GetInfoWithMods(base.PlayableCard, "wstl_servantOfWrath");
                    yield return base.PlayableCard.TransformIntoCard(cardByName);
                    yield return new WaitForSeconds(0.5f);
                    yield return DialogueHelper.PlayDialogueEvent("ServantOfWrathTransform");
                    break;
            }
        }
    }
    public class RulebookEntrySwordWithTears : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }
    public partial class LobotomyPlugin
    {
        private void Rulebook_SwordWithTears()
            => RulebookEntrySwordWithTears.ability = LobotomyAbilityHelper.CreateRulebookAbility<RulebookEntrySwordWithTears>(SwordWithTears.rName, SwordWithTears.rDesc).Id;
        private void SpecialAbility_SwordWithTears()
            => SwordWithTears.specialAbility = AbilityHelper.CreateSpecialAbility<SwordWithTears>(pluginGuid, SwordWithTears.rName).Id;
    }
}
