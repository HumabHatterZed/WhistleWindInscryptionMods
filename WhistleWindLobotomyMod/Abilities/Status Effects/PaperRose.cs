using DiskCardGame;
using InscryptionAPI.Triggers;
using System.Collections;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils.StatusEffects;
using WhistleWind.Core.Helpers;

namespace WhistleWindLobotomyMod {
    public partial class Abilities {
        private static void StatusEffect_PaperRose() {
            const string rName = "Paper Rose";
            const string rDesc = "At the start of combat, a card bearing this effect loses Health equal to its Paper Rose.";

            StatusEffectManager.FullStatusEffect data = StatusEffectManager.New<PaperRose>(
                LobotomyPlugin.pluginGuid, rName, rDesc, -1, GameColors.Instance.glowRed,
                TextureLoader.LoadTextureFromFile("sigilPaperRose.png", LobotomyPlugin.ModAssembly))
                .AddMetaCategories(StatusMetaCategory.Part1StatusEffect)
                .SetIrremovable(true);

            PaperRose.specialAbility = data.Id;
            PaperRose.iconId = data.IconInfo.ability;
        }
    }

    public class PaperRose : StatusEffectBehaviour, IOnBellRung {
        public static Ability iconId;
        public static SpecialTriggeredAbility specialAbility;

        public override Ability IconAbility => iconId;
        public override SpecialTriggeredAbility StatusEffect => specialAbility;

        public override List<string> EffectDecalIds() => new();

        public bool RespondsToBellRung(bool playerCombatPhase) {
            return playerCombatPhase;
        }

        public IEnumerator OnBellRung(bool playerCombatPhase) {
            yield return base.PlayableCard.Heal(-EffectPotency);

            if (base.PlayableCard.Health < 1) {
                yield return base.PlayableCard.Die(false);
            }
        }
    }
}
