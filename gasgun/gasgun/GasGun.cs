using System;
using System.Windows.Forms;
using GTA;
using GTA.UI;
using GTA.Math;
using GTA.Native;

namespace gasgun
{
    public class GasGun :Script
    {
        public Boolean Enabled { get; set; }
        public static readonly Config config = new Config("scripts//GasGun.ini");
        public GasGun()
        {
            Enabled = false;
            Tick += OnTick;
            KeyDown += OnKeyDown;
            Notification.Show("Gas Gun - By Rafael Faustini");

        }
        public void OnTick(object sender, EventArgs e)
        {
            OnKeyUp();
        }
        public void OnKeyUp()
        {
            Ped player = Game.Player.Character;
            if (Game.IsControlJustPressed(GTA.Control.Attack) && Enabled && player.Weapons.Current == config.weapon && !player.IsReloading)
            {
                int ammo = player.Weapons.Current.Ammo;

                WeaponAsset bullet = config.bullet;

                if (ammo > 0)
                {
                    Vector3 target = player.Position + player.ForwardVector * 45;

                    World.ShootBullet(player.Weapons.CurrentWeaponObject.Position, target, player, bullet, 0, config.bulletspeed);

                    Game.Player.Character.Weapons.Current.Ammo = 0;

                    Game.Player.Character.Weapons.Current.Ammo = ammo - 1;
                }
            }
        }
        public void OnKeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Scroll)
            {
                Enabled = !Enabled;
                string message = Enabled ? "Enabled :)" : "Disabled";
                Notification.Hide(0);
                
                Notification.Show(NotificationIcon.Multiplayer,"Rafael Faustini", "GasGun", message, true);
                if (Enabled)
                {
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player, config.bullet, 100, true, false);
                    if (config.giveweapon)
                    {
                        Game.Player.Character.Weapons.Give(config.weapon, 50, true, true);
                    }
                }
            }
        }
    }
}
