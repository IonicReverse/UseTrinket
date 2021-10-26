using System;
using System.ComponentModel;
using System.Threading;
using UseTrinket;
using wManager.Plugin;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;

public class Main : IPlugin
{
  public bool _isLaunched;
  private BackgroundWorker pulseThread;

  public void Start()
  {
    pulseThread = new BackgroundWorker();
    pulseThread.DoWork += Pulse;
    pulseThread.RunWorkerAsync();
  }

  public void Pulse(object sender, DoWorkEventArgs args)
  {
    try
    {
      while (_isLaunched)
      {
        if (Conditions.InGameAndConnectedAndAliveAndProductStarted 
          && ObjectManager.Me.IsAlive 
          && ObjectManager.Me.InCombat)
        {
          Helpers.UseTrinket();
          Thread.Sleep(1000);
        }
      }
    }
    catch (Exception ex)
    {
      Helpers.Log("Something wrong (Pulse) " + ex);
    }
  }

  public void Dispose()
  {
    _isLaunched = false;
    Helpers.Log("Stopped");
  }

  public void Initialize()
  {
    try
    {
      _isLaunched = true;
      UseTrinketSettings.Load();
      Start();
    }
    catch (Exception ex)
    {
      Helpers.Log("Something wrong (Initialize) : " + ex);
    }
  }

  public void Settings()
  {
    UseTrinketSettings.Load();
    UseTrinketSettings.CurrentSettings.ToForm();
    UseTrinketSettings.CurrentSettings.Save();
  }
}

