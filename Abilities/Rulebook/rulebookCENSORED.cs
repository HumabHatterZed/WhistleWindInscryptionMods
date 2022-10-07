using InscryptionAPI;
using DiskCardGame;
using System.Collections;
using UnityEngine;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Rulebook_CENSORED()
        {
            const string rulebookName = "CENSORED";
            const string rulebookDescription = "On killing a card, <CENSORED> them and add the resulting minion to your hand. The minion is defined as: X Power, 1 Health.";
            const string dialogue = "femboy";
            EntryCENSORED.ability = AbilityHelper.CreateAbility<EntryCENSORED>(
                Artwork.sigilAbnormality, Artwork.sigilAbnormality_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 0,
                addModular: false, opponent: false, canStack: false, isPassive: true,
                overrideModular: true).Id;
        }
    }
    public class EntryCENSORED : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }
}
