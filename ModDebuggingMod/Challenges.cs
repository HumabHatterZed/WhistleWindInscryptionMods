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
            Id1 = AddDebug("Challenge1Boss", "challenge1_1.png", "challenge1_2.png", 1, 5, true);
            
            //Id2 = AddDebug("Challenge2", "challenge2 (1).png", "challenge2 (2).png", 1, 0);
            //Id3 = AddDebug("Challenge3", "challenge3 (1).png", "challenge3 (2).png", 1, 0);
            //Id4 = AddDebug("Challenge4Boss", "challenge4 (1).png", "challenge4 (2).png", 1, 0, true);

            Id5 = AddDebug("Challenge5Boss", "challenge5 (1).png", "challenge5 (2).png", 1, -5, true);
            //Id6 = AddDebug("Challenge6", "challenge6 (1).png", "challenge6 (2).png", 2, -5);
            //Id7 = AddDebug("Challenge7", "challenge7 (1).png", "challenge7 (2).png", 2, 0);
            //Id8 = AddDebug("Challenge8", "challenge8 (1).png", "challenge8 (2).png", num);
            //Id9 = AddDebug("Challenge9", "challenge9 (1).png", "challenge9 (2).png", num);
            //Id10 = AddDebug("Challenge10", "challenge91 (1).png", "challenge91 (2).png", num);
            //Id11 = AddDebug("Challenge11", "challenge911 (1).png", "challenge911 (2).png", num);
            //Id12 = AddDebug("Challenge12", "challenge912 (1).png", "challenge912 (2).png", num);
            //Id13 = AddDebug("Challenge13", "challenge913 (1).png", "challenge913 (2).png", num);
            //Id14 = AddDebug("Challenge14", "challenge914 (1).png", "challenge914 (2).png", num);
            //Id15 = AddDebug("Challenge15", "challenge915 (1).png", "challenge915 (2).png", num);
            //Id16 = AddDebug("Challenge16", "challenge916 (1).png", "challenge916 (2).png", num);
            //Id17 = AddDebug("Challenge17", "challenge917 (1).png", "challenge917 (2).png", num);
            //Id18 = AddDebug("Challenge18", "challenge918 (1).png", "challenge918 (2).png", num);
            //Id19 = AddDebug("Challenge19", "challenge919 (1).png", "challenge919 (2).png", num);
            //Id20 = AddDebug("Challenge20", "challenge920 (1).png", "challenge920 (2).png", num);
            //Id21 = AddDebug("Challenge21", "challenge921 (1).png", "challenge921 (2).png", num);
            //Id22 = AddDebug("Challenge22", "challenge922 (1).png", "challenge922 (2).png", num); 
            //Id23 = AddDebug("Challenge23", "challenge923 (1).png", "challenge923 (2).png", num);
            //Id24 = AddDebug("Challenge24", "challenge924 (1).png", "challenge924 (2).png", num);
        }
        private AscensionChallenge AddDebug(string name, string tex1, string tex2, int num = 1, int value = 50, bool boss = false)
        {
            Assembly ass = Assembly.GetExecutingAssembly();
            return ChallengeManager.Add(pluginGuid, name, name, value, TextureHelper.GetImageAsTexture(tex1, ass), TextureHelper.GetImageAsTexture(tex2, ass), boss ? 1 : 0, num > 1)
                .SetAppearancesInChallengeScreen(num).SetBoss(boss).Challenge.challengeType;
        }
    }
}
