using DiskCardGame;
using System.Collections;
using System.Linq;
using UnityEngine;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public class Pink : SpecialCardBehaviour
    {
        public static SpecialTriggeredAbility specialAbility;
        public SpecialTriggeredAbility SpecialAbility => specialAbility;

        public const string rName = "Pink";
        public const string rDesc = "Army in Pink will transform when 3 ally cards die.";
        private int deaths = 0;

        public override bool RespondsToOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            // return true if the target was in an adjacent slot
            if (base.PlayableCard.OnBoard && fromCombat)
                return card.OpponentCard == base.PlayableCard.OpponentCard;

            return false;
        }
        public override IEnumerator OnOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            deaths++;
            if (deaths != 3)
                yield break;

            yield return new WaitForSeconds(0.15f);
            base.Card.Anim.StrongNegationEffect();
            yield return new WaitForSeconds(0.4f);

            // reset damage taken since the evolution has less base Health
            CardInfo cardByName = HelperMethods.GetInfoWithMods(base.PlayableCard, "wstl_armyInBlack");
            yield return base.PlayableCard.TransformIntoCard(cardByName);
            yield return new WaitForSeconds(0.5f);
            yield return CreateArmyInHand();
            yield return DialogueHelper.PlayDialogueEvent("ArmyInBlackTransform");
        }
        private IEnumerator CreateArmyInHand()
        {
            CardInfo cardByName = CardLoader.GetCardByName("wstl_armyInBlackSpell");

            HelperMethods.ChangeCurrentView(View.Hand);
            
            for (int i = 0; i < 2; i++)
                yield return Singleton<CardSpawner>.Instance.SpawnCardToHand(cardByName, null, 0.25f, null);

            yield return new WaitForSeconds(0.45f);
        }
    }
    public class RulebookEntryPink : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }
    public partial class LobotomyPlugin
    {
        private void Rulebook_Pink()
            => RulebookEntryPink.ability = LobotomyAbilityHelper.CreateRulebookAbility<RulebookEntryPink>(Pink.rName, Pink.rDesc).Id;
        private void SpecialAbility_Pink()
            => Pink.specialAbility = AbilityHelper.CreateSpecialAbility<Pink>(pluginGuid, Pink.rName).Id;
    }
}
