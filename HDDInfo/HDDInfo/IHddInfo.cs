using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace HDDInfo
{
    [ServiceContract]
    public interface IHddInfo
    {
        [OperationContract]
        List<PhisicalDrive> GetHddInfo();
    }

    

    [DataContract]
    public class PhisicalDrive
    {

        private string _caption;
        private string _deviceID;
        private string _size;
        private string _status;
        private List<string> _logicalDrives;

        public PhisicalDrive()
        {
            _logicalDrives = new List<string>();
        }

        [DataMember]
        public string Caption
        {
            get { return _caption; }
            set { _caption = value; }
        }

        [DataMember]
        public string DeviceID
        {
            get { return _deviceID; }
            set { _deviceID = value; }
        }

        [DataMember]
        public string Size
        {
            get { return _size; }
            set { _size = value; }
        }

        [DataMember]
        public string Status
        {
            get { return _status; }
            set { _status = value; }
        }

        [DataMember]
        public List<string> LogicalDrives
        {
            get { return _logicalDrives; }
        }

        public void AddLogicalDrive(string ld)
        {
            if (!LogicalDrives.Contains(ld))
                LogicalDrives.Add(ld);
        }
    }

    //[DataContract]
    public class LogicalDrive
    {
        private string _deviceID;
        private string _name;
        private string _valumeLabel;
        private string _description;
        private string _driveFormat;
        private string _driveType;
        private string _freeSpaceTotal;
        private string _freeSpacePercents;
        private string _totalSize;


        //[DataMember]
        public string DeviceID
        {
            get { return _deviceID; }
            set { _deviceID = value; }
        }

        //[DataMember]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        //[DataMember]
        public string ValumeLabel
        {
            get { return _valumeLabel; }
            set { _valumeLabel = value; }
        }

        //[DataMember]
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        //[DataMember]
        public string DriveFormat
        {
            get { return _driveFormat; }
            set { _driveFormat = value; }
        }

        //[DataMember]
        public string DriveType
        {
            get { return _driveType; }
            set { _driveType = value; }
        }

        //[DataMember]
        public string FreeSpaceTotal
        {
            get { return _freeSpaceTotal; }
            set { _freeSpaceTotal = value; }
        }

        //[DataMember]
        public string TotalSize
        {
            get { return _totalSize; }
            set { _totalSize = value; }
        }

        //[DataMember]
        public string FreeSpacePercents
        {
            get { return _freeSpacePercents; }
        }

        public void CalcFreePercs(string totalSpace, string totalFreeSpace)
        {
            try
            {
                double ts = Int64.Parse(totalSpace);
                double tfs = Int64.Parse(totalFreeSpace);
                _freeSpacePercents = string.Format("{0}%", Math.Round(tfs * 100 / ts));
            }
            catch (Exception e)
            {
                _freeSpacePercents = e.Message;
            }
        }
    }
}
