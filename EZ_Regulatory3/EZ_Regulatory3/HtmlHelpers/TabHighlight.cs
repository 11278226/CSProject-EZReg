﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EZ_Regulatory3.HtmlHelpers
{
    public static class TabHighlight
  {

    public static string ActivePage(this HtmlHelper helper, string controller, string action)
    {
      string classValue = "";
 
      string currentController = helper.ViewContext.Controller.ValueProvider.GetValue("controller").RawValue.ToString();
      string currentAction = helper.ViewContext.Controller.ValueProvider.GetValue("action").RawValue.ToString();
 
      if (currentController == controller && currentAction == action)
      {
        classValue = "selected";
      }
 
      return classValue;
    }
    
  }
}

