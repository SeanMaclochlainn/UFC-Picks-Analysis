﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace FightDataUI.Controllers
{
    //public class FightEventsController : Controller
    //{
    //    DataUtilities dataUtilities;

    //    public FightEventsController()
    //    {
    //        dataUtilities = new DataUtilities();
    //    }

    //    public ActionResult Index()
    //    {
    //        FightEventVM fightEventVM = dataUtilities.GetFightEventVM();
    //        return View(fightEventVM);
    //    }

    //    public ActionResult Details(int id)
    //    {
    //        return View();
    //    }

    //    public ActionResult Create()
    //    {
    //        return View();
    //    }

    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public ActionResult Create(IFormCollection collection)
    //    {
    //        try
    //        {
    //            // TODO: Add insert logic here

    //            return RedirectToAction(nameof(Index));
    //        }
    //        catch
    //        {
    //            return View();
    //        }
    //    }

    //    public ActionResult Edit(int id)
    //    {
    //        return View();
    //    }

    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public ActionResult Edit(int id, IFormCollection collection)
    //    {
    //        try
    //        {
    //            // TODO: Add update logic here

    //            return RedirectToAction(nameof(Index));
    //        }
    //        catch
    //        {
    //            return View();
    //        }
    //    }

    //    public ActionResult Delete(int id)
    //    {
    //        return View();
    //    }

    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public ActionResult Delete(int id, IFormCollection collection)
    //    {
    //        try
    //        {
    //            // TODO: Add delete logic here

    //            return RedirectToAction(nameof(Index));
    //        }
    //        catch
    //        {
    //            return View();
    //        }
    //    }
    //}
}