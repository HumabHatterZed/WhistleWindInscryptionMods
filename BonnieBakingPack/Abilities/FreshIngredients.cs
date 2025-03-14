using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections;
using UnityEngine;

namespace BonniesBakingPack
{
    public partial class BakingPlugin
    {
        private void AddFreshIngredients()
        {
            const string rulebookName = "Fresh Ingredients";
            const string rulebookDescription = "When [creature] strikes a card and it perishes, create a random Food in your hand and gain 1 Bone.";
            const string dialogue = "Made with the freshest ingredients.";

            FreshIngredients.ability = AbilityManager.New(pluginGuid, rulebookName, rulebookDescription, typeof(FreshIngredients), GetTexture("sigilFreshIngredients.png"))
                .SetAbilityLearnedDialogue(dialogue)
                .SetPowerlevel(4)
                .SetPixelAbilityIcon(GetTexture("sigilFreshIngredients_pixel.png"))
                .AddMetaCategories(AbilityMetaCategory.Part1Rulebook, AbilityMetaCategory.Part3Rulebook, AbilityMetaCategory.GrimoraRulebook)
                .ability;
        }
    }

    public class FreshIngredients : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            return killer == base.Card;
        }

        public override IEnumerator OnOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            CardInfo info = CardLoader.GetCardByName(FreshFood.GetRandomFoodName(base.GetRandomSeed()));

            yield return base.PreSuccessfulTriggerSequence();
            if (!SaveManager.SaveFile.IsMagnificus)
            {
                yield return ResourcesManager.Instance.AddBones(1, deathSlot);
                yield return new WaitForSeconds(0.3f);
            }
            
            ViewManager.Instance.SwitchToView(View.Hand);
            yield return new WaitForSeconds(0.2f);
            yield return CardSpawner.Instance.SpawnCardToHand(info);
            yield return new WaitForSeconds(0.5f);
            yield return base.LearnAbility();
            ViewManager.Instance.SwitchToView(View.Board);
        }
    }
}