using DiskCardGame;
using InscryptionAPI.Ascension;
using InscryptionAPI.Helpers;
using System.Reflection;
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

        public void AddChallenges()
        {
            int num = 2;
            Id1 = AddDebug("Challenge1", "challenge1_1.png", "challenge1_2.png", num);

            /*Id2 = AddDebug("Challenge2", "challenge2__1_.png", "challenge2__2_.png", num);
            Id3 = AddDebug("Challenge3", "challenge3__1_.png", "challenge3__2_.png", num);
            Id4 = AddDebug("Challenge4", "challenge4__1_.png", "challenge4__2_.png", num);
            Id5 = AddDebug("Challenge5", "challenge5__1_.png", "challenge5__2_.png", num);
            Id6 = AddDebug("Challenge6", "challenge6__1_.png", "challenge6__2_.png", num);
            Id7 = AddDebug("Challenge7", "challenge7__1_.png", "challenge7__2_.png", num);
            Id8 = AddDebug("Challenge8", "challenge8__1_.png", "challenge8__2_.png", num);
            Id9 = AddDebug("Challenge9", "challenge9__1_.png", "challenge9__2_.png", num);
            Id10 = AddDebug("Challenge10", "challenge91__1_.png", "challenge91__2_.png", num);
            Id11 = AddDebug("Challenge11", "challenge911__1_.png", "challenge911__2_.png", num);
            Id12 = AddDebug("Challenge12", "challenge912__1_.png", "challenge912__2_.png", num);
            Id13 = AddDebug("Challenge13", "challenge913__1_.png", "challenge913__2_.png", num);
            Id14 = AddDebug("Challenge14", "challenge914__1_.png", "challenge914__2_.png", num);
            Id15 = AddDebug("Challenge15", "challenge915__1_.png", "challenge915__2_.png", num);
            Id16 = AddDebug("Challenge16", "challenge916__1_.png", "challenge916__2_.png", num);
            Id17 = AddDebug("Challenge17", "challenge917__1_.png", "challenge917__2_.png", num);
            Id18 = AddDebug("Challenge18", "challenge918__1_.png", "challenge918__2_.png", num);
            Id19 = AddDebug("Challenge19", "challenge919__1_.png", "challenge919__2_.png", num);
            Id20 = AddDebug("Challenge20", "challenge920__1_.png", "challenge920__2_.png", num);
            Id21 = AddDebug("Challenge21", "challenge921__1_.png", "challenge921__2_.png", num);
            Id22 = AddDebug("Challenge22", "challenge922__1_.png", "challenge922__2_.png", num); 
            Id23 = AddDebug("Challenge23", "challenge923__1_.png", "challenge923__2_.png", num);
            Id24 = AddDebug("Challenge24", "challenge924__1_.png", "challenge924__2_.png", num);*/
        }
        private AscensionChallenge AddDebug(string name, string tex1, string tex2, int num)
        {
            Assembly ass = Assembly.GetExecutingAssembly();
            return ChallengeManager.Add(pluginGuid, name, name, 50, TextureHelper.GetImageAsTexture(tex1, ass), TextureHelper.GetImageAsTexture(tex2, ass), 0, num > 1)
                .SetAppearancesInChallengeScreen (num).Challenge.challengeType;
        }
    }
}
