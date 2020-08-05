using System;
using System.Collections.Generic;

namespace neatproject.Config
{
  public class AppSettings
  {
    public string ServiceName { get; set; }
    public ConnectionStrings ConnectionStrings { get; set; }
  }

  public class ConnectionStrings
  {
    public string AppConfig { get; set; }
  }
}
