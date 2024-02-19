using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_Nosferatu_F01113()
        {
            const string nosferatuName = "Nosferatu";
            const string nosferatu = "nosferatu";
            const string nosferatuBeast = "nosferatuBeast";
            Tribe[] tribes = new[] { TribeFae };

            CardInfo beast = NewCard(nosferatuBeast, nosferatuName,
                attack: 3, health: 1, blood: 2, temple: CardTemple.Undead)
                .SetPortraits(nosferatuBeast)
                .AddAbilities(Bloodfiend.ability, Bloodfiend.ability)
                .AddTribes(tribes)
                .Build(cardType: ModCardType.Ruina);

            NewCard(nosferatu, nosferatuName, "A creature of the night, noble and regal. Will you help sate its thirst?",
                attack: 2, health: 1, blood: 2, temple: CardTemple.Undead)
                .SetPortraits(nosferatu)
                .AddAbilities(Ability.Evolve, Bloodfiend.ability)
                .AddTribes(tribes)
                .SetEvolve(beast, 1)
                .Build(CardHelper.ChoiceType.Common, RiskLevel.Waw, ModCardType.Ruina);
        }
    }
}