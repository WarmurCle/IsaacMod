using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace IsaacMod.Content.Rarities
{
    public class Collectibles : ModRarity
    {
        public override Color RarityColor => new Color(255, 180, 0);

        public override int GetPrefixedRarity(int offset, float valueMult) => Type;
    }
}
