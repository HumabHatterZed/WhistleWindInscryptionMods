using DiskCardGame;
using GrimoraMod.Extensions;
using InscryptionAPI.Encounters;
using InscryptionAPI.Nodes;
using InscryptionAPI.TalkingCards;
using Pixelplacement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Challenges;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Opponents;
using WhistleWindLobotomyMod.Opponents.Apocalypse;
using WhistleWindLobotomyMod.Opponents.Prospector;

namespace WhistleWindLobotomyMod
{
    public class OrdealIndigoNoon : OrdealBattleSequencer
    {
        private int seed = -1;
        public override void ModifyQueuedCard(PlayableCard card)
        {
            if (seed == -1) seed = base.GetRandomSeed();

            if (SeededRandom.Range(0, 15 - TurnManager.Instance.Opponent.Difficulty, seed++) == 0)
            {
                card.AddTemporaryMod(new(SeededRandom.Bool(seed++) ? 1 : 0, SeededRandom.Bool(seed++) ? 1 : 0));
            }
        }

        public override EncounterData ConstructOrdealBlueprint(EncounterData encounterData)
        {
            int numTurns = (encounterData.Difficulty - 1) / 2;
            for (int i = 0; i < numTurns; i++)
            {
                if (i % 2 == 0) encounterData.Blueprint.AddTurn();

                List<EncounterBlueprintData.CardBlueprint> turn = new()
                {
                    EncounterManager.NewCardBlueprint("wstl_sweeper"),
                    EncounterManager.NewCardBlueprint("wstl_sweeper")
                };
                encounterData.Blueprint.AddTurn(turn);
            }
            return encounterData;
        }
    }
}