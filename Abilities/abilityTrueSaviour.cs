using System.Linq;
using System.Collections;
using DiskCardGame;
using UnityEngine;
using APIPlugin;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private NewAbility Ability_TrueSaviour()
        {
            const string rulebookName = "";
            const string rulebookDescription = "My story is nowhere, unknown to all.";
            const string dialogue = "I am death and life. Darkness and light.";

            return WstlUtils.CreateAbility<TrueSaviour>(
                Resources.sigilTrueSaviour,
                rulebookName, rulebookDescription, dialogue, -3);
        }
    }
    public class TrueSaviour : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        private readonly string dialogue = "Do not deny me.";
        private readonly string dialogueHammer = "I shall not leave thee until I have completed my mission.";

        public override bool RespondsToOtherCardResolve(PlayableCard otherCard)
        {
            if (otherCard != null)
            {
                if (!otherCard.Info.HasTrait(Trait.Pelt) && !otherCard.Info.HasTrait(Trait.Terrain))
                {
                    if (!otherCard.Slot.IsPlayerSlot)
                    {
                        return !base.Card.Slot.IsPlayerSlot;
                    }
                    return base.Card.Slot.IsPlayerSlot;
                }
            }
            return false;
        }
        public override IEnumerator OnOtherCardResolve(PlayableCard otherCard)
        {
            yield return base.PreSuccessfulTriggerSequence();

            foreach (CardSlot slot in Singleton<BoardManager>.Instance.AllSlotsCopy.Where(slot => slot.Card.name.ToLowerInvariant().Contains("good deeds")))
            if (otherCard!= null && !otherCard.Info.HasTrait(Trait.Pelt) && !otherCard.Info.HasTrait(Trait.Terrain))
            {
                CardInfo randApostle = CardLoader.GetCardByName("wstl_apostleScythe");

                // 1/12 chance of being Heretic
                if (new System.Random().Next(0, 12) != 0)
                {
                    switch (new System.Random().Next(0, 2))
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
                else
                {
                    Plugin.Log.LogInfo($"Heretic");
                    //randApostle = CardLoader.GetCardByName("wstl_heretic");
                }
                yield return otherCard.TransformIntoCard(randApostle);
            }
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
                CardInfo sinInfo = CardLoader.GetCardByName("wstl_hundredsGoodDeeds");
                if (killer.Info != sinInfo)
                {
                    yield return Singleton<BoardManager>.Instance.CreateCardInSlot(base.Card.Info, base.Card.Slot, 0.15f);
                    yield return new WaitForSeconds(0.2f);

                    if (!PersistentValues.WhiteNightKilled)
                    {
                        PersistentValues.WhiteNightKilled = true;
                        yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(dialogue, -0.65f, 0.4f, Emotion.Anger, speaker: DialogueEvent.Speaker.Bonelord);
                        yield return new WaitForSeconds(0.2f);
                    }
                    yield return killer.Die(false, base.Card);
                }
            }
            else
            {
                yield return Singleton<BoardManager>.Instance.CreateCardInSlot(base.Card.Info, base.Card.Slot, 0.15f);

                if (!PersistentValues.WhiteNightHammer)
                {
                    yield return new WaitForSeconds(0.2f);
                    PersistentValues.WhiteNightHammer = true;
                    yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(dialogueHammer, -0.65f, 0.4f, Emotion.Anger, speaker: DialogueEvent.Speaker.Bonelord);
                    yield return new WaitForSeconds(0.2f);
                }
                // yield return new WaitForSeconds(0.2f);
                // yield return Singleton<LifeManager>.Instance.ShowDamageSequence(1, 1, toPlayer: true, 0.25f, ResourceBank.Get<GameObject>("Prefabs/Environment/ScaleWeights/Weight_RealTooth"));
            }
        }
    }
}
