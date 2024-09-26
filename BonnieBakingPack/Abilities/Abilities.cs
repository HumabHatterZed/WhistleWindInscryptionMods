using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers;
using UnityEngine;

namespace BonniesBakingPack
{
    public partial class BakingPlugin
    {
        private void AddAbilities()
        {
            AddFreshFood();
            AddFreshIngredients();

            StatIconInfo bingusStatInfo = ScriptableObject.CreateInstance<StatIconInfo>()
                .SetRulebookInfo("Infinity", "The value represented with this sigil will be equal to the concept of infinity.")
                .SetIcon(GetTexture("infiniteSigil.png"))
                .SetAppliesToStats(true, true)
                .SetDefaultPart1Ability();
            BingusStatIcon.Icon = StatIconManager.Add(pluginGuid, bingusStatInfo, typeof(BingusStatIcon)).Id;
            BingusAbility.SpecialAbility = SpecialTriggeredAbilityManager.Add(pluginGuid, "BingusAbility", typeof(BingusAbility)).Id;
            BonnieAbility.SpecialAbility = SpecialTriggeredAbilityManager.Add(pluginGuid, "BonnieAbility", typeof(BonnieAbility)).Id;
            BunnieAbility.SpecialAbility = SpecialTriggeredAbilityManager.Add(pluginGuid, "BunnieAbility", typeof(BunnieAbility)).Id;
            BonnieDiskAbility.SpecialAbility = SpecialTriggeredAbilityManager.Add(pluginGuid, "BonnieDiskAbility", typeof(BonnieDiskAbility)).Id;
            BunnieDiskAbility.SpecialAbility = SpecialTriggeredAbilityManager.Add(pluginGuid, "BunnieDiskAbility", typeof(BunnieDiskAbility)).Id;

            PandaAbility.SpecialAbility = SpecialTriggeredAbilityManager.Add(pluginGuid, "PandaAbility", typeof(PandaAbility)).Id;
            NineAbility.SpecialAbility = SpecialTriggeredAbilityManager.Add(pluginGuid, "NineAbility", typeof(NineAbility)).Id;
            DuckRabbitAbility.SpecialAbility = SpecialTriggeredAbilityManager.Add(pluginGuid, "DuckitAbility", typeof(DuckRabbitAbility)).Id;
        }
    }
}
