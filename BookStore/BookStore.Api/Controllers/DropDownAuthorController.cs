//using BookStore.Entities.Entities;
//using BookStore.Persistence.Persistence.Repositories;
//using Microsoft.AspNetCore.Mvc;
//using System.Collections.Generic;
//using System.Diagnostics.Metrics;

//namespace BookStore.Api.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]

//    public class DropDownAuthorController : Controller
//    {
//        private readonly BookStoreDbContext _bookStoreDb;

//        public DropDownAuthorController(BookStoreDbContext bookStoreDb)
//        {
//            _bookStoreDb = bookStoreDb;
//        }
//        [HttpGet("Index")]
//        public IActionResult Index()
//        {
//            List<AuthorEntity> authorList = new List<AuthorEntity>();
//            authorList = (from a in _bookStoreDb.tbl_Author select a).ToList();
//            authorList.Insert(0, new AuthorEntity
//            {
//                AuthorId = 0,
//                AuthorName = "--Select Country Name--"
//            });
//            ViewBag.message = authorList;

//        }
//    }
//}
