﻿using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers;
using System.Collections;
using UnityEngine;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core.Helpers;


namespace WhistleWindLobotomyMod
{
    public class Life : CreateCardsAdjacent
    {
        public static Ability ability;
        public override Ability Ability => ability;

        int turnsTillActivation = 2;
        public override string SpawnedCardId => GetRandomCardId();

        public override string CannotSpawnDialogue => "The pain is refused.";

        public override bool RespondsToResolveOnBoard() => false;
        public override bool RespondsToUpkeep(bool playerUpkeep) => base.Card.OpponentCard != playerUpkeep;
        public override IEnumerator OnUpkeep(bool playerUpkeep)
        {
            turnsTillActivation--;
            if (turnsTillActivation == 0)
            {
                yield return base.OnResolveOnBoard();
                turnsTillActivation = 2;
            }
        }
        private string GetRandomCardId()
        {
            // .48 .66 .80 .92 1
            // .41 .57 .75 .89 1
            // .34 .48 .70 .86 1
            int randomSeed = base.GetRandomSeed();
            float randomValue = SeededRandom.Value(randomSeed);
            int extraDifficulty = AscensionSaveData.Data.GetNumChallengesOfTypeActive(AscensionChallenge.BaseDifficulty);
            if (randomValue <= Mathf.Max(0.15f, 0.55f - extraDifficulty * 0.07f))
            {
                return "wstl_doubtA";
            }
            else if (randomValue <= Mathf.Max(0.25f, 0.75f - extraDifficulty * 0.09f))
            {
                return "wstl_doubtB";
            }
            else if (randomValue <= Mathf.Max(0.45f - extraDifficulty * 0.05f))
            {
                return "wstl_doubtY";
            }
            else if (randomValue <= Mathf.Max(0.75f, 0.95f - extraDifficulty * 0.03f))
            {
                return "wstl_doubtO";
            }
            else
            {
                return "wstl_processUnderstanding";
            }
        }
    }

    public partial class LobotomyPlugin
    {
        private void Ability_Life()
        {
            AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();
            info.rulebookName = "Life";
            info.rulebookDescription = "At the start of every other turn for the owner, create a random Doubt or Process of Understanding in empty adjacent spaces.";
            info.powerLevel = 4;
            Life.ability = AbilityManager.Add(pluginGuid, info, typeof(Life), TextureLoader.LoadTextureFromFile("sigilLife.png")).Id;
        }
    }
}
