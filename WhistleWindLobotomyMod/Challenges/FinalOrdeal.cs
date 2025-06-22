using DiskCardGame;
using InscryptionAPI.Ascension;
using InscryptionAPI.Helpers;
using System.Linq;
using UnityEngine;
using WhistleWind.Core.Helpers;

namespace WhistleWindLobotomyMod.Challenges {
    public static class FinalOrdeal // taken from infiniscryption
    {
        internal const string title = "Final Ordeals";
        internal const string description = "Leshy is replaced as the final boss of the run with the Ordeals of White.";

        public static AscensionChallenge Id { get; private set; }

        internal static void Register() {
            AscensionChallengeInfo info = ScriptableObject.CreateInstance<AscensionChallengeInfo>();
            info.title = title;
            info.description = description;
            info.pointValue = 50;
            info.iconSprite = TextureLoader.LoadTextureFromFile("ascensionFinalOrdeal.png").ConvertTexture(TextureHelper.SpriteType.ChallengeIcon);
            info.activatedSprite = TextureLoader.LoadTextureFromFile("ascensionFinalOrdeal_activated.png").ConvertTexture(new(0.5f, 0.5f));
            Id = ChallengeManager.Add(LobotomyPlugin.pluginGuid, info)
                .SetBoss(true)
                .SetFlags("ModdedFinalBoss")
                .SetIncompatibleChallengeGetter(x =>
                (from x2 in x
                 where x2?.Flags != null && x2.Flags.Exists(x3 => x3?.ToString() == "ModdedFinalBoss")
                 select x2 into x4
                 select x4.Challenge.challengeType).Concat(new AscensionChallenge[1] { AscensionChallenge.FinalBoss })
                )
                .Challenge.challengeType;
        }
    }
}
