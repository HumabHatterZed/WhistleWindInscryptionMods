using DiskCardGame;
using InscryptionAPI.Card;
using UnityEngine;

namespace BonniesBakingPack
{
    public partial class BakingPlugin
    {
        private void AddAbilities()
        {
            AddFreshFood();
            AddFreshIngredients();
            AddFreshIngredientsMagnificus();

            StatIconInfo bingusStatInfo = ScriptableObject.CreateInstance<StatIconInfo>()
                .SetRulebookInfo("Infinity", "The value represented with this sigil will be equal to the concept of infinity.")
                .SetIcon(GetTexture("infiniteSigil.png"))
                .SetAppliesToStats(true, true)
                .SetDefaultPart1Ability();
            BingusStatIcon.Icon = StatIconManager.Add(pluginGuid, bingusStatInfo, typeof(BingusStatIcon)).Id;
            BingusAbility.SpecialAbility = SpecialTriggeredAbilityManager.Add(pluginGuid, "BingusAbility", typeof(BingusAbility)).Id;

            TalkingBonnieAbility.SpecialAbility = SpecialTriggeredAbilityManager.Add(pluginGuid, "BonnieAbility", typeof(TalkingBonnieAbility)).Id;
            TalkingBunnieAbility.SpecialAbility = SpecialTriggeredAbilityManager.Add(pluginGuid, "BunnieAbility", typeof(TalkingBunnieAbility)).Id;
            TalkingBonnieDiskAbility.SpecialAbility = SpecialTriggeredAbilityManager.Add(pluginGuid, "BonnieDiskAbility", typeof(TalkingBonnieDiskAbility)).Id;
            TalkingBunnieDiskAbility.SpecialAbility = SpecialTriggeredAbilityManager.Add(pluginGuid, "BunnieDiskAbility", typeof(TalkingBunnieDiskAbility)).Id;
            BunnieAttackAbility.SpecialAbility = SpecialTriggeredAbilityManager.Add(pluginGuid, "BunnieAttackAbility", typeof(BunnieAttackAbility)).Id;

            PandaAbility.SpecialAbility = SpecialTriggeredAbilityManager.Add(pluginGuid, "PandaAbility", typeof(PandaAbility)).Id;
            NineAbility.SpecialAbility = SpecialTriggeredAbilityManager.Add(pluginGuid, "NineAbility", typeof(NineAbility)).Id;
            DuckRabbitAbility.SpecialAbility = SpecialTriggeredAbilityManager.Add(pluginGuid, "DuckitAbility", typeof(DuckRabbitAbility)).Id;
        }
    }
}
