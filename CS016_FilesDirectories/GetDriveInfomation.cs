using System;
using System.IO;
namespace CS016_FilesDirectories {
    public class GetDriveInfomation {
        /// <summary>
        /// In các thông tin ổ đĩa trong máy
        /// </summary>
        public static void GetDrivesInfo () {
            DriveInfo[] allDrives = DriveInfo.GetDrives ();

            foreach (DriveInfo d in allDrives) {
                Console.WriteLine ("Drive {0}", d.Name);
                Console.WriteLine ("  Drive type: {0}", d.DriveType);
                if (d.IsReady == true) {
                    Console.WriteLine ("  Volume label: {0}", d.VolumeLabel);
                    Console.WriteLine ("  File system: {0}", d.DriveFormat);
                    Console.WriteLine ("  Available space to current user:{0, 15} bytes", d.AvailableFreeSpace);
                    Console.WriteLine ("  Total available space:          {0, 15} bytes", d.TotalFreeSpace);
                    Console.WriteLine ("  Total size of drive:            {0, 15} bytes ", d.TotalSize);
                }
            }
        }
    }
}