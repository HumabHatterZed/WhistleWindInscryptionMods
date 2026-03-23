using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Card;
using Sirenix.Utilities;
using System.Collections.Generic;
using System.Linq;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;


namespace WhistleWindLobotomyMod {
    /// <summary>
    /// Utility class that contains all ability classes.
    /// </summary>
    public partial class Abilities {
        /// <summary>
        /// Register this mod's abilities with the API.
        /// </summary>
        internal static void AddAbilities(LobotomyPlugin plugin) {


            if (LobotomyConfigManager.ReskinSigils) {
                AbilityManager.ModifyAbilityList += delegate (List<AbilityManager.FullAbility> abilities) {
                    AbilityManager.AllAbilities.AbilityByID(Shadowed.ID).Info
                        .AddMetaCategories(AbilityMetaCategory.Part1Rulebook);

                    AbilityManager.AllAbilities.AbilityByID(Ability.MoveBeside).Info
                        .AddMetaCategories(AbilityMetaCategory.Part1Rulebook);

                    if (LobotomyConfigManager.ReskinSigils) {
                        AbilityInfo info;
                        abilities.AbilityByID(Ability.Sniper).Info
                            .SetRulebookName("Marksman")
                            .SetAbilityLearnedDialogue("Your beast strikes with precision.")
                            .SetIcon(TextureLoader.LoadTextureFromFile("sigilMarksman.png"))
                            .SetPixelAbilityIcon(TextureLoader.LoadTextureFromFile("sigilMarksman_pixel.png"))
                            .AddMetaCategories(AbilityMetaCategory.Part1Rulebook);

                        abilities.AbilityByID(Ability.Sentry).Info
                            .SetRulebookName("Quick Draw")
                            .SetAbilityLearnedDialogue("The early bird gets the worm.")
                            .SetIcon(TextureLoader.LoadTextureFromFile("sigilQuickDraw.png"))
                            .SetPixelAbilityIcon(TextureLoader.LoadTextureFromFile("sigilQuickDraw_pixel.png"))
                            .SetCanStack()
                            .SetFlipYIfOpponent()
                            .AddMetaCategories(AbilityMetaCategory.Part1Rulebook);

                        info = abilities.AbilityByID(PotshotPop.ID).Info;
                        info.rulebookDescription = info.rulebookDescription.Replace("Sentry", "Quick Draw");

                        info = abilities.AbilityByID(PotshotPopEffect.iconId).Info;
                        info.rulebookDescription = info.rulebookDescription.Replace("Sentry", "Quick Draw");

                        info = abilities.AbilityByID(SurefireDrink.ID).Info;
                        info.rulebookDescription = info.rulebookDescription.Replace("Sniper", "Marksman");

                        info = abilities.AbilityByID(SurefireDrinkEffect.iconId).Info;
                        info.rulebookDescription = info.rulebookDescription.Replace("Sniper", "Marksman");

                        info = abilities.AbilityByID(ActivatedSniper.ID).Info;
                        info.rulebookDescription = info.rulebookDescription.Replace("Sniper", "Marksman");

                        abilities.AbilityByID(Ability.Transformer).Info
                            .SetRulebookDescription("[creature] will transform into a different form after 1 turn on the board.")
                            .AddMetaCategories(AbilityMetaCategory.Part1Rulebook);

                        abilities.AbilityByID(Ability.ExplodeOnDeath).Info
                            .SetRulebookName("Volatile")
                            .SetIcon(TextureLoader.LoadTextureFromFile("sigilVolatile.png"))
                            .SetCustomFlippedTexture(TextureLoader.LoadTextureFromFile("sigilVolatile_flipped.png", LobotomyPlugin.ModAssembly))
                            .SetPixelAbilityIcon(TextureLoader.LoadTextureFromFile("sigilVolatile_pixel.png"))
                            .SetFlipYIfOpponent(false)
                            .AddMetaCategories(AbilityMetaCategory.Part1Rulebook);
                    }

                    return abilities;
                };
            }

            AddPpodaeStinky();
            AddSteelTrapSweetHome();
            AddTimeMachine();
            AddApostle();
            AddTrueSaviour();
            AddConfession();

            AddLife();
            AddHarmony();
            AddFood();
            AddSweeperPersistence();

            AddTower();

            AddDelusion();
            AddGodRed();
            AddGodWhite();
            AddGodBlack();
            AddGodPale();

            AddSurvival();

            AddApocalypse();
            AddApocalypseGiant();
            AddBigEyes();

            StatusEffect_Enchanted();
            AddDazzling();

            AddSmallBeak();
            AddMisdeeds();
            AddLongArms();

            StatusEffect_Sin();
            AddUnjustScale();

            AddScenarioOverseer();

            StatusEffect_PaperRose();


            if (LobotomyConfigManager.RevealSpecials) {
                LobotomyPlugin.Log.LogDebug("Adding rulebook entries for cards' special abilities.");
                AccessTools.GetDeclaredMethods(typeof(Abilities)).Where(mi => mi.Name.StartsWith("Rulebook")).ForEach(mi => mi.Invoke(plugin, null));
            }

            AccessTools.GetDeclaredMethods(typeof(Abilities)).Where(mi => mi.Name.StartsWith("AddSpecial")).ForEach(mi => mi.Invoke(plugin, null));
        }
    }
}
