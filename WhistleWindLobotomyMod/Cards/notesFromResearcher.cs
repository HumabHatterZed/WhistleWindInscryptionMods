using DiskCardGame;
using Infiniscryption.Spells.Sigils;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_NotesFromResearcher_T0978()
        {
            List<Ability> abilities = new()
            {
                Volatile.ability,
                GiveStatsSigils.AbilityID
            };
            LobotomyCardHelper.CreateCard(
                "wstl_notesFromResearcher", "Notes from a Crazed Researcher",
                "An insane garble of guilty confessions and incoherent gibberish.",
                atk: 0, hp: 3,
                blood: 0, bones: 2, energy: 0,
                Artwork.notesFromResearcher, Artwork.notesFromResearcher_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(), appearances: new(),
                choiceType: CardHelper.CardChoiceType.Basic, riskLevel: LobotomyCardHelper.RiskLevel.He,
                metaTypes: CardHelper.CardMetaType.Terrain, spellType: LobotomyCardHelper.SpellType.TargetedStatsSigils);
        }
    }
}