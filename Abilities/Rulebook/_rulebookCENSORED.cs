using InscryptionAPI;
using DiskCardGame;
using System.Collections;
using UnityEngine;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Rulebook_CENSORED()
        {
            const string rulebookName = "CENSORED";
            const string rulebookDescription = "Whenever it kills a card, <CENSORED> them and add the resulting minion to your hand.";
            const string dialogue = "femboy";
            EntryCENSORED.ability = AbilityHelper.CreateAbility<EntryCENSORED>(
                Resources.sigilAbnormality, Resources.sigilAbnormality_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 0,
                overrideModular: true).Id;
        }
    }
    public class EntryCENSORED : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }
}
