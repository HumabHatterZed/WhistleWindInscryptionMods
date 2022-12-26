using DiskCardGame;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Rulebook_JudgementBird()
        {
            const string rulebookName = "Judgement Bird";
            const string rulebookDescription = "Instantly kills the targeted card. This card is not affected by the target's abilities.";
            const string dialogue = "femboy";
            EntryJudgementBird.ability = AbilityHelper.CreateAbility<EntryJudgementBird>(
                Resources.sigilAbnormality, Resources.sigilAbnormality_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 0,
                addModular: false, opponent: false, canStack: false, isPassive: true,
                overrideModular: true).Id;
        }
    }
    public class EntryJudgementBird : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }
}
