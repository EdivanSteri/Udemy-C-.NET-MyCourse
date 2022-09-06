﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyCourse.Models.Exceptions;

namespace MyCourse.Controllers
{
    public class ErrorController : Controller
    {
        [AllowAnonymous]
        public IActionResult Index()
        {
            var feature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            switch (feature.Error)
            {
                case CourseNotFoundException exc:
                    ViewData["Title"] = "Corso non trovato";
                    Response.StatusCode = 404;
                    return View("CourseNotFound");

                case CourseDeletionException exc:
                    ViewData["Title"] = "Impossibile elimanare il corso dato che ha già degli iscritti";
                    Response.StatusCode = 400;
                    return View("CoursesDeletionError");

                case CourseSubscriptionException exc:
                    ViewData["Title"] = "Non è stato possibile iscriverti al corso";
                    Response.StatusCode = 400;
                    return View();

                case UserUnknownException exc:
                    ViewData["Title"] = "Utente sconosciuto";
                    Response.StatusCode = 400;
                    return View();

                case SendException exc:
                    ViewData["Title"] = "Non è stato possibile inviare il messaggio, riprova più tardi";
                    Response.StatusCode = 500;
                    return View();

                default:
                    ViewData["Title"] = "Errore";
                    return View();
            }
        }
    }
}
