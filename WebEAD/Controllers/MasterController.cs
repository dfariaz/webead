using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebEAD.Models;

namespace WebEAD.Controllers
{
    public abstract class MasterController : Controller
    {
        public const string SystemMessage = "MY_DIALOG";

        protected void ShowMessageError(string htmlContent, string htmlTitle = "Mensagem do Sistema", MyDialog.DialogType type = MyDialog.DialogType.Error)
        {
            this.ShowMessageError(new MyDialog { Title = htmlTitle, Content = htmlContent, @Type = type });
        }

        protected void ShowMessageError(MyDialog dialog)
        {
            this.TempData[SystemMessage] = dialog.ToString();
        }

        protected void ShowMessageInfo(string htmlContent, string htmlTitle = "Mensagem do Sistema", MyDialog.DialogType type = MyDialog.DialogType.Success)
        {
            this.ShowMessageInfo(new MyDialog { Title = htmlTitle, Content = htmlContent, @Type = type });
        }

        protected void ShowMessageInfo(MyDialog dialog)
        {
            this.TempData[SystemMessage] = dialog.ToString();
        }

        protected override void HandleUnknownAction(string actionName)
        {
            Response.Redirect("~/Site/Pagedoesnotexist");
        }
    }
}