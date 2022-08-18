using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections;
using System.Linq;
using UnityEngine;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Ability_Spores()
        {
            const string rulebookName = "Fungal Infector";
            const string rulebookDescription = "At the end of the owner's turn, adjacent cards gain 1 Spore. Cards with Spore take damage equal to their Spore at turn's end and create a Spore Mold Creature in their slot on death. A Spore Mold Creature is defined as: [ Spore ] Power, [ Spore ] Health.";
            const string dialogue = "Even if this turns out to be a curse, they will love this curse like a blessing.";
            Spores.ability = AbilityHelper.CreateAbility<Spores>(
                Resources.sigilSpores, Resources.sigilSpores_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 2,
                addModular: false, opponent: false, canStack: false, isPassive: false).Id;
        }
    }
    public class Spores : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
        public static CardModificationInfo fungusDecal = new()
        {
            DecalIds = { "decal_fungus"}
        };
        public override bool RespondsToTurnEnd(bool playerTurnEnd)
        {
            if (base.Card != null)
            {
                return base.Card.OpponentCard != playerTurnEnd;
            }
            return false;
        }
        public override IEnumerator OnTurnEnd(bool playerTurnEnd)
        {
            CardSlot toLeft = Singleton<BoardManager>.Instance.GetAdjacent(base.Card.Slot, adjacentOnLeft: true);
            CardSlot toRight = Singleton<BoardManager>.Instance.GetAdjacent(base.Card.Slot, adjacentOnLeft: false);

            Singleton<ViewManager>.Instance.SwitchToView(View.Board);
            yield return new WaitForSeconds(0.1f);
            yield return EmitSpores(toLeft, toRight);
        }

        private bool CheckValid(CardSlot slot)
        {
            return slot != null && slot.Card != null && slot.Card.Info.name != "wstl_theLittlePrinceMinion";
        }
        private IEnumerator EmitSpores(CardSlot toLeft, CardSlot toRight)
        {
            bool forceLeft = false;
            bool validLeft = CheckValid(toLeft);
            bool validRight = CheckValid(toRight);
            if (!validLeft && !validRight)
            {
                yield break;
            }
            yield return base.PreSuccessfulTriggerSequence();
            if (validLeft)
            {
                if (toLeft.Card.Info.GetExtendedProperty("wstl:HasSpore") == null)
                {
                    yield return toLeft.Card.Info.SetExtendedProperty("wstl:HasSpore", true);
                    toLeft.Card.AddPermanentBehaviour<SporeDamage>();
                    //toLeft.Card.Info.TempDecals.Clear();
                    //toLeft.Card.Info.TempDecals.Add(ResourceBank.Get<Texture>("Art/Cards/Decals/decal_fungus"));
                    toLeft.Card.OnStatsChanged();
                    toLeft.Card.Anim.StrongNegationEffect();

                    // Because of the way triggers work, if the card is on the left we have to manually make it take its first turn of damage here
                    // Check if it has no Spore, then do the damage thing if true
                    if (!(toLeft.Card.Info.GetExtendedPropertyAsInt("wstl:Spore") != null))
                    {
                        forceLeft = true;
                    }
                }
            }
            if (validRight)
            {
                if (toRight.Card.Info.GetExtendedProperty("wstl:HasSpore") == null)
                {
                    yield return toRight.Card.Info.SetExtendedProperty("wstl:HasSpore", true);
                    toRight.Card.AddPermanentBehaviour<SporeDamage>();
                    //toRight.Card.Info.TempDecals.Clear();
                    //toRight.Card.Info.TempDecals.Add(ResourceBank.Get<Texture>("Art/Cards/Decals/decal_fungus"));
                    toRight.Card.OnStatsChanged();
                    toRight.Card.Anim.StrongNegationEffect();
                }
            }
            yield return new WaitForSeconds(0.2f);
            if (validLeft || validRight)
            {
                yield return base.LearnAbility(0.4f);
            }
            if (forceLeft)
            {
                yield return new WaitForSeconds(0.2f);
                yield return toLeft.Card.Info.SetExtendedProperty("wstl:Spore", 1);
                yield return toLeft.Card.TakeDamage((int)toLeft.Card.Info.GetExtendedPropertyAsInt("wstl:Spore"), null);
                yield return new WaitForSeconds(0.4f);
            }
        }
    }
}