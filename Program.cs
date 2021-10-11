using MarsStation.Model;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace MarsStation
{
    class Program
    {
        static StringBuilder sb;
        /// <summary>
        /// uydu ile  heberlesilecek port
        /// </summary>
        private const int listenPort = 11001;
        public static  IList<Shape> shapeList;
        public static Shape selectedShape;
        public static Robot selectedRobot;

        class StationAwake
        {
            bool sleepStation = false;
            public bool SleepSwitch
            {
                set { sleepStation = value; }
            }
            public StationAwake() { }
            public void ThreadMethod()
            {
                // string  builder FIFO mantıgında çalıştırılıyor
                while (!sleepStation)
                {
                    do
                    {
                        if (IsShape(ref sb, ref selectedShape))
                        {
                            ///Listeden shape işlmeleri  çalıştırıldı :)
                        }
                        else if (IsPoint(ref sb, ref selectedShape, ref selectedRobot))
                        {
                            ///Listeden point işlemleri çalıştırıldı :)
                        }
                        else
                            //Listeden komut çalıştırıldı.
                            IsCommand(ref sb, ref selectedRobot);

                    } while (sb.ToString().Length > 0  && !sleepStation);
                    Thread.SpinWait(10000000);
                }
                try
                {
                    if (selectedRobot != null && selectedShape != null)
                    {
                        selectedShape.AddRobot(selectedRobot);
                        shapeList.Add(selectedShape);
                    }
                    //Son işlem yapilan  objeler listeye eklendi ve output yazdırıliyor.
                    Console.WriteLine("Output:");
                    foreach (var obj in shapeList)
                    {
                        foreach (var rb in obj.RobotList)
                            Console.WriteLine(rb.GetInfo());
                    }
                    Console.ReadLine();
                    Thread.Sleep(Timeout.Infinite);
                }
                catch (ThreadInterruptedException)
                {
                    Console.WriteLine("Thread interrupt  yapamadı.");
                }
            }
        }
        static void Main(string[] args)
        {
            #region ThreadStep
            sb = new StringBuilder();
            shapeList = new List<Shape>();
            StationAwake stationAwake = new StationAwake();   
            Thread newThread =
                new Thread(new ThreadStart(stationAwake.ThreadMethod));
            #endregion

            #region CominicationStep
            byte[] bytes;
            UdpClient listener = new UdpClient(listenPort);
            IPEndPoint sataliteEP = new IPEndPoint(IPAddress.Any, listenPort);
            bool listenStatu = true;
            #endregion

            try
            {
                Console.WriteLine("Data get from MarsSatelite");
                Console.WriteLine("input:");
                newThread.Start();
                while (listenStatu)
                {
                    bytes = listener.Receive(ref sataliteEP);
                    Console.WriteLine($" {Encoding.ASCII.GetString(bytes, 0, bytes.Length)}");

                    foreach (char c in Encoding.ASCII.GetString(bytes, 0, bytes.Length))
                        if (char.IsNumber(c) || Regex.IsMatch(c.ToString().ToUpper(), "[MLRWSEN]") || c.Equals('-') || c.Equals(' ') || c.Equals('X'))
                            sb.Append(c.ToString().ToUpper());
                    if (Regex.IsMatch(sb.ToString(sb.Length - 1, 1), "[0-9]") ||
                        Regex.IsMatch(sb.ToString(sb.Length - 1, 1), "[WSEN]"))
                    {
                        sb.Append(" ");
                    }
                       
                    //Uyddan X gelmesi input bittiğini gösteriyor.
                    if (sb.ToString().Contains("X")){
                        listenStatu = false;
                        stationAwake.SleepSwitch = true;
                    }
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                listener.Close();
                newThread.Join();
             }
        }
        private static void IsCommand(ref StringBuilder sb, ref Robot selectedRobot)
        {
            if (sb.ToString().Length >= 1 && (Regex.IsMatch(sb.ToString(0, 1),"[LRM]")||sb.ToString(0,1).Equals(" ")))
            {
                selectedRobot.DoSomeThings(sb.ToString(0, 1));
                sb.Remove(0, 1);
            }
        }
        private static bool IsPoint(ref StringBuilder sb, ref Shape selectedShape, ref Robot selectedRobot)
        {
            string[] s = sb.ToString().Split(' ');
            if (s.Length < 3)
                return false;
            if (int.TryParse(s[0], out int x) & int.TryParse(s[1], out int y) & 
                (s[2].Equals("W")| s[2].Equals("E")| s[2].Equals("N")| s[2].Equals("S")))
            {
                switch (selectedRobot)
                {
                    case null:
                        break;
                    default:
                        selectedShape.AddRobot(selectedRobot);
                        break;
                }
                selectedRobot = new Robot(x, y, s[2]);
                sb.Remove(0, s[0].Length + s[1].Length + s[2].Length + 3);
                return true;
            }
            else
                return false;
        }
        private static bool IsShape(ref StringBuilder sb, ref Shape selectedShape)
        {
            string[] s = sb.ToString().Split(' ');
            if (s.Length< 3)
                return false;

            if (int.TryParse(s[0], out int height) && int.TryParse(s[1], out int weight)  & int.TryParse(s[2], out int Robotx))
                if (height>0 & weight>0)
            {
                switch (selectedShape)
                {
                    case null:
                        break;
                    default:
                        shapeList.Add(selectedShape);
                        break;
                }
                selectedShape = new Shape(height,weight);
                sb.Remove(0, s[0].Length + s[1].Length + 2);
                return true;
            }
            else
                return false;
            else
                return false; ;
        }
    }
}


