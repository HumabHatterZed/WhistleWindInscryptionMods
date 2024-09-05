using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using InscryptionAPI.Rulebook;
using InscryptionAPI.Triggers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.StatusEffects;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public class Fervent : StatusEffectBehaviour, ISetupAttackSequence, IOnOtherCardDieInHand
    {
        internal static StatusEffectManager.FullStatusEffect data;
        public static Ability iconId;
        public static SpecialTriggeredAbility specialAbility;
        public override Ability IconAbility => iconId;
        public override SpecialTriggeredAbility StatusEffect => specialAbility;

        public bool RespondsToModifyAttackSlots(PlayableCard card, OpposingSlotTriggerPriority modType, List<CardSlot> originalSlots, List<CardSlot> currentSlots, int attackCount, bool didRemoveDefaultSlot)
        {
            return card == base.PlayableCard && modType == OpposingSlotTriggerPriority.PostAdditionModification;
        }
        public List<CardSlot> CollectModifyAttackSlots(PlayableCard card, OpposingSlotTriggerPriority modType, List<CardSlot> originalSlots, List<CardSlot> currentSlots, ref int attackCount, ref bool didRemoveDefaultSlot)
        {
            List<CardSlot> allSlots = BoardManager.Instance.AllSlotsCopy;
            allSlots.Remove(base.PlayableCard.Slot);
            allSlots.RemoveAll(x => x.Card != null && x.Card.IsConductor());
            List<CardSlot> allSlots2 = new(allSlots);
            allSlots.AddRange(allSlots2.Where(x => x.Card != null));
            allSlots.AddRange(allSlots2.Where(x => x.Card != null && x.Card.HasStatusEffect<Fervent>(true)));
            allSlots.Randomize();

            int amtToKeep = currentSlots.Count > 0 ? currentSlots.Count : 1;
            allSlots.RemoveRange(0, allSlots.Count - amtToKeep);
            return allSlots;
        }
        public int GetTriggerPriority(PlayableCard card, OpposingSlotTriggerPriority modType, List<CardSlot> originalSlots, List<CardSlot> currentSlots, int attackCount, bool didRemoveDefaultSlot)
        {
            return -1000;
        }

        public override bool RespondsToOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            return card != null && card.OpponentCard == base.PlayableCard.OpponentCard && card.IsConductor();
        }
        public override IEnumerator OnOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            List<PlayableCard> cards = BoardManager.Instance.GetCards(!base.PlayableCard.OpponentCard, delegate (PlayableCard c)
            {
                return c != base.PlayableCard && c != card;
            });
            if (!cards.Exists(x => x.IsConductor()))
            {
                bool faceDown = base.PlayableCard.FaceDown;
                yield return base.PlayableCard.FlipFaceUp(faceDown);
                base.PlayableCard.Anim.StrongNegationEffect();
                yield return base.RemoveFromCard(true);
                yield return new WaitForSeconds(0.4f);
                yield return base.PlayableCard.FlipFaceDown(faceDown);

            }
        }

        public bool RespondsToOtherCardDieInHand(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            return RespondsToOtherCardDie(card, deathSlot, fromCombat, killer);
        }

        public IEnumerator OnOtherCardDieInHand(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            yield return OnOtherCardDie(card, deathSlot, fromCombat, killer);
        }
    }
    public partial class AbnormalPlugin
    {
        private void StatusEffect_Fervent()
        {
            const string rName = "Fervent Adoration";
            const string rDesc = "While there is a Movement, a card bearing this effect will strike at random spaces, prioritising other cards with this effect. If there is no Movement, remove this effect.";
            StatusEffectManager.FullStatusEffect data = StatusEffectManager.New<Fervent>(
                pluginGuid, rName, rDesc, -1, GameColors.Instance.nearBlack,
                TextureLoader.LoadTextureFromFile("sigilFervent.png", Assembly),
                TextureLoader.LoadTextureFromFile("sigilFervent_pixel.png", Assembly))
                .AddMetaCategories(StatusMetaCategory.Part1StatusEffect);

            Fervent.specialAbility = data.Id;
            Fervent.iconId = data.IconInfo.ability;
            Fervent.data = data;
        }
    }
}
