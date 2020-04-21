using GTA;
using GTA.Native;
using GTA.UI;
using NativeUI;
using System.Drawing;

namespace gasgun
{
    public class Menu
    {
        public MenuPool MenuPool { get; set; }
        public UIMenu MainMenu { get; set; }
        public UIMenuItem toggleMod { get; set; }

        private GasGun parent;

        public UIMenu bulletType { get; set; }

        public Menu(GasGun parent)
        {
            this.parent = parent;
            MenuPool = new MenuPool();
            MainMenu = new UIMenu("GasGun Menu", "Please, select an option");
            MenuPool.Add(MainMenu);

            toggleMod = new UIMenuItem("Toggle GasGun");
            MainMenu.AddItem(toggleMod);
            setupMainMenu();

            bulletType = MenuPool.AddSubMenu(MainMenu, "Change bullet type");
            setupBulletType();
        }
        private void setupBulletType()
        {
            UIMenuItem bzgas = new UIMenuItem("BZ Gas");
            bulletType.AddItem(bzgas);
            UIMenuItem teargas = new UIMenuItem("Teargas");
            bulletType.AddItem(teargas);

            bulletType.OnItemSelect += (sender, item, index) =>
            {
                if(item == bzgas)
                {
                    parent.config.bullet = 2694266206;
                } else if(item == teargas)
                {
                    parent.config.bullet = 4256991824;
                }
            };
        }
        private void setupMainMenu()
        {
            MainMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == toggleMod)
                {
                    parent.Enabled = !parent.Enabled;
                    string message = parent.Enabled ? "Enabled :)" : "Disabled";
                    Notification.Hide(0);
                    Notification.Show("Gasgun " + message);

                    Notification.Show(NotificationIcon.Multiplayer, "Rafael Faustini", "GasGun", message, true);
                    if (parent.Enabled)
                    {
                        Game.Player.Character.Weapons.Give(WeaponHash.BZGas, 100, true, true);
                        Game.Player.Character.Weapons.Give(WeaponHash.GrenadeLauncher, 100, true, true);

                        Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player, parent.config.bullet, 100, true, false);
                        if (parent.config.giveweapon)
                        {
                            Game.Player.Character.Weapons.Give(parent.config.weapon, 50, true, true);
                        }
                    }
                }

            };
            }

        public void toggle()
        {
            MainMenu.Visible = !MainMenu.Visible;
        }

        public void process()
        {
            if(MenuPool != null && MenuPool.IsAnyMenuOpen())
            {
                MenuPool.ProcessMenus();
            }
        }
    }


}
