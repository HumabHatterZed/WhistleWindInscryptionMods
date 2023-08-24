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
    public class Bind : StatusEffectBehaviour
    {
        public static SpecialTriggeredAbility specialAbility;
        public static Ability iconId;
        public override string CardModSingletonName => "bind";

        public override bool RespondsToTurnEnd(bool playerTurnEnd) => base.PlayableCard && base.PlayableCard.OpponentCard != playerTurnEnd;

        public override IEnumerator OnTurnEnd(bool playerTurnEnd)
        {
            yield break;
            yield return HelperMethods.ChangeCurrentView(View.Board);
            base.PlayableCard.Anim.LightNegationEffect();
            UpdateStatusEffectCount(-StatusEffectCount, false);

            Bind component = base.PlayableCard.gameObject.GetComponent<Bind>();
            if (component != null)
                Destroy(component);

            yield return new WaitForSeconds(0.2f);
        }
    }
    public partial class AbnormalPlugin
    {
        private void StatusEffect_Bind()
        {
            const string rName = "Bind";
            const string rDesc = "This card attacks after creatures with less Bind. At 5+ Bind, this card will attack after opposing cards as well. At the end of the owner's turn, lose all Bind.";

            StatusEffectManager.FullStatusEffect data = StatusEffectManager.NewStatusEffect<Bind>(
                pluginGuid, rName, rDesc,
                iconTexture: "sigilBind", pixelIconTexture: "sigilBind_pixel",
                powerLevel: -1, iconColour: GameColors.Instance.orange,
                categories: new() { StatusEffectManager.StatusMetaCategory.Part1StatusEffect });

            Bind.specialAbility = data.BehaviourId;
            Bind.iconId = data.IconId;
        }
    }
}
