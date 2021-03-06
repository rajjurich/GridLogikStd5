﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Domain.Entities
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class etools_devEntities : DbContext
    {
        public etools_devEntities()
            : base("name=etools_devEntities")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<actiontype> actiontypes { get; set; }
        public DbSet<alarmcondition> alarmconditions { get; set; }
        public DbSet<alarmhistory> alarmhistories { get; set; }
        public DbSet<alarm> alarms { get; set; }
        public DbSet<backupscheduledetail> backupscheduledetails { get; set; }
        public DbSet<blockaddressdetail> blockaddressdetails { get; set; }
        public DbSet<blockranx> blockranges { get; set; }
        public DbSet<clientfoldermap> clientfoldermaps { get; set; }
        public DbSet<communicationdetaillink> communicationdetaillinks { get; set; }
        public DbSet<communicationdetail> communicationdetails { get; set; }
        public DbSet<consumer> consumers { get; set; }
        public DbSet<consumercategory> consumercategories { get; set; }
        public DbSet<contactdetail> contactdetails { get; set; }
        public DbSet<databaseversion> databaseversions { get; set; }
        public DbSet<dcsg> dcsgs { get; set; }
        public DbSet<emailconfigdetail> emailconfigdetails { get; set; }
        public DbSet<emaildetail> emaildetails { get; set; }
        public DbSet<groupconfiguration> groupconfigurations { get; set; }
        public DbSet<grouptemplatequery> grouptemplatequeries { get; set; }
        public DbSet<htalarm> htalarms { get; set; }
        public DbSet<instancedata> instancedatas { get; set; }
        public DbSet<ipwiselogger> ipwiseloggers { get; set; }
        public DbSet<manualreport> manualreports { get; set; }
        public DbSet<manualrptlist> manualrptlists { get; set; }
        public DbSet<memorymap_addressdetails> memorymap_addressdetails { get; set; }
        public DbSet<memorymap_range> memorymap_range { get; set; }
        public DbSet<metergroup> metergroups { get; set; }
        public DbSet<metermodel> metermodels { get; set; }
        public DbSet<meter> meters { get; set; }
        public DbSet<metertype> metertypes { get; set; }
        public DbSet<mstdivision> mstdivisions { get; set; }
        public DbSet<mstemployee> mstemployees { get; set; }
        public DbSet<msterrorlog> msterrorlogs { get; set; }
        public DbSet<mstfeeder> mstfeeders { get; set; }
        public DbSet<mstholiday> mstholidays { get; set; }
        public DbSet<mstmenu> mstmenus { get; set; }
        public DbSet<mstmetergroupdetail> mstmetergroupdetails { get; set; }
        public DbSet<mstmodule> mstmodules { get; set; }
        public DbSet<mstregion> mstregions { get; set; }
        public DbSet<mstrole> mstroles { get; set; }
        public DbSet<mstrolemenuaccess> mstrolemenuaccesses { get; set; }
        public DbSet<mstsubstation> mstsubstations { get; set; }
        public DbSet<mstuser> mstusers { get; set; }
        public DbSet<mstutility> mstutilities { get; set; }
        public DbSet<mstzone> mstzones { get; set; }
        public DbSet<opc_clienttag> opc_clienttag { get; set; }
        public DbSet<opc_metername> opc_metername { get; set; }
        public DbSet<opc_server_tag> opc_server_tag { get; set; }
        public DbSet<parametermf> parametermfs { get; set; }
        public DbSet<prmglobal> prmglobals { get; set; }
        public DbSet<querydetail> querydetails { get; set; }
        public DbSet<reportscheduledetail> reportscheduledetails { get; set; }
        public DbSet<rtalarm> rtalarms { get; set; }
        public DbSet<smsdetail> smsdetails { get; set; }
        public DbSet<stageconfiguration> stageconfigurations { get; set; }
        public DbSet<standardalarm> standardalarms { get; set; }
        public DbSet<tablewiseparameter> tablewiseparameters { get; set; }
        public DbSet<tsinstancedatalog> tsinstancedatalogs { get; set; }
        public DbSet<tsloadsurveyexport> tsloadsurveyexports { get; set; }
        public DbSet<tsloadsurveyimport> tsloadsurveyimports { get; set; }
        public DbSet<tsloadsurveylog> tsloadsurveylogs { get; set; }
        public DbSet<tspreviousblkmiss> tspreviousblkmisses { get; set; }
        public DbSet<txnloadblock> txnloadblocks { get; set; }
        public DbSet<uirate> uirates { get; set; }
        public DbSet<consumptionexpdaywise> consumptionexpdaywises { get; set; }
        public DbSet<consumptionexphourwise> consumptionexphourwises { get; set; }
        public DbSet<consumptionexpmonthwise> consumptionexpmonthwises { get; set; }
        public DbSet<consumptionimpdaywise> consumptionimpdaywises { get; set; }
        public DbSet<consumptionimphourwise> consumptionimphourwises { get; set; }
        public DbSet<consumptionimpmonthwise> consumptionimpmonthwises { get; set; }
        public DbSet<daywise> daywises { get; set; }
        public DbSet<evt_30secdatalogs> evt_30secdatalogs { get; set; }
        public DbSet<evt_absloadsurvey> evt_absloadsurvey { get; set; }
        public DbSet<evt_consumption> evt_consumption { get; set; }
        public DbSet<evt_consumption_blocks> evt_consumption_blocks { get; set; }
        public DbSet<evt_loadsurvey> evt_loadsurvey { get; set; }
        public DbSet<evt_loadsurvey_modify> evt_loadsurvey_modify { get; set; }
        public DbSet<expconsumption_blocks> expconsumption_blocks { get; set; }
        public DbSet<hourwise> hourwises { get; set; }
        public DbSet<hourwisecurrent> hourwisecurrents { get; set; }
        public DbSet<hourwisevoltage> hourwisevoltages { get; set; }
        public DbSet<impconsumption_blocks> impconsumption_blocks { get; set; }
        public DbSet<instancedatalog> instancedatalogs { get; set; }
        public DbSet<kwh_exp_con_1> kwh_exp_con_1 { get; set; }
        public DbSet<kwh_exp_read_1> kwh_exp_read_1 { get; set; }
        public DbSet<kwh_imp_con_1> kwh_imp_con_1 { get; set; }
        public DbSet<kwh_imp_read_1> kwh_imp_read_1 { get; set; }
        public DbSet<loadprofile> loadprofiles { get; set; }
        public DbSet<loadsurveyexport> loadsurveyexports { get; set; }
        public DbSet<loadsurveyimport> loadsurveyimports { get; set; }
        public DbSet<loadsurveylog> loadsurveylogs { get; set; }
        public DbSet<monthwise> monthwises { get; set; }
        public DbSet<mquery_consumption> mquery_consumption { get; set; }
        public DbSet<previousblkmiss> previousblkmisses { get; set; }
        public DbSet<evt_conexp_blockwise> evt_conexp_blockwise { get; set; }
        public DbSet<evt_conexp_daywise> evt_conexp_daywise { get; set; }
        public DbSet<evt_conexp_hourwise> evt_conexp_hourwise { get; set; }
        public DbSet<evt_conexp_monthwise> evt_conexp_monthwise { get; set; }
        public DbSet<evt_conimp_blockwise> evt_conimp_blockwise { get; set; }
        public DbSet<evt_conimp_daywise> evt_conimp_daywise { get; set; }
        public DbSet<evt_conimp_hourwise> evt_conimp_hourwise { get; set; }
        public DbSet<evt_conimp_monthwise> evt_conimp_monthwise { get; set; }
        public DbSet<evt_daywise> evt_daywise { get; set; }
        public DbSet<evt_hourwise> evt_hourwise { get; set; }
        public DbSet<evt_hourwisecurrent> evt_hourwisecurrent { get; set; }
        public DbSet<evt_hourwisevoltage> evt_hourwisevoltage { get; set; }
        public DbSet<evt_kwh_exp_con_1> evt_kwh_exp_con_1 { get; set; }
        public DbSet<evt_kwh_exp_read_1> evt_kwh_exp_read_1 { get; set; }
        public DbSet<evt_kwh_imp_con_1> evt_kwh_imp_con_1 { get; set; }
        public DbSet<evt_kwh_imp_read_1> evt_kwh_imp_read_1 { get; set; }
        public DbSet<evt_monthwise> evt_monthwise { get; set; }
        public DbSet<evt_mquery_consumption_b> evt_mquery_consumption_b { get; set; }
    }
}
