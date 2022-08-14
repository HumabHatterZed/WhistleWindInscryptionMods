using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void TheLittlePrinceMinion_O0466()
        {
            List<UnityEngine.Texture> decals = new()
            {
                ResourceBank.Get<UnityEngine.Texture>("Art/Cards/Decals/decal_fungus")
            };
            CardHelper.CreateCard(
                "wstl_theLittlePrinceMinion", "Spore Mold Creature",
                "A creature consumed by cruel, kind fungus.",
                0, 0, 0, 0,
                Resources.theLittlePrinceMinion, Resources.theLittlePrinceMinion_emission,
                abilities: new(), specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(), decals: decals);
        }
    }
}