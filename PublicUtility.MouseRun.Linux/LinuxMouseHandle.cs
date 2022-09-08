using System.Security.Cryptography;
using System.Diagnostics;
using PublicUtility.Nms;
using PublicUtility.Nms.Enums;

namespace PublicUtility.MouseRun.Linux {
  public class LinuxMouseHandle {
    private static void MouseEvent(MouseAction action) {
      

    }

    private static void InvokeXdotool(string commands) {
      var proc = new Process {
        StartInfo = {
          FileName = "xdotool",
          Arguments = commands,
          UseShellExecute = false,
          RedirectStandardError = false,
          RedirectStandardInput = false,
          RedirectStandardOutput = false
        }
      };
      proc.Start();
    }

    public static void Ask() {
      var args = "";
      switch(output) {
        case "{RIGHT}":
          args = "key Right";
          break;
        case "{LEFT}":
          args = "key Left";
          break;
        default:
          if(output.StartsWith("{") && output.EndsWith("}"))
            output = output.Substring(1, output.Length - 2);

          args = "type \"" + output + "\"";
          break;
      }


    }

  }
}