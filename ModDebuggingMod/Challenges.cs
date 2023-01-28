using DiskCardGame;
using InscryptionAPI.Ascension;
using WhistleWind.Core.Helpers;

namespace ModDebuggingMod
{
    public partial class Plugin
    {
        public static AscensionChallenge Id1 { get; private set; }
        public static AscensionChallenge Id2 { get; private set; }
        public static AscensionChallenge Id3 { get; private set; }
        public static AscensionChallenge Id4 { get; private set; }
        public static AscensionChallenge Id5 { get; private set; }
        public static AscensionChallenge Id6 { get; private set; }
        public static AscensionChallenge Id7 { get; private set; }
        public static AscensionChallenge Id8 { get; private set; }
        public static AscensionChallenge Id9 { get; private set; }
        public static AscensionChallenge Id10 { get; private set; }
        public static AscensionChallenge Id11 { get; private set; }
        public static AscensionChallenge Id12 { get; private set; }
        public static AscensionChallenge Id13 { get; private set; }
        public static AscensionChallenge Id14 { get; private set; }
        public static AscensionChallenge Id15 { get; private set; }
        public static AscensionChallenge Id16 { get; private set; }
        public static AscensionChallenge Id17 { get; private set; }
        public static AscensionChallenge Id18 { get; private set; }
        public static AscensionChallenge Id19 { get; private set; }
        public static AscensionChallenge Id20 { get; private set; }
        public static AscensionChallenge Id21 { get; private set; }
        public static AscensionChallenge Id22 { get; private set; }
        public static AscensionChallenge Id23 { get; private set; }
        public static AscensionChallenge Id24 { get; private set; }

