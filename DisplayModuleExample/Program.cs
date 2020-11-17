using System;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using CTRE.Gadgeteer.Module;
using CTRE.Phoenix.Controller;
using CTRE.Phoenix;

namespace DisplayModuleExample
{
    public class Program
    {
        public static void Main()
        {
            // Game Controller
            GameController gamepad = new GameController(UsbHostDevice.GetInstance());

            // NinaB Font
            Font ninaB = Properties.Resources.GetFont(Properties.Resources.FontResources.NinaB);

            // Initializing a display module: DisplayModule(port, orientation)
            DisplayModule displayModule = new DisplayModule(CTRE.HERO.IO.Port8, DisplayModule.OrientationType.Landscape);

            // Adding a label: [Display Module Name].AddLabelSprite(font, colour, x_pos, y_pos, width, height)
            DisplayModule.LabelSprite x_title = displayModule.AddLabelSprite(ninaB, DisplayModule.Color.White, 40, 0, 80, 16);
            DisplayModule.LabelSprite y_title = displayModule.AddLabelSprite(ninaB, DisplayModule.Color.White, 40, 30, 80, 16);

            while (true)
            {
                if (gamepad.GetConnectionStatus() == UsbDeviceConnection.Connected)
                {
                    // Adding rectangles: [Display Module Name].AddRectSprite(colour, x_pos, y_pos, width, height)
                    DisplayModule.RectSprite x_status = displayModule.AddRectSprite(DisplayModule.Color.White, 5, 50, 20, 55);
                    DisplayModule.RectSprite y_status = displayModule.AddRectSprite(DisplayModule.Color.White, 32, 50, 20, 55);
                   
                    while (gamepad.GetConnectionStatus() == UsbDeviceConnection.Connected)
                    {
                        // Changes the color of the rectangle depending on the x-value on the joystick
                        // [Rectangle Name].SetColor(colour)
                        if (gamepad.GetAxis(0) > 0)
                        {
                            x_status.SetColor(DisplayModule.Color.Green);
                        }
                        else if (gamepad.GetAxis(0) < 0)
                        {
                            x_status.SetColor(DisplayModule.Color.Red);
                        }
                        else
                        {
                            x_status.SetColor(DisplayModule.Color.White);
                        }
                        
                        // Changes the color of the rectangle depending on the y-value on the joystick
                        // [Rectangle Name].SetColor(colour)
                        if (gamepad.GetAxis(1) > 0)
                        {
                            y_status.SetColor(DisplayModule.Color.Green);
                        }
                        else if (gamepad.GetAxis(1) < 0)
                        {
                            y_status.SetColor(DisplayModule.Color.Red);
                        }
                        else
                        {
                            y_status.SetColor(DisplayModule.Color.White);
                        }

                        // Sets the text that the label displays: [Label Name].SetText(text: string)
                        x_title.SetText(gamepad.GetAxis(0).ToString());
                        y_title.SetText(gamepad.GetAxis(1).ToString());
                    }
                }
                else
                {
                    // Clears and erases everything on the display
                    displayModule.Clear();
                }
                System.Threading.Thread.Sleep(100);
            }
        }
    }
}
