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
    public class Sinking : StatusEffectBehaviour, IPassiveAttackBuff, IModifyDamageTaken
    {
        public static Ability iconId;
        public static SpecialTriggeredAbility specialAbility;
        public override Ability IconAbility => iconId;
        public override SpecialTriggeredAbility StatusEffect => specialAbility;

        public override bool RespondsToTakeDamage(PlayableCard source) => true;

        public override IEnumerator OnTakeDamage(PlayableCard source)
        {
            yield return base.RemoveFromCard(true);
            yield break;
        }

        public int GetPassiveAttackBuff(PlayableCard target) => target == base.PlayableCard ? -EffectPotency : 0;

        public bool RespondsToModifyDamageTaken(PlayableCard target, int damage, PlayableCard attacker, int originalDamage) => target == base.PlayableCard;
        public int OnModifyDamageTaken(PlayableCard target, int damage, PlayableCard attacker, int originalDamage) => damage + EffectPotency;
        public int TriggerPriority(PlayableCard target, int damage, PlayableCard attacker) => -9000;
    }
    public partial class AbnormalPlugin
    {
        private void StatusEffect_Sinking()
        {
            const string rName = "Sinking";
            const string rDesc = "A card bearing this effect loses Power equal to its Sinking. When this card is struck, take damage equal to its Sinking then remove this effect.";
            StatusEffectManager.FullStatusEffect data = StatusEffectManager.New<Sinking>(
                pluginGuid, rName, rDesc, -2, GameColors.Instance.glowSeafoam,
                TextureLoader.LoadTextureFromFile("sigilSinking.png", Assembly),
                TextureLoader.LoadTextureFromFile("sigilSinking_pixel.png", Assembly))
                .AddMetaCategories(StatusMetaCategory.Part1StatusEffect, StatusMetaCategory.MagnificusStatusEffect, StatusMetaCategory.GrimoraStatusEffect, StatusMetaCategory.Part3StatusEffect);

            Sinking.specialAbility = data.Id;
            Sinking.iconId = data.IconInfo.ability;
        }
    }
}
