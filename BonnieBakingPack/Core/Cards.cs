using BepInEx.Bootstrap;
using DiskCardGame;
using Infiniscryption.PackManagement;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers;
using UnityEngine;

namespace BonniesBakingPack
{
    public partial class BakingPlugin
    {
        private void AddCards()
        {
            // Shool Mouse
            CardManager.New(pluginPrefix, "mouseShool", "Shool Mouse", 0, 1, "Young and full of potential. Who knows what kind of Mouse it'll grow into?")
                .SetBloodCost(1)
                .AddTribes(Tribe.Squirrel)
                .SetPortraitAndEmission(TextureHelper.GetImageAsTexture("mouseShool.png", Assembly), TextureHelper.GetImageAsTexture("mouseShool_emission.png", Assembly))
                .AddAbilities(Ability.Evolve)
                .SetDefaultPart1Card();

            // Mouse
            CardManager.New(pluginPrefix, "mouse", "Mouse", 1, 3, "Just a regular, law-abiding mouse.")
                .SetBloodCost(1)
                .AddTribes(Tribe.Squirrel)
                .SetPortraitAndEmission(TextureHelper.GetImageAsTexture("mouse.png", Assembly), TextureHelper.GetImageAsTexture("mouse_emission.png", Assembly))
                .SetDefaultPart1Card();
            
            // Mean Mouse
            CardManager.New(pluginPrefix, "mouseMean", "Mean Mouse", 1, 2, "Don't get to close to this mouse and its pepper spray.")
                .SetBonesCost(4)
                .AddTribes(Tribe.Squirrel)
                .SetPortraitAndEmission(TextureHelper.GetImageAsTexture("mouseMean.png", Assembly), TextureHelper.GetImageAsTexture("mouseMean_emission.png", Assembly))
                .AddAbilities(Ability.Sharp)
                .SetDefaultPart1Card();

            // Phone Mouse
            CardManager.New(pluginGuid, "mousePhone", "Phone Mouse", 1, 1, "")
                .SetEnergyCost(3)
                .AddTribes(Tribe.Squirrel)
                .SetPortraitAndEmission(TextureHelper.GetImageAsTexture("mousePhone.png", Assembly), TextureHelper.GetImageAsTexture("mousePhone_emission.png", Assembly))
                .AddAbilities(Ability.DrawRandomCardOnDeath)
                .SetDefaultPart1Card();

            // Loud Mouse
            CardManager.New(pluginPrefix, "mouseLoud", "Loud Mouse", 2, 1, "Some people don't know when to shut up.")
                .SetBloodCost(1)
                .AddTribes(Tribe.Squirrel)
                .SetPortraitAndEmission(TextureHelper.GetImageAsTexture("mouseLoud.png", Assembly), TextureHelper.GetImageAsTexture("mouseLoud_emission.png", Assembly))
                .AddAbilities(Ability.BuffEnemy)
                .SetDefaultPart1Card();

            // Police Wolf
            CardManager.New(pluginPrefix, "policeWolf", "Police Wolf", 2, 2, "An officer of the law, quick to respond to any trouble.")
                .SetEnergyCost(4)
                .AddTribes(Tribe.Canine)
                .AddAbilities(Ability.CorpseEater)
                .SetDefaultPart1Card();

            // Cat
            CardManager.New(pluginPrefix, "cat", "Cat", 0, 2, "A stuffy cat with elite tastes, in more ways than one.")
                .SetBonesCost(2)
                .AddAbilities(Ability.Morsel)
                .SetDefaultPart1Card();

            // Bunny
            CardManager.New(pluginPrefix, "bunny", "Bunny", 0, 1, "A brother looking for his sister. Perhaps you've seen her?")
                .SetEnergyCost(2)
                .AddAbilities(Ability.TailOnHit)
                .SetDefaultPart1Card();

            // Dog
            CardManager.New(pluginPrefix, "dog", "Dog", 1, 6, "A diligent, if unappreciated, worker.")
                .SetBloodCost(2)
                .AddTribes(Tribe.Canine)
                .AddAbilities(Ability.Reach)
                .SetDefaultPart1Card();

            // Panda
            CardManager.New(pluginPrefix, "panda", "Panda", 2, 4, "A detective on the hunt for a killer. Armed and dangerous.")
                .SetCost(2, 2)
                .AddAbilities(Ability.Sniper)
                .SetDefaultPart1Card();

            // Bingus
            CardManager.New(pluginPrefix, "bingus", "Bingus", 0, 0, "...I don't know what this thing is.")
                .SetPortraitAndEmission(TextureHelper.GetImageAsTexture("bingus.png", Assembly), TextureHelper.GetImageAsTexture("bingus_emission.png", Assembly))
                .SetStatIcon(BingusStatIcon.Icon)
                .AddAbilities()
                .SetRare()
                .RemoveAppearances(CardAppearanceBehaviour.Appearance.RareCardBackground);

            // Moose
            CardManager.New(pluginPrefix, "moose", "Moose", 3, 6, "An enigmatic moose with a knack for telling tales.")
                .SetBloodCost(3)
                .AddTribes(Tribe.Hooved)
                .AddAbilities(Ability.WhackAMole)
                .SetDefaultPart1Card();

            // ???
            CardManager.New(pluginPrefix, "protagonist", "???", 2, 2, "A mysterious woman with a bad habit of sticking her nose where it shouldn't be.")
                .SetBloodCost(2)
                .AddAbilities(Ability.Tutor)
                .SetDefaultPart1Card()
                .SetOnePerDeck();

            // Pirate
            CardManager.New(pluginPrefix, "pirate", "Pirate", 2, 3, "A scallywag with a great treasure. He reminds me of someone I know...")
                .SetBloodCost(2)
                .AddTribes(Tribe.Canine)
                .AddAbilities(Ability.Submerge, Ability.StrafeSwap)
                .SetDefaultPart1Card();

            // The Ethereal Lady
            CardManager.New(pluginPrefix, "etherealLady", "Ethereal Lady", 3, 6, "Under her protection, there will be no misery and strife.")
                .SetCost(3, 3, 3)
                .AddAbilities(Ability.AllStrike, Ability.DeathShield)
                .SetRare()
                .SetOnePerDeck();
        }
    }
}