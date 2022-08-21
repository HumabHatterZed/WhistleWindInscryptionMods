using InscryptionAPI;
using DiskCardGame;
using System.Collections;
using UnityEngine;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Rulebook_MeltingLove()
        {
            const string rulebookName = "Melting Love";
            const string rulebookDescription = "When Health is 1, kills adjacent Slimes and absorb their Health until Health is equal or above its maximum.";
            const string dialogue = "femboy";
            EntryMeltingLove.ability = AbilityHelper.CreateAbility<EntryMeltingLove>(
                Resources.sigilAbnormality, Resources.sigilAbnormality_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 0,
                addModular: false, opponent: false, canStack: false, isPassive: true,
                overrideModular: true).Id;
        }
    }
    public class EntryMeltingLove : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }
}
