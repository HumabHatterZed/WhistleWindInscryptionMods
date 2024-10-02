using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.RuleBook;
using InscryptionAPI.Triggers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils.Patches;
using WhistleWind.AbnormalSigils.StatusEffects;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public class Haste : ModifyOnUpkeepStatusEffectBehaviour/*, IGetAttackingSlots, IOnPostSlotAttackSequence*/
    {
        public static Ability iconId;
        public static SpecialTriggeredAbility specialAbility;
        public override Ability IconAbility => iconId;
        public override SpecialTriggeredAbility StatusEffect => specialAbility;
        public override int PotencyModification => -EffectPotency;
    }
    public partial class AbnormalPlugin
    {
        private void StatusEffect_Haste()
        {
            const string rName = "Haste";
            const string rDesc = "A card bearing this effect gains Speed equal to its Haste. At the start of the owner's next turn, remove this effect.";
            StatusEffectManager.FullStatusEffect data = StatusEffectManager.New<Haste>(
                pluginGuid, rName, rDesc, 1, GameColors.Instance.orange,
                TextureLoader.LoadTextureFromFile("sigilHaste.png", Assembly),
                TextureLoader.LoadTextureFromFile("sigilHaste_pixel.png", Assembly))
                .AddMetaCategories(StatusMetaCategory.Part1StatusEffect, StatusMetaCategory.Part3StatusEffect, StatusMetaCategory.GrimoraStatusEffect, StatusMetaCategory.MagnificusStatusEffect);

            Haste.specialAbility = data.Id;
            Haste.iconId = data.IconInfo.ability;
            data.IconInfo.SetUniqueRedirect("Speed", "wstl:Mechanic_Speed", GameColors.Instance.orange);
        }
    }
}
