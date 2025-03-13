using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections;
using UnityEngine;

namespace BonniesBakingPack
{
    public partial class BakingPlugin
    {
        private void AddFreshIngredientsMagnificus()
        {
            const string rulebookName = "Fresh Ingredients Magnificus";
            const string rulebookDescription = "When [creature] strikes a card and it perishes, create a random Food in your hand.";
            const string dialogue = "Made with the freshest ingredients.";

            FreshIngredientsMagnificus.ability = AbilityManager.New(pluginGuid, rulebookName, rulebookDescription, typeof(FreshIngredientsMagnificus), GetTexture("sigilFreshIngredients.png"))
                .SetAbilityLearnedDialogue(dialogue)
                .SetRulebookName("Fresh Ingredients")
                .SetPowerlevel(4)
                .SetPixelAbilityIcon(GetTexture("sigilFreshIngredients_pixel.png"))
                .AddMetaCategories(AbilityMetaCategory.MagnificusRulebook)
                .ability;
        }
    }

    public class FreshIngredientsMagnificus : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            return killer == base.Card;
        }

        public override IEnumerator OnOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            yield return base.PreSuccessfulTriggerSequence();
            CardInfo info = CardLoader.GetCardByName(FreshFood.GetRandomFoodName(base.GetRandomSeed()));
            ViewManager.Instance.SwitchToView(View.Hand);
            yield return new WaitForSeconds(0.2f);
            yield return CardSpawner.Instance.SpawnCardToHand(info);
            yield return new WaitForSeconds(0.5f);
            yield return base.LearnAbility();
            ViewManager.Instance.SwitchToView(View.Board);
        }
    }
}