using APIPlugin;
using DiskCardGame;
using System.Collections;
using UnityEngine;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private NewSpecialAbility SpecialAbility_Bath()
        {
            const string rulebookName = "Bath";
            const string rulebookDescription = "Reacts to cards being sacrificed.";
            return WstlUtils.CreateSpecialAbility<BloodBath>(
                AbilitiesUtil.LoadAbilityIcon("None"),
                rulebookName, rulebookDescription, false, false, false);
        }
    }
    public class BloodBath : SpecialCardBehaviour
    {
        public static SpecialTriggeredAbility specialAbility;
        public static SpecialAbilityIdentifier GetSpecialAbilityId
        {
            get
            {
                return SpecialAbilityIdentifier.GetID(WhistleWindLobotomyMod.Plugin.pluginGUID, "Bath");
            }

        }

        private readonly string bathDialogue1 = "A hand rises from the sanguine pool.";
        private readonly string bathDialogue2 = "Another pale hand emerges.";
        private readonly string bathDialogue3 = "A third hand reaches out, as if asking for help.";

        public override bool RespondsToOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            if (base.PlayableCard.InHand && base.PlayableCard.Info.name.ToLowerInvariant().Equals("wstl_bloodbath"))
            {
                return !fromCombat && Singleton<BoardManager>.Instance.currentSacrificeDemandingCard == base.PlayableCard;
            }
            return !fromCombat;
        }

        public override IEnumerator OnOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            int name = 0;
            CardInfo evolution = CardLoader.GetCardByName("wstl_bloodBath");
            switch (base.PlayableCard.Info.name.ToLowerInvariant())
            {
                case "wstl_bloodbath":
                    name = 1;
                    evolution = CardLoader.GetCardByName("wstl_bloodBath1");
                    break;
                case "wstl_bloodbath1":
                    name = 2;
                    evolution = CardLoader.GetCardByName("wstl_bloodBath2");
                    break;
                case "wstl_bloodbath2":
                    name = 3;
                    evolution = CardLoader.GetCardByName("wstl_bloodBath3");
                    break;
            }
            yield return new WaitForSeconds(0.25f);
            foreach (CardModificationInfo item in base.Card.Info.Mods.FindAll((CardModificationInfo x) => !x.nonCopyable))
            {
                CardModificationInfo cardModificationInfo = (CardModificationInfo)item.Clone();
                evolution.Mods.Add(cardModificationInfo);
            }
            yield return base.PlayableCard.TransformIntoCard(evolution);
            yield return new WaitForSeconds(0.5f);

            switch (name)
            {
                case 1:
                    if (!PersistentValues.HasSeenBloodbathHand)
                    {
                        PersistentValues.HasSeenBloodbathHand = true;
                        yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(bathDialogue1, -0.65f, 0.4f);
                    }
                    break;
                case 2:
                    if (!PersistentValues.HasSeenBloodbathHand1)
                    {
                        PersistentValues.HasSeenBloodbathHand1 = true;
                        yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(bathDialogue2, -0.65f, 0.4f);
                    }
                    break;
                case 3:
                    if (!PersistentValues.HasSeenBloodbathHand2)
                    {
                        PersistentValues.HasSeenBloodbathHand2 = true;
                        yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(bathDialogue3, -0.65f, 0.4f);
                    }
                    break;
            }
        }
    }
}
