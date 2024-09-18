using DiskCardGame;
using Infiniscryption.Spells.Sigils;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_ShelterFrom27March_T0982()
        {
            const string shelterFrom27March = "shelterFrom27March";

            CardManager.New(pluginPrefix, shelterFrom27March, "Shelter From the 27th of March",
                attack: 0, health: 0, "It makes itself the safest place in the world by altering the reality around it.")
                .SetEnergyCost(3)
                .SetCardTemple(CardTemple.Tech)
                .SetPortraits(ModAssembly, shelterFrom27March)
                .AddAbilities(GiveSigils.AbilityID, Ability.PreventAttack, Aggravating.ability)
                .SetSpellType(SpellType.TargetedSigils)
                .Build(CardHelper.CardType.Common, RiskLevel.Waw, true);
        }
    }
}