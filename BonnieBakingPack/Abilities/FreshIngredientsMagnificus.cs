using DiskCardGame;
using InscryptionAPI.Card;

namespace BonniesBakingPack {
    public partial class BakingPlugin {
        private void AddFreshIngredientsMagnificus() {
            const string rulebookName = "Fresh Ingredients Magnificus";
            const string rulebookDescription = "When [creature] strikes a card and it perishes, create a random Food in your hand.";
            const string dialogue = "Made with the freshest ingredients.";

            FreshIngredientsMagnificus.Id = AbilityManager.New(pluginGuid, rulebookName, rulebookDescription, typeof(FreshIngredientsMagnificus), GetTexture("sigilFreshIngredients.png"))
                .SetAbilityLearnedDialogue(dialogue)
                .SetRulebookName("Fresh Ingredients")
                .SetPowerlevel(4)
                .SetPixelAbilityIcon(GetTexture("sigilFreshIngredients_pixel.png"))
                .AddMetaCategories(AbilityMetaCategory.MagnificusRulebook)
                .ability;
        }
    }

    public class FreshIngredientsMagnificus : FreshIngredients {
        public static Ability Id;
        public override Ability Ability => Id;
    }
}