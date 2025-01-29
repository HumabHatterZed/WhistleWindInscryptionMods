using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Ascension;
using WhistleWind.Core.Helpers;


namespace WhistleWindLobotomyMod.Challenges
{
    public static class AllOrdeals // taken from infiniscryption
    {
        public static AscensionChallenge Id { get; private set; }

        // Creates the challenge then calls the relevant patches
        internal static void Register(Harmony harmony)
        {
            Id = ChallengeManager.Add(
                LobotomyPlugin.pluginGuid,
                "All Ordeals",
                "All non-boss battles are replaced with Ordeals.",
                20,
                TextureLoader.LoadTextureFromFile("ascensionOrdeals.png"),
                TextureLoader.LoadTextureFromFile("ascensionOrdeals_activated.png")
                )
                .SetIncompatibleChallengeGetterStatic(AbnormalEncounters.Id)
                .Challenge.challengeType;

            harmony.PatchAll(typeof(AllOrdeals));
        }

        [HarmonyPostfix, HarmonyPatch(typeof(RunState), nameof(RunState.NextRegion))]
        private static void ShowAllOrdealsActivation()
        {
            if (RunState.CurrentRegionTier < 3)
            {
                ChallengeActivationUI.TryShowActivation(AllOrdeals.Id);
            }
        }
    }
}
