using PublicUtility.Nms.Enums;
using PublicUtility.Nms.Structs;
using System.Runtime.InteropServices;

namespace PublicUtility.MouseRun.Windows {

  public static class WinMouseHandle {

    #region INTEROPT DLL IMPORTS

    [DllImport("user32.dll")]
    private static extern bool GetCursorPos(out PointIntoScreen lpPoint);

    [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
    private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint data, uint dwExtraInfo);

    [DllImport("User32.Dll")]
    private static extern long SetCursorPos(int x, int y);

    [DllImport("User32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
    private static extern int GetSystemMetrics(int nIndex);

    #endregion

    #region PRIVATE METHODS
    
    private static bool CheckCorner(PointIntoScreen point) {
      var size = new ScreenSize(GetSystemMetrics(0), GetSystemMetrics(1));
      bool response = false;

      if(point.X < 0)
        return response;

      else if(point.Y < 0)
        return response;

      else if(point.X > size.Width)
        return response;

      else if(point.Y > size.Height)
        return response;

      else
        response = true;

      return response;
    }

    private static void MouseMoveControl(PointIntoScreen start, PointIntoScreen end, MouseSpeed speed) {
      bool startOk = CheckCorner(start);
      bool endOk = CheckCorner(end);

      if(!startOk)
        throw new Exception($"the {nameof(start)} it's out of screen limits");

      if(!endOk)
        throw new Exception($"the {nameof(end)} it's out of screen limits");

      int x = start.X, y = start.Y, cc = 0;
      bool endx = false, endy = false;

      // Sets instantaneous movement to the end position of the cursor.
      if(speed == MouseSpeed.Full) {
        SetCursorPos(end.X, end.Y);
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

        SetCursorPos(x, y);
        cc++;
      }
    }

    #endregion

    public static void RollDown(uint clicks) {
      if(clicks < 0)
        throw new Exception($"{nameof(clicks)} cannot be less than zero.");

      mouse_event((uint)MouseAction.Absolute | (uint)MouseAction.Wheel, 0, 0, (uint)-clicks, 0);
    }

    public static void RollUp(uint clicks) {
      if(clicks < 0)
        throw new Exception($"{nameof(clicks)} cannot be less than zero.");

      mouse_event((uint)MouseAction.Wheel, 0, 0, clicks, 0);
    }

    public static PointIntoScreen GetPosition() {
      try {
        GetCursorPos(out PointIntoScreen current);
        return current;
      } catch(Exception) { return new(0, 0); }

    }

    public static void Drag(PointIntoScreen start, PointIntoScreen end, MouseSpeed speed = MouseSpeed.X1) {
      MoveTo(start, speed);
      Thread.Sleep(300);
      mouse_event((uint)MouseAction.LeftDown, 0, 0, 0, 0);
      MoveTo(end, speed);
      Thread.Sleep(300);
      mouse_event((uint)MouseAction.LeftUp, 0, 0, 0, 0);
    }

    public static void LeftClick(bool doubleClick = false) {
      if(doubleClick) {
        mouse_event((uint)MouseAction.LeftDown | (uint)MouseAction.LeftUp, 0, 0, 0, 0);
        Thread.Sleep(100);
        mouse_event((uint)MouseAction.LeftDown | (uint)MouseAction.LeftUp, 0, 0, 0, 0);
        return;
      }
      mouse_event((uint)MouseAction.LeftDown | (uint)MouseAction.LeftUp, 0, 0, 0, 0);

    }

    public static void RightClick(bool doubleClick = false) {
      if(doubleClick) {
        mouse_event((uint)MouseAction.RightDown | (uint)MouseAction.RightUp, 0, 0, 0, 0);
        Thread.Sleep(100);
        mouse_event((uint)MouseAction.RightDown | (uint)MouseAction.RightUp, 0, 0, 0, 0);
        return;
      }
      mouse_event((uint)MouseAction.RightDown | (uint)MouseAction.RightUp, 0, 0, 0, 0);

    }

    public static void MoveToAndClick(PointIntoScreen point, MouseSpeed speed = MouseSpeed.X2, bool doubleClick = false, bool leftbtn = true) {
      MoveTo(point, speed);
      if(leftbtn)
        LeftClick(doubleClick);
      else
        RightClick(doubleClick);

    }

    public static void MoveTo(PointIntoScreen point, MouseSpeed speed = MouseSpeed.X1) => MouseMoveControl(GetPosition(), point, speed);
    
  
  }

}
