using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using System.Linq.Expressions;
using Domain.Core;
using System.Data.Entity;
using Domain.Extension;
using System.Data;
using Domain.Common;

namespace Domain.Services
{
    public interface IMeterLogService
    {
        Task<htalarm> Add(htalarm entity);
        Task<IEnumerable<htalarm>> AddRange(IEnumerable<htalarm> entities);
        Task<htalarm> Delete(htalarm entity);
        Task<htalarm> Edit(htalarm entity);
        Task<htalarm> Get(int Key);
        IQueryable<htalarm> GetMeterLog();

        IQueryable<htalarm> GetNiuLog();
        Task<htalarm> Remove(htalarm entity);
        IQueryable<htalarm> FindBy(Expression<Func<htalarm, bool>> predicate);
        Task<IEnumerable<htalarm>> RemoveRange(IEnumerable<htalarm> entities);

        IQueryable<meter> meters(long id);

        IQueryable<htalarm> meterfilter(HtAlarmext obj);

        IQueryable<htalarm> Niufilter(HtAlarmext obj);

        IQueryable<metergroup> group();

        Task<IEnumerable<string>> GetWebAlarms(int? id = null);
    }
    public class MeterLogService : IMeterLogService
    {
        IEntityRepository<htalarm> _entityRepository;
        IUnitOfWork _unitOfWork;
        etools_devEntities _db;

        public MeterLogService(IEntityRepository<htalarm> entityRepository, IUnitOfWork unitOfWork, DbContext db)
        {
            _entityRepository = entityRepository;
            _unitOfWork = unitOfWork;
            _db = (etools_devEntities)db;
        }

        public Task<htalarm> Add(htalarm entity)
        {
            return _entityRepository.Add(entity);
        }

        public Task<IEnumerable<htalarm>> AddRange(IEnumerable<htalarm> entities)
        {
            throw new NotImplementedException();
        }

        public Task<htalarm> Delete(htalarm entity)
        {
            return _entityRepository.Delete(entity);
        }

        public Task<htalarm> Edit(htalarm entity)
        {
            return _entityRepository.Edit(entity);
        }

        public Task<htalarm> Get(int Key)
        {
            var x = _entityRepository.Get(Key);
            return x;
        }

