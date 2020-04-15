using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GridLogikViewer.Utilities
{
    public static class MessageConfig
    {
        public static string htmlSuccessString = "<button id='btn-success-trigger' type='button' class='btn btn-popup-success collapse' data-toggle='modal' data-target='#modal_message' data-dismiss='modal'><i class='fa fa-check'></i>&nbsp;</button>";
        public static string htmlErrorString = "<button id='btn-error-trigger' type='button' class='btn btn-popup-danger collapse' data-toggle='modal' data-target='#modal_message' data-dismiss='modal'><i class='fa fa-check'></i>&nbsp;</button>";

    }
}