using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_NosferatuBeast_F01113()
        {
            List<Ability> abilities = new()
            {
                Bloodfiend.ability
            };
            CardHelper.CreateCard(
                "wstl_nosferatuBeast", "Nosferatu",
                "A creature of the night, noble and regal. Will you help sate its thirst?",
                atk: 3, hp: 2,
                blood: 3, bones: 0, energy: 0,
                Artwork.nosferatu, Artwork.nosferatu_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                metaTypes: CardHelper.MetaType.Ruina);
        }
    }
}