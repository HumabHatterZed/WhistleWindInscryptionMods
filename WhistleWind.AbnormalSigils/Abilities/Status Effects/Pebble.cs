using DiskCardGame;
using InscryptionAPI.Helpers.Extensions;
using InscryptionAPI.Triggers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils.StatusEffects;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public class Pebble : StatusEffectBehaviour
    {
        public static Ability iconId;
        public static SpecialTriggeredAbility specialAbility;
        public override Ability IconAbility => iconId;
        public override SpecialTriggeredAbility StatusEffect => specialAbility;

        public override bool RespondsToTurnEnd(bool playerTurnEnd) => base.PlayableCard.OpponentCard != playerTurnEnd;
        public override IEnumerator OnTurnEnd(bool playerTurnEnd)
        {
            if (base.PlayableCard.Health < base.PlayableCard.MaxHealth)
            {
                base.PlayableCard.Anim.LightNegationEffect();
                base.PlayableCard.HealDamage(1);
                yield return new WaitForSeconds(0.2f);
            }
        }
        public override bool RespondsToUpkeep(bool playerUpkeep) => base.PlayableCard.OpponentCard != playerUpkeep;
        public override IEnumerator OnUpkeep(bool playerUpkeep) => OnTurnEnd(playerUpkeep);

        public override bool RespondsToDie(bool wasSacrifice, PlayableCard killer) => true;

        public override IEnumerator OnDie(bool wasSacrifice, PlayableCard killer)
        {
            foreach (PlayableCard card in BoardManager.Instance.GetCards(!base.PlayableCard.OpponentCard))
            {
                yield return card.AddStatusEffectToFaceDown<Grief>(1, modifyTurnGained: delegate (int x)
                {
                    return x + 1;
                });
            }

            yield return DialogueHelper.PlayDialogueEvent("LonelyDie", card: base.PlayableCard);
        }
    }
    public partial class AbnormalPlugin
    {
        private void StatusEffect_Pebble()
        {
            const string rName = "Pebble";
            const string rDesc = "At the start and end of its owner's turn, this card regains 1 Health. When this card perishes, inflict Grief on all allied creatures.";
            StatusEffectManager.FullStatusEffect data = StatusEffectManager.New<Pebble>(
                pluginGuid, rName, rDesc, 2, GameColors.Instance.nearWhite,
                TextureLoader.LoadTextureFromFile("sigilPebble.png", Assembly),
                TextureLoader.LoadTextureFromFile("sigilPebble_pixel.png", Assembly))
                .AddMetaCategories(StatusMetaCategory.Part1StatusEffect);

            Pebble.specialAbility = data.Id;
            Pebble.iconId = data.IconInfo.ability;
        }
    }
}
