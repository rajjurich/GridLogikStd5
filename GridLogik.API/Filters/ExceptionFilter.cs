using Domain.Entities;
using Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Mvc;

namespace GridLogik.API.Filters
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        
        public IExceptionService _exceptionService { get; set; } 
        public ExceptionFilter()
        {
            //_exceptionService = DependencyResolver.Current.GetService<IExceptionService>();
        }
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            //_exceptionService = DependencyResolver.Current.GetService<IExceptionService>();
            msterrorlog _msterrorlog = new msterrorlog();
            
            var ex = actionExecutedContext.Exception;
            _msterrorlog.errordescription = ex.Message;
            _msterrorlog.errortrace = ex.StackTrace;
            _msterrorlog.errordate = DateTime.Now;
            _msterrorlog.errormodule = actionExecutedContext.ActionContext.ControllerContext.Controller.ToString();
            //_exceptionService.Add(_msterrorlog);

            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent(ex.Message)
            };

            actionExecutedContext.Response = response;
        }
    }
}