using IsaacMod;
using IsaacMod.Content.Items.Collectibles;
using IsaacMod.Utils;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
namespace IsaacMod.Common
{
    public class IsaacPlayer : ModPlayer
    {
        public Dictionary<string, int> collectiblesOwned = new Dictionary<string, int>();
        public int suplexDashTime = 0;
        public Vector2 suplexDashvel = Vector2.Zero;

        public int immuneTime = 0;
        public void AddCollectible(string ID, bool add = true, int num = 1)
        {
            if (!collectiblesOwned.ContainsKey(ID))
            {
                collectiblesOwned.Add(ID, 0);
            }
            collectiblesOwned[ID] += num * (add ? 1 : -1);
        }
        public bool hasCollectible(string ID)
        {
            if (!collectiblesOwned.ContainsKey(ID))
            {
                return false;
            }
            return collectiblesOwned[ID] > 0;
        }
        public int getCollectibleCount(string ID)
        {
            if (collectiblesOwned.ContainsKey(ID))
            {
                return collectiblesOwned[ID];
            }
            return 0;

        }

        public override void SaveData(TagCompound tag)
        {
            foreach (string id in IsaacMod.CollectiblesID)
            {
                tag.Add("IsaacModCollectibles" + id, getCollectibleCount(id));
            }
        }

        public override void LoadData(TagCompound tag)
        {
            foreach (string id in IsaacMod.CollectiblesID)
            {
                if (tag.ContainsKey("IsaacModCollectibles" + id))
                {
                    collectiblesOwned[id] = tag.GetInt("IsaacModCollectibles" + id);
                }
            }
        }
        public override void PostUpdateMiscEffects()
        {
            Player player = Player;
            player.GetAttackSpeed(DamageClass.Generic) += 0.02f * getCollectibleCount(NumberOne.Id);
            if (getCollectibleCount(InnerEye.Id) > 0)
            {
                player.GetAttackSpeed(DamageClass.Generic) *= 1f / (getCollectibleCount(InnerEye.Id) * 3);
            }
            player.GetDamage(DamageClass.Generic) += 0.02f * getCollectibleCount(BloodOfTheMartyr.Id);
            player.GetDamage(DamageClass.Generic) += 0.04f * getCollectibleCount(CricketsHead.Id);
            player.GetDamage(DamageClass.Generic) *= 1 + (0.022f * getCollectibleCount(CricketsHead.Id));
            player.GetDamage(DamageClass.Generic) += 0.01f * getCollectibleCount(MyReflection.Id);
            player.luck -= 0.02f * getCollectibleCount(MyReflection.Id);
            player.GetAttackSpeed(DamageClass.Generic) += 0.032f * getCollectibleCount(TheSadOnion.Id);
        }
        public override bool FreeDodge(Player.HurtInfo info)
        {
            if(suplexDashTime > 0 || suplexNPCTime > 0 || immuneTime > 0)
            {
                return true;
            }
            return false;
        }
        public int suplexNPC = -1;
        public int suplexNPCTime = 0;
        public Vector2 spScreenVec = Vector2.Zero;
        public override void ModifyScreenPosition()
        {
            if(suplexNPCTime > 0)
            {
                Main.screenPosition = spScreenVec;
            }
        }
        public override void PreUpdateMovement()
        {
            if(suplexNPCTime > 0)
            {
                Player.gravity = 0;
                Player.velocity *= 0;
            }
        }
        public override void PreUpdate()
        {
            if(immuneTime > 0)
            {
                immuneTime--;
            }
            if (suplexDashTime > 0)
            {
                suplexDashTime--;
                Player.velocity = suplexDashvel;
                Player.eocDash = suplexDashTime;
                if(suplexDashvel.X < 0)
                {
                    Player.direction = -1;
                }
                else
                {
                    Player.direction = 1;
                }
                foreach(NPC n in Main.ActiveNPCs)
                {
                    if (n.getRect().Intersects(Player.getRect()))
                    {
                        suplexNPC = n.whoAmI;
                        suplexNPCTime = 60;
                        suplexDashTime = 0;
                        n.velocity *= 0;
                        if (!Main.dedServ)
                        {
                            SoundEngine.PlaySound(new("IsaacMod/Sounds/suplex"), Player.Center);
                        }
                        Player.Isaac().spScreenVec = Main.screenPosition;
                        n.Isaac().suplexTime = 60;
                        n.Isaac().suplexPlayer = Player.whoAmI;
                        break;
                    }
                }
                if(suplexDashTime == 0)
                {
                    Player.velocity *= 0.1f;
                }
            }
            if(suplexNPCTime > 0)
            {
                immuneTime = 120;
                if(suplexNPCTime > 30)
                {
                    Player.Center += new Vector2(0, -70);
                   
                }
                else
                {
                    if (suplexNPCTime > 1)
                    {
                        Player.Center += new Vector2(0, 70);
                    }
                    Player.fullRotation = MathHelper.Pi;
                }
                suplexNPCTime--;
                if(suplexNPCTime == 0)
                {
                    Player.fullRotation = 0;
                }
                if(suplexNPCTime == 0 && Main.myPlayer == Player.whoAmI)
                {
                    int s = Math.Max(800, Main.npc[suplexNPC].lifeMax / 16);
                    Main.npc[suplexNPC].defense -= 600;
                    NPC.HitInfo hit = Main.npc[suplexNPC].CalculateHitInfo(s, 0, false, 0, DamageClass.Melee);
                    Main.npc[suplexNPC].defense += 600;
                    Main.npc[suplexNPC].StrikeNPC(hit, false, false);
                    if (!Main.dedServ)
                    {
                        SoundEngine.PlaySound(new("IsaacMod/Sounds/landsmash"), Player.Center);
                    }
                }
            }
        }
        public override void PostUpdate()
        {
        }

    }
}