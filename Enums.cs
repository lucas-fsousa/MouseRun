using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseRun {
  public static class Enums {
    public enum MouseSpeed {
      X1 = 1,
      X2 = 2,
      X3 = 3,
      X4 = 4,
      Full = 5
    }

    [Flags]
    public enum MouseAction {
      Wheel = 0x0800,
      HWhell = 0x01000,
      LeftDown = 0x02,
      LeftUp = 0x04,
      RightDown = 0x08,
      RightUp = 0x10,
      Absolute = 0x8000,
      Move = 0x0001
    }
  }
}
