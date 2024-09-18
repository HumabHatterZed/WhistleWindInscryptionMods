using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers;
using System.Collections;
using UnityEngine;
using WhistleWindLobotomyMod.Core.Helpers;


namespace WhistleWindLobotomyMod
{
    public class BoneMeal : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
        public override bool RespondsToResolveOnBoard() => true;

        public override IEnumerator OnResolveOnBoard()
        {
            int bones = Mathf.FloorToInt(ResourcesManager.Instance.PlayerBones / 2f);
            yield return ResourcesManager.Instance.SpendBones(bones);
        }
    }

    public partial class LobotomyPlugin
    {
        private void Ability_BoneMeal()
        {
            StatIconInfo icon = StatIconManager.AllStatIconInfos.Find(x => x.iconType == SpecialStatIcon.Bones);
            AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();
            info.rulebookName = "Bone Meal";
            info.rulebookDescription = "When [creature] is played, spend half of the owner's current Bones, rounded down.";
            info.powerLevel = -3;
            info.pixelIcon = icon.pixelIconGraphic;
            BoneMeal.ability = AbilityManager.Add(pluginGuid, info, typeof(BoneMeal), icon.iconGraphic).Id;
        }
    }
}
