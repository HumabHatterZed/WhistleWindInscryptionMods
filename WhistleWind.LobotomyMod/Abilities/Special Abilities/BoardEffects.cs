using DiskCardGame;
using System.Collections;
using UnityEngine;
using WhistleWind.Core.Helpers;
using WhistleWind.LobotomyMod.Core;

namespace WhistleWind.LobotomyMod
{
    public class BoardEffects : SpecialCardBehaviour
    {
        public static SpecialTriggeredAbility specialAbility;
        public SpecialTriggeredAbility SpecialAbility => specialAbility;

        public override bool RespondsToResolveOnBoard() => true;
        public override IEnumerator OnResolveOnBoard()
        {
            if (base.PlayableCard.Info.name == "wstl_apocalypseBird" && !LobotomySaveManager.BoardEffectsApocalypse)
                yield return ApocalypseTableEffects();

            if (base.PlayableCard.Info.name == "wstl_jesterOfNihil" && !LobotomySaveManager.BoardEffectsEntropy)
                yield return EntropyTableEffects();

            if (base.PlayableCard.Info.name == "wstl_lyingAdult" && !LobotomySaveManager.BoardEffectsEmerald)
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

            LobotomySaveManager.BoardEffectsApocalypse = true;
        }
        public static IEnumerator EntropyTableEffects()
        {
            ChangeTableColours(
                GameColors.Instance.nearBlack, GameColors.Instance.lightGray, GameColors.Instance.gray,
                GameColors.Instance.lightGray, GameColors.Instance.lightGray);

            AudioController.Instance.PlaySound2D($"grimoravoice_laughing#1");

            yield return ThumpTable();

            LobotomySaveManager.BoardEffectsEntropy = true;
        }
        public static IEnumerator EmeraldTableEffects()
        {
            ChangeTableColours(
                GameColors.Instance.darkLimeGreen, GameColors.Instance.darkLimeGreen, GameColors.Instance.darkLimeGreen,
                GameColors.Instance.glowSeafoam, GameColors.Instance.glowSeafoam);

            AudioController.Instance.PlaySound2D($"grimoravoice_laughing#1");

            yield return ThumpTable();

            LobotomySaveManager.BoardEffectsEmerald = true;
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
            BoardEffects.specialAbility = AbilityHelper.CreateSpecialAbility<BoardEffects>(pluginGuid, rulebookName).Id;
        }
    }
}
