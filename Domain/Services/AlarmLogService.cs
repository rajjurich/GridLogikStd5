using Domain.Core;
using Domain.Entities;
using GridLogik.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{

    public interface IAlarmLogService
    {
        Task<htalarm> Add(htalarm entity);
        Task<IEnumerable<htalarm>> AddRange(IEnumerable<htalarm> entities);
        Task<htalarm> Delete(htalarm entity);
        Task<htalarm> Edit(htalarm entity);
        Task<htalarm> Get(int Key);
        IQueryable<HTAlarm> GetAlarmlog(HTAlarm obj);
        IQueryable<HTAlarm> GetAlarmlogpost(HTAlarm h);

        bool updateall(List<htalarm> sd);
    }

    public class AlarmLogService : IAlarmLogService
    {

        IEntityRepository<htalarm> _entityRepository;
        IUnitOfWork _unitOfWork;
        etools_devEntities _db;

        public AlarmLogService(IEntityRepository<htalarm> entityRepository, IUnitOfWork unitOfWork, DbContext db)
        {
            _entityRepository = entityRepository;
            _unitOfWork = unitOfWork;
            _db = (etools_devEntities)db;
        }

        public Task<htalarm> Add(htalarm entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<htalarm>> AddRange(IEnumerable<htalarm> entities)
        {
            throw new NotImplementedException();
        }

        public Task<htalarm> Delete(htalarm entity)
        {
            throw new NotImplementedException();
        }

        public Task<htalarm> Edit(htalarm entity)
        {
            throw new NotImplementedException();
        }

        public Task<htalarm> Get(int Key)
        {
            throw new NotImplementedException();
        }

        public IQueryable<HTAlarm> GetAlarmlog(HTAlarm obj)
        {
            List<HTAlarm> htalarmList = new List<HTAlarm>();
            try
            {
                DateTime dt = CMSDateTime.CMSDateTime.ConvertToDateTime(DateTime.Now.Date.ToString("MM/dd/yyyy") + " " + "00:00", "MM/dd/yyyy HH:mm");
                //  var alarms = db.htalarms.Where(m => m.starttime > dt && (m.type == "Standard" || m.type == "RealTime") && m.endtime==null).ToList();
                var alarms = _db.htalarms.Where(m => (m.type == "Standard" || m.type == "RealTime") && m.endtime == null).OrderBy(x => x.id).ToList();

                
                foreach (var item in alarms)
                {
                    string meterNamesRT = "";
                    if (item.type == "Standard")
                    {
                        try
                        {
                            meterNamesRT = (from st in _db.standardalarms
                                            join mtr in _db.meters on st.meterid equals mtr.id
                                            where st.isdeleted == 0 && mtr.isdeleted == 0 && st.id == item.alarmid
                                            select mtr.metername).FirstOrDefault();
                        }
                        catch (Exception ex)
                        {
                            //new clsExceptionRepository().DBErrorLog(ex.Message, ex.ToString(), "GetAlarmLog-Standard");
                        }

                    }
                    else if (item.type == "RealTime")
                    {
                        try
                        {
                            var mtrIds = (from rt in _db.rtalarms

                                          where rt.id == item.alarmid && rt.isdeleted == 0
                                          select rt.meterid).FirstOrDefault();
                            if (mtrIds != null && mtrIds != "")
                            {
                                long[] nums = mtrIds.Split(',').Select(long.Parse).ToArray();
                                var mtrnames = (from mtr in _db.meters
                                                where nums.Contains(mtr.id)
                                                select mtr.metername).ToList();

                                foreach (var meterNames in mtrnames)
                                {
                                    meterNamesRT += meterNamesRT == "" ? meterNames : "," + meterNames;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            //new clsExceptionRepository().DBErrorLog(ex.Message, ex.ToString(), "GetAlarmLog-RealTime");
                        }

                    }
                    if (item.id != null)
                    {
                        htalarmList.Add(new HTAlarm
                        {
                            ID = Convert.ToInt32(item.id),
                            alarmname = item.alarmname,
                            alarmmessage = item.alarmmessage,
                            starttime = item.starttime,
                            starttimelog = Convert.ToString(item.starttime == null ? DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") : Convert.ToDateTime(item.starttime).ToString("dd/MM/yyyy HH:mm:ss")),
                            metername = meterNamesRT,
                            duration = item.duration,
                            ismanualack = item.ismanualack,
                            fltrFromDate = DateTime.Now.ToString("MM'/'dd'/'yyyy"),
                            fltrToDate = DateTime.Now.ToString("MM'/'dd'/'yyyy")
                        });
                    }
                }
                //if (htalarmList.Count == 0)
                //{
                //    htalarmList.Add(new HTAlarm
                //    {
                //        fltrFromDate = DateTime.Now.ToString("MM'/'dd'/'yyyy"),
                //        fltrToDate = DateTime.Now.ToString("MM'/'dd'/'yyyy")
                //    });
                //}
            }
            catch (Exception ex)
            {
                return null;
            }
            var dynamic = htalarmList.AsQueryable();
            return dynamic;
        }


        public IQueryable<HTAlarm> GetAlarmlogpost(HTAlarm h)
        {
            List<HTAlarm> htalarmList = new List<HTAlarm>();
            List<htalarm> alarms = new List<htalarm>();
            try
            {
                if (h.AllChecked == "")
                {
                    h.AllChecked = "All";
                }
                DateTime dt = CMSDateTime.CMSDateTime.ConvertToDateTime(h.fltrFromDate + " " + "00:00", "MM/dd/yyyy HH:mm");
                // DateTime  = Convert.ToDateTime(h.fltrFromDate);
                //  DateTime dtto = Convert.ToDateTime(h.fltrToDate);

                DateTime dtto = CMSDateTime.CMSDateTime.ConvertToDateTime(h.fltrToDate + " " + "00:00", "MM/dd/yyyy HH:mm").AddDays(1);

                if (h.AllChecked.ToUpper() == "ALL")
                {
                    alarms = _db.htalarms.Where(m => m.starttime > dt && m.starttime < dtto && (m.type == "Standard" || m.type == "RealTime") && m.endtime == null).ToList();
                }
                else
                {
                    alarms = _db.htalarms.Where(m => m.starttime > dt && m.starttime < dtto && (m.type == "Standard" || m.type == "RealTime") && m.endtime != null).ToList();
                }
                foreach (var item in alarms)
                {
                    string meterNamesRT = "";
                    if (item.type == "Standard")
                    {
                        meterNamesRT = (from st in _db.standardalarms
                                        join mtr in _db.meters on st.meterid equals mtr.id
                                        where st.isdeleted == 0 && mtr.isdeleted == 0 && st.meterid == item.alarmid
                                        select mtr.metername).FirstOrDefault();
                    }
                    else if (item.type == "RealTime")
                    {
                        var meteriditem=Convert.ToString(item.alarmid);
                        var mtrIds = (from rt in _db.rtalarms
                                      where rt.meterid == meteriditem && rt.isdeleted == 0
                                      select rt.meterid).FirstOrDefault();
                        long[] nums = mtrIds.Split(',').Select(long.Parse).ToArray();
                        var mtrnames = (from mtr in _db.meters
                                        where nums.Contains(mtr.id)
                                        select mtr.metername).ToList();
                        foreach (var meterNames in mtrnames)
                        {
                            meterNamesRT += meterNamesRT == "" ? meterNames : "," + meterNames;
                        }
                    }
                    var demoname = meterNamesRT;
                    htalarmList.Add(new HTAlarm
                    {
                        ID = Convert.ToInt32(item.id),
                        alarmname = item.alarmname,
                        alarmmessage = item.alarmmessage,
                        starttime = item.starttime,
                        starttimelog = Convert.ToString(item.starttime == null ? DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") : Convert.ToDateTime(item.starttime).ToString("dd/MM/yyyy HH:mm:ss")),
                        stoptimelog = Convert.ToString(item.endtime == null ? "" : Convert.ToDateTime(item.endtime).ToString("dd/MM/yyyy HH:mm:ss")),
                        endtime = item.endtime,
                        metername = meterNamesRT,
                        duration = item.duration,
                        fltrFromDate = h.fltrFromDate,
                        fltrToDate = h.fltrToDate,
                        ismanualack = item.ismanualack
                    });


                }
                if (htalarmList.Count == 0)
                {
                    htalarmList.Add(new HTAlarm
                    {
                        fltrFromDate = h.fltrFromDate,
                        fltrToDate = h.fltrToDate
                    });
                }
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
            var dynamic = htalarmList.AsQueryable();
            return dynamic;
        }


        public bool updateall(List<htalarm> sd)
        {
            try
            {


                foreach (var item in sd)
                {
                    var htalarm = new htalarm() { id = item.id, ismanualack = item.ismanualack };
                    _db.htalarms.Attach(htalarm);

                    _db.Entry(htalarm).Property(m => m.ismanualack).IsModified = true;


                    _db.SaveChanges();

                }
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
