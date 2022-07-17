using InscryptionAPI;
using DiskCardGame;
using System.Collections;
using UnityEngine;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Rulebook_JudgementBird()
        {
            const string rulebookName = "Judgement Bird";
            const string rulebookDescription = "Kills selected cards. This does not activate abilities like Sharp Quills.";
            const string dialogue = "femboy";
            EntryJudgementBird.ability = AbilityHelper.CreateAbility<EntryJudgementBird>(
                Resources.sigilAbnormality, Resources.sigilAbnormality_pixel,
                rulebookName, rulebookDescription, dialogue, powerlevel: 0,
                overrideModular: true).Id;
        }
    }
    public class EntryJudgementBird : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }
}
