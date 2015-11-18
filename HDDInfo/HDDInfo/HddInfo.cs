using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Management;
using System.IO;

namespace HDDInfo
{
    public class HddInfo : IHddInfo
    {
        public List<PhisicalDrive> GetHddInfo()
        {
            var res = new List<PhisicalDrive>();
            if(ServiceSecurityContext.Current.PrimaryIdentity.IsAuthenticated)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Loged in as " + System.Threading.Thread.CurrentPrincipal.Identity.Name);
                Console.ResetColor();
                var query = "SELECT Caption, DeviceID, SerialNumber, Size, Status FROM Win32_DiskDrive";
                var s1 = new ManagementObjectSearcher(query);
                if (s1 != null)
                    foreach (var wmi_HD in s1.Get())
                    {
                        var pd = new PhisicalDrive();
                        pd.Caption = wmi_HD["Caption"].ToString();
                        pd.DeviceID = wmi_HD["DeviceID"].ToString();
                        pd.Status = wmi_HD["Status"].ToString();
                        pd.Size = wmi_HD["Size"].ToString();
                        Console.WriteLine(string.Format("Caption: {0}\r\nDeviceID: {1}\r\nStatus: {2}\r\nSize: {3}\r\n", pd.Caption, pd.DeviceID, pd.Status, pd.Size));
                    
                        query = "ASSOCIATORS OF {Win32_DiskDrive.DeviceID='" + pd.DeviceID + "'} WHERE AssocClass = Win32_DiskDriveToDiskPartition";
                        var s2 = new ManagementObjectSearcher(query);
                        if (s2 != null)
                        {
                            foreach (var wmiDiskPartition in s2.Get())
                            {
                                query = "ASSOCIATORS OF {Win32_DiskPartition.DeviceID='" + wmiDiskPartition["DeviceID"] + "'} WHERE AssocClass = Win32_LogicalDiskToPartition";
                                var s3 = new ManagementObjectSearcher(query);
                                if (s3 != null)
                                {
                                    var ld = new LogicalDrive();
                                    foreach (var wmi_LD in s3.Get())
                                    {
                                        var cLD = new DriveInfo(wmi_LD["DeviceID"].ToString());
                                        ld.DeviceID = wmi_LD["DeviceID"].ToString();
                                        ld.Description = wmi_LD["Description"].ToString();
                                        ld.DriveFormat = cLD.DriveFormat.ToString();
                                        ld.DriveType = cLD.DriveType.ToString();
                                        ld.Name = cLD.Name.ToString();
                                        ld.ValumeLabel = cLD.VolumeLabel.ToString();
                                        ld.TotalSize = cLD.TotalSize.ToString();
                                        ld.FreeSpaceTotal = cLD.TotalFreeSpace.ToString();
                                        ld.CalcFreePercs(cLD.TotalSize.ToString(), cLD.TotalFreeSpace.ToString());
                                        var logDiskInfo = string.Format("Partition: {0} - {1} | {2} | {3} | Total size: {4} | Total free space: {5} | Free space in %: {6}",
                                            wmiDiskPartition["DeviceID"], ld.DeviceID, ld.Description, ld.DriveFormat, ld.TotalSize, ld.FreeSpaceTotal, ld.FreeSpacePercents);
                                        Console.WriteLine(logDiskInfo);
                                        pd.LogicalDrives.Add(logDiskInfo);
                                    }
                                }
                            }
                        }
                    
                        res.Add(pd);
                        Console.WriteLine("=============================================");
                    }
        }
            return res;
        }
    }
}