using DiskCardGame;
using HarmonyLib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    [HarmonyPatch]
    public class TheHomingInstinct : SpecialCardBehaviour
    {
        public SpecialTriggeredAbility SpecialAbility => specialAbility;

        public static SpecialTriggeredAbility specialAbility;

        public static readonly string rName = "The Homing Instinct";
        public static readonly string rDesc = "When The Road Home is played, create a Scaredy Cat in your hand. [define:wstl_scaredyCat]. Whenever this card moves, turn its previous space into a Paved Road. When all spaces on the owner's side of the board are Paved Roads, all ally cards gain 1 Power.";

        internal static Texture PavedSlotTexture => TextureLoader.LoadTextureFromBytes(Artwork.slotPavedRoad);
        internal static Texture DefaultSlotTexture;
        internal static List<CardSlot> PavedSlots = new();
        private bool hasResolved = false;
        public override bool RespondsToResolveOnBoard() => true;
        public override bool RespondsToOtherCardAssignedToSlot(PlayableCard otherCard) => hasResolved && otherCard == base.PlayableCard;
        public override IEnumerator OnResolveOnBoard()
        {
            hasResolved = true;
            DefaultSlotTexture = base.PlayableCard.Slot.transform.Find("Quad").GetComponent<Renderer>().material.mainTexture;

            CardInfo CardToDraw = CardLoader.GetCardByName("wstl_scaredyCat");
            ModifySpawnedCard(CardToDraw);

            if (base.PlayableCard.OpponentCard)
                yield return HelperMethods.QueueCreatedCard(CardToDraw);
            else
                yield return CreateDrawnCard(CardToDraw);
        }
        public override IEnumerator OnOtherCardAssignedToSlot(PlayableCard otherCard)
        {
            return base.OnOtherCardAssignedToSlot(otherCard);
        }
        private IEnumerator CreateDrawnCard(CardInfo CardToDraw)
        {
            if (Singleton<ViewManager>.Instance.CurrentView != View.Default)
            {
                yield return new WaitForSeconds(0.2f);
                Singleton<ViewManager>.Instance.SwitchToView(View.Default);
                yield return new WaitForSeconds(0.2f);
            }
            yield return Singleton<CardSpawner>.Instance.SpawnCardToHand(CardToDraw);
            yield return new WaitForSeconds(0.45f);
        }
        private void ModifySpawnedCard(CardInfo card)
        {
            List<Ability> abilities = base.Card.Info.Abilities;
            foreach (CardModificationInfo temporaryMod in base.PlayableCard.TemporaryMods)
            {
                abilities.AddRange(temporaryMod.abilities);
            }
            abilities.RemoveAll((Ability x) => x == YellowBrickRoad.ability);
            if (abilities.Count > 0)
            {
                if (abilities.Count > 4)
                    abilities.RemoveRange(3, abilities.Count - 4);

                CardModificationInfo cardModificationInfo = new()
                {
                    fromCardMerge = true,
                    abilities = abilities
                };
                card.Mods.Add(cardModificationInfo);
            }
        }

        [HarmonyPatch(typeof(TurnManager), nameof(TurnManager.SetupPhase))]
        [HarmonyPostfix]
        private static void ResetPavedSlots() => PavedSlots.Clear();

        [HarmonyPatch(typeof(TurnManager), nameof(TurnManager.CleanupPhase))]
        [HarmonyPostfix]
        private static void CleanUpPavedSlots() => ClearPavedRoads();
        [HarmonyPatch(typeof(PlayableCard), nameof(PlayableCard.GetPassiveHealthBuffs))]
        [HarmonyPostfix]
        private static void AddBuffForCardSlot(PlayableCard __instance, ref int __result)
        {
            if (PavedSlots.Count == 4 && PavedSlots.Contains(__instance.Slot))
                    __result += 2;
        }

        private static void PaveSlot(CardSlot slot)
        {
            if (slot == null)
                return;
            // if this is a new, unpaved slot
            if (!PavedSlots.Contains(slot))
            {
                PavedSlots.Add(slot);
                slot.SetTexture(PavedSlotTexture);
            }
        }
        private static void ResetCardSlot(CardSlot slot) => slot.SetTexture(DefaultSlotTexture);
        private static void ClearPavedRoads()
        {
            foreach (CardSlot slot in PavedSlots)
                ResetCardSlot(slot);
            PavedSlots.Clear();
        }
    }
    public class RulebookEntryTheHomingInstinct : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }
    public partial class LobotomyPlugin
    {
        private void Rulebook_TheHomingInstinct()
            => RulebookEntryTheHomingInstinct.ability = LobotomyAbilityHelper.CreateRulebookAbility<RulebookEntryTheHomingInstinct>(TheHomingInstinct.rName, TheHomingInstinct.rDesc).Id;
        private void SpecialAbility_TheHomingInstinct()
            => TheHomingInstinct.specialAbility = AbilityHelper.CreateSpecialAbility<TheHomingInstinct>(pluginGuid, TheHomingInstinct.rName).Id;
    }
}
