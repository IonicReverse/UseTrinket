using robotManager.Helpful;
using wManager.Wow.Enums;
using wManager.Wow.Helpers;

namespace UseTrinket
{
  internal class Helpers
  {
    public static void Log(string message)
    {
      Logging.Write("[UseTrinket] " + message, Logging.LogType.Normal, System.Drawing.Color.AliceBlue);
    }

    public static int GetInventoryCooldown(WoWInventorySlot slotId)
    {
      return Lua.LuaDoString<int>(
        @"
          local start, duration, enable = GetInventoryItemCooldown(""player"", " + (int)slotId + @")
          local coolDown = duration-(GetTime()-start);
          if (coolDown < 0) then 
              return 0;
          end
          return coolDown;
        ");
    }

    public static void UseTrinket()
    {
      if (UseTrinketSettings.CurrentSettings.Trinket1 && GetInventoryCooldown(WoWInventorySlot.Trinket1) == 0)
        Lua.RunMacroText("/use 13");

      if (UseTrinketSettings.CurrentSettings.Trinket2 && GetInventoryCooldown(WoWInventorySlot.Trinket2) == 0)
        Lua.RunMacroText("/use 14");
    }
  }
}
