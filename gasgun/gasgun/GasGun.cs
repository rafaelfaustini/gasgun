using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GTA;
using GTA.UI;
using System.Drawing;
using GTA.Native;
using GTA.Math;

namespace gasgun
{
    public class GasGun :Script
    {
        public Boolean Enabled { get; set; }
        public GasGun()
        {
            Enabled = false;
            Tick += OnTick;
            KeyDown += OnKeyDown;
            Notification.Show("Gas Gun - By Rafael Faustini");

        }
        public void OnTick(object sender, EventArgs e)
        {
            OnKeyDown();
        }
        public void OnKeyDown()
        {
            if (Game.IsControlJustPressed(GTA.Control.Attack) && Enabled && Game.Player.Character.Weapons.Current == WeaponHash.GrenadeLauncher)
            {
                int ammo = Game.Player.Character.Weapons.Current.Ammo;

                WeaponAsset bullet = WeaponHash.BZGas;


                if (ammo > 0)
                {
                    Vector3 target = Game.Player.Character.Position + Game.Player.Character.ForwardVector * 45;

                    World.ShootBullet(Game.Player.Character.Weapons.CurrentWeaponObject.Position, target, Game.Player.Character, bullet, 0, 1f);


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
                string message = Enabled ? "enabled" : "disabled";
                Notification.Hide(0);
                Notification.Show("Gasgun " + message);
                if (Enabled)
                {
                    Game.Player.Character.Weapons.Give(WeaponHash.BZGas, 100, true, true);
                    Game.Player.Character.Weapons.Give(WeaponHash.GrenadeLauncher, 100, true, true);

                }
            }
        }
    }
}
