using NET_Task.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;



namespace NET_Task.Controllers
{
    public static class Memory
    {
        public static ICollection<Book> Books;
        public static int time;

        static Memory()
        {
            Books = new List<Book>();

            time = 1;
        }
    }

    public class HomeController : Controller
    {
        public ICollection<Book> books;

        public HomeController()
        {
            books = Memory.Books;
        }

        public ActionResult Index()
        {
            ViewBag.Books = new List<Book>[2] { books.Where(b => b.TypeOfBook == typeOfBook.Fiction).ToList(), books.Where(b => b.TypeOfBook == typeOfBook.Nonfiction).ToList() };
            ViewBag.Time = Memory.time;
            return View();
        }

        [HttpPost]
        public JsonResult Upload()
        {
            return Json(new { Fiction = books.Where(b => b.TypeOfBook == typeOfBook.Fiction), Nonfiction = books.Where(b => b.TypeOfBook == typeOfBook.Nonfiction) }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return PartialView();
        }


        [HttpPost]
        public ActionResult Create(Book[] book)
        {

            List<string> lstEr = new List<string>();
            List<string> lstErName = new List<string>();
            var validationResults = new List<ValidationResult>();

            if (book != null && book.Length != 0)
                foreach (var bb in book)
                {
                    var context = new ValidationContext(bb);
                    var isValid = Validator.TryValidateObject(bb, context, validationResults);

                    if(isValid)
                    { 
                        books.Add(bb);
                    }
                    else
                    {
                        lstEr.Add(bb.Id);
                        lstErName.Add(bb.Name);
                    }
                    
                }


            if (lstEr.Count == 0)
            {
                return Json(new { Message = "<li color=\"green\" size=\"5\">Successfully Added</li>" }, JsonRequestBehavior.AllowGet);
            }

            string message = "<li><span color=\"red\" size=\"5\">Error!</span> book(s) has not been added: ";
            foreach (var lse in lstErName)
                message += lse + "; ";

            message += "</li>";
            return Json(new { Message = message, ListError = lstEr }, JsonRequestBehavior.AllowGet);
        }
        /*

                [HttpPost]
                public ActionResult Create(Book[] book)
                {
                    List<int> lstEr = new List<int>();
                    List<string> lstErName = new List<string>();
                    if (!book.Equals(null))
                        if (book.Length != 0)
                        {
                            foreach (var bk in book)
                            {

                            }
                        }

                    return PartialView();
                }
                */


        [HttpPost]
        public ActionResult Delete(string[] Ides)
        {
            List<string> lstEr = new List<string>();
            List<string> lstErName = new List<string>();
            if (!Ides.Equals(null))
                if(Ides.Length != 0)
                {
                    foreach(var id in Ides)
                    {
                        Book b = books.Where(bb => bb.Id == id).FirstOrDefault();
                        if(b != null)
                            if(!books.Remove(b))
                            {
                                lstEr.Add(b.Id);
                                lstErName.Add(b.Name);
                            }
                        
                    }
                }

            if(lstEr.Count == 0)
            {
                return Json(new { Message = "<li color=\"green\" size=\"5\">Successfully deleted</li>"}, JsonRequestBehavior.AllowGet);
            }
            string message = "<li><span color=\"red\" size=\"5\">Error!</span><br/>book(s) has not been deleted: ";
            foreach (var lse in lstErName)
                message += lse + "; ";

            message += "</li>";
            return Json(new { Message = message, ListError = lstEr }, JsonRequestBehavior.AllowGet);
        }
    }
}