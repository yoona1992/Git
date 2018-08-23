﻿using CCSIM.BLL;
using CCSIM.DAL.Model;
using System;
using System.Web.Mvc;

namespace CCSIM.Web.Areas.Car
{
    public class CarAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Car";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Car_default",
                "Car/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }

    }
}