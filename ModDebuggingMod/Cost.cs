using DiskCardGame;
using InscryptionAPI.CardCosts;
using InscryptionAPI.Helpers;
using InscryptionCommunityPatch;
using InscryptionCommunityPatch.Card;
using System.Collections;
using UnityEngine;

namespace ModDebuggingMod {
    public class MyTestCost : CustomCardCost // create a class inheriting from CustomCardCost
    {
        public override string CostName => "MyTestCost"; // cost name

        // you need to call CardCostManager.Register() in order to actually add your cost to the API
        // in this example, we're defining a static method that we will call in our plugin's Awake() method, but you can also do this directly in Awake()
        public static void Init() {
            CardCostManager.FullCardCost fullCardCost = CardCostManager.Register("myPluginGuid", "MyTestCost", typeof(MyTestCost), Texture3D, TexturePixel);

            // what type of resource this cost uses/provides
            // default is None, but if you want your cost to appear as an option at cost choice nodes you'll need to set this
            // you can use GuidManager.GetEnumValue<ResourceType>() to create a new ResourceType that's unique to your cost
            // like so: fullCardCost.ResourceType = GuidManager.GetEnumValue<ResourceType>("myPluginGuid", "MyTestCost");
            // alternatively, you can use an existing one - this will make it so cards with your cost can be obtained from that pool of cards
            // for this example cost, we're setting the ResourceType to Gems - this means that cards with this cost will appear in the same choice pool with other mox cards
            fullCardCost.ResourceType = ResourceType.Gems;

            // if you're using a custom ResourceType, this will control what the back of the choice card looks like
            fullCardCost.GetRewardBackTexture = ChoiceTextures;

            // in that same vein, this will control what individual values you can choose at cost choice nodes
            // for example, the Bones choice just picks any card that costs Bones
            // while the Blood choice lets you choose between a card that costs 1 Blood, 2 Blood, or 3 Blood
            // since our cost is part of the Gems choice pool, this field will be ignored
            // if we had a custom ResourceType though, at cost choice nodes the player could choose between cards that cost 1, 2, or 3 of our custom cost
            fullCardCost.ChoiceAmounts = new int[] { 1, 2, 3 };

            // assign the logic for getting this cost's tiers
            // tiers are used in some parts of the game to determine things like the potential strength of a card
            // not incredibly important but it's here if you need it
            fullCardCost.GetCostTier = GetCostTier;
        }

        // static method we can assign to our custom cost's GetRewardBackTexture
        // if your cost has different choice amounts, use this to control what texture is displayed
        public static Texture2D ChoiceTextures(int amount) {
            Debug.Log($"MyTestCost: {amount}"); // this is a stub method, but in an actual cost you would want to return the correct Texture
            return null;
        }

        // method for getting cost textures that will be displayed on cards in Acts 1, 3, Grimora, Magnificus
        // this is passed into CardCostManager.Register (see Init)
        // note that the argument types MUST be { int, CardInfo, PlayableCard } otherwise the compiler will yell at you
        public static Texture2D Texture3D(int cardCost, CardInfo cardInfo, PlayableCard playableCard) {
            // return a different texture for each value that our card cost can be
            //if (cardCost == 1)
            //    return TextureHelper.GetImageAsTexture("textCost1.png");
            //else if (cardCost == 2)
            //    return TextureHelper.GetImageAsTexture("textCost2.png");
            // rest of if-else tree goes here (or use a switch)

            // for this example, we'll just get the energy cost textures used by the community patches
            return TextureHelper.GetImageAsTexture($"energy_cost_{Mathf.Min(7, cardCost)}.png", typeof(PatchPlugin).Assembly);
        }

        // method for getting cost textures that will be displayed on cards in Act 2 and in the starter deck selection screen
        // this is passed into CardCostManager.Register (see Init)
        public static Texture2D TexturePixel(int cardCost, CardInfo info, PlayableCard playableCard) {
            // this example uses the CommunityPatch to automaticatically generate a texture with a number
            // 
            return Part2CardCostRender.CombineIconAndCount(cardCost, TextureHelper.GetImageAsTexture($"pixel_energy.png", typeof(PatchPlugin).Assembly));
        }

        // determine this cost's tiers based on how much of this cost a card needs to be played
        public static int GetCostTier(int cost) {
            // for this example we'll use the logic for Bones' cost tiers
            return cost / 3;
        }

        // method that determines if a card with this cost can be played or not
        public override bool CostSatisfied(int cardCost, PlayableCard card) {
            // for this example we're just reusing the logic for Energy while accounting for a card's actual Energy cost
            return cardCost <= (Singleton<ResourcesManager>.Instance.PlayerEnergy - card.EnergyCost);
        }

        // if a card with this custom cost cannot be played, this is the message that Leshy (for Act 1) will speak
        public override string CostUnsatisfiedHint(int cardCost, PlayableCard card) {
            return "You cannot afford to play " + card.Info.DisplayedNameLocalized + ". You need more MyTestCost.";
        }

        // the logic for when a card with this cost is played
        // if your cost spends a resource when played, this is where you would spend it
        public override IEnumerator OnPlayed(int cardCost, PlayableCard card) {
            // pay an Energy cost when a card with our custom cost is played
            yield return Singleton<ResourcesManager>.Instance.SpendEnergy(cardCost);
        }
    }
}
