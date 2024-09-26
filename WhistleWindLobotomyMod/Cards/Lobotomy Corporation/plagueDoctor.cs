using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private const string plagueDoctor = "plagueDoctor";
        public static readonly List<Sprite> PlagueDoctorPortraits = new();
        private void Card_PlagueDoctor_O0145()
        {
            RegisterPortraitsAndEmissions();

            CardManager.New(pluginPrefix, plagueDoctor, "Plague Doctor",
                attack: 0, health: 3, "A worker of miracles. He humbly requests to join you.")
                .SetBonesCost(3)
                .SetPortrait(PlagueDoctorPortraits.FirstOrDefault())
                .SetEmissivePortrait(UpdateDoctorEmission(0))
                .SetPixelPortrait(UpdateDoctorPixelPortrait(0))
                .AddAbilities(Ability.Flying, Healer.ability)
                .AddSpecialAbilities(Bless.specialAbility)
                .AddTribes(TribeDivine)
                .AddAppearances(MiracleWorkerAppearance.appearance)
                .SetOnePerDeck()
                .Build(CardHelper.CardType.Common, RiskLevel.Zayin, true);
        }
        private void RegisterPortraitsAndEmissions()
        {
            for (int i = 0; i < 12; i++)
            {
                Sprite portrait = UpdateDoctorPortrait(i);
                Sprite emission = UpdateDoctorEmission(i);
                PlagueDoctorPortraits.Add(portrait);
                portrait.RegisterEmissionForSprite(emission);
            }
        }
        public static Sprite UpdateDoctorPortrait(int key)
        {
            Sprite retval = TextureLoader.LoadSpriteFromFile(GetDoctorName(key) + ".png");
            retval.name = GetDoctorName(key) + "_portrait";
            return retval;
        }
        public static Sprite UpdateDoctorPixelPortrait(int key) => TextureLoader.LoadSpriteFromFile(GetDoctorName(key) + "_pixel.png");
        public static Sprite UpdateDoctorEmission(int key)
        {
            string portraitName = key switch
            {
                5 => "plagueDoctor5",
                9 => "plagueDoctor9",
                11 => "plagueDoctor11",
                _ => plagueDoctor,
            };
            return TextureLoader.LoadSpriteFromFile(portraitName + "_emission.png");
        }
        private static string GetDoctorName(int key)
        {
            return key switch
            {
                0 => plagueDoctor,
                1 => "plagueDoctor1",
                2 => "plagueDoctor2",
                3 => "plagueDoctor3",
                4 => "plagueDoctor4",
                5 => "plagueDoctor5",
                6 => "plagueDoctor6",
                7 => "plagueDoctor7",
                8 => "plagueDoctor8",
                9 => "plagueDoctor9",
                10 => "plagueDoctor10",
                _ => "plagueDoctor11",
            };
        }
    }
}