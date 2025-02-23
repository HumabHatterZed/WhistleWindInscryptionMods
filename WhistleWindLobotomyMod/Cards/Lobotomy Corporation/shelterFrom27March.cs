using DiskCardGame;
using Infiniscryption.Spells.Sigils;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class Cards
    {
        public const string shelterFrom27March = "wstl_shelterFrom27March";
        private static void ShelterFrom27March_T0982()
        {
            string name = "Shelter From the 27th of March";
            string desc = "It makes itself the safest place in the world by altering the reality around it.";
            string textureName = "shelterFrom27March";
            CardManager.New(LobotomyPlugin.pluginPrefix, shelterFrom27March, name,
                attack: 0, health: 0, desc)
                .SetEnergyCost(3)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(GiveSigils.AbilityID, Ability.PreventAttack, Aggravating.ability)
                .SetSpellType(SpellType.TargetedSigils)
                .Build(CardHelper.CardType.Common, RiskLevel.Waw);

            CardManager.New(LobotomyPlugin.pixelPrefix, textureName, name,
                attack: 0, health: 0, desc)
                .SetEnergyCost(3)
                .SetCardTemple(CardTemple.Tech)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(GiveSigils.AbilityID, Ability.PreventAttack, Aggravating.ability)
                .SetSpellType(SpellType.TargetedSigils)
                .Build(CardHelper.CardType.Common, RiskLevel.Waw, true);
        }
    }
}