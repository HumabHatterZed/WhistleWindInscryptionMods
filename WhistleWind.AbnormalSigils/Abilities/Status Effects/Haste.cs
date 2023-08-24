using DiskCardGame;
using Infiniscryption.Spells.Sigils;
using InscryptionAPI.Card;
using Steamworks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.StatusEffects;

using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public class Haste : StatusEffectBehaviour
    {
        public static SpecialTriggeredAbility specialAbility;
        public static Ability iconId;
        public override string CardModSingletonName => "haste";

        public override bool RespondsToTurnEnd(bool playerTurnEnd) => base.PlayableCard && base.PlayableCard.OpponentCard != playerTurnEnd;

        public override IEnumerator OnTurnEnd(bool playerTurnEnd)
        {
            yield return HelperMethods.ChangeCurrentView(View.Board);
            base.PlayableCard.Anim.LightNegationEffect();
            UpdateStatusEffectCount(-StatusEffectCount, false);

            Haste component = base.PlayableCard.gameObject.GetComponent<Haste>();
            if (component != null)
                Destroy(component);

            yield return new WaitForSeconds(0.2f);
        }
    }
    public partial class AbnormalPlugin
    {
        private void StatusEffect_Haste()
        {
            const string rName = "Haste";
            const string rDesc = "This card attacks before creatures with less Haste. At 5+ Haste, this card will attack before opposing cards as well. At the end of the owner's turn, lose all Haste.";

            StatusEffectManager.FullStatusEffect data = StatusEffectManager.NewStatusEffect<Haste>(
                pluginGuid, rName, rDesc,
                iconTexture: "sigilHaste", pixelIconTexture: "sigilHaste_pixel",
                powerLevel: 1, iconColour: GameColors.Instance.orange,
                categories: new() { StatusEffectManager.StatusMetaCategory.Part1StatusEffect });

            Haste.specialAbility = data.BehaviourId;
            Haste.iconId = data.IconId;
        }
    }
}
