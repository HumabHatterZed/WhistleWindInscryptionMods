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
            EntryMagicalGirlH.ability = WstlUtils.CreateAbility<EntryMagicalGirlH>(
                Resources.sigilAbnormality,
                rulebookName, rulebookDescription, dialogue, 0,
                overrideModular: true).Id;
        }
    }
    public class EntryMagicalGirlH : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }
}
