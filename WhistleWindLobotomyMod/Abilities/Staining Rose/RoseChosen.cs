using DiskCardGame;
using Infiniscryption.Spells.Patchers;
using InscryptionAPI.Card;
using UnityEngine;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;


namespace WhistleWindLobotomyMod {
    public partial class Abilities {
        private static void AddRoseChosen() {
            AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();
            info.rulebookName = "Chosen by the Rose";
            info.rulebookDescription = "[creature] is resonating with Staining Rose. When Staining Rose is satisfied, this card will die and be removed from your deck.";
            info.powerLevel = 0;
            info.passive = true;
            info.canStack = true;
            info.metaCategories.Add(AbilityMetaCategory.Part1Rulebook);
            RoseChosen.ID = AbilityManager.Add(LobotomyPlugin.pluginGuid, info, null, TextureLoader.LoadTextureFromFile("sigilRose.png"))
                .SetExtendedProperty("Uninheritable", true)
                .Id;
        }
    }

    /// <summary>
    /// [creature] is resonating with Staining Rose. When Staining Rose is satisfied, this card will die and be removed from your deck.
    /// </summary>
    public class RoseChosen {
        public static Ability ID { get; internal set; }
        public static bool CanBeRoseChosen(CardInfo info) {
            return info.Sacrificable && !info.IsSpell() && info.LacksTrait(AbnormalPlugin.ImmuneToInstaDeath) && info.PowerLevel >= 7;
        }
    }
}
