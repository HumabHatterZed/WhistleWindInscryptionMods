using DiskCardGame;
using InscryptionAPI.Ascension;
using InscryptionAPI.Helpers;
using System.Linq;
using UnityEngine;
using WhistleWind.Core.Helpers;

namespace WhistleWindLobotomyMod.Challenges {
    public static class FinalOrdeal // taken from infiniscryption
    {
        internal const string TITLE = "Final Ordeals";
        internal const string DESCRIPTION = "Leshy is replaced as the final boss of the run with the Ordeals of White.";

        public static AscensionChallenge ID { get; private set; }

        internal static void Register() {
            // since our activated sprite is larger than it should be, we need to construct the info manually before passing it to the API
            AscensionChallengeInfo info = ScriptableObject.CreateInstance<AscensionChallengeInfo>();
            info.title = TITLE;
            info.description = DESCRIPTION;
            info.pointValue = 50;
            info.iconSprite = TextureLoader.LoadTextureFromFile("ascensionFinalOrdeal.png").ConvertTexture(TextureHelper.SpriteType.ChallengeIcon);
            info.activatedSprite = TextureLoader.LoadTextureFromFile("ascensionFinalOrdeal_activated.png").ConvertTexture(new(0.5f, 0.5f));
            ID = ChallengeManager.Add(LobotomyPlugin.pluginGuid, info)
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
