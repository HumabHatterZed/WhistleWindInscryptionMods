using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Ascension;
using InscryptionAPI.Card;
using System.Collections.Generic;
using static BonniesBakingPack.BakingPlugin;

namespace BonniesBakingPack
{
    public static class BonnieAct3
    {
        public static AscensionChallenge Id { get; private set; }
        private static bool addedBonnieModToList = false;

        public static void Register()
        {
            Id = ChallengeManager.Add(
                BakingPlugin.pluginGuid,
                "Bonnie is Here!",
                "Adds Bonnie to the pool of obtainable rare cards.",
                0,
                BakingPlugin.GetTexture("bonnie_config.png"),
                BakingPlugin.GetTexture("bonnie_config_on.png"), 0)
                .SetFlags("P03", "noleshy")
                .Challenge.challengeType;

            ChallengeManager.ModifyChallenges += delegate (List<ChallengeManager.FullChallenge> challenges)
            {
                if (!ScrybeCompat.P03Enabled)
                {
                    ChallengeManager.FullChallenge chall = challenges.Find(f => f.Challenge.challengeType == Id);
                    challenges.Remove(chall);
                }
                return challenges;
            };
        }

        private static List<CardInfo> ModifyBonnieAct3(List<CardInfo> cards)
        {
            CardInfo bonnie_act3 = cards.Find(x => x.name == "bbp_act3_bonnie");
            if (bonnie_act3 == null || !BakingPlugin.ScrybeCompat.P03Enabled)
                return cards;

            bonnie_act3.RemoveMetaCategories(ScrybeCompat.NatureRegion);
            if (AscensionSaveData.Data.ChallengeIsActive(Id))
            {
                bonnie_act3.AddMetaCategories(ScrybeCompat.NatureRegion);
            }
            return cards;
        }

        [HarmonyPatch(typeof(AscensionMenuScreens), "TransitionToGame")]
        [HarmonyPrefix]
        private static void BonnieAct3ChallengeConfig()
        {
            if (BakingPlugin.ScrybeCompat.P03Enabled && !addedBonnieModToList)
            {
                CardManager.ModifyCardList += ModifyBonnieAct3;
                addedBonnieModToList = true;
            }

            CardManager.SyncCardList();
        }
    }
}
