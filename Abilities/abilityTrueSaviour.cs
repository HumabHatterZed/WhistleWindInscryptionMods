using InscryptionAPI;
using DiskCardGame;
using HarmonyLib;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Ability_TrueSaviour()
        {
            const string rulebookName = "True Saviour";
            string rulebookDescription = ConfigUtils.Instance.RevealWhiteNight ? "Cannot die. Transforms non-Terrain and non-Pelt cards into Apostles." : "My story is nowhere, unknown to all.";
            const string dialogue = "[c:bR]I am death and life. Darkness and light.[c:]";

            TrueSaviour.ability = WstlUtils.CreateAbility<TrueSaviour>(
                Resources.sigilTrueSaviour,
                rulebookName, rulebookDescription, dialogue, -3,
                overrideModular: true).Id;
        }
    }
    public class TrueSaviour : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        private int softLock = 0;
        private int count = 0;
        private bool heretic = false;

        private readonly string killedDialogue = "[c:bR]Do not deny me.[c:]";
        private readonly string hammerDialogue = "[c:bR]I shall not leave thee until I have completed my mission.[c:]";
        private readonly string hereticDialogue = "[c:bR]Have I not chosen you, the Twelve? Yet one of you is a devil.[c:]";

        public override bool RespondsToOtherCardResolve(PlayableCard otherCard)
        {
            if (otherCard != null)
            {
                if (otherCard.Info.name != "wstl_hundredsGoodDeeds" && !otherCard.Info.name.Contains("wstl_apostle") && otherCard != base.Card)
                {
                    return otherCard.Slot.IsPlayerSlot ? base.Card.Slot.IsPlayerSlot : !base.Card.Slot.IsPlayerSlot;
                }
            }
            return false;
        }
        public override IEnumerator OnOtherCardResolve(PlayableCard otherCard)
        {
            yield return base.PreSuccessfulTriggerSequence();
            if (otherCard.Info.HasTrait(Trait.Pelt) ||
                otherCard.Info.HasTrait(Trait.Terrain) ||
                otherCard.Info.SpecialAbilities.Contains(SpecialTriggeredAbility.PackMule))
            {
                softLock++;
                yield return otherCard.Die(false, base.Card);
                if (softLock >= 6)
                {
                    softLock = 0;
                    yield break;
                }
            }
            else
            {
                CardInfo randApostle = CardLoader.GetCardByName("wstl_apostleScythe");

                // 1/12 chance of being Heretic, there can only be one Heretic
                if (new System.Random().Next(0, 12) == 0 && !heretic)
                {
                    heretic = true;
                    randApostle = CardLoader.GetCardByName("wstl_apostleHeretic");
                }
                else
                {
                    switch (new System.Random().Next(0, 3))
                    {
                        case 0: // Scythe
                            break;
                        case 1: // Spear
                            randApostle = CardLoader.GetCardByName("wstl_apostleSpear");
                            break;
                        case 2: // Staff
                            randApostle = CardLoader.GetCardByName("wstl_apostleStaff");
                            break;
                    }
                }
                yield return otherCard.TransformIntoCard(randApostle);
                if (heretic && !PersistentValues.ApostleHeretic)
                {
                    PersistentValues.ApostleHeretic = true;
                    yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(hereticDialogue, -0.65f, 0.4f, speaker: DialogueEvent.Speaker.Bonelord);
                    yield return new WaitForSeconds(0.2f);
                }
            }
        }

        public override bool RespondsToTakeDamage(PlayableCard source)
        {
            if (source.Info.name == "wstl_hundredsGoodDeeds" || source.Info.name == "wstl_apostleHeretic")
            {
                return false;
            }
            return true;
        }
        public override IEnumerator OnTakeDamage(PlayableCard source)
        {
            yield return base.PreSuccessfulTriggerSequence();
            base.Card.HealDamage(base.Card.MaxHealth - base.Card.Health);
        }

        public override bool RespondsToDie(bool wasSacrifice, PlayableCard killer)
        {
            return true;
        }
        public override IEnumerator OnDie(bool wasSacrifice, PlayableCard killer)
        {
            yield return base.PreSuccessfulTriggerSequence();
            if (killer != null)
            {
                if (killer.Info.name == "wstl_hundredsGoodDeeds" || killer.Info.name == "wstl_apostleHeretic")
                {
                    AudioController.Instance.PlaySound2D("mycologist_scream");
                    Singleton<UIManager>.Instance.Effects.GetEffect<ScreenGlitchEffect>().SetIntensity(1f, 0.4f);
                    yield break;
                }
                yield return Singleton<BoardManager>.Instance.CreateCardInSlot(base.Card.Info, base.Card.Slot, 0.15f);
                yield return new WaitForSeconds(0.2f);

                if (!PersistentValues.WhiteNightKilled)
                {
                    PersistentValues.WhiteNightKilled = true;
                    yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(killedDialogue, -0.65f, 0.4f, Emotion.Anger, speaker: DialogueEvent.Speaker.Bonelord);
                    yield return new WaitForSeconds(0.2f);
                }
                yield return killer.Die(false, base.Card);
            }
            else
            {
                yield return Singleton<BoardManager>.Instance.CreateCardInSlot(base.Card.Info, base.Card.Slot, 0.15f);
                if (!PersistentValues.WhiteNightHammer)
                {
                    yield return new WaitForSeconds(0.2f);
                    PersistentValues.WhiteNightHammer = true;
                    yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(hammerDialogue, -0.65f, 0.4f, speaker: DialogueEvent.Speaker.Bonelord);
                    yield return new WaitForSeconds(0.2f);

                }
                yield return new WaitForSeconds(0.2f);
                yield return Singleton<LifeManager>.Instance.ShowDamageSequence(1, 1, toPlayer: true, 0.25f, ResourceBank.Get<GameObject>("Prefabs/Environment/ScaleWeights/Weight_RealTooth"));
                yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(killedDialogue, -0.65f, 0.4f, Emotion.Anger, speaker: DialogueEvent.Speaker.Bonelord);
            }
        }
    }
}
