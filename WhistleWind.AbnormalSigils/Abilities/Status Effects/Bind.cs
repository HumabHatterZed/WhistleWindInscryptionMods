using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Triggers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils.Patches;
using WhistleWind.AbnormalSigils.StatusEffects;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public class Bind : ModifyOnUpkeepStatusEffectBehaviour/*, IGetAttackingSlots*/
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
            const string rDesc = "This card's Speed is reduced by this effect's Potency. At the start of the owner's next turn, remove this effect.";
            StatusEffectManager.FullStatusEffect data = StatusEffectManager.New<Bind>(
                pluginGuid, rName, rDesc, -1, GameColors.Instance.orange,
                TextureLoader.LoadTextureFromFile("sigilBind.png", Assembly),
                TextureLoader.LoadTextureFromFile("sigilBind_pixel.png", Assembly))
                .AddMetaCategories(StatusMetaCategory.Part1StatusEffect);

            Bind.specialAbility = data.Id;
            Bind.iconId = data.IconInfo.ability;

            const string desc = "Determines the order that cards will attack during combat. Player-owned cards have a base Speed of 3 while opponent-owned cards have a base Speed of 0.";
            StatusEffectManager.New<Bind>(
                pluginGuid, "Speed", desc, 0, GameColors.Instance.orange,
                TextureLoader.LoadTextureFromFile("sigilSpeed.png", Assembly),
                null)
                .AddMetaCategories(StatusMetaCategory.Part1StatusEffect)
                .IconInfo.SetPassive();
        }
    }
}
