﻿using DiskCardGame;
using EasyFeedback.APIs;
using InscryptionAPI.Card;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.StatusEffects;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_SeeMore()
        {
            const string rulebookName = "See More";
            const string rulebookDescription = "Display the next page of status effects on this card.";
            AbilityInfo info = AbilityHelper.NewFiller<SeeMore>(pluginGuid, "sigilSeeMore", rulebookName, rulebookDescription)
                .Info.SetPassive(false).SetActivated().SetCanStack().SetHasColorOverride(true, GameColors.Instance.nearBlack);
            info.metaCategories.Clear();
            SeeMore.ability = info.ability;

            const string desc = "Determines the order that cards will attack during combat. Player-owned cards have a base Speed of 3 while opponent-owned cards have a base Speed of 0.";
            AbilityInfo info2 = AbilityHelper.NewFiller<Speed>(
                pluginGuid, TextureLoader.LoadTextureFromFile("sigilSpeed.png", Assembly), "Speed", desc)
                .Info.SetPassive();
            info2.metaCategories.Clear();
            Speed.ability = info2.ability;
        }
    }
    public class SeeMore : ActivatedAbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public List<Ability> RunOffAbilities = new();

        public bool switchingPages = false;

        public Dictionary<int, List<Ability>> AllPages = new();

        public int currentPage = 0;
        public override IEnumerator Activate()
        {
            currentPage++;
            if (currentPage > base.Card.GetAbilityStacks(this.Ability))
                currentPage = 0;

            switchingPages = true;
            base.Card.OnStatsChanged();
            yield break;
        }
    }

    public class Speed : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }
}