using System;
using GTA;
using GTA.UI;

namespace gasgun
{
    public class Config
    {
        private string filename;

        #region Parameters
        public WeaponHash weapon { get; set; }
        public uint bullet { get; set; }
        public float bulletspeed { get; set; }
        public float bulletforce { get; set; }
        public Boolean giveweapon { get; set; }
        #endregion

        public Config(string filename)
        {
            this.filename = filename;
            #region Default Config Values
                this.bulletspeed = 1f;
                this.bulletforce = 45f;
                this.weapon = WeaponHash.GrenadeLauncher;
                this.giveweapon = true;
                this.bullet = 2694266206; // Bz Gas uint
            #endregion
            this.Load();
        }

        private void Load()
        {
            try
            {
                ScriptSettings config = ScriptSettings.Load(filename);

                #region Set Config Values
                    weapon = (WeaponHash)System.Enum.Parse(typeof(WeaponHash), config.GetValue<String>("Configuration", "WeaponName", "GrenadeLauncher"));
                    bulletspeed *= config.GetValue<float>("Configuration", "BulletSpeedMultiplier", 1);
                    bulletforce *= config.GetValue<float>("Configuration", "BulletForceMultiplier", 1);
                    giveweapon = config.GetValue<Boolean>("Configuration", "GiveWeaponOnEnable", true);
                    bullet = config.GetValue<int>("Configuration", "BulletType", 0) != 0 ? 4256991824 : 2694266206;
                #endregion
            }
            catch (Exception)
            {
                Notification.Show(string.Format("Error while loading {0}", filename));
                throw;
            }
        }
    }
}
