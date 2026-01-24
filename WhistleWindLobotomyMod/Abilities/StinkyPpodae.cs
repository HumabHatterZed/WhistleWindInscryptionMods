using DiskCardGame;
using InscryptionAPI.Card;
using UnityEngine;


namespace WhistleWindLobotomyMod {
    public partial class Abilities {
        private static void AddPpodaeStinky() {
            AbilityManager.FullAbility full = AbilityManager.AllAbilities.AbilityByID(Ability.DebuffEnemy);
            AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();
            info.SetRulebookName("Goodest Boy in the World")
                .SetRulebookDescription(full.BaseRulebookDescription)
                .SetPowerlevel(full.Info.powerLevel)
                .SetPixelAbilityIcon(full.Info.pixelIcon.texture)
                .SetCanStack(full.Info.canStack)
                .SetFlipYIfOpponent(full.Info.flipYIfOpponent)
                .SetOpponentUsable(full.Info.opponentUsable)
                .SetDefaultPart1Ability()
                .SetPassive();

            PpodaeStinky = AbilityManager.Add(LobotomyPlugin.pluginGuid, info, typeof(AbilityBehaviour), full.Texture).Id;
        }

        public static Ability PpodaeStinky;
    }
}
