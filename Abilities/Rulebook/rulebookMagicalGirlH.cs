using InscryptionAPI;
using DiskCardGame;
using System.Collections;
using UnityEngine;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Rulebook_MagicalGirlH()
        {
            const string rulebookName = "Magical Girl H";
            const string rulebookDescription = "Keeps track of card deaths for each side. Transforms when the difference between these amounts is two or greater.";
            const string dialogue = "femboy";
            EntryMagicalGirlH.ability = AbilityHelper.CreateAbility<EntryMagicalGirlH>(
                Resources.sigilAbnormality, Resources.sigilAbnormality_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 0,
                addModular: false, opponent: false, canStack: false, isPassive: true,
                overrideModular: true).Id;
        }
    }
    public class EntryMagicalGirlH : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }
}
