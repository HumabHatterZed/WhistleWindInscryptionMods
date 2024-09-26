using DiskCardGame;
using EasyFeedback.APIs;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using Pixelplacement;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BonniesBakingPack
{
    public partial class BakingPlugin
    {
        private void AddFreshIngredients()
        {
            const string rulebookName = "Fresh Ingredients";
            const string rulebookDescription = "When [creature] attacks a card and it perishes, create a random Food in your hand and gain 1 Bone.";
            const string dialogue = "Made with the freshest ingredients.";

            FreshIngredients.ability = AbilityManager.New(pluginGuid, rulebookName, rulebookDescription, typeof(FreshIngredients), GetTexture("sigilFreshIngredients.png"))
                .SetAbilityLearnedDialogue(dialogue)
                .SetPowerlevel(4)
                .SetPixelAbilityIcon(GetTexture("sigilFreshIngredients_pixel.png"))
                .AddMetaCategories(AbilityMetaCategory.Part1Rulebook, AbilityMetaCategory.Part3Rulebook)
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
            yield return ResourcesManager.Instance.AddBones(1, deathSlot);

            yield return base.PreSuccessfulTriggerSequence();
            yield return new WaitForSeconds(0.3f);
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