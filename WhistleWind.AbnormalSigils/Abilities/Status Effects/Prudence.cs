using DiskCardGame;
using InscryptionAPI.Triggers;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils.StatusEffects;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public class Prudence : StatusEffectBehaviour, IModifyDamageTaken
    {
        public static Ability iconId;
        public static SpecialTriggeredAbility specialAbility;
        public override Ability IconAbility => iconId;
        public override SpecialTriggeredAbility StatusEffect => specialAbility;


        public override List<string> EffectDecalIds() => new();

        public bool RespondsToModifyDamageTaken(PlayableCard target, int damage, PlayableCard attacker, int originalDamage)
        {
            return target == base.PlayableCard;
        }

        public int OnModifyDamageTaken(PlayableCard target, int damage, PlayableCard attacker, int originalDamage)
        {
            damage += EffectPotency;
            return damage;
        }

        public int TriggerPriority(PlayableCard target, int damage, PlayableCard attacker) => 0;
    }
    public partial class AbnormalPlugin
    {
        private void StatusEffect_Prudence()
        {
            const string rName = "Flagellation";
            const string rDesc = "When this card is struck, receive additional damage equal to its Flagellation.";
            StatusEffectManager.FullStatusEffect data = StatusEffectManager.New<Prudence>(
                pluginGuid, rName, rDesc, -1, GameColors.Instance.lightPurple,
                TextureLoader.LoadTextureFromFile("sigilPrudence.png", Assembly),
                TextureLoader.LoadTextureFromFile("sigilPrudence_pixel.png", Assembly))
                .AddMetaCategories(StatusMetaCategory.Part1StatusEffect);

            Prudence.specialAbility = data.Id;
            Prudence.iconId = data.IconInfo.ability;
        }
    }
}
