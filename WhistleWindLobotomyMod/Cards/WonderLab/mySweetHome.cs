using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        public const string mySweetHome = "wstlWonder_mySweetHome";

        public const string mySweetHomeA = "wstlWonder_mySweetHome_a";
        public const string mySweetHomeBr = "wstlWonder_mySweetHome_br";
        public const string mySweetHomeC = "wstlWonder_mySweetHome_c";
        public const string mySweetHomeH = "wstlWonder_mySweetHome_h";
        public const string mySweetHomeR = "wstlWonder_mySweetHome_r";
        public const string mySweetHomeI = "wstlWonder_mySweetHome_i";
        public const string mySweetHomeB = "wstlWonder_mySweetHome_b";
        public const string mySweetHomeD = "wstlWonder_mySweetHome_d";
        public const string mySweetHomeF = "wstlWonder_mySweetHome_f";
        public const string mySweetHomeM = "wstlWonder_mySweetHome_m";

        private static void MySweetHome() {
            string textureName = "mySweetHome";
            string cardName = "My Sweet Home";

            string textureNameBr = "mySweetHomeBr";
            string textureNameC = "mySweetHomeC";
            string textureNameH = "mySweetHomeH";
            string textureNameR = "mySweetHomeR";
            string textureNameI = "mySweetHomeI";
            string textureNameB = "mySweetHomeB";
            string textureNameD = "mySweetHomeD";
            string textureNameF = "mySweetHomeF";
            string textureNameM = "mySweetHomeM";
            string textureNameA = "mySweetHomeA";

            CardManager.New(LobotomyPlugin.wonderlabPrefix, mySweetHome, cardName,
                attack: 0, health: 1)
                .SetBloodCost(1)
                .SetEnergyCost(2)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Alluring.ability, Abilities.SteelTrapSweetHome)
                .SetTerrain()
                .AddTraits(Trait.Structure)
                .Build(CardHelper.CardType.Common, RiskLevel.Teth, true);

            CardManager.New(LobotomyPlugin.wonderlabPrefix, mySweetHomeBr, cardName,
                attack: 2, health: 1)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureNameBr)
                .AddAbilities(Ability.Flying)
                .AddTribes(Tribe.Bird)
                .Build();

            CardManager.New(LobotomyPlugin.wonderlabPrefix, mySweetHomeC, cardName,
                attack: 1, health: 3)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureNameC)
                .AddAbilities(Ability.GuardDog)
                .AddTribes(Tribe.Canine)
                .Build();

            CardManager.New(LobotomyPlugin.wonderlabPrefix, mySweetHomeH, cardName,
                attack: 1, health: 3)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureNameH)
                .AddAbilities(Ability.Strafe)
                .AddTribes(Tribe.Hooved)
                .Build();

            CardManager.New(LobotomyPlugin.wonderlabPrefix, mySweetHomeR, cardName,
                attack: 2, health: 1)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureNameR)
                .AddAbilities(Ability.TailOnHit)
                .AddTribes(Tribe.Reptile)
                .Build();

            CardManager.New(LobotomyPlugin.wonderlabPrefix, mySweetHomeI, cardName,
                attack: 1, health: 3)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureNameI)
                .AddAbilities(Ability.Deathtouch)
                .AddTribes(Tribe.Insect)
                .Build();

            CardManager.New(LobotomyPlugin.wonderlabPrefix, mySweetHomeB, cardName,
                attack: 1, health: 3)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureNameB)
                .AddAbilities(Ability.Sharp)
                .AddTribes(AbnormalPlugin.TribeBotanic)
                .Build();

            CardManager.New(LobotomyPlugin.wonderlabPrefix, mySweetHomeD, cardName,
                attack: 2, health: 1)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureNameD)
                .AddAbilities(Piercing.ability)
                .AddTribes(AbnormalPlugin.TribeDivine)
                .Build();

            CardManager.New(LobotomyPlugin.wonderlabPrefix, mySweetHomeF, cardName,
                attack: 2, health: 1)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureNameF)
                .AddAbilities(Bloodfiend.ability)
                .AddTribes(AbnormalPlugin.TribeFae)
                .Build();

            CardManager.New(LobotomyPlugin.wonderlabPrefix, mySweetHomeM, cardName,
                attack: 2, health: 1)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureNameM)
                .AddAbilities(Grinder.ability)
                .AddTribes(AbnormalPlugin.TribeMechanical)
                .Build();

            CardManager.New(LobotomyPlugin.wonderlabPrefix, mySweetHomeA, cardName,
                attack: 1, health: 3)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureNameA)
                .AddAbilities(Persistent.ability)
                .AddTribes(AbnormalPlugin.TribeAnthropoid)
                .Build();
        }
    }
}