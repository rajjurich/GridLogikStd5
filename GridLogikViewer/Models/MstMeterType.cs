using GridLogikViewer.CustomValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GridLogikViewer.Models
{
    public class MstMeterType
    {
        [Display(Name = "MTypRecID")]
        public long MTypRecID { get; set; }

        [CustRequiredAttribute("MTypID")]
        [Display(Name = "Meter type id")]
        [Pad]
        public string MTypID { get; set; }

        [CustRequiredAttribute("MTypMeterType")]
        [Display(Name = "Meter type")]        
        public string MTypMeterType { get; set; }

        [CustRequiredAttribute("MTypSanctionedLoad")]
        [Display(Name = "Sanctioned load")]        
        public string MTypSanctionedLoad { get; set; }

        [CustRequiredAttribute("MTypOverVoltageLimit")]
        [Display(Name = "Over voltage limit")]        
        public string MTypOverVoltageLimit { get; set; }

        [CustRequiredAttribute("MTypUnderVoltageLimit")]
        [Display(Name = "Under voltage limit")]        
        public string MTypUnderVoltageLimit { get; set; }

        [CustRequiredAttribute("MTypOverCurrentLimit")]
        [Display(Name = "Over current limit")]        
        public string MTypOverCurrentLimit { get; set; }

        [CustRequiredAttribute("MTypDemandLimit")]
        [Display(Name = "Demand limit")]        
        public string MTypDemandLimit { get; set; }

        [Display(Name = "Demand control")]        
        public bool MTypDemandControl { get; set; }

        [CustRequiredAttribute("MTypOverloadLimit")]
        [Display(Name = "Overload Limit")]        
        public string MTypOverloadLimit { get; set; }

        [CustRequiredAttribute("MTypOverloadExistenceTime")]
        [Display(Name = "Overload existence time")]        
        public string MTypOverloadExistenceTime { get; set; }

        [CustRequiredAttribute("MTypDemandIntervalPeriod")]
        [Display(Name = "Demand interval period")]        
        public string MTypDemandIntervalPeriod { get; set; }

        [CustRequiredAttribute("MTypLED1PulsesPerKWH")]
        [Display(Name = "LED1 pulses per KWH")]        
        public string MTypLED1PulsesPerKWH { get; set; }

        [CustRequiredAttribute("MTypLED2BlinkOnDuration")]
        [Display(Name = "LED2 blink on duration")]        
        public string MTypLED2BlinkOnDuration { get; set; }

        [CustRequiredAttribute("MTypLED2BlinkOffDurationTime")]
        [Display(Name = "LED2 blink off duration")]        
        public string MTypLED2BlinkOffDurationTime { get; set; }

        [CustRequiredAttribute("MTypRelayReconnectionCount")]
        [Display(Name = "Relay reconnection count")]        
        public string MTypRelayReconnectionCount { get; set; }

        [CustRequiredAttribute("MTypRelayOffDurationShort")]
        [Display(Name = "Relay off duration short")]        
        public string MTypRelayOffDurationShort { get; set; }

        [CustRequiredAttribute("MTypRelayOffDurationLong")]
        [Display(Name = "Relay off duration long")]        
        public string MTypRelayOffDurationLong { get; set; }

        [Display(Name = "MTypIsDeleted")]
        public bool MTypIsDeleted { get; set; }
    }
}