using IsaacMod.Common;
using IsaacMod.Content.Rarities;
using IsaacMod.Utils;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace IsaacMod.Content.Items.Collectibles
{
	public abstract class Collectible : ModItem
	{
        public virtual bool isPassive => true;
		public virtual string ID => "";
        public static List<Collectible> InitList;
        public override void Load()
        {
            if (InitList == null) {
                InitList = new List<Collectible>(); 
            }
            InitList.Add(this);
            if(IsaacMod.CollectiblesID == null)
            {
                IsaacMod.CollectiblesID = new List<string>();
            }
            IsaacMod.CollectiblesID.Add(ID);
        }
        public virtual float getWeight()
        {
            return 5f;
        }
        public static void spawnNewItemFromItemPool(Pool<int> pool, IEntitySource source, Rectangle rect)
        {
            Item.NewItem(source, rect, new Item(pool.Draw().Value));
        }
        public virtual List<string> getItemPools()
        {
            return new List<string>() { "normal" };
            /*
            Item Pools:
            normal - Drops when boss first be killed
             */
        }
        public override void SetDefaults() {
			Item.width = 64;
			Item.height = 64;
            Item.value = 2000;
			Item.consumable = isPassive;
			Item.useTime = 32;
			Item.useAnimation = 32;
			Item.useStyle = ItemUseStyleID.RaiseLamp;
            Item.noMelee = true;
            Item.rare = ModContent.RarityType<Rarities.Collectibles>();
		}

        public override bool? UseItem(Player player)
        {
            if (isPassive)
            {
                player.Isaac().AddCollectible(ID);
                SoundEngine.PlaySound(new("IsaacMod/Sounds/Pickup"));
            }
            return true;
        }
    }
}
