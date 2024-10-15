using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.RuleBook;
using InscryptionAPI.Triggers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core;
using WhistleWind.AbnormalSigils.Patches;
using WhistleWind.AbnormalSigils.StatusEffects;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public class Bind : ModifyOnUpkeepStatusEffectBehaviour
    {
        public static Ability iconId;
        public static SpecialTriggeredAbility specialAbility;
        public override Ability IconAbility => iconId;
        public override SpecialTriggeredAbility StatusEffect => specialAbility;
        public override int PotencyModification => -EffectPotency;
    }
    public partial class AbnormalPlugin
    {
        private void StatusEffect_Bind()
        {
            const string rName = "Bind";
            const string rDesc = "A card bearing this effect loses Speed equal to its Bind. At the start of the owner's next turn, remove this effect.";
            StatusEffectManager.FullStatusEffect data = StatusEffectManager.New<Bind>(
                pluginGuid, rName, rDesc, -1, GameColors.Instance.orange,
                TextureLoader.LoadTextureFromFile("sigilBind.png", Assembly),
                TextureLoader.LoadTextureFromFile("sigilBind_pixel.png", Assembly))
                .AddMetaCategories(StatusMetaCategory.Part1StatusEffect, StatusMetaCategory.Part3StatusEffect, StatusMetaCategory.GrimoraStatusEffect, StatusMetaCategory.MagnificusStatusEffect, StatusMetaCategory.MagnificusStatusEffect);

            Bind.specialAbility = data.Id;
            Bind.iconId = data.IconInfo.ability;
            data.IconInfo.SetMechanicRedirect("Speed", "Speed", GameColors.Instance.orange);
        }
    }
}
