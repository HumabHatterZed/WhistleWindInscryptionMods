using DiskCardGame;
using System.Collections;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.Properties;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_Sporogenic()
        {
            const string rulebookName = "Sporogenic";
            const string rulebookDescription = "Creatures adjacent to this card gain 1 Spores at the end of its owner's turn. This sigil activates before other sigils.";
            const string dialogue = "They will love this curse like a blessing.";
            Sporogenic.ability = AbnormalAbilityHelper.CreateAbility<Sporogenic>(
                Artwork.sigilSporogenic, Artwork.sigilSporogenic_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 2,
                modular: false, opponent: true, canStack: false).Id;
        }
    }
    public class Sporogenic : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
        private bool CheckValid(CardSlot slot) => slot.Card.Info.name != "wstl_theLittlePrinceMinion" && !slot.Card.Info.Mods.Exists(x => x.singletonId == "spore_status");

        public override bool RespondsToTurnEnd(bool playerTurnEnd) => base.Card.OpponentCard != playerTurnEnd;
        public override IEnumerator OnTurnEnd(bool playerTurnEnd)
        {
            List<CardSlot> adjacentSlots = Singleton<BoardManager>.Instance.GetAdjacentSlots(base.Card.Slot)
                .FindAll(s => s.Card != null && CheckValid(s));

            if (adjacentSlots.Count > 0)
            {
                yield return base.PreSuccessfulTriggerSequence();
                yield return HelperMethods.ChangeCurrentView(View.Board);
                foreach (CardSlot slot in adjacentSlots)
                {
                    CardModificationInfo decal = new()
                    {
                        singletonId = "spore_status",
                        DecalIds = { "wstl_spore_0" },
                        nonCopyable = true,
                    };

                    CardInfo copy = slot.Card.Info.Clone() as CardInfo;
                    copy.Mods = new(slot.Card.Info.Mods) { decal };

                    slot.Card.SetInfo(copy);
                    slot.Card.AddPermanentBehaviour<Spores>();
                }
                base.LearnAbility(0.4f);
            }
        }
    }
}