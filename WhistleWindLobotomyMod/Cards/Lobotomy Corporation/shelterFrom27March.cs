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

            NewCard(shelterFrom27March, "Shelter From the 27th of March", "It makes itself the safest place in the world by altering the reality around it.",
                attack: 0, health: 0, energy: 3, temple: CardTemple.Tech)
                .SetPortraits(shelterFrom27March)
                .AddAbilities(Ability.PreventAttack, Aggravating.ability, GiveSigils.AbilityID)
                .SetSpellType(SpellType.TargetedSigils)
                .Build(CardHelper.ChoiceType.Common, RiskLevel.Waw);
        }
    }
}