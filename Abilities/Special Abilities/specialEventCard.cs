using InscryptionAPI;
using DiskCardGame;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void SpecialAbility_EventCard()
        {
            const string rulebookName = "Event Card";
            const string rulebookDescription = "Changes the table effects when played.";
            EventCard.specialAbility = AbilityHelper.CreateSpecialAbility<EventCard>(rulebookName, rulebookDescription).Id;
        }
    }

    public class EventCard : SpecialCardBehaviour
    {
        public SpecialTriggeredAbility SpecialAbility => specialAbility;

        public static SpecialTriggeredAbility specialAbility;

        public override bool RespondsToResolveOnBoard()
        {
            return true;
        }
        public override IEnumerator OnResolveOnBoard()
        {
            // Create table effects if Apocalypse Bird then break
            if (base.PlayableCard.Info.name == "wstl_apocalypseBird" && !WstlSaveManager.HasSeenApocalypseEffects)
                yield return ApocalypseTableEffects();

            if (base.PlayableCard.Info.name == "wstl_jesterOfNihil" && !WstlSaveManager.HasSeenJesterEffects)
                yield return EntropyTableEffects();

            yield break;
        }
        private IEnumerator ApocalypseTableEffects()
        {
            WstlSaveManager.HasSeenApocalypseEffects = true;

            Color glowRed = GameColors.Instance.glowRed;
            Color darkRed = GameColors.Instance.darkRed;
            darkRed.a = 0.5f;
            Color gray = GameColors.Instance.gray;
            gray.a = 0.5f;

            Singleton<TableVisualEffectsManager>.Instance.ChangeTableColors(GameColors.Instance.nearBlack, GameColors.Instance.brown, GameColors.Instance.gray, darkRed, darkRed, glowRed, glowRed, glowRed, glowRed);

            Singleton<TableVisualEffectsManager>.Instance.ThumpTable(0.1f);
            yield return new WaitForSeconds(0.166f);
            Singleton<TableVisualEffectsManager>.Instance.ThumpTable(0.1f);
            yield return new WaitForSeconds(1.418f);

            AudioController.Instance.StopLoop(1);
            AudioController.Instance.SetLoopVolume((Singleton<GameFlowManager>.Instance as Part1GameFlowManager).GameTableLoopVolume, 0.25f);
        }

        private IEnumerator EntropyTableEffects()
        {
            WstlSaveManager.HasSeenJesterEffects = true;

            Color glowRed = GameColors.Instance.lightGray;
            Color darkRed = GameColors.Instance.lightGray;
            darkRed.a = 0.5f;
            Color gray = GameColors.Instance.gray;
            gray.a = 0.5f;

            Singleton<TableVisualEffectsManager>.Instance.ChangeTableColors(GameColors.Instance.nearBlack, GameColors.Instance.lightGray, GameColors.Instance.gray, darkRed, darkRed, glowRed, glowRed, glowRed, glowRed);

            AudioController.Instance.PlaySound2D($"grimoravoice_laughing#1");

            Singleton<TableVisualEffectsManager>.Instance.ThumpTable(0.1f);
            yield return new WaitForSeconds(0.166f);
            Singleton<TableVisualEffectsManager>.Instance.ThumpTable(0.1f);
            yield return new WaitForSeconds(1.418f);
        }
    }
}