        public static void AddChallenges()
        {
            int num = 1;
            Id1 = ChallengeManager.Add(pluginGuid, "Challenge1", "Challenge1", 50, TextureLoader.LoadTextureFromBytes(Properties.Resources.challenge1__1_), TextureLoader.LoadTextureFromBytes(Properties.Resources.challenge1__2_)).SetAppearancesInChallengeScreen(num).Challenge.challengeType;
            Id2 = ChallengeManager.Add(pluginGuid, "Challenge2", "Challenge1", 50, TextureLoader.LoadTextureFromBytes(Properties.Resources.challenge2__1_), TextureLoader.LoadTextureFromBytes(Properties.Resources.challenge2__2_)).SetAppearancesInChallengeScreen(num).Challenge.challengeType;
            Id3 = ChallengeManager.Add(pluginGuid, "Challenge3", "Challenge1", 50, TextureLoader.LoadTextureFromBytes(Properties.Resources.challenge3__1_), TextureLoader.LoadTextureFromBytes(Properties.Resources.challenge3__2_)).SetAppearancesInChallengeScreen(num).Challenge.challengeType;
            Id4 = ChallengeManager.Add(pluginGuid, "Challenge4", "Challenge1", 50, TextureLoader.LoadTextureFromBytes(Properties.Resources.challenge4__1_), TextureLoader.LoadTextureFromBytes(Properties.Resources.challenge4__2_)).SetAppearancesInChallengeScreen(num).Challenge.challengeType;
            Id5 = ChallengeManager.Add(pluginGuid, "Challenge5", "Challenge1", 50, TextureLoader.LoadTextureFromBytes(Properties.Resources.challenge5__1_), TextureLoader.LoadTextureFromBytes(Properties.Resources.challenge5__2_)).SetAppearancesInChallengeScreen(num).Challenge.challengeType;
            Id6 = ChallengeManager.Add(pluginGuid, "Challenge6", "Challenge1", 50, TextureLoader.LoadTextureFromBytes(Properties.Resources.challenge6__1_), TextureLoader.LoadTextureFromBytes(Properties.Resources.challenge6__2_)).SetAppearancesInChallengeScreen(num).Challenge.challengeType;
            Id7 = ChallengeManager.Add(pluginGuid, "Challenge7", "Challenge1", 50, TextureLoader.LoadTextureFromBytes(Properties.Resources.challenge7__1_), TextureLoader.LoadTextureFromBytes(Properties.Resources.challenge7__2_)).SetAppearancesInChallengeScreen(num).Challenge.challengeType;
            Id8 = ChallengeManager.Add(pluginGuid, "Challenge8", "Challenge1", 50, TextureLoader.LoadTextureFromBytes(Properties.Resources.challenge8__1_), TextureLoader.LoadTextureFromBytes(Properties.Resources.challenge8__2_)).SetAppearancesInChallengeScreen(num).Challenge.challengeType;
            Id9 = ChallengeManager.Add(pluginGuid, "Challenge9", "Challenge1", 50, TextureLoader.LoadTextureFromBytes(Properties.Resources.challenge9__1_), TextureLoader.LoadTextureFromBytes(Properties.Resources.challenge9__2_)).SetAppearancesInChallengeScreen(num).Challenge.challengeType;
            Id10 = ChallengeManager.Add(pluginGuid, "Challenge10", "Challenge1", 50, TextureLoader.LoadTextureFromBytes(Properties.Resources.challenge91__1_), TextureLoader.LoadTextureFromBytes(Properties.Resources.challenge91__2_)).SetAppearancesInChallengeScreen(num).Challenge.challengeType;
            Id11 = ChallengeManager.Add(pluginGuid, "Challenge11", "Challenge1", 50, TextureLoader.LoadTextureFromBytes(Properties.Resources.challenge911__1_), TextureLoader.LoadTextureFromBytes(Properties.Resources.challenge911__2_)).SetAppearancesInChallengeScreen(num).Challenge.challengeType;
            Id12 = ChallengeManager.Add(pluginGuid, "Challenge12", "Challenge1", 50, TextureLoader.LoadTextureFromBytes(Properties.Resources.challenge912__1_), TextureLoader.LoadTextureFromBytes(Properties.Resources.challenge912__2_)).SetAppearancesInChallengeScreen(num).Challenge.challengeType;
            Id13 = ChallengeManager.Add(pluginGuid, "Challenge13", "Challenge1", 50, TextureLoader.LoadTextureFromBytes(Properties.Resources.challenge913__1_), TextureLoader.LoadTextureFromBytes(Properties.Resources.challenge913__2_)).SetAppearancesInChallengeScreen(num).Challenge.challengeType;
            Id14 = ChallengeManager.Add(pluginGuid, "Challenge14", "Challenge1", 50, TextureLoader.LoadTextureFromBytes(Properties.Resources.challenge914__1_), TextureLoader.LoadTextureFromBytes(Properties.Resources.challenge914__2_)).SetAppearancesInChallengeScreen(num).Challenge.challengeType;
            Id15 = ChallengeManager.Add(pluginGuid, "Challenge15", "Challenge1", 50, TextureLoader.LoadTextureFromBytes(Properties.Resources.challenge915__1_), TextureLoader.LoadTextureFromBytes(Properties.Resources.challenge915__2_)).SetAppearancesInChallengeScreen(num).Challenge.challengeType;
            Id16 = ChallengeManager.Add(pluginGuid, "Challenge16", "Challenge1", 50, TextureLoader.LoadTextureFromBytes(Properties.Resources.challenge916__1_), TextureLoader.LoadTextureFromBytes(Properties.Resources.challenge916__2_)).SetAppearancesInChallengeScreen(num).Challenge.challengeType;
            Id17 = ChallengeManager.Add(pluginGuid, "Challenge17", "Challenge1", 50, TextureLoader.LoadTextureFromBytes(Properties.Resources.challenge917__1_), TextureLoader.LoadTextureFromBytes(Properties.Resources.challenge917__2_)).SetAppearancesInChallengeScreen(num).Challenge.challengeType;
            Id18 = ChallengeManager.Add(pluginGuid, "Challenge18", "Challenge1", 50, TextureLoader.LoadTextureFromBytes(Properties.Resources.challenge918__1_), TextureLoader.LoadTextureFromBytes(Properties.Resources.challenge918__2_)).SetAppearancesInChallengeScreen(num).Challenge.challengeType;
            Id19 = ChallengeManager.Add(pluginGuid, "Challenge19", "Challenge1", 50, TextureLoader.LoadTextureFromBytes(Properties.Resources.challenge919__1_), TextureLoader.LoadTextureFromBytes(Properties.Resources.challenge919__2_)).SetAppearancesInChallengeScreen(num).Challenge.challengeType;
            Id20 = ChallengeManager.Add(pluginGuid, "Challenge20", "Challenge1", 50, TextureLoader.LoadTextureFromBytes(Properties.Resources.challenge920__1_), TextureLoader.LoadTextureFromBytes(Properties.Resources.challenge920__2_)).SetAppearancesInChallengeScreen(num).Challenge.challengeType;
            Id21 = ChallengeManager.Add(pluginGuid, "Challenge21", "Challenge1", 50, TextureLoader.LoadTextureFromBytes(Properties.Resources.challenge921__1_), TextureLoader.LoadTextureFromBytes(Properties.Resources.challenge921__2_)).SetAppearancesInChallengeScreen(num).Challenge.challengeType;
            Id22 = ChallengeManager.Add(pluginGuid, "Challenge22", "Challenge1", 50, TextureLoader.LoadTextureFromBytes(Properties.Resources.challenge922__1_), TextureLoader.LoadTextureFromBytes(Properties.Resources.challenge922__2_)).SetAppearancesInChallengeScreen(num).Challenge.challengeType;
            Id23 = ChallengeManager.Add(pluginGuid, "Challenge23", "Challenge1", 50, TextureLoader.LoadTextureFromBytes(Properties.Resources.challenge923__1_), TextureLoader.LoadTextureFromBytes(Properties.Resources.challenge923__2_)).SetAppearancesInChallengeScreen(num).Challenge.challengeType;
            Id24 = ChallengeManager.Add(pluginGuid, "Challenge24", "Challenge1", 50, TextureLoader.LoadTextureFromBytes(Properties.Resources.challenge924__1_), TextureLoader.LoadTextureFromBytes(Properties.Resources.challenge924__2_)).SetAppearancesInChallengeScreen(num).Challenge.challengeType;
        }
    }
}
