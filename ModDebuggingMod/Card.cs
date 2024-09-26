using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.CardCosts;
using InscryptionAPI.Guid;
using InscryptionAPI.Helpers;
using InscryptionAPI.Localizing;
using InscryptionCommunityPatch;
using InscryptionCommunityPatch.Card;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod;

namespace ModDebuggingMod
{
    public partial class Plugin
    {
        private void CARD_DEBUG()
        {
            //MyTestCost.Init();

            CardInfo info = CardManager.New("wstl", "wstlcard", "Debug",
                attack: 10, health: 100)
                .SetCost(0, 0, 0)
                .AddAbilities(NimbleFoot.ability, Test.ability)
                //.AddSpecialAbilities(BlindRage.specialAbility)
                //.SetTransformerCardId("Squirrel")
                .SetEvolve("Squirrel", 1)
                .SetPortraits(typeof(Plugin).Assembly, "misterWin_grimora", emissionName: "misterWin_grimora_emission", pixelPortraitName: "buffBell.png")
                ;
        }
    }

    public class MyTestCost : CustomCardCost
    {
        public override string CostName => "MyTestCost";

        public static void Init()
        {
            CardCostManager.FullCardCost fullCardCost = CardCostManager.Register("wstl", "MyTestCost", typeof(MyTestCost), Texture3D, TexturePixel);
            fullCardCost.ResourceType = CardCostManager.AllCustomCosts.CostByBehaviour(typeof(InscryptionCommunityPatch.Tests.TestCost)).ResourceType;
            fullCardCost.GetRewardBackTexture = ChoiceTextures;
            fullCardCost.ChoiceAmounts = new int[] { 1, 2, 3 };
        }

        public static Texture2D ChoiceTextures(int amount)
        {
            Debug.Log($"MyTestCost: {amount}");
            return null;
        }
        public static Texture2D Texture3D(int cardCost, CardInfo cardInfo, PlayableCard playableCard)
        {
            Texture2D tex = TextureHelper.GetImageAsTexture($"energy_cost_{Mathf.Min(7, cardCost)}.png", typeof(PatchPlugin).Assembly);
            return ReverseColours(tex);
        }
        public static Texture2D TexturePixel(int cardCost, CardInfo info, PlayableCard playableCard)
        {
            Texture2D tex = Part2CardCostRender.CombineIconAndCount(cardCost, TextureHelper.GetImageAsTexture($"pixel_energy.png", typeof(PatchPlugin).Assembly));
            return ReverseColours(tex);
        }
        private static Texture2D ReverseColours(Texture2D texToReverse)
        {
            for (int x = 0; x < texToReverse.width; x++)
            {
                for (int y = 0; y < texToReverse.height; y++)
                {
                    Color pixel = texToReverse.GetPixel(x, y);
                    Color.RGBToHSV(pixel, out float H, out float S, out float V);
                    Color final_color = Color.HSVToRGB((H + 0.5f) % 1f, (S + 0.5f) % 1f, (V + 0.5f) % 1f);
                    final_color.a = pixel.a;

                    texToReverse.SetPixel(x, y, final_color);
                }
            }
            texToReverse.Apply();
            return texToReverse;
        }

        public override bool CostSatisfied(int cardCost, PlayableCard card)
        {
            return cardCost <= (Singleton<ResourcesManager>.Instance.PlayerEnergy - card.EnergyCost);
        }

        public override string CostUnsatisfiedHint(int cardCost, PlayableCard card)
        {
            return "Eat your greens aby. " + card.Info.DisplayedNameLocalized;
        }

        public override IEnumerator OnPlayed(int cardCost, PlayableCard card)
        {
            yield return Singleton<ResourcesManager>.Instance.SpendEnergy(cardCost);
        }
    }
}
