using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void AddCustomDeathCards()
        {
            List<CardModificationInfo> defaultMods = new()
            {
                new CardModificationInfo(3, 3)
                    .SetNameReplacement("Mirabelle").SetSingletonId("wstl_mirabelle")
                    .SetBloodCost(2).AddAbilities(Ability.GuardDog)
                    .SetDeathCardPortrait(CompositeFigurine.FigurineType.SettlerWoman, 5, 1),
                new CardModificationInfo(2, 1)
                    .SetNameReplacement("Poussey").SetSingletonId("wstl_poussey")
                    .SetBonesCost(4).AddAbilities(Ability.Strafe, Ability.Flying)
                    .SetDeathCardPortrait(CompositeFigurine.FigurineType.SettlerMan, 5, 5),
                new CardModificationInfo(1, 3)
                    .SetNameReplacement("Stemcell-642").SetSingletonId("wstl_stemCell642")
                    .SetBloodCost(1).AddAbilities(Ability.SplitStrike)
                    .SetDeathCardPortrait(CompositeFigurine.FigurineType.Chief, 5, 2),
                new CardModificationInfo(3, 2)
                    .SetNameReplacement("Noah").SetSingletonId("wstl_noah")
                    .SetBonesCost(3).AddAbilities(Bloodfiend.ability)
                    .SetDeathCardPortrait(CompositeFigurine.FigurineType.Prospector, 1, 4)
            };
            List<CardModificationInfo> ascensionMods = new()
            {
                new CardModificationInfo(3, 1)
                    .SetNameReplacement("Yumi").SetSingletonId("wstl_yumi")
                    .SetBonesCost(6).AddAbilities(Ability.DebuffEnemy)
                    .SetDeathCardPortrait(CompositeFigurine.FigurineType.SettlerWoman, 2, 3),
                new CardModificationInfo(1, 1)
                    .SetNameReplacement("Summer").SetSingletonId("wstl_summer")
                    .SetBloodCost(1).AddAbilities(Ability.Sniper)
                    .SetDeathCardPortrait(CompositeFigurine.FigurineType.Wildling, 3, 0),
                new CardModificationInfo(2, 4)
                    .SetNameReplacement("Currince").SetSingletonId("wstl_currince")
                    .SetBloodCost(2).AddAbilities(Ability.StrafePush)
                    .SetDeathCardPortrait(CompositeFigurine.FigurineType.Enchantress, 4, 2),
                new CardModificationInfo(1, 2)
                    .SetNameReplacement("Igoree").SetSingletonId("wstl_igoree")
                    .SetBloodCost(1).AddAbilities(Ability.BeesOnHit)
                    .SetDeathCardPortrait(CompositeFigurine.FigurineType.SettlerWoman, 2, 2),
                new CardModificationInfo(2, 5)
                    .SetNameReplacement("Genie").SetSingletonId("wstl_genie")
                    .SetBloodCost(3).AddAbilities(Assimilator.ability)
                    .SetDeathCardPortrait(CompositeFigurine.FigurineType.Robot, 2, 5),
                new CardModificationInfo(4, 2)
                    .SetNameReplacement("Mao").SetSingletonId("wstl_mao")
                    .SetBloodCost(3).AddAbilities(Woodcutter.ability)
                    .SetDeathCardPortrait(CompositeFigurine.FigurineType.Gravedigger, 0, 3),
                new CardModificationInfo(1, 2)
                    .SetNameReplacement("Evangeline").SetSingletonId("wstl_evangeline")
                    .SetBonesCost(2).AddAbilities(Ability.Evolve)
                    .SetDeathCardPortrait(CompositeFigurine.FigurineType.Wildling, 3, 0),
                new CardModificationInfo(1, 1)
                    .SetNameReplacement("Ttungsil").SetSingletonId("wstl_ttungsil")
                    .SetBonesCost(2).AddAbilities(BitterEnemies.ability)
                    .SetDeathCardPortrait(CompositeFigurine.FigurineType.SettlerMan, 4, 4),
                new CardModificationInfo(2, 2)
                    .SetNameReplacement("Mabel").SetSingletonId("wstl_mabel")
                    .SetBonesCost(2).AddAbilities(Ability.SplitStrike)
                    .SetDeathCardPortrait(CompositeFigurine.FigurineType.SettlerWoman, 5, 2)
            };
            defaultMods.ForEach(x => DeathCardManager.AddDefaultDeathCard(x));
            ascensionMods.ForEach(x => DeathCardManager.AddDefaultDeathCard(x, true));
        }
    }
}