using robotManager.Helpful;
using System;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;

[Serializable]
public class UseTrinketSettings : Settings
{
  [Setting]
  [Category("Settings")]
  [DisplayName("Trinket 1")]
  [Description("Enable Use Trinket 1")]
  public bool Trinket1 { get; set; }

  [Setting]
  [Category("Settings")]
  [DisplayName("Trinket 2")]
  [Description("Enable Use Trinket 2")]
  public bool Trinket2 { get; set; }

  public UseTrinketSettings()
  {
    Trinket1 = true;
    Trinket2 = false;
  }

  public static UseTrinketSettings CurrentSettings { get; set; }

  public bool Save()
  {
    try
    {
      return Save(AdviserFilePathAndName("UseTrinket", ObjectManager.Me.Name + "." + Usefuls.RealmName));
    }
    catch (Exception e)
    {
      Logging.WriteError("[UseTrinket] > Save() : " + e);
      return false;
    }
  }

  public static bool Load()
  {
    try
    {
      if (File.Exists(AdviserFilePathAndName("UseTrinket", ObjectManager.Me.Name + "." + Usefuls.RealmName)))
      {
        CurrentSettings = Load<UseTrinketSettings>(AdviserFilePathAndName("UseTrinket", ObjectManager.Me.Name + "." + Usefuls.RealmName));
        return true;
      }
      CurrentSettings = new UseTrinketSettings();
    }
    catch (Exception e)
    {
      Logging.WriteError("[UseTrinket] > Load() : " + e);
    }
    return false;
  }
}