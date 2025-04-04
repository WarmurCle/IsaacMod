using CalamityMod.Projectiles.Ranged;
using IsaacMod.Content.Items.Collectibles;
using IsaacMod.Utils;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
namespace IsaacMod
{

    internal static partial class CalamityWeakRef
    {
        [JITWhenModsEnabled("Calamitymod")]
        internal static class ItemType
        {
            internal static int hGaleProj => ModContent.ProjectileType<HeavenlyGaleProj>();
        }
    }
}
namespace IsaacMod.Common
{
    public class IsaacGlobalProjectile : GlobalProjectile
    {
        public static List<int> heldProjs;
        public override void Load()
        {
            heldProjs = new List<int>();
            
        }
        
        public override void Unload()
        {
            heldProjs = null;
        }
        public override bool InstancePerEntity => true;
        public float homingSpeed { get; set; }
        public static bool canInnerEyeSpawnProjNow = true;
        public float InnerEyeRot = 0;
        public static bool overrideNewProjsInnerEyeRot = false;
        public static float newProjsInnerEyeRot = 0;
        public static Vector2 lastMouse;
        public bool resetMousePos = false;
        public bool InnerEyeInited = false;
        public int counter = 0;
        public int timeleftMax = 10000;
        public Entity shootOwner = null;
        public bool hited = false;
        public override bool PreAI(Projectile projectile)
        {
            counter++;
            if((counter > timeleftMax * 0.27f || counter > 160) && projectile.friendly && projectile.owner >= 0 && Main.player[projectile.owner].Isaac().hasCollectible(MyReflection.Id))
            {
                if (shootOwner != null)
                {
                    projectile.velocity += (shootOwner.Center - projectile.Center).SafeNormalize(Vector2.Zero) * 0.1f;
                    projectile.velocity *= 0.99f;

                }
            }
            if(InnerEyeRot != 0 && !InnerEyeInited)
            {
                InnerEyeInited = true;
                projectile.usesLocalNPCImmunity = true;
                projectile.localNPCHitCooldown = 12;
            }
            if (projectile.owner >= 0 && InnerEyeRot != 0)
            {
                lastMouse = new Vector2(Main.mouseX, Main.mouseY);
                Vector2 newMousePos = Main.player[projectile.owner].Center + (Main.MouseWorld - Main.player[projectile.owner].Center).RotatedBy(InnerEyeRot);
                Vector2 newScreenMouse = newMousePos - Main.screenPosition;
                Main.mouseX = (int)newScreenMouse.X;
                Main.mouseY = (int)newScreenMouse.Y;
                resetMousePos = true;
            }
            if(!hited && projectile.owner >= 0 && Main.player[projectile.owner].Isaac().hasCollectible(SpoonBender.Id) && projectile.friendly)
            {
                NPC target = projectile.FindTargetWithinRange(Math.Max(projectile.width, projectile.height) + 500, projectile.tileCollide);
                if (target != null)
                {
                    for(int i = 0; i < Main.player[projectile.owner].Isaac().getCollectibleCount(SpoonBender.Id); i++)
                    {
                        projectile.velocity += (target.Center - projectile.Center).SafeNormalize(Vector2.Zero) * homingSpeed;
                        projectile.velocity *= 1 - homingSpeed * (projectile.tileCollide ? 0.05f : 0.02f);
                    }
                }
            }
            return true;
        }
        public override bool PreDraw(Projectile projectile, ref Color lightColor)
        {
            if (projectile.friendly && projectile.owner >= 0 && Main.player[projectile.owner].Isaac().hasCollectible(SpoonBender.Id))
            {
                lightColor = new Color(255, 64, 255);
            }
            if (projectile.friendly && projectile.owner >= 0 && Main.player[projectile.owner].Isaac().hasCollectible(NumberOne.Id))
            {
                lightColor = new Color(255, 255, 40);
            }
            if (projectile.friendly && projectile.owner >= 0 && Main.player[projectile.owner].Isaac().hasCollectible(BloodOfTheMartyr.Id))
            {
                lightColor = new Color(255, 25, 25);
            }
            return base.PreDraw(projectile, ref lightColor);
        }
        public override void PostAI(Projectile projectile)
        {
            if (resetMousePos)
            {
                Main.mouseX = (int)lastMouse.X;
                Main.mouseY = (int)lastMouse.Y;
                resetMousePos = false;
            }
        }
        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            timeleftMax = projectile.timeLeft;
            bool noNewRot = false;
            homingSpeed = projectile.velocity.Length() * 0.1f;
            if (source is EntitySource_Parent es)
            {
                shootOwner = es.Entity;
                if(es.Entity is Projectile proj)
                {
                    if (proj.Isaac().InnerEyeRot != 0)
                    {
                        noNewRot = true;
                    }
                    InnerEyeRot = proj.Isaac().InnerEyeRot;
                }
                if(es.Entity is Player plr)
                {
                    if (plr.Isaac().hasCollectible(NumberOne.Id))
                    {
                        projectile.timeLeft = (int)Math.Round(projectile.timeLeft * 0.8f);
                    }
                }
            }
            if (overrideNewProjsInnerEyeRot && !noNewRot)
            {
                InnerEyeRot = newProjsInnerEyeRot;
                projectile.netUpdate = true;
            }
            
            if(projectile.friendly && projectile.owner >= 0 && canInnerEyeSpawnProjNow && heldProjs.Contains(projectile.type))
            {
                Player player = Main.player[projectile.owner];
                var modPlayer = player.Isaac();
                if (modPlayer.hasCollectible(InnerEye.Id))
                {
                    canInnerEyeSpawnProjNow = false;
                    Mod calEntropy = null;
                    ModLoader.TryGetMod("CalamityEntropy", out calEntropy);

                    
                    var rots = InnerEye.getRots(player);
                    foreach (float rot in rots)
                    {
                        int p = Projectile.NewProjectile(source, player.Center, projectile.velocity.RotatedBy(rot), projectile.type, projectile.damage, projectile.knockBack, projectile.owner);
                        Projectile proj = Main.projectile[p];
                        
                        proj.Isaac().InnerEyeRot = rot;
                        proj.netUpdate = true;
                        if (calEntropy != null && (bool)calEntropy.Call("GetTTHoldoutCheck"))
                        {
                            overrideNewProjsInnerEyeRot = true;
                            newProjsInnerEyeRot = rot;
                            calEntropy.Call("CopyProjForTTwin", p);
                            overrideNewProjsInnerEyeRot = false;

                        }
                    }
                    canInnerEyeSpawnProjNow = true;
                }
                
            }
        }

        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            hited = true;
        }
        public override void SendExtraAI(Projectile projectile, BitWriter bitWriter, BinaryWriter binaryWriter)
        {
            binaryWriter.Write(InnerEyeRot);
        }

        public override void ReceiveExtraAI(Projectile projectile, BitReader bitReader, BinaryReader binaryReader)
        {
            InnerEyeRot = binaryReader.ReadInt32();
        }

        public override GlobalProjectile Clone(Projectile from, Projectile to)
        {
            var clone = to.Isaac();
            clone.InnerEyeRot = InnerEyeRot;
            return clone;
        }
    }
}
