using Gma.System.MouseKeyHook;
using LedCSharp;
using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace LogitechLEDChanger
{
    class LogitechLEDChanger
    {
        //Beenden
        private static volatile bool cancelRequested = false;

        //Keylogger
        [DllImport("User32.dll")]
        
        public static extern int GetAsyncKeyState(Int32 i);
        static string kelLog = "";

        //Maus

        static public Random colorgenerator = new Random();
        public static void Farbeaendern()
        {
            
            int red, green, blue;
            red = colorgenerator.Next(0, 100);
            green = colorgenerator.Next(0, 100);
            blue = colorgenerator.Next(0, 100);
            if ( red == 0 && blue == 0 && green == 0)
            {
                red = colorgenerator.Next(0, 100);
                green = colorgenerator.Next(0, 100);
                blue = colorgenerator.Next(0, 100);
            }
            else
            {
                LogitechGSDK.LogiLedSetTargetDevice(LogitechGSDK.LOGI_DEVICETYPE_RGB);
                LogitechGSDK.LogiLedSetLighting(red, green, blue);
            }          
        }

        static void Main(string[] args)
        {
            LogitechGSDK.LogiLedInit();        
            int counter = 0;
            int counteroverall = 0;
            char before, after;
            before = ' ';
            after = ' ';
            while (true)
            {
                Thread.Sleep(5);
                for (int i = 0; i < 127; i++)
                {
                    int keyState = GetAsyncKeyState(i);                    
                    if (keyState == 32769)
                    {                       
                        Console.Write((char)i + ", ");
                        after = (char)i;
                        counter++;
                        counteroverall++;
                        if (counter == 1 && after != before)
                        {
                            Farbeaendern();
                            counter = 0;
                            //Console.Clear();
                            before = after;
                        }
                        else
                        {
                            Console.WriteLine("1");
                            counter = 0;
                            counteroverall++;
                        }
                        
                    }
                    if (counteroverall>100)
                    {
                        Console.Clear();
                        counteroverall = 0;                       
                    }
                    
                }
            }



        }

        //public static void ListenForMouseEvents()
        //{
        //    Console.WriteLine("Listening to mouse clicks.");

        //    //When a mouse button is pressed 
        //    Hook.GlobalEvents().MouseDown += async (sender, e) =>
        //    {
        //        Console.WriteLine($"Mouse {e.Button} Down");
        //    };
        //    //When a double click is made
        //    Hook.GlobalEvents().MouseDoubleClick += async (sender, e) =>
        //    {
        //        Console.WriteLine($"Mouse {e.Button} button double clicked.");
        //    };
        //}

        static void ExitConsole(object sender, ConsoleCancelEventArgs e)
        {
            Console.WriteLine("Beenden");
            e.Cancel = true;
            cancelRequested = true;
            LogitechGSDK.LogiLedShutdown(); 
        }


    }
}
