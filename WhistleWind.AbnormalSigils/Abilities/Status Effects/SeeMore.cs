using DiskCardGame;
using EasyFeedback.APIs;
using InscryptionAPI.Card;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_SeeMore()
        {
            const string rulebookName = "See More";
            const string rulebookDescription = "Display the next page of status effects on this card.";
            SeeMore.ability = AbilityHelper.NewFiller<SeeMore>(pluginGuid, "sigilSeeMore", rulebookName, rulebookDescription)
                .Info.SetPassive(false).SetActivated().SetCanStack().SetHasColorOverride(true, GameColors.Instance.nearBlack)
                .ability;
        }
    }
    public class SeeMore : ActivatedAbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public List<Ability> RunOffAbilities = new();

        public bool switchingPages = false;

        public Dictionary<int, List<Ability>> AllPages = new()
        {

        };

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
}