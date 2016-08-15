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

        public JsonResult CountSizes(string directory)
        {
            SizeModel m = new SizeModel();
            List<FileModel> files = new List<FileModel>();
            try
            {
                GetBranchFiles(directory, files);
                SizeModel sModel = new SizeModel();
                sModel.Smallest = files.Where(f => f.Lenght <= 1024 * 10).Count();
                sModel.Middle = files.Where(f => f.Lenght > 1024 * 10 && f.Lenght <= 1024 * 50).Count();
                sModel.Biggest = files.Where(f => f.Lenght >= 1024 * 100).Count();
                return Json(sModel, JsonRequestBehavior.AllowGet);
            }
            catch(Exception e)
            {
                return Json(new DirectoryModel() { Current = e.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public void GetBranchFiles(string dir, List<FileModel> files)
        {
            try
            {
                Directory.GetFiles(dir).ToList()
                    .ForEach(f => files.Add(new FileModel( new FileInfo(f))));
                Directory.GetDirectories(dir).ToList()
                    .ForEach(d => GetBranchFiles(d, files));
            }
            catch(Exception e)
            {
                throw e;
            }
        }

    }
}
