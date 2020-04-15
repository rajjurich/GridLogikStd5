using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class MeterStatus
    {
        public long ID { get; set; }
        public long METERID { get; set; }
        public string Meter_Name { get; set; }
        public Nullable<DateTime> Date_Time { get; set; }
        public string IpAddress { get; set; }
        public string MeterType1 { get; set; }
        public string ModelName { get; set; }
        public string UtilityName { get; set; }
        public string GroupName { get; set; }
        public string Status { get; set; }
        public string Location { get; set; }
        public long? ModBusID { get; set; }
        public string SerialNo { get; set; }
        public string Port { get; set; }

    }
    public interface IMeterStatusService
    {
        Tuple<List<MeterStatus>, int> GetMeterStatusUtilities(string iGroupID, string iStatus);

        IQueryable<MeterStatus> GetMeterStatus();
    }

    public class MeterStatusService : IMeterStatusService
    {
        etools_devEntities _db;
        public MeterStatusService(DbContext db)
        {
            _db = (etools_devEntities)db;

        }
        public Tuple<List<MeterStatus>, int> GetMeterStatusUtilities(string iGroupID, string iStatus)
        {

            DateTime dtTimeToCompare = DateTime.Now.AddMinutes(-10);

            long lGroupID, lStatus;
            lGroupID = Convert.ToInt32(iGroupID);
            lStatus = Convert.ToInt32(iStatus);
            //new clsExceptionRepository().DBErrorLog("Query Run 1 Strat=" + lGroupID, lStatus.ToString(), "TestM");



            var varQuery = (from tblMtr in _db.meters
                            join tblID in _db.instancedatas on tblMtr.id equals tblID.meterid into mtr
                            from mtrs in mtr.DefaultIfEmpty()
                            join tblMtrGrpcfg in _db.mstmetergroupdetails on tblMtr.id equals tblMtrGrpcfg.meterid
                            join tblMtrGrp in _db.metergroups on tblMtrGrpcfg.grpid equals tblMtrGrp.id
                            join tblMtrMdl in _db.metermodels on tblMtr.modelid equals tblMtrMdl.id
                            join tblMtrType in _db.metertypes on tblMtr.metertypeid equals tblMtrType.id
                            join tblcommlink in _db.communicationdetaillinks on tblMtr.id equals tblcommlink.meterid
                            join tblcomm in _db.communicationdetails on tblcommlink.converterid equals tblcomm.id
                            where tblMtrGrp.groupname != null && ((lStatus == 0 && 1 == 1) || (lStatus == 1 && mtrs.tstamp > dtTimeToCompare) || (lStatus == 2 && (mtrs.tstamp < dtTimeToCompare || mtrs.tstamp == null)))

                            && (tblMtrGrp.isdeleted == 0 || tblMtrGrp.isdeleted == null)
                            && (tblMtrMdl.isdeleted == 0 || tblMtrMdl.isdeleted == null)
                            && (tblMtrType.isdeleted == 0 || tblMtrType.isdeleted == null)
                            && (tblcommlink.active == 1)
                            && (tblcomm.isdeleted == 0 || tblcomm.isdeleted == null)
                            select new MeterStatus()
                            {
                                METERID = tblMtr.id,
                                Meter_Name = tblMtr.metername,
                                SerialNo = tblMtr.serialno,
                                Date_Time = mtrs.tstamp == null ? DateTime.Now : (DateTime)mtrs.tstamp,
                                GroupName = tblMtrGrp.groupname,
                                IpAddress = tblcomm.ipaddress,
                                ModBusID = tblcommlink.modbusid,
                                ModelName = tblMtrMdl.modelname,
                                MeterType1 = tblMtrType.metertypename,
                                Status = mtrs.tstamp > dtTimeToCompare ? "Yes" : "No",
                                Location = tblMtr.location
                            }).Distinct().ToList();



            var finaldata = (from r in varQuery
                             group r by r.METERID into g
                             select new MeterStatus()
                             {
                                 METERID = g.Key,
                                 Meter_Name = g.Select(x => x.Meter_Name).First(),
                                 SerialNo = g.Select(x => x.SerialNo).First(),
                                 Date_Time = g.Max(x => x.Date_Time) == null ? DateTime.Now : (DateTime)g.Select(x => x.Date_Time).First(),
                                 GroupName = string.Join(", ", g.Select(ee => ee.GroupName).ToList()),
                                 IpAddress = g.Select(x => x.IpAddress).First(),
                                 ModBusID = g.Select(x => x.ModBusID).First(),
                                 ModelName = g.Select(x => x.ModelName).First(),
                                 MeterType1 = g.Select(x => x.MeterType1).First(),
                                 Status = g.Max(x => x.Status),// > dtTimeToCompare ? "Yes" : "No",
                                 Location = g.Select(x => x.Location).First()
                             }).ToList();




            if (lGroupID != 0)
            {
                var groupName = (from item in _db.metergroups
                                 where item.id == lGroupID && item.isdeleted == 0
                                 select item.groupname).SingleOrDefault();

                finaldata = finaldata.Where(m => m.GroupName.Contains(groupName)).ToList();
            }
            if (varQuery != null)
            {
                //return new Tuple<List<MeterStatus>, int>(finaldata.Skip((Convert.ToInt16(page) - 1) * Convert.ToInt16(rows)).Take(Convert.ToInt16(rows)).ToList(), finaldata.Count);
                return new Tuple<List<MeterStatus>, int>(finaldata.ToList(), finaldata.Count);
            }
            else
            {
                return null;
            }

        }


        public IQueryable<MeterStatus> GetMeterStatus()
        {       
     
            DateTime dtTimeToCompare = DateTime.Now.AddMinutes(-10);
            var varQuery = (from tblMtr in _db.meters
                            join tblID in _db.instancedatas on tblMtr.id equals tblID.meterid into mtr
                            from mtrs in mtr.DefaultIfEmpty()
                            join tblMtrGrpcfg in _db.mstmetergroupdetails on tblMtr.id equals tblMtrGrpcfg.meterid
                            join tblMtrGrp in _db.metergroups on tblMtrGrpcfg.grpid equals tblMtrGrp.id
                            join tblMtrMdl in _db.metermodels on tblMtr.modelid equals tblMtrMdl.id
                            join tblMtrType in _db.metertypes on tblMtr.metertypeid equals tblMtrType.id
                            join tblcommlink in _db.communicationdetaillinks on tblMtr.id equals tblcommlink.meterid
                            join tblcomm in _db.communicationdetails on tblcommlink.converterid equals tblcomm.id
                            where tblMtrGrp.groupname != null 
                            && (tblMtrGrp.isdeleted == 0 || tblMtrGrp.isdeleted == null)
                            && (tblMtrMdl.isdeleted == 0 || tblMtrMdl.isdeleted == null)
                            && (tblMtrType.isdeleted == 0 || tblMtrType.isdeleted == null)
                            && (tblcommlink.active == 1)
                            && (tblcomm.isdeleted == 0 || tblcomm.isdeleted == null)
                            select new MeterStatus()
                            {
                                METERID = tblMtr.id,
                                Meter_Name = tblMtr.metername,
                                SerialNo = tblMtr.serialno,
                                Date_Time = mtrs.tstamp == null ? DateTime.Now : (DateTime)mtrs.tstamp,
                                GroupName = tblMtrGrp.groupname,
                                IpAddress = tblcomm.ipaddress,
                                ModBusID = tblcommlink.modbusid,
                                ModelName = tblMtrMdl.modelname,
                                MeterType1 = tblMtrType.metertypename,
                                Status = mtrs.tstamp > dtTimeToCompare ? "Yes" : "No",
                                Location = tblMtr.location,
                                Port = tblcomm.port
                            }).Distinct().ToList();



            var finaldata = (from r in varQuery
                             group r by r.METERID into g
                             select new MeterStatus()
                             {
                                 METERID = g.Key,
                                 Meter_Name = g.Select(x => x.Meter_Name).First(),
                                 SerialNo = g.Select(x => x.SerialNo).First(),
                                 Date_Time = g.Max(x => x.Date_Time) == null ? DateTime.Now : (DateTime)g.Select(x => x.Date_Time).First(),
                                 GroupName = string.Join(", ", g.Select(ee => ee.GroupName).ToList()),
                                 IpAddress = g.Select(x => x.IpAddress).First(),
                                 ModBusID = g.Select(x => x.ModBusID).First(),
                                 ModelName = g.Select(x => x.ModelName).First(),
                                 MeterType1 = g.Select(x => x.MeterType1).First(),
                                 Status = g.Max(x => x.Status),// > dtTimeToCompare ? "Yes" : "No",
                                 Location = g.Select(x => x.Location).First(),
                                 Port = g.Select(x => x.Port).First()
                             }).ToList().AsQueryable();

            return finaldata;
        }
    }
}
