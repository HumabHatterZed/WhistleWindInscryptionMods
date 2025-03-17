using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Card;
using System.Collections.Generic;
using System.Linq;

namespace BonniesBakingPack
{
    [HarmonyPatch]
    public static class BloodGemPatches
    {
        [HarmonyPostfix, HarmonyPatch(typeof(BoardManager), nameof(BoardManager.SacrificesCreateRoomForCard))]
        private static void HandleGemBloodCombinationCost(ref bool __result, PlayableCard card, List<CardSlot> sacrifices)
        {
            if (!__result || !card.Info.ModPrefixIs(BakingPlugin.pluginPrefixM))
                return;

            List<GemType> gems = card.GemsCost();
            if (gems.Count == 0 || card.BloodCost() == 0) // if this card does not cost both gems and blood
                return;

            GetIndividualGemCosts(card, out int blueGems, out int greenGems, out int orangeGems);
            GetGemProvidersFromSacrifices(sacrifices, out int blueProviders, out int greenProviders, out int orangeProviders);

            // once we get the total amount of cards that provide gems, exclude sacrificial cards from the following checks
            int nonSacrificialCards = sacrifices.Count(x => x.Card != null && x.Card.LacksAbility(Ability.Sacrificial));
            // if this card costs gems and sacrificing any non-Sacrificial card on the board will cause us to lose the ability to afford playing
            if (blueGems > 0 && blueProviders == nonSacrificialCards)
            {
                __result = false;
            }
            if (greenGems > 0 && greenProviders == nonSacrificialCards)
            {
                __result = false;
            }
            if (orangeGems > 0 && orangeProviders == nonSacrificialCards)
            {
                __result = false;
            }
        }

        [HarmonyPostfix, HarmonyPatch(typeof(PlayableCard), nameof(PlayableCard.CanBeSacrificed), MethodType.Getter)]
        private static void DontSacGemProviders(ref bool __result, PlayableCard __instance)
        {
            PlayableCard sacrificingCard = BoardManager.Instance.CurrentSacrificeDemandingCard;
            if (!__result || __instance.OpponentCard || sacrificingCard == null || !sacrificingCard.Info.ModPrefixIs(BakingPlugin.pluginPrefixM))
                return;

            List<GemType> gems = sacrificingCard.GemsCost();
            if (gems.Count == 0)
                return;

            GetIndividualGemCosts(sacrificingCard, out int blueGems, out int greenGems, out int orangeGems);
            GetGemProvidersFromSacrifices(BoardManager.Instance.PlayerSlotsCopy, out int blueProviders, out int greenProviders, out int orangeProviders);
            // using this method as a makeshift boolean - by only passing the instance's slot, we can check if it provides any gems
            GetGemProvidersFromSacrifices(new() { __instance.Slot }, out int cardProvidesBlue, out int cardProvidesGreen, out int cardProvidesOrange);

            // if this card provides a required gem and killing it will put the player below their gem requirement
            if (blueGems > 0 && cardProvidesBlue > 0 && blueProviders - 1 < blueGems)
            {
                __result = false;
            }
            if (greenGems > 0 && cardProvidesGreen > 0 && greenProviders - 1 < greenGems)
            {
                __result = false;
            }
            if (orangeGems > 0 && cardProvidesOrange > 0 && orangeProviders - 1 < orangeGems)
            {
                __result = false;
            }
        }

        [HarmonyPrefix, HarmonyPatch(typeof(BoardManager), nameof(BoardManager.ChooseSacrificesForCard))]
        private static bool PreventSacrificingNecessaryGems(ref List<CardSlot> validSlots, PlayableCard card)
        {
            if (!card.Info.ModPrefixIs(BakingPlugin.pluginPrefixM))
                return true;

            List<GemType> gems = card.GemsCost();
            if (gems.Count == 0)
                return true;

            GetIndividualGemCosts(card, out int blueGems, out int greenGems, out int orangeGems);
            GetGemProvidersFromSacrifices(validSlots, out int blueProviders, out int greenProviders, out int orangeProviders);

            List<CardSlot> newValidSlots = new(validSlots);
            Ability ab = AbilityManager.AllAbilities.Find(x => x.ModGUID == BakingPlugin.ScrybeCompat.MagnificusGuid && x.Info.rulebookName == "Magnus Mox")?.Id ?? Ability.None;
            foreach (CardSlot slot in validSlots)
            {
                if (slot.Card == null)
                    continue;

                if (blueGems > 0 && ProvidesGems(slot.Card, GemType.Blue, ab))
                {
                    if (blueProviders - 1 < blueGems) // if this card dying would prevent us from paying the gem cost, exclude from list
                        newValidSlots.Remove(slot);
                    else
                        blueProviders--;
                }
                if (greenGems > 0 && ProvidesGems(slot.Card, GemType.Green, ab))
                {
                    if (greenProviders - 1 < greenProviders)
                        newValidSlots.Remove(slot);
                    else
                        greenProviders--;
                }
                if (orangeGems > 0 && ProvidesGems(slot.Card, GemType.Orange, ab))
                {
                    if (orangeProviders - 1 < orangeGems)
                        newValidSlots.Remove(slot);
                    else
                        orangeProviders--;
                }
            }

            validSlots = newValidSlots;
            return true;
        }

        public static void GetIndividualGemCosts(PlayableCard card, out int blueCost, out int greenCost, out int orangeCost)
        {
            List<GemType> gems = card.GemsCost();
            blueCost = gems.Count(x => x == GemType.Blue);
            greenCost = gems.Count(x => x == GemType.Green);
            orangeCost = gems.Count(x => x == GemType.Orange);
        }
        public static void GetGemProvidersFromSacrifices(List<CardSlot> sacrifices, out int blueProviders, out int greenProviders, out int orangeProviders, Ability magnificusMox = Ability.None)
        {
            if (magnificusMox != Ability.None)
            {
                blueProviders = sacrifices.Count(x => x.Card != null && ProvidesGems(x.Card, GemType.Blue, magnificusMox));
                greenProviders = sacrifices.Count(x => x.Card != null && ProvidesGems(x.Card, GemType.Green, magnificusMox));
                orangeProviders = sacrifices.Count(x => x.Card != null && ProvidesGems(x.Card, GemType.Orange, magnificusMox));
            }
            else
            {
                blueProviders = sacrifices.Count(x => x.Card != null && ProvidesGems(x.Card, GemType.Blue));
                greenProviders = sacrifices.Count(x => x.Card != null && ProvidesGems(x.Card, GemType.Green));
                orangeProviders = sacrifices.Count(x => x.Card != null && ProvidesGems(x.Card, GemType.Orange));
            }
        }

        public static bool ProvidesGems(PlayableCard card, GemType gem, Ability magnificusMox = Ability.None)
        {
            if (card.HasAbility(Ability.GainGemTriple) || (magnificusMox != Ability.None && card.HasAbility(magnificusMox)))
                return true;

            return gem switch {
                GemType.Green => card.HasAbility(Ability.GainGemGreen),
                GemType.Blue => card.HasAbility(Ability.GainGemBlue),
                GemType.Orange => card.HasAbility(Ability.GainGemOrange),
                _ => false,
            };
        }
    }
}