        public IQueryable<htalarm> GetMeterLog()
        {
            List<htalarm> meterlog = null;
            meterlog = (from m in _db.htalarms
                        join meters in _db.meters on m.alarmid equals meters.id
                        where (m.endtime == null && m.type == "MeterFailure")
                        select new { id = m.id, alarmid = m.alarmid, metername = meters.metername, location = meters.location, starttime = m.starttime, stoptime = m.endtime }
                           ).ToList().Select(m => new HtAlarmext() { id = m.id, alarmid = m.alarmid, metername = m.metername, location = m.location, starttimelog = Convert.ToString(m.starttime == null ? DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") : Convert.ToDateTime(m.starttime).ToString("dd/MM/yyyy HH:mm:ss")), stoptimelog = Convert.ToString(m.stoptime == null ? "" : Convert.ToDateTime(m.stoptime).ToString("dd/MM/yyyy HH:mm:ss")) }).ToList<htalarm>();

            var dynamic = meterlog.AsQueryable();
            return dynamic;
        }

        public IQueryable<htalarm> GetNiuLog()
        {
            List<htalarm> meterlog = null;

            meterlog = (from m in _db.htalarms
                        join c in _db.communicationdetails on m.alarmid equals c.id
                        where (m.endtime == null && m.type == "ConverterFailure")
                        select new { id = m.id, alarmid = m.alarmid, starttime = m.starttime, stoptime = m.endtime, converterip = c.ipaddress }
                            ).ToList().Select(m => new HtAlarmext() { id = m.id, alarmid = m.alarmid, starttimelog = Convert.ToString(m.starttime == null ? DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") : Convert.ToDateTime(m.starttime).ToString("dd/MM/yyyy HH:mm:ss")), stoptimelog = Convert.ToString(m.stoptime == null ? "" : Convert.ToDateTime(m.stoptime).ToString("dd/MM/yyyy HH:mm:ss")), converterip = m.converterip }).ToList<htalarm>();
            var dynamic = meterlog.AsQueryable();
            return dynamic;
        }
        public IQueryable<meter> meters(long id)
        {
            List<meter> meter = null;
            meter = (from mtr in _db.meters
                     join grp in _db.groupconfigurations on mtr.id equals grp.meterid
                     where (mtr.isdeleted == 0 && mtr.isactive == 1 && grp.groupid == id)
                     select new meter { metername = mtr.metername, id = mtr.id }).Distinct().ToList();
            var dynamic = meter.AsQueryable();
            return dynamic;
        }

        public IQueryable<htalarm> meterfilter(HtAlarmext obj)
        {
            List<htalarm> meter = null;

            DateTime dtfrom = CMSDateTime.CMSDateTime.ConvertToDateTime(obj.fltrFromDate + " " + "00:00", "MM/dd/yyyy HH:mm");
            DateTime dtto = CMSDateTime.CMSDateTime.ConvertToDateTime(obj.fltrToDate + " " + "00:00", "MM/dd/yyyy HH:mm").AddDays(1);

            //   dtfrom = dtfrom.AddDays(-1);

            if (obj.onoff == "1")
            {
                meter = (from m in _db.htalarms
                         join meters in _db.meters on m.alarmid equals meters.id
                         where (m.endtime == null && m.type == "MeterFailure" && m.starttime > dtfrom && m.starttime < dtto)
                         select new { id = m.id, alarmid = m.alarmid, metername = meters.metername, location = meters.location, starttime = m.starttime, stoptime = m.endtime }
                                ).ToList().Select(m => new HtAlarmext() { id = m.id, alarmid = m.alarmid, metername = m.metername, location = m.location, starttimelog = Convert.ToString(m.starttime == null ? DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") : Convert.ToDateTime(m.starttime).ToString("dd/MM/yyyy HH:mm:ss")), stoptimelog = Convert.ToString(m.stoptime == null ? "" : Convert.ToDateTime(m.stoptime).ToString("dd/MM/yyyy HH:mm:ss")) }).ToList<htalarm>();

            }
            else
            {

                meter = (from m in _db.htalarms
                         join meters in _db.meters on m.alarmid equals meters.id
                         where (m.endtime != null && m.type == "MeterFailure" && m.starttime > dtfrom && m.starttime < dtto)
                         select new { id = m.id, alarmid = m.alarmid, metername = meters.metername, location = meters.location, starttime = m.starttime, stoptime = m.endtime }
                                               ).ToList().Select(m => new HtAlarmext() { id = m.id, alarmid = m.alarmid, metername = m.metername, location = m.location, starttimelog = Convert.ToString(m.starttime == null ? DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") : Convert.ToDateTime(m.starttime).ToString("dd/MM/yyyy HH:mm:ss")), stoptimelog = Convert.ToString(m.stoptime == null ? "" : Convert.ToDateTime(m.stoptime).ToString("dd/MM/yyyy HH:mm:ss")) }).ToList<htalarm>();



            }
            var dynamic = meter.AsQueryable();
            return dynamic;
        }

        public IQueryable<htalarm> Niufilter(HtAlarmext obj)
        {
            List<htalarm> meter = null;
            DateTime dtfrom = CMSDateTime.CMSDateTime.ConvertToDateTime(obj.fltrFromDate + " " + "00:00", "MM/dd/yyyy HH:mm");
            DateTime dtto = CMSDateTime.CMSDateTime.ConvertToDateTime(obj.fltrToDate + " " + "00:00", "MM/dd/yyyy HH:mm").AddDays(1);

            //   dtfrom = dtfrom.AddDays(-1);

            if (obj.onoff == "1")
            {
                meter = (from m in _db.htalarms
                         join c in _db.communicationdetails on m.alarmid equals c.id
                         where (m.endtime == null && m.type == "ConverterFailure" && m.starttime > dtfrom && m.starttime < dtto)
                         select new { id = m.id, alarmid = m.alarmid, starttime = m.starttime, stoptime = m.endtime, converterip = c.ipaddress }
                            ).ToList().Select(m => new HtAlarmext() { id = m.id, alarmid = m.alarmid, starttimelog = Convert.ToString(m.starttime == null ? DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") : Convert.ToDateTime(m.starttime).ToString("dd/MM/yyyy HH:mm:ss")), stoptimelog = Convert.ToString(m.stoptime == null ? "" : Convert.ToDateTime(m.stoptime).ToString("dd/MM/yyyy HH:mm:ss")), converterip = m.converterip }).ToList<htalarm>();

            }
            else
            {
                meter = (from m in _db.htalarms
                         join c in _db.communicationdetails on m.alarmid equals c.id
                         where (m.endtime != null && m.type == "ConverterFailure" && m.starttime > dtfrom && m.starttime < dtto)
                         select new { id = m.id, alarmid = m.alarmid, starttime = m.starttime, stoptime = m.endtime, converterip = c.ipaddress }
                           ).ToList().Select(m => new HtAlarmext() { id = m.id, alarmid = m.alarmid, starttimelog = Convert.ToString(m.starttime == null ? DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") : Convert.ToDateTime(m.starttime).ToString("dd/MM/yyyy HH:mm:ss")), stoptimelog = Convert.ToString(m.stoptime == null ? "" : Convert.ToDateTime(m.stoptime).ToString("dd/MM/yyyy HH:mm:ss")), converterip = m.converterip }).ToList<htalarm>();


            }
            var dynamic = meter.AsQueryable();
            return dynamic;
        }

        public IQueryable<metergroup> group()
        {
            List<metergroup> metergroup = null;
            metergroup = (from mgrp in _db.metergroups
                          where mgrp.isdeleted == 0
                          select new metergroup { groupname = mgrp.groupname, id = mgrp.id }).ToList();
            var dynamic = metergroup.AsQueryable();
            return dynamic;
        }


        public Task<htalarm> Remove(htalarm entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<htalarm> FindBy(Expression<Func<htalarm, bool>> predicate)
        {
            return _entityRepository.FindBy(predicate);
        }

        public Task<IEnumerable<htalarm>> RemoveRange(IEnumerable<htalarm> entities)
        {
            throw new NotImplementedException();
        }



        public async Task<IEnumerable<string>> GetWebAlarms(int? id = null)
        {
            List<string> HTAlarms = new List<string>();

            string meterName = null, niuName = null, name, uri = "/";

            string strQuery = "Select * from htalarm where isactive = 1 and popup = 1 and  (ismanualack = 0 or ismanualack is  null) and endtime is null and type in('Standard','RealTime','MeterFailure') and alarmid in (select id from standardalarm where status=1 union select id from rtalarm where status=1 union select id from meters )";
            strQuery += " union ";
            strQuery += " Select * from htalarm where isactive = 1 and popup = 1 and  (ismanualack = 0 or ismanualack is  null) and endtime is null and type not in('Standard','RealTime','MeterFailure') and alarmid in (select converterid from communicationdetaillink) ";

            //string strQuery = "Select * from htalarm where isactive = 1 and popup = 1 and  (ismanualack = 0 or ismanualack is  null)";
            DataTable drList = new DataTable();
            if (strQuery != null)
            {
                drList = await DatabaseHandler.Handler(strQuery);
            }
            using (drList)
            {


                if (drList != null && drList.Rows.Count > 0)
                {

                    foreach (DataRow row in drList.Rows)
                    {
                        if (row["type"].ToString().ToUpper() == "ConverterFailure".ToUpper())
                            niuName = "select IpAddress  from htalarm h,CommunicationDetails c where c.ID = h.alarmid and h.type = 'ConverterFailure' and h.alarmid = " + row["alarmid"];
                        else if (row["type"].ToString().ToUpper() == "RealTime".ToUpper())
                            meterName = "select m.metername  from meters m join  rtalarm r on m.id =r.meterid   join htalarm h on r.id = h.alarmid and h.type='RealTime' and h.alarmid = " + row["alarmid"];
                        else if (row["type"].ToString().ToUpper() == "Standard".ToUpper())
                            meterName = "select m.metername  from meters m join standardalarm r on m.id =r.meterid   join htalarm h on r.id = h.alarmid and h.type='Standard' and h.alarmid = " + row["alarmid"];
                        else
                            meterName = "select metername  from meters m , htalarm h where m.id = h.alarmid and h.type ='MeterFailure' and h.alarmid = " + row["alarmid"];

                        DataTable mtrList = new DataTable();

                        if (niuName != null)
                        {
                            mtrList = await DatabaseHandler.Handler(niuName);

                            try
                            {
                                name = Convert.ToString(mtrList.Rows[0]["ipaddress"]);
                                uri = "../Meterlog/niulog";
                                niuName = null;
                            }
                            catch (Exception ex)
                            {
                                name = "";
                                meterName = null;
                            }
                        }
                        else
                        {
                            //    conn.Open();
                            if (row["type"].ToString().ToUpper() == "RealTime".ToUpper() || row["type"].ToString().ToUpper() == "Standard".ToUpper())
                            {
                                uri = "../Alarmlog/Index";
                            }
                            else
                            {
                                uri = "../Meterlog/Index";
                            }

                            mtrList = await DatabaseHandler.Handler(meterName);


                            try
                            {
                                name = Convert.ToString(mtrList.Rows[0]["metername"]);
                                meterName = null;
                            }
                            catch (Exception ex)
                            {
                                name = "";
                                meterName = null;
                            }

                        }

                        StringBuilder sb = new StringBuilder();
                        sb.Append("<a href='" + uri + "'> Alarm message : " + Convert.ToString(row["alarmmessage"]) + ", ");
                        sb.Append("Time : " + Convert.ToString(row["starttime"]) + ", ");
                        sb.Append("Meter Name : " + name + "</a>");
                        HTAlarms.Add(sb.ToString());
                    }
                }


            }



            return HTAlarms;
        }
    }
}
