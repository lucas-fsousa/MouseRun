﻿using System.Drawing;
using System.Runtime.InteropServices;
using static MouseRun.Enums;

namespace MouseRun {

  public static class Mouse {

    #region INTEROPT DLL IMPORTS

    [DllImport("user32.dll")]
    private static extern bool GetCursorPos(out Point lpPoint);

    [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
    private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint data, uint dwExtraInfo);

    [DllImport("User32.Dll")]
    private static extern long SetCursorPos(int x, int y);

    [DllImport("User32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
    private static extern int GetSystemMetrics(int nIndex);

    #endregion

    #region PRIVATE METHODS
    private static bool CheckCorner(Point point) {
      Size size = new(GetSystemMetrics(0), GetSystemMetrics(1));
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

    private static void MouseMoveControl(Point start, Point end, MouseSpeed speed) {
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

    public static Point GetPosition() {
      Point current = new();
      try {
        GetCursorPos(out current);
      } catch(Exception) { }
      return current;
    }

    public static void Drag(Point start, Point end, MouseSpeed speed = MouseSpeed.X1) {
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

    #region OVERLOAD MOVETOANDCLICK

    public static void MoveToAndClick(int x, int y, MouseSpeed speed = MouseSpeed.X2, bool doubleClick = false, bool leftbtn = true) {
      MoveTo(x, y, speed);
      if(leftbtn)
        LeftClick(doubleClick);
      else
        RightClick(doubleClick);

    }

    public static void MoveToAndClick(this Point point, MouseSpeed speed = MouseSpeed.X2, bool doubleClick = false, bool leftbtn = true) {
      MoveTo(point, speed);
      if(leftbtn)
        LeftClick(doubleClick);
      else
        RightClick(doubleClick);

    }

    #endregion

    #region OVERLOAD MOVETO

    public static void MoveTo(this Point point, MouseSpeed speed = MouseSpeed.X1) {
      Point start = GetPosition();
      MouseMoveControl(start, point, speed);
    }

    public static void MoveTo(int x, int y, MouseSpeed speed = MouseSpeed.X1) {
      Point start = GetPosition();
      MouseMoveControl(start, new(x, y), speed);
    }

    #endregion
  }
}
