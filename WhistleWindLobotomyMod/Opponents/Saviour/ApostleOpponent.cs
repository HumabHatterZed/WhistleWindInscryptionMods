using DiskCardGame;
using InscryptionAPI.Encounters;
using Pixelplacement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;
using static WhistleWindLobotomyMod.LobotomyPlugin;

namespace WhistleWindLobotomyMod.Opponents.Saviour
{
    public class ApostleOpponent : TotemOpponent
    {
        public static readonly Opponent.Type ID;/* = OpponentManager.Add(
            LobotomyPlugin.pluginGuid, "ApostleOpponent",
            null, typeof(ApostleOpponent), new()
            {
                TextureLoader.LoadTextureFromFile("nodeApocalypseBoss1"),
                TextureLoader.LoadTextureFromFile("nodeApocalypseBoss2"),
                TextureLoader.LoadTextureFromFile("nodeApocalypseBoss3"),
                TextureLoader.LoadTextureFromFile("nodeApocalypseBoss4")
            }).Id;*/

        public override bool GiveCurrencyOnDefeat => false;
        public override Color InteractablesGlowColor => GameColors.Instance.gold;

        public override IEnumerator LifeLostSequence()
        {
            yield break;
        }
    }
}