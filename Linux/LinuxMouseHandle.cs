using PublicUtility.Nms.Enums;
using PublicUtility.Nms.Structs;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;

namespace PublicUtility.MouseRun.Linux {
  internal static class LinuxMouseHandle {
    private const string _requiredInstallMessageError = "Need to install xdotool. \n For Ubuntu, Debian or Linux Mint, you can just do: \"sudo apt-get install xdotool\". \n For Fedora: \"sudo yum install xdotool\". \n For other items try using: \"sudo pacman -S xdotool\". \n [ Read more infos in https://github.com/jordansissel/xdotool ]";

    #region PRIVATE
    private static void MouseMoveControl(PointIntoScreen start, PointIntoScreen end, MouseSpeed speed) {
      int x = start.X, y = start.Y, cc = 0;
      bool endx = false, endy = false;

      // Sets instantaneous movement to the end position of the cursor.
      if(speed == MouseSpeed.Full) {
        SetCursor(end.X, end.Y);
        return;
      }

      while(!endx || !endy) {

        /*Uses the Speed Variable value as an increment/decrement that changes the velocity at which drag occurs
         controls X to change the value of the looping stop variable
         */
        if(start.X > end.X) {
          if(x >= end.X)
            x -= (int)speed;
          else
            endx = true;

        } else {
          if(x < end.X)
            x += (int)speed;
          else
            endx = true;

        }

        /*Uses the Speed Variable value as an increment/decrement that changes the velocity at which drag occurs
         controls Y to change the value of the looping stop variable
         */
        if(start.Y > end.Y) {
          if(y >= end.Y)
            y -= (int)speed;
          else
            endy = true;

        } else {
          if(y < end.Y)
            y += (int)speed;
          else
            endy = true;

        }

        // Adds a delay so that the drag is not too fast and ends up crashing the application or not working properly
        if(cc % 4 == 0)
          Thread.Sleep(10);

        SetCursor(x, y);
        cc++;
      }
    }

    private static void SetCursor(int x, int y) => InvokeXdotool($"mousemove {x} {y}");

    private static string InvokeXdotool(string commands) {
      try {
        var proc = new Process {
          StartInfo = {
          FileName = "xdotool",
          Arguments = commands,
          UseShellExecute = false,
          RedirectStandardError = false,
          RedirectStandardInput = false,
          RedirectStandardOutput = true
          }
        };
        proc.Start();
        return proc.StandardOutput.ReadToEnd();
      } catch(Win32Exception) {
        throw new Exception(_requiredInstallMessageError);

      } catch(Exception ex) {
        throw new Exception(ex.Message, ex);

      }

    }
    #endregion

    public static PointIntoScreen GetPosition() {
      try {
        var obj = InvokeXdotool("getmouselocation").Split(' ');
        var x = Convert.ToInt32(obj[0].Replace("x:", ""));
        var y = Convert.ToInt32(obj[1].Replace("y:", ""));
        return new(x, y);
      } catch { return new(0, 0); }

    }

    public static void LeftClick(bool doubleClick = false) {
      var command = new StringBuilder();
      command.Append("click "); // command to execut
      command.Append(doubleClick ? "--repeat 2 " : ""); // set a double click
      command.Append('1'); // mouse left button
      InvokeXdotool(command.ToString());
    }

    public static void RightClick(bool doubleClick = false) {
      var command = new StringBuilder();
      command.Append("click "); // command to execut
      command.Append(doubleClick ? "--repeat 2 " : ""); // set a double click
      command.Append('3'); // mouse right button
      InvokeXdotool(command.ToString());
    }

    public static void MoveTo(PointIntoScreen point, MouseSpeed speed = MouseSpeed.X1) => MouseMoveControl(GetPosition(), point, speed);

    public static void MoveToAndClick(PointIntoScreen point, MouseSpeed speed = MouseSpeed.X2, bool doubleClick = false, bool leftbtn = true) {
      MoveTo(point, speed);
      if(leftbtn)
        LeftClick(doubleClick);
      else
        RightClick(doubleClick);

    }

    public static void RollDown(uint clicks) {
      if(clicks <= 0)
        throw new Exception($"{nameof(clicks)} cannot be less than zero.");

      var command = new StringBuilder();
      command.Append("click "); // command to execut
      command.Append($"--repeat {clicks} "); // set a clicks
      command.Append('5'); // wheel down button
      InvokeXdotool(command.ToString());
    }

    public static void RollUp(uint clicks) {
      if(clicks <= 0)
        throw new Exception($"{nameof(clicks)} cannot be less than zero.");

      var command = new StringBuilder();
      command.Append("click "); // command to execut
      command.Append($"--repeat {clicks} "); // set a clicks
      command.Append('4'); // wheel up button
      InvokeXdotool(command.ToString());
    }

    public static void Drag(PointIntoScreen start, PointIntoScreen end, MouseSpeed speed = MouseSpeed.X1) {
      MoveTo(start, speed);
      Thread.Sleep(300);
      InvokeXdotool("mousedown 1");
      MoveTo(end, speed);
      Thread.Sleep(300);
      InvokeXdotool("mouseup 1");
    }

  }
}