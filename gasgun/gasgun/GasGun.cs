using System;
using System.Windows.Forms;
using GTA;
using GTA.UI;
using GTA.Math;

namespace gasgun
{
    public class GasGun :Script
    {
        public Boolean Enabled { get; set; }
        public Config config { get; set; }
        public Menu menu { get; set; }
        public GasGun()
        {
            Enabled = false;
            config = new Config("scripts//GasGun.ini");
            menu = new Menu(this);

            Tick += OnTick;
            KeyDown += OnKeyDown;
            Notification.Show("Gas Gun - By Rafael Faustini");

        }
        public void OnTick(object sender, EventArgs e)
        {
            OnKeyUp();
            menu.process();
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
                menu.toggle();
            }
        }
    }
}
