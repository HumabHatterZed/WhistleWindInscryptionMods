using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers;
using System.Collections;
using UnityEngine;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core.Helpers;


namespace WhistleWindLobotomyMod
{
    public class Survival : CreateCardsAdjacent
    {
        public static Ability ability;
        public override Ability Ability => ability;

        int turnsTillActivation = 2;
        public override string SpawnedCardId => "wstl_foodChain";

        public override string CannotSpawnDialogue => "The banquest is halted.";

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
    }

    public partial class LobotomyPlugin
    {
        private void Ability_Survival()
        {
            AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();
            info.rulebookName = "Survival";
            info.rulebookDescription = "At the start of every other turn for the owner, this card creates a Food Chain in empty adjacent spaces. [define:wstl_foodChain]";
            info.powerLevel = 4;
            Survival.ability = AbilityManager.Add(pluginGuid, info, typeof(Survival), TextureLoader.LoadTextureFromFile("sigilSurvival.png")).Id;
        }
    }
}
