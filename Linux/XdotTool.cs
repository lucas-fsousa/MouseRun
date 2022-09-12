using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicUtility.MouseRun.Linux {
  internal static class XdotTool {
    internal const char LEFT_CLICK = '1';
    internal const char MID_CLICK = '2';
    internal const char RIGHT_CLICK = '3';
    internal const char WHEEL_UP = '4';
    internal const char WHEEL_DOWN = '5';
    internal const string MOUSE_MOVE = "mousemove";
    internal const string REPEAT = "--repeat";
    internal const string CLICK = "click";
  }
}
