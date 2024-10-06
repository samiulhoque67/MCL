using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SILDMS.Web.UI.Areas.MCL.Controllers
{
    public class Test7Controller : Controller
    {
        // GET: MCL/Test7
        public ActionResult Index()
        {
            return View();
        }

        // GET: MCL/Test7/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MCL/Test7/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MCL/Test7/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: MCL/Test7/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MCL/Test7/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: MCL/Test7/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MCL/Test7/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
