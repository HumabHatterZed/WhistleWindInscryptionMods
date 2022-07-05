using InscryptionAPI;
using DiskCardGame;
using System.Collections;
using UnityEngine;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Rulebook_DreamOfABlackSwan()
        {
            const string rulebookName = "Dream of a Black Swan";
            const string rulebookDescription = "Creates a random Brother card on the board whenever she moves to a new slot. Adjacent Brothers take 1 less damage.";
            const string dialogue = "femboy";
            EntryDreamOfABlackSwan.ability = WstlUtils.CreateAbility<EntryDreamOfABlackSwan>(
                Resources.sigilAbnormality,
                rulebookName, rulebookDescription, dialogue, 0,
                overrideModular: true).Id;
        }
    }
    public class EntryDreamOfABlackSwan : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }
}
