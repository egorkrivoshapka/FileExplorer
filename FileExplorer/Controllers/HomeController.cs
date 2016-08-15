using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using FileExplorer.Models;

namespace FileExplorer.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            return View();
        }

        [HttpGet]
        public JsonResult GetDirectory()
        {
            try
            {
                var root = Directory.GetCurrentDirectory();
                DirectoryModel a = new DirectoryModel();
                Directory.GetDirectories(root).ToList().ForEach(t => a.Sub.Add(t));
                a.Root = root;
                a.Current = root;
                Directory.GetFiles(root).ToList()
                    .ForEach(f => a.Files.Add(new FileModel(new FileInfo(f))));
                var b = Json(a, JsonRequestBehavior.AllowGet);
                return b;
            }
            catch (Exception e)
            {
                return Json(new DirectoryModel() { Current = e.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpGet]
        public JsonResult GetDirectories(string directory)
        {
            try
            {
                string[] subDirectories = Directory.GetDirectories(directory);
                DirectoryModel a = new DirectoryModel();
                a.Current = directory;
                Directory.GetFiles(directory)
                    .ToList()
                    .ForEach(f => a.Files.Add(new FileModel( new FileInfo(f))));
                foreach (var subDir in subDirectories)
                {
                    a.Sub.Add(subDir);
                }
                return Json(a, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new DirectoryModel() { Current = e.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult GetParentDirectory(string directory)
        {
            try
            {
                directory = Directory.GetParent(directory).FullName;
                string[] subDirectories = Directory.GetDirectories(directory);
                DirectoryModel a = new DirectoryModel();
                a.Current = directory;
                Directory.GetFiles(directory).ToList().ForEach(
                        f => a.Files.Add(new FileModel( new FileInfo(f)))
                        );

                foreach (var subDir in subDirectories)
                {
                    a.Sub.Add(subDir);
                }
                return Json(a, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new DirectoryModel() { Current = e.Message }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}
