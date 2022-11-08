using DiskCardGame;
using System.Collections;
using UnityEngine;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public class BoardEffects : SpecialCardBehaviour
    {
        public static SpecialTriggeredAbility specialAbility;
        public SpecialTriggeredAbility SpecialAbility => specialAbility;

        public override bool RespondsToResolveOnBoard() => true;
        public override IEnumerator OnResolveOnBoard()
        {
            if (base.PlayableCard.Info.name == "wstl_apocalypseBird" && !WstlSaveManager.BoardEffectsApocalypse)
                yield return ApocalypseTableEffects();

            if (base.PlayableCard.Info.name == "wstl_jesterOfNihil" && !WstlSaveManager.BoardEffectsEntropy)
                yield return EntropyTableEffects();

            if (base.PlayableCard.Info.name == "wstl_lyingAdult" && !WstlSaveManager.BoardEffectsEmerald)
                yield return EmeraldTableEffects();

            yield break;
        }
        public static IEnumerator ApocalypseTableEffects()
        {
            ChangeTableColours(
                GameColors.Instance.nearBlack, GameColors.Instance.brown, GameColors.Instance.gray,
                GameColors.Instance.darkRed, GameColors.Instance.glowRed);

            yield return ThumpTable();

            AudioController.Instance.StopLoop(1);
            AudioController.Instance.SetLoopVolume((Singleton<GameFlowManager>.Instance as Part1GameFlowManager).GameTableLoopVolume, 0.25f);

            WstlSaveManager.BoardEffectsApocalypse = true;
        }
        public static IEnumerator EntropyTableEffects()
        {
            ChangeTableColours(
                GameColors.Instance.nearBlack, GameColors.Instance.lightGray, GameColors.Instance.gray,
                GameColors.Instance.lightGray, GameColors.Instance.lightGray);

            AudioController.Instance.PlaySound2D($"grimoravoice_laughing#1");

            yield return ThumpTable();

            WstlSaveManager.BoardEffectsEntropy = true;
        }
        public static IEnumerator EmeraldTableEffects()
        {
            ChangeTableColours(
                GameColors.Instance.darkLimeGreen, GameColors.Instance.darkLimeGreen, GameColors.Instance.darkLimeGreen,
                GameColors.Instance.glowSeafoam, GameColors.Instance.glowSeafoam);

            AudioController.Instance.PlaySound2D($"grimoravoice_laughing#1");

            yield return ThumpTable();

            WstlSaveManager.BoardEffectsEmerald = true;
        }

        private static void ChangeTableColours(Color mainLight, Color cardLight, Color interactableLight, Color slotColour, Color slotGlow)
        {
            slotColour.a = 0.5f;
            Singleton<TableVisualEffectsManager>.Instance.ChangeTableColors(mainLight, cardLight, interactableLight, slotColour, slotColour, slotGlow, slotGlow, slotGlow, slotGlow);
        }
        private static IEnumerator ThumpTable()
        {
            Singleton<TableVisualEffectsManager>.Instance.ThumpTable(0.1f);
            yield return new WaitForSeconds(0.166f);
            Singleton<TableVisualEffectsManager>.Instance.ThumpTable(0.1f);
            yield return new WaitForSeconds(1.418f);
        }
    }
    public partial class LobotomyPlugin
    {
        private void SpecialAbility_BoardEffects()
        {
            const string rulebookName = "BoardEffects";
            BoardEffects.specialAbility = LobotomyAbilityHelper.CreateSpecialAbility<BoardEffects>(rulebookName).Id;
        }
    }
}
