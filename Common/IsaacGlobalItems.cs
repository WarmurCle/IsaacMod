using IsaacMod.Content.Items.Collectibles;
using IsaacMod.Utils;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace IsaacMod.Common
{
    public class IsaacGlobalItems : GlobalItem
    {
        public override bool InstancePerEntity => true;
        public static bool checkInnerEye = true;
        public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.Isaac().hasCollectible(InnerEye.Id) && checkInnerEye)
            {
                checkInnerEye = false;
                List<float> rots = InnerEye.getRots(player);
                if (item.ModItem is ModItem modItem)
                {
                    
                    foreach(float rot in rots)
                    {
                        Vector2 newVel = velocity.RotatedBy(rot);
                        
                        IsaacGlobalProjectile.overrideNewProjsInnerEyeRot = true;
                        IsaacGlobalProjectile.newProjsInnerEyeRot = rot;
                        bool vanillaShoot = ItemLoader.Shoot(item, player, source, position, newVel, type, damage, knockback);

                        IsaacGlobalProjectile.overrideNewProjsInnerEyeRot = false;

                        if (vanillaShoot)
                        {
                            int p = Projectile.NewProjectile(source, position, newVel, type, damage, knockback, player.whoAmI);
                            Projectile proj = Main.projectile[p];
                            proj.Isaac().InnerEyeRot = rot;
                            proj.netUpdate = true;
                        }
                    }
                }
                else
                {
                    foreach (float rot in rots)
                    {
                        Vector2 newVel = velocity.RotatedBy(rot);
                        int p = Projectile.NewProjectile(source, position, newVel, type, damage, knockback, player.whoAmI);
                        Projectile proj = Main.projectile[p];
                        proj.Isaac().InnerEyeRot = rot;
                        proj.netUpdate = true;
                    }
                }
                checkInnerEye = true;
            }
            return true;
        }
    }
}
