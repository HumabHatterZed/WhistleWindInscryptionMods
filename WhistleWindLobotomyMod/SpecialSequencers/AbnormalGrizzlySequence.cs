using DiskCardGame;
using System.Collections;
using UnityEngine;
using WhistleWindLobotomyMod.Challenges;

namespace WhistleWindLobotomyMod.Core.SpecialSequencers {
    public class AbnormalGrizzlySequence : SpecialBattleSequencer {
        private static void GiveCardReach(PlayableCard card) {
            CardModificationInfo cardModificationInfo = new(Ability.Reach);
            cardModificationInfo.fromTotem = true;
            if (!card.HasAbility(ApostleSigil.ID)) {
                cardModificationInfo.healthAdjustment = 1;
            }
            card.AddTemporaryMod(cardModificationInfo);
        }
        public static IEnumerator ApostleGlitchSequence(Opponent opponent, bool apostles) {
            if (apostles) {
                ChallengeActivationUI.TryShowActivation(ApostleGrizzlies.Id);
            }
            else {
                ChallengeActivationUI.TryShowActivation(AscensionChallenge.GrizzlyMode);
            }
            opponent.TurnPlan.Clear();
            Singleton<ViewManager>.Instance.SwitchToView(View.Default);
            yield return new WaitForSeconds(0.1f);
            yield return opponent.ClearBoard();
            yield return opponent.ClearQueue();
            yield return new WaitForSeconds(0.1f);
            LeshyAnimationController.Instance.SetEyesTexture(ResourceBank.Get<Texture>("Art/Effects/red"));
            yield return AbnormalGlitchSequence(apostles);
            if (!SaveFile.IsAscension) {
                yield return Singleton<TextDisplayer>.Instance.ShowUntilInput("[c:bR]Too fast. Too soon.[c:]", -2.5f, 0.5f, Emotion.Anger);
            }
        }

        private static IEnumerator AbnormalGlitchSequence(bool apostles) {
            CardInfo grizzlyInfo = CardLoader.GetCardByName(apostles ? Cards.apostleGuardian : Cards.alriune);
            Singleton<UIManager>.Instance.Effects.GetEffect<ScreenGlitchEffect>().SetIntensity(1f, 1f);
            Singleton<CameraEffects>.Instance.Shake(0.1f, 1f);
            AudioController.Instance.PlaySound2D("broken_hum");
            foreach (CardSlot slot in Singleton<BoardManager>.Instance.OpponentSlotsCopy) {
                if (!Singleton<TurnManager>.Instance.Opponent.QueuedSlots.Contains(slot)) {
                    yield return Singleton<TurnManager>.Instance.Opponent.QueueCard(grizzlyInfo, slot, doTween: false, changeView: false, setStartPosition: false);
                }
                if (slot.Card == null) {
                    yield return Singleton<BoardManager>.Instance.CreateCardInSlot(grizzlyInfo, slot, 0f);
                }
                if (slot.Card != null) {
                    GiveCardReach(slot.Card);
                }
            }
            foreach (PlayableCard item in Singleton<TurnManager>.Instance.Opponent.Queue) {
                if (item.Info.name == grizzlyInfo.name) {
                    GiveCardReach(item);
                }
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
}
