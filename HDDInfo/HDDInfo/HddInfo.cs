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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "HddInfo" in both code and config file together.
    public class HddInfo : IHddInfo
    {
        public List<PhisicalDrive> GetHddInfo()
        {
            List<PhisicalDrive> PDList = new List<PhisicalDrive>();

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
                                    ld.TotalFreeSpace = cLD.TotalFreeSpace.ToString();
                                    ld.CalcFreePercs(cLD.TotalSize.ToString(), cLD.TotalFreeSpace.ToString());
                                    Console.WriteLine(string.Format("Partition: {0} - {1}\n{2}\n{3}\n{4}\nTotal size: {5}\nTotal free space: {6}\nFreespace in %: {7}\n", wmiDiskPartition["DeviceID"], ld.DeviceID, ld.Description, ld.DriveFormat, ld.DriveType, ld.TotalSize, ld.TotalFreeSpace, ld.PercentFree));
//                                    pd.LogicalDrives.Add(ld);
                                }
                            }
                        }
                    }
                    PDList.Add(pd);
                    Console.WriteLine("=============================================");
                }
            Console.ReadLine();

            return PDList;
        }

        public string Test()
        {
            return "test_value";
        }

        public List<MyObject> TestMyObject()
        {
            //var res = new List<MyObject>();
            //res.Add(new MyObject("str111", 111,1.1f));
            //res.Add(new MyObject("str222", 222,2.2f));
            //res.Add(new MyObject("str333", 333,3.3f));
            //res.Add(new MyObject("str444", 444,4.4f));
            //return res;

            var res = new List<MyObject>();
            var query = "SELECT Caption, DeviceID, SerialNumber, Size, Status FROM Win32_DiskDrive";
            var s1 = new ManagementObjectSearcher(query);
            if (s1 != null)
                foreach (var wmi_HD in s1.Get())
                {
                    var pd = new MyObject();
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
                                var ld = new MyObject2();
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
                                    Console.WriteLine(string.Format("Partition: {0} - {1}\n{2}\n{3}\n{4}\nTotal size: {5}\nTotal free space: {6}\nFreespace in %: {7}\n", 
                                        wmiDiskPartition["DeviceID"], ld.DeviceID, ld.Description, ld.DriveFormat, ld.DriveType, ld.TotalSize, ld.FreeSpaceTotal, ld.FreeSpacePercents));
                                    pd.LogicalDrives.Add(ld);
                                }
                            }
                        }
                    }
                    res.Add(pd);
                    Console.WriteLine("=============================================");
                }
            return res;
        }
    }
}
