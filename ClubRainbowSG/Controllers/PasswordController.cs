﻿using Microsoft.AspNetCore.Mvc;

namespace clubrainbow.Controllers
{
	public class PasswordController : Controller
	{
		public IActionResult ResetPassword()
		{
			return View();
		}
        public IActionResult checkyouremail()
        {
            return View();
        }
        [HttpGet]
        public IActionResult newpassword()
        {
            return View();
        }
        [HttpPost]
        public IActionResult passwordchanged(string newPW, string confPW)
        { var pw = "password do not match";
            if(newPW==confPW){
                 pw=newPW.ToString();
                return View("PasswordChanged",pw);
            }
            else
            {
                return View("newpassword", pw);
            }
            
           
        }
    }
}
