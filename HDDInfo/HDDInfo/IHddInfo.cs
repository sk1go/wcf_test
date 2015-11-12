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
        //[WebGet(UriTemplate = "GetHDDsInfo", ResponseFormat=WebMessageFormat.Xml )]
        [OperationContract]
        List<PhisicalDrive> GetHddInfo();

        //[WebGet(UriTemplate = "GetTest", ResponseFormat = WebMessageFormat.Xml)]
        [OperationContract]
        string Test();

        [OperationContract]
        List<MyObject> TestMyObject();
    }

    [DataContract]
    public class LogicalDrive
    {
        private string _name;
        private string _valumeLabel;
        private string _description;
        private string _driveFormat;
        private string _driveType;
        private string _totalFreeSpace;
        private string _totalSize;
        private string _percentFree;
        private string _deviceID;


        /// <summary>
        /// Конструктор
        /// </summary>
        public LogicalDrive()
        { }

        /// <summary>
        /// ID логического диска
        /// </summary>
        [DataMember]
        public string DeviceID
        {
            get { return _deviceID; }
            set { _deviceID = value; }
        }

        /// <summary>
        /// Имя логического диска
        /// </summary>
        [DataMember]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// Метка логического диска
        /// </summary>
        [DataMember]
        public string ValumeLabel
        {
            get { return _valumeLabel; }
            set { _valumeLabel = value; }
        }
        /// <summary>
        /// Описание логического диска
        /// </summary>
        [DataMember]
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        /// <summary>
        /// Название файловой системы логического диска: FAT32 или NTFS
        /// </summary>
        [DataMember]
        public string DriveFormat
        {
            get { return _driveFormat; }
            set { _driveFormat = value; }
        }

        /// <summary>
        /// Тип логического диска
        /// </summary>
        [DataMember]
        public string DriveType
        {
            get { return _driveType; }
            set { _driveType = value; }
        }

        /// <summary>
        /// Всего свободного места на логическом диске
        /// </summary>
        [DataMember]
        public string TotalFreeSpace
        {
            get { return _totalFreeSpace; }
            set { _totalFreeSpace = (value); }
        }

        /// <summary>
        /// Общий размер логического диска
        /// </summary>
        [DataMember]
        public string TotalSize
        {
            get { return _totalSize; }
            set { _totalSize = (value); }
        }

        /// <summary>
        /// Всего свободного места на логическом диске в %
        /// </summary>
        [DataMember]
        public string PercentFree
        {
            get { return _percentFree; }
        }

        /// <summary>
        /// Рассчитать процент свободного места на логическом диске
        /// </summary>
        /// <param name="totalSpace"></param>
        /// <param name="totalFreeSpace"></param>
        public void CalcFreePercs(string totalSpace, string totalFreeSpace)
        {
            try
            {
                double ts = Int64.Parse(totalSpace);
                double tfs = Int64.Parse(totalFreeSpace);
                _percentFree = string.Format("{0}%", Math.Round(tfs * 100 / ts));
            }
            catch (Exception e)
            {
                _percentFree = e.Message;
            }
        }
    }

    [DataContract]
    public class PhisicalDrive
    {
        private string _caption;
        private string _deviceID;
        private string _size;
        private string _status;
        //private List<LogicalDrive> _logicalDrives;

        /// <summary>
        /// Конструктор
        /// </summary>
        public PhisicalDrive()
        {
            //_logicalDrives = new List<LogicalDrive>();
        }

        /// <summary>
        /// Название физического диска
        /// </summary>
        [DataMember]
        public string Caption
        {
            get { return _caption; }
            set { _caption = value; }
        }

        /// <summary>
        /// ID физического диска
        /// </summary>
        [DataMember]
        public string DeviceID
        {
            get { return _deviceID; }
            set { _deviceID = value; }
        }

        /// <summary>
        /// Размер физического диска
        /// </summary>
        [DataMember]
        public string Size
        {
            get { return _size; }
            //set { _size = UpdateSize(value); }
            set { _size = value; }
        }

        /// <summary>
        /// Статус физического диска
        /// </summary>
        [DataMember]
        public string Status
        {
            get { return _status; }
            set { _status = value; }
        }

        /// <summary>
        /// Список логических дисков на данном физическом диске
        /// </summary>
        //[DataMember]
        //public List<LogicalDrive> LogicalDrives
        //{
        //    get { return _logicalDrives; }
        //}

        /// <summary>
        /// Добавляет в список логических дисков новый диск
        /// </summary>
        /// <param name="ld"></param>
        //public void AddLogicalDrive(LogicalDrive ld)
        //{
        //    if (!LogicalDrives.Contains(ld))
        //        LogicalDrives.Add(ld);
        //}

        /// <summary>
        /// Скоращает размер в байтах до кило, мега, гига, тера -байтов
        /// </summary>
        /// <param name="currentSize">размер в байтах</param>
        /// <returns></returns>
        //private string UpdateSize(string currentSize)
        //{
        //    try
        //    {
        //        var cs = Int64.Parse(currentSize);

        //        double csKilo = cs / 1024;
        //        if (Math.Truncate(csKilo) == 0)
        //            return string.Format("{0} bytes", cs);

        //        double csMega = csKilo / 1024;
        //        if (Math.Truncate(csMega) == 0)
        //            return string.Format("{0} Kbytes", Math.Round(csKilo, 2));

        //        double csGiga = csMega / 1024;
        //        if (Math.Truncate(csGiga) == 0)
        //            return string.Format("{0} Mbytes", Math.Round(csMega, 2));

        //        double csTera = csGiga / 1024;
        //        if (Math.Truncate(csTera) == 0)
        //            return string.Format("{0} Gbytes", Math.Round(csGiga, 2));
        //        return string.Format("{0} Tbytes", Math.Round(csTera, 2));
        //    }
        //    catch (Exception e)
        //    {
        //        return e.Message;
        //    }
        //}
    }

    [DataContract]
    public class MyObject
    {

        private string _caption;
        private string _deviceID;
        private string _size;
        private string _status;
        private List<MyObject2> _logicalDrives;

        public MyObject()
        {
            _logicalDrives = new List<MyObject2>();
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
        public List<MyObject2> LogicalDrives
        {
            get { return _logicalDrives; }
        }


        public void AddLogicalDrive(MyObject2 ld)
        {
            if (!LogicalDrives.Contains(ld))
                LogicalDrives.Add(ld);
        }

        

       /* public MyObject(string str,int num, float Float)
        {
            this.Str = str;
            this.Num = num;
            this.Float = Float;
        }

        [DataMember]
        public string Str
        { get; set; }

        [DataMember]
        public int Num
        { get; set; }


        public float Float
        { get; set; }
        */
    }

    [DataContract]
    public class MyObject2
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


        [DataMember]
        public string DeviceID
        {
            get { return _deviceID; }
            set { _deviceID = value; }
        }

        [DataMember]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        [DataMember]
        public string ValumeLabel
        {
            get { return _valumeLabel; }
            set { _valumeLabel = value; }
        }

        [DataMember]
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        [DataMember]
        public string DriveFormat
        {
            get { return _driveFormat; }
            set { _driveFormat = value; }
        }

        [DataMember]
        public string DriveType
        {
            get { return _driveType; }
            set { _driveType = value; }
        }

        [DataMember]
        public string FreeSpaceTotal
        {
            get { return _freeSpaceTotal; }
            set { _freeSpaceTotal = value; }
        }

        [DataMember]
        public string TotalSize
        {
            get { return _totalSize; }
            set { _totalSize = value; }
        }

        [DataMember]
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
